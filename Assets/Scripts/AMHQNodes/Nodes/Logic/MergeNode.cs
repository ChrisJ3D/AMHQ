using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using UnityEditor;
using UnityEngine;
using AMHQ;

[Node(false, "Logic/Merge", new Type[] { typeof(AMHQCanvas) })]
public class MergeNode : BaseConversationNode
{
	public override string Title { get { return "Merge"; } }
	public override Vector2 DefaultSize { get { return new Vector2(100, 90); } }
	public override Vector2 MinSize { get { return new Vector2(100, 90); } }
	public override bool AutoLayout { get { return true; } }

	private const string Id = "mergeNode";
	public override string GetID { get { return Id; } }
	public override Type GetObjectType { get { return typeof(MergeNode); } }

	//Previous Node Connections
	[ValueConnectionKnob("", Direction.In, "DialogueForward", NodeSide.Left)]
	public ValueConnectionKnob inputKnob1;

	[ValueConnectionKnob("", Direction.In, "DialogueForward", NodeSide.Left)]
	public ValueConnectionKnob inputKnob2;

	[ValueConnectionKnob("", Direction.Out, "DialogueForward", NodeSide.Right)]
	public ValueConnectionKnob outputKnob;

	private ValueConnectionKnobAttribute dynaCreationAttribute 
	= new ValueConnectionKnobAttribute(
		"", Direction.In, "DialogueForward", NodeSide.Left);

	[SerializeField]
	private int _dynamicPorts = 0;

	private const int StartValue = 54;
	private const int SizeValue = 24;

	protected override void OnCreate ()
	{
		base.OnCreate ();
	}

	public override void NodeGUI()
	{
		// base.NodeGUI();
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();

		inputKnob1.DisplayLayout();
		inputKnob2.DisplayLayout();
		DrawOptions();

		GUILayout.EndVertical();
		GUILayout.BeginVertical();

		outputKnob.DisplayLayout();

		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.BeginVertical();

		if (GUILayout.Button("+", GUILayout.Width(20))) {
			AddNewOption();
			IssueEditorCallBacks();
		}
		GUILayout.EndVertical();
		GUILayout.Space(20);
		GUILayout.BeginVertical();

		if (dynamicConnectionPorts.Count >= 1) {
			if (GUILayout.Button("‒", GUILayout.Width(20))) {
				DeleteConnectionPort (dynamicConnectionPorts.Count-1);
				_dynamicPorts--;
			} 
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}

	private void DrawOptions()
	{
		for (var i = 0; i < _dynamicPorts; i++)
		{
			GUILayout.Space(25);
			((ValueConnectionKnob)dynamicConnectionPorts[i]).SetPosition();
			
		}
	}

	private void AddNewOption()
	{
		CreateValueConnectionKnob(dynaCreationAttribute);
		_dynamicPorts++;
	}

	//For Resolving the Type Mismatch Issue
	private void IssueEditorCallBacks()
	{
		NodeEditorCallbacks.IssueOnAddConnectionPort (dynamicConnectionPorts[dynamicConnectionPorts.Count - 1]);
	}

	public override BaseConversationNode GetDownstreamNode(int inputValue)
	{
		if (IsNextAvailable()) {
			return getTargetNode(outputKnob).PassAhead(inputValue);
		}
		return null;
	}

	public override bool IsBackAvailable()
	{
		return IsAvailable(inputKnob1);
	}

	public override bool IsNextAvailable()
	{
		return IsAvailable (outputKnob);
	}

	public override BaseConversationNode PassAhead(int inputValue)
	{
		return GetDownstreamNode(inputValue);
	}
}

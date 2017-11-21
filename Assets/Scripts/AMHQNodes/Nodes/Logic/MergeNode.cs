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
	public override Vector2 MinSize { get { return new Vector2(150, 40); } }
	public override bool AutoLayout { get { return true; } }

	private const string Id = "mergeNode";
	public override string GetID { get { return Id; } }
	public override Type GetObjectType { get { return typeof(MergeNode); } }

	//Previous Node Connections
	[ValueConnectionKnob("", Direction.In, "DialogueForward", NodeSide.Left, 10)]
	public ValueConnectionKnob inputKnob1;

	[ValueConnectionKnob("", Direction.In, "DialogueForward", NodeSide.Left, 20)]
	public ValueConnectionKnob inputKnob2;

	[ValueConnectionKnob("", Direction.Out, "DialogueForward", NodeSide.Right, 10)]
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
		CharacterName = "Character";
		DialogLine = "Insert dialog text here";
		CharacterPotrait = null;

	}

	public override void NodeGUI()
	{
		base.NodeGUI();

		#region Options
		GUILayout.ExpandWidth(false);

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("+", GUILayout.Width(20)))
		{
			AddNewOption();
			IssueEditorCallBacks();
		}
		if (dynamicConnectionPorts.Count >= 1) {
		if (GUILayout.Button("‒", GUILayout.Width(20)))
			{
				DeleteConnectionPort (dynamicConnectionPorts.Count-1);
				_dynamicPorts--;
			} 
		}

		GUILayout.EndHorizontal();
		GUILayout.Space(5);

		DrawOptions();

		GUILayout.ExpandWidth(false);
		#endregion
	}

	private void DrawOptions()
	{
		EditorGUILayout.BeginVertical();
		for (var i = 0; i < _dynamicPorts; i++)
		{
			GUILayout.BeginVertical();
			GUILayout.BeginHorizontal();
			((ValueConnectionKnob)dynamicConnectionPorts[i]).SetPosition();

			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			GUILayout.Space(4);
		}
		GUILayout.EndVertical();
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

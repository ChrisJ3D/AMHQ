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
	public override Vector2 MinSize { get { return new Vector2(150, 60); } }
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

		// GUILayout.BeginHorizontal();
		// GUILayout.BeginVertical();

		// GUILayout.Space(5);
		// if (GUILayout.Button("Add Input"))
		// {
		// 	AddNewOption();
		// 	IssueEditorCallBacks();
		// }

		// GUILayout.EndVertical();
		// GUILayout.EndHorizontal();

		// GUILayout.BeginHorizontal();
		// GUILayout.BeginVertical();

		// GUILayout.Space(5);
		// if (GUILayout.Button("Remove Last Input"))
		// {
		// 	RemoveLastOption();
		// }

		// GUILayout.EndVertical();
		// GUILayout.EndHorizontal();
	}

	private void RemoveLastOption()
	{
		DeleteConnectionPort(dynamicConnectionPorts.Count-1);
	}

	private void AddNewOption()
	{
		CreateValueConnectionKnob(dynaCreationAttribute);
	}

	//For Resolving the Type Mismatch Issue
	private void IssueEditorCallBacks()
	{
		NodeEditorCallbacks.IssueOnAddConnectionPort (dynamicConnectionPorts[dynamicConnectionPorts.Count - 1]);
	}

	public override BaseConversationNode GetDownstreamNode(int inputValue)
	{
		switch (inputValue)
		{
		case (int)EDialogInputValue.Next:
			if (IsNextAvailable ())
				return getTargetNode (outputKnob).PassAhead((int)EDialogInputValue.Next);
			break;
		case (int)EDialogInputValue.Back:
			if (IsBackAvailable ())
				return getTargetNode (inputKnob1);
			break;
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

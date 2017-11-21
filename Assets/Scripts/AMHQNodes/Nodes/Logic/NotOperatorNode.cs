using System;
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[System.Serializable]
[Node (false, "Logic/Not", new Type[]{typeof(AMHQCanvas)})]
public class NotOperatorNode : BaseConversationNode 
{
	public const string ID = "NotOperatorNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "NOT"; } }
	public override Vector2 DefaultSize { get { return new Vector2 (100, 60); } }
	public override Vector2 MinSize { get { return new Vector2(100, 60); } }
	public override bool AutoLayout { get { return true; } }
	public override string description { get { return "The NOT operator takes two boolean inputs and returns false if both inputs are true."; } }

	public override Type GetObjectType { get { return typeof(NotOperatorNode); } }

	public bool Input1Val;
	public bool Input2Val;

	[ValueConnectionKnob("A", Direction.In, "Number")]
	public ValueConnectionKnob aKnob;

	[ValueConnectionKnob("B", Direction.In, "Number")]
	public ValueConnectionKnob bKnob;

	[ValueConnectionKnob("Bool", Direction.Out, "Number")]
	public ValueConnectionKnob outputKnob;

	public override void NodeGUI () 
	{
		GUILayout.BeginHorizontal ();
		GUILayout.BeginVertical ();

		aKnob.DisplayLayout();
		bKnob.DisplayLayout();

		GUILayout.EndVertical ();
		GUILayout.BeginVertical ();

		outputKnob.DisplayLayout ();

		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool Calculate () 
	{
		if (aKnob.connected()) {
			getTargetNode(aKnob).Calculate();
			Input1Val = aKnob.GetValue<Number> ().ToBool();
		}
		if (bKnob.connected()) {
			getTargetNode(bKnob).Calculate();
			Input2Val = bKnob.GetValue<Number> ().ToBool();
		}

		outputKnob.SetValue<Number> ((Input1Val && Input2Val) ? false : true);
		return outputKnob.GetValue<Number>();
	}

	public override bool IsBackAvailable()
	{
		return false;
	}

	public override bool IsNextAvailable()
	{
		return IsAvailable (outputKnob);
	}

	public override BaseConversationNode GetDownstreamNode(int inputValue)
	{
		switch (inputValue)
		{
		case (int)EDialogInputValue.Next:
			if (IsNextAvailable ())
				return getTargetNode (outputKnob);
			break;
		case (int)EDialogInputValue.Back:
			break;
		}
		return null;
	}
}
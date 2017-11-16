using System;
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node (false, "Logic/Conditional")]
public class ConditionalNode : BaseConversationNode 
{
	public enum ConditionType { AIsLessThanB, AIsLessThanOrEqualB, AEqualsB, AIsGreaterThanOrEqualB, AIsGreaterThanB}
	public ConditionType method = ConditionType.AEqualsB;

	public const string ID = "ConditionalNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Conditional"; } }
	public override Vector2 DefaultSize { get { return new Vector2 (250, 100); } }
	public override Vector2 MinSize { get { return new Vector2(250, 100); } }
	public override bool AutoLayout { get { return true; } }
	public override string description { get { return "The conditional node compares two inputs (A and B) according to the chosen method, and outputs a boolean value depending on the result. You can use this to compare attributes and other values."; } }

	public override Type GetObjectType { get { return typeof(ConditionalNode); } }

	public float Input1Val = 1f;
	public float Input2Val = 1f;

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

		if (aKnob.connected())
			aKnob.DisplayLayout();
		else
			Input1Val = RTEditorGUI.FloatField (GUIContent.none, Input1Val);
		
		// --
		if (bKnob.connected())
			bKnob.DisplayLayout();
		else
			Input2Val = RTEditorGUI.FloatField (GUIContent.none, Input2Val);

		GUILayout.EndVertical ();
		GUILayout.BeginVertical ();

		outputKnob.DisplayLayout ();

		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);

#if UNITY_EDITOR
		method = (ConditionType)UnityEditor.EditorGUILayout.EnumPopup (new GUIContent ("Method", "The type of calculation performed on Input 1 and Input 2"), method);
#else
		GUILayout.Label (new GUIContent ("Method: " + type.ToString (), "The type of calculation performed on Input 1 and Input 2"));
#endif
	}

	public override bool Calculate () 
	{
		if (aKnob.connected())
			Input1Val = aKnob.GetValue<Number> ();
		if (bKnob.connected())
			Input2Val = bKnob.GetValue<Number> ();

		switch (method) 
		{
		case ConditionType.AIsLessThanB:
			if (outputKnob != null) {
				outputKnob.SetValue<Number> ((Input1Val < Input2Val) ? true : false);
			}
			break;
		case ConditionType.AIsLessThanOrEqualB:
			if (outputKnob != null) {
				outputKnob.SetValue<Number> ((Input1Val <= Input2Val) ? true : false);
			}
			break;
		case ConditionType.AEqualsB:
			if (outputKnob != null) {
				outputKnob.SetValue<Number> ((Input1Val == Input2Val) ? true : false);
			}
			break;
		case ConditionType.AIsGreaterThanOrEqualB:
			if (outputKnob != null) {
				outputKnob.SetValue<Number> ((Input1Val >= Input2Val) ? true : false);
			}
			break;
		case ConditionType.AIsGreaterThanB:
			if (outputKnob != null) {
				outputKnob.SetValue<Number> ((Input1Val > Input2Val) ? true : false);
			}
			break;
		}
		Debug.Log(outputKnob.GetValue<Number>());
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[Node (false, "Character/Attribute Picker", new Type[]{typeof(AMHQCanvas)})]
public class AttributePickerNode : BaseConversationNode 
{
	public const string ID = "AttributePickerNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Attribute Picker"; } }
	public override Vector2 DefaultSize { get { return new Vector2 (120, 40); } }

	public override Vector2 MinSize { get { return new Vector2 (120, 40); } }
	public override bool AutoLayout { get { return true; } }

	public override Type GetObjectType { get { return typeof(AttributePickerNode); } }

	[ValueConnectionKnob("Output", Direction.Out, "Number")]
	public ValueConnectionKnob outputKnob;

	[SerializeField]
	private CharacterAttributeType attribute = 0;

	[SerializeField]
	public Number output = new Number();

	protected string label = "";
	
	public override void NodeGUI () 
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(5);
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		attribute = (CharacterAttributeType)EditorGUILayout.EnumPopup(new GUIContent(""), attribute, GUILayout.Width(105));

		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		outputKnob.DisplayLayout();

		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);

	
	}
	
	public override bool Calculate () 
	{
		output = (int)attribute;
		outputKnob.SetValue<Number>(output);

		return true;
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
		Calculate();
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
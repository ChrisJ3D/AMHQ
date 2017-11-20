using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[Node (false, "Character/Set Attribute Value")]
public class SetAttributeValueNode : BaseConversationNode 
{
	public const string ID = "SetAttributeValueNode";
	public override string GetID { get { return ID; } }
	public override string Title { get { return "Set Attribute"; } }
	public override Vector2 MinSize { get { return new Vector2(120, 90); } }
	public override bool AutoLayout { get { return true; } }
	public override Vector2 DefaultSize { get { return new Vector2 (120, 90); } }

	public override string description { get { return "The Branch node reroutes the level flow based on the incoming condition. The condition should be in a boolean format (true/false)."; } }
	public override Type GetObjectType { get { return typeof(SetAttributeValueNode); } }

	[ValueConnectionKnob("", Direction.In, "DialogueForward", NodeSide.Left, 10)]
	public ValueConnectionKnob flowIn;

	[ValueConnectionKnob("", Direction.Out, "DialogueForward", NodeSide.Right, 10)]
	public ValueConnectionKnob flowOut;

	[ValueConnectionKnob("Attribute", Direction.In, "Number", NodeSide.Left, 20)]
	public ValueConnectionKnob attributeKnob;

	[ValueConnectionKnob("Value", Direction.In, "Number", NodeSide.Left, 30)]
	public ValueConnectionKnob valueKnob;

	public object speaker;
	public string content = "";
	
	public override void NodeGUI () 
	{
		base.NodeGUI();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}
	
	public override bool Calculate () 
	{
		Debug.Log("Calculating SetAttributeNode");
		Number attribute = 0;
		Number value = 0f;

		if (attributeKnob.connected()) {
			getTargetNode(attributeKnob).Calculate();
			attribute = attributeKnob.GetValue<Number>();
		}

		if (valueKnob.connected()) {
			getTargetNode(valueKnob).Calculate();
			value = valueKnob.GetValue<Number>();
		}

		Debug.Log("Attribute set to " + attribute.ToStringShort());
		Debug.Log("Value set to " + value.ToStringShort());

		var gameManager = FindObjectOfType<GameManager>();

		gameManager.SetPlayerAttribute((CharacterAttributeType)attribute.ToInt32(), value);

		return true;
	}

	public override bool IsBackAvailable() {
		return IsAvailable(flowIn);
	}

	public override bool IsNextAvailable() {
		return IsAvailable(flowOut);
	}

	public override BaseConversationNode PassAhead(int inputValue)
	{
		Calculate();
		return GetDownstreamNode(inputValue);
	}

	public override BaseConversationNode GetDownstreamNode(int inputValue)
	{
		switch (inputValue)
		{
		case (int)EDialogInputValue.Next:
			if (IsNextAvailable ())
				return getTargetNode (flowOut).PassAhead((int)EDialogInputValue.Next);
			break;
		case (int)EDialogInputValue.Back:
			if (IsBackAvailable ())
				return getTargetNode (flowIn);
			break;
		}
		return null;
	}
}
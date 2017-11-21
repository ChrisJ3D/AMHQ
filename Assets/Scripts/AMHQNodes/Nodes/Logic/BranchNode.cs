using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[Node (false, "Logic/Branch", new Type[]{typeof(AMHQCanvas)})]
public class BranchNode : BaseConversationNode 
{
	public override string Title {get { return "Branch"; } }
	public override Vector2 MinSize { get { return new Vector2(150, 60); } }
	public override bool AutoLayout { get { return true; } }

	private const string Id = "branchNode";
	public override string GetID { get { return Id; } }
	public override Type GetObjectType { get { return typeof(BranchNode); } }
	public override string description { get { return "The Branch node reroutes the level flow based on the incoming condition. The condition should be in a boolean format (true/false)."; } }

	[ValueConnectionKnob("In", Direction.In, "DialogueForward", NodeSide.Left, 10)]
	public ValueConnectionKnob flowIn;

	[ValueConnectionKnob("Bool", Direction.In, "Number", NodeSide.Left, 20)]
	public ValueConnectionKnob conditionKnob;

	[ValueConnectionKnob("True", Direction.Out, "DialogueForward", NodeSide.Right, 10)]
	public ValueConnectionKnob trueKnob;

	[ValueConnectionKnob("False", Direction.Out, "DialogueForward", NodeSide.Right, 20)]
	public ValueConnectionKnob falseKnob;		

	public object speaker;
	public string content = "";

	[SerializeField]
	public bool value = false;
	
	public override void NodeGUI () 
	{
		base.NodeGUI();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool IsBackAvailable()
	{
		return IsAvailable(flowIn);
	}

	public override bool IsNextAvailable()
	{
		return IsAvailable (Evaluate());
	}

	public ValueConnectionKnob Evaluate() {
		if (conditionKnob.connected()) {
			value = getTargetNode(conditionKnob).Calculate();
			if (value == true) {
				return trueKnob;
			}
		}
		return falseKnob;
	}

	public override BaseConversationNode PassAhead(int inputValue)
	{
		return GetDownstreamNode(inputValue);
	}

	public override BaseConversationNode GetDownstreamNode(int inputValue)
	{
		switch (inputValue)
		{
		case (int)EDialogInputValue.Next:
			if (IsNextAvailable ())
				return getTargetNode (Evaluate());
			break;
		case (int)EDialogInputValue.Back:
			if (IsBackAvailable ())
				return getTargetNode (Evaluate());
			break;
		}
		return null;
	}
}
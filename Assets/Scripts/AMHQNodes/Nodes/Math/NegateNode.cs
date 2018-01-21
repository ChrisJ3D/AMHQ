using System;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[Node (false, "Math/Negate")]
public class NegateNode : BaseConversationNode 
{
	public const string ID = "NegateNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Negate"; } }
	public override Vector2 DefaultSize { get { return new Vector2 (120, 50); } }
	public override Vector2 MinSize { get { return new Vector2(120, 50); } }
	public override bool AutoLayout { get { return true; } }
	public override Type GetObjectType { get { return typeof(NegateNode); } }
	public override string description { get { return "The negate node will return any given input in its negative equivalent."; } }


	[ValueConnectionKnob("Input", Direction.In, "Number")]
	public ValueConnectionKnob inputKnob;

	[ValueConnectionKnob("Output", Direction.Out, "Number")]
	public ValueConnectionKnob outputKnob;

	public Number input = new Number();

	protected string label = "";
	
	public override void NodeGUI () 
	{
		GUILayout.Space(5f);

		GUILayout.BeginHorizontal ();
		GUILayout.BeginVertical ();

		inputKnob.DisplayLayout ();

		GUILayout.EndVertical ();
		GUILayout.BeginVertical ();
		
		outputKnob.DisplayLayout (new GUIContent(label));
		
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}
	
	public override bool Calculate () 
	{
		input = 0f;

		if (inputKnob.connected()) {
			getTargetNode(inputKnob).Calculate();
			input = inputKnob.GetValue<Number>();
		}

		input = -input;
		Debug.Log("Value set to " + input);
		outputKnob.SetValue<Number>(input);

		label = input.ToStringShort();
			
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
		switch (inputValue)
		{
		default:
			if (IsNextAvailable ())
				return getTargetNode (outputKnob).PassAhead(inputValue);
			break;
		}
		return null;
	}
}
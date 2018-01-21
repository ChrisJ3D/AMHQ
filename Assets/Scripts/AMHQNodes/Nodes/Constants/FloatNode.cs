using System;
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[System.Serializable]
[Node (false, "Constants/Float", new Type[] { typeof(AMHQCanvas) })]
public class FloatNode : BaseConversationNode 
{
	public const string ID = "FloatNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Float"; } }
	public override Vector2 DefaultSize { get { return new Vector2 (130, 60); } }

	public override Type GetObjectType { get { return typeof(FloatNode); } }

	public override Vector2 MinSize { get { return new Vector2 (130, 60); } }
	public override string description { get { return "The Float node outputs a floating-point number (meaning you can have decimals). Floating-point numbers have higher precision than integers, but require more memory."; } }

	[ValueConnectionKnob("Float", Direction.Out, "Number")]
	public ValueConnectionKnob outputKnob;		

	[SerializeField]
	public Number value = new Number();

	public override void NodeGUI () 
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.BeginVertical();

		GUILayout.Space(5);

		value = RTEditorGUI.FloatField (value);

		GUILayout.EndVertical();

		GUILayout.BeginVertical();

		outputKnob.DisplayLayout(new GUIContent(""));

		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);

		Calculate();
	}

	public override bool Calculate () 
	{	
		outputKnob.SetValue<Number> (value);
		return true;
	}

	public override BaseConversationNode GetDownstreamNode(int inputValue) {
		return (BaseConversationNode)outputKnob.connection(0).body;
	}

	public override bool IsBackAvailable()
	{
		return false;
	}

	public override bool IsNextAvailable()
	{
		return false;
	}
}
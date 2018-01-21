using System;
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[System.Serializable]
[Node (false, "Constants/Boolean", new Type[] { typeof(AMHQCanvas) })]
public class BoolNode : BaseConversationNode 
{
	public const string ID = "BoolNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Boolean"; } }
			public override Vector2 MinSize { get { return new Vector2(80, 50); } }
		public override bool AutoLayout { get { return true; } }
	public override string description { get { return "The boolean node outputs a value that is either true or false. Behind the scenes this is simply an integer with the value 1 or 0. Any positive number, i.e. larger than 0 will evaluate as True, while any negative number will evaluate to False."; } }

	public override Type GetObjectType { get { return typeof(BoolNode); } }

	[SerializeField]
	public Number output = new Number();

	[ValueConnectionKnob("Output", Direction.Out, "Number")]
	public ValueConnectionKnob outputKnob;

	public override void NodeGUI () 
	{			
		GUILayout.Space(5f);

		GUILayout.BeginHorizontal ();
		GUILayout.Space(12);
		GUILayout.BeginVertical ();
		
		output = (Number)RTEditorGUI.Toggle (output , "");

		GUILayout.EndVertical();
		GUILayout.BeginVertical();

		outputKnob.DisplayLayout(new GUIContent(""));
		
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);

		Calculate();
	}

	public override BaseConversationNode GetDownstreamNode(int inputValue) {
		return (BaseConversationNode)outputKnob.connection(0).body;
	}

	public override bool Calculate ()
	{
		outputKnob.SetValue<Number> (output);
		return outputKnob.GetValue<Number>();
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
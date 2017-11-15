using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Constants/Integer")]
	public class IntNode : Node 
	{
		public const string ID = "IntNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Integer"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (130, 60); } }
		public override string description { get { return "The integer node outputs a whole number. It can be positive or negative, but if you need decimals you should use the Float node."; } }

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number value = new Number();

		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			GUILayout.BeginVertical();
	
			GUILayout.Space(5);

			value = RTEditorGUI.IntField (value);

			GUILayout.EndVertical();

			GUILayout.Space(10);

			GUILayout.BeginVertical();

			outputKnob.DisplayLayout(new GUIContent(""));

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}

		public override bool Calculate () 
		{
			outputKnob.SetValue<Number> (value);
			return true;
		}
	}
}
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Constants/Float")]
	public class FloatNode : Node 
	{
		public const string ID = "FloatNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Float"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (130, 60); } }

		[ValueConnectionKnob("Float", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;		

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
		}

		public override bool Calculate () 
		{	
			outputKnob.SetValue<Number> (value);
			return true;
		}
	}
}
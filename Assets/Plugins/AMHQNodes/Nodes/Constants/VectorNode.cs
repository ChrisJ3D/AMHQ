using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Constants/Vector")]
	public class VectorNode : Node 
	{
		public const string ID = "VectorNode";
		public override string GetID { get { return ID; } }
		public override string Title { get { return "Vector"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (130, 135); } }

		public Number value = new Number();

		[ValueConnectionKnob("Vector", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{
			GUILayout.Space(5);

			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			GUILayout.BeginVertical();

			value.x = RTEditorGUI.FloatField (value.x);
			value.y = RTEditorGUI.FloatField (value.y);
			value.z = RTEditorGUI.FloatField (value.z);
			value.w = RTEditorGUI.FloatField (value.w);

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
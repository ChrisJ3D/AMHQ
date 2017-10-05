using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Constants/Boolean")]
	public class BoolNode : Node 
	{
		public const string ID = "BoolNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Boolean"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (75, 50); } }

		public Number value = new Number();
		private string label = "";
		
		[ValueConnectionKnob("Input", Direction.In, "Number")]
		public ValueConnectionKnob inputKnob;

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{			
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			value = RTEditorGUI.Toggle (value, label);

			GUILayout.EndVertical();
			GUILayout.BeginVertical();

			outputKnob.DisplayLayout(new GUIContent(label));

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		public override bool Calculate () 
		{
			outputKnob.SetValue<Number> (value);
			label = value.ToBool().ToString();
			return true;
		}
	}
}
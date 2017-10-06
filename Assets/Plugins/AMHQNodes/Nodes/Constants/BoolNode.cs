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
		public override Vector2 DefaultSize { get { return new Vector2 (80, 50); } }
		public override string description { get { return "The boolean node outputs a value that is either true or false. Behind the scenes this is simply an integer with the value 1 or 0. Any positive number, i.e. larger than 0 will evaluate as True, while any negative number will evaluate to False."; } }

		public Number value = new Number();

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{			
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.Space(12);
			GUILayout.BeginVertical ();
			
			value = RTEditorGUI.Toggle (value , "");

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
			outputKnob.SetValue<Number> (value);
			return true;
		}
	}
}
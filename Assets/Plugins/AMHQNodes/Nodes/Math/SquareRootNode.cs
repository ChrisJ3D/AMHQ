using System;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Square Root")]
	public class SquareRootNode : Node 
	{
		public const string ID = "SquareRootNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Square Root"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 50); } }

		[ValueConnectionKnob("Radicand", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;
		[ValueConnectionKnob("Root", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number radicand = new Number();
		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			if (aKnob.connected())
				aKnob.DisplayLayout();
			else
				radicand = RTEditorGUI.FloatField (GUIContent.none, radicand);

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
			if (aKnob.connected()) {
				radicand = aKnob.GetValue<Number> ();
			}

			outputKnob.SetValue<Number> (Math.Sqrt(radicand));

			label = outputKnob.GetValue<Number> ();

			return true;
		}
	}
}
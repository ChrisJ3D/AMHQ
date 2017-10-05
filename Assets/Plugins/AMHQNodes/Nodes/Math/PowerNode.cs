using System;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Power")]
	public class PowerNode : Node 
	{
		public const string ID = "powerNode";
		public override string GetID { get { return ID; } }
		public override string Title { get { return "Power"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 70); } }

		[ValueConnectionKnob("Base", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;
		[ValueConnectionKnob("Exponent", Direction.In, "Number")]
		public ValueConnectionKnob bKnob;
		[ValueConnectionKnob("Power", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number Base = new Number();
		public Number exponent = new Number();
		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			if (aKnob.connected())
				aKnob.DisplayLayout ();
			else
				Base = RTEditorGUI.FloatField (GUIContent.none, Base);

			GUILayout.Space(5f);
			
			// --
			if (bKnob.connected())
				bKnob.DisplayLayout ();
			else
				exponent = RTEditorGUI.FloatField (GUIContent.none, exponent);

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

			if (aKnob.connected())
				Base = aKnob.GetValue<Number> ();
				
			if (bKnob.connected())
				exponent = bKnob.GetValue<Number> ();

			outputKnob.SetValue<Number> (Math.Pow(Base, exponent));

			label = outputKnob.GetValue<Number> ().ToString();

			return true;
		}
	}
}
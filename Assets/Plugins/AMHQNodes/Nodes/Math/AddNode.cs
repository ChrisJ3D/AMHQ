using System;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Add")]
	public class AddNode : Node 
	{
		public const string ID = "addNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Add"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 70); } }
		public override string description { get { return "Addition (often signified by the plus symbol \"+\") is one of the four basic operations of arithmetic, with the others being subtraction, multiplication and division. The addition of two whole numbers is the total amount of those quantities combined."; } }


		[ValueConnectionKnob("Summand 1", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;
		[ValueConnectionKnob("Summand 2", Direction.In, "Number")]
		public ValueConnectionKnob bKnob;
		[ValueConnectionKnob("Sum", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number summand1 = new Number();
		public Number summand2 = new Number();
		
		public override void NodeGUI () 
		{
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			if (aKnob.connected())
				aKnob.DisplayLayout();
			else
				summand1 = RTEditorGUI.FloatField (GUIContent.none, summand1);

			GUILayout.Space(5f);
			
			// --
			if (bKnob.connected())
				bKnob.DisplayLayout();
			else
				summand2 = RTEditorGUI.FloatField (GUIContent.none, summand2);

			GUILayout.EndVertical ();
			GUILayout.BeginVertical ();
			
			outputKnob.DisplayLayout ();
			
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();			
		}
		
		public override bool Calculate () 
		{
			if (aKnob.connected()) {
				summand1 = aKnob.GetValue<Number>();
			}

			if (bKnob.connected()) {
				summand2 = bKnob.GetValue<Number>();
			} 
			
			outputKnob.SetValue<Number> (summand1 + summand2);

			return true;
		}
	}
}
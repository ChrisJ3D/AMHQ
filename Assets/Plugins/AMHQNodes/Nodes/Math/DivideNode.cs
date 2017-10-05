using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Divide")]
	public class DivideNode : Node 
	{
		public const string ID = "DivideNode";
		public override string GetID { get { return ID; } }
		public override string Title { get { return "Divide"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 70); } }
		public override string description { get { return "Division is one of the four basic operations of arithmetic, the others being addition, subtraction, and multiplication. The division of two natural numbers is the process of calculating the number of times one number is contained within one another."; } }

		[ValueConnectionKnob("Summand 1", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;
		[ValueConnectionKnob("Summand 2", Direction.In, "Number")]
		public ValueConnectionKnob bKnob;
		[ValueConnectionKnob("Quotient", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number numerator = 0f;
		public Number denominator = 0f;
		protected string label = "";
				
		public override void NodeGUI () 
		{
			//	GUILayout.Label (label);

			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			// Inputs [0].DisplayLayout ();
			// Inputs [1].DisplayLayout();

			if (aKnob.connected()) {
				GUILayout.Label (aKnob.name);
				aKnob.DisplayLayout();
			}
			else
				numerator = RTEditorGUI.FloatField (GUIContent.none, numerator);

			GUILayout.Space(5f);
			
			// --
			if (bKnob.connected()) {
				GUILayout.Label (bKnob.name);
				bKnob.DisplayLayout();
			}
			else
				denominator = RTEditorGUI.FloatField (GUIContent.none, denominator);

			GUILayout.EndVertical ();
			GUILayout.BeginVertical ();
			
			outputKnob.DisplayLayout ();
			
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
			
		}
		
		public override bool Calculate () 
		{
			if (aKnob.connected())
				numerator = aKnob.GetValue<Number> ();
			if (bKnob.connected())
				denominator = bKnob.GetValue<Number> ();

			outputKnob.SetValue<Number> (numerator / denominator);

			label = outputKnob.GetValue(typeof(Number)).ToString();

			return true;
		}
	}
}
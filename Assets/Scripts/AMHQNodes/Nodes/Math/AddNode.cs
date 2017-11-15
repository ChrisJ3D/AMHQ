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
		public override Vector2 DefaultSize { get { return new Vector2 (150, 80); } }
		public override string description { get { return "Addition (often signified by the plus symbol \"+\") is one of the four basic operations of arithmetic, with the others being subtraction, multiplication and division. The addition of two whole numbers is the total amount of those quantities combined."; } }


		[ValueConnectionKnob("Summand", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;
		[ValueConnectionKnob("Summand", Direction.In, "Number")]
		public ValueConnectionKnob bKnob;
		[ValueConnectionKnob("Sum", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number summand1 = new Number();
		public Number summand2 = new Number();
		private string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			aKnob.DisplayLayout();

			GUILayout.Space(5f);
			
			bKnob.DisplayLayout();

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
			summand1 = 0f;
			summand2 = 0f;

			if (aKnob.connected()) {
				summand1 = aKnob.GetValue<Number>();
			}

			if (bKnob.connected()) {
				summand2 = bKnob.GetValue<Number>();
			} 
			
			outputKnob.SetValue<Number> (summand1 + summand2);

			label = outputKnob.GetValue<Number>().ToStringShort();

			return true;
		}
	}
}
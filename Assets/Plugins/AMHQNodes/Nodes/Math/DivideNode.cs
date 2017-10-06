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
		public override Vector2 DefaultSize { get { return new Vector2 (150, 80); } }
		public override string description { get { return "Division is one of the four basic operations of arithmetic, the others being addition, subtraction, and multiplication. The division of two natural numbers is the process of calculating the number of times one number is contained within one another."; } }

		[ValueConnectionKnob("Numerator", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;
		[ValueConnectionKnob("Denominator", Direction.In, "Number")]
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
			numerator = 0f;
			denominator = 1f;

			if (aKnob.connected())
				numerator = aKnob.GetValue<Number> ();
			if (bKnob.connected())
				denominator = bKnob.GetValue<Number> ();

			outputKnob.SetValue<Number> (numerator / denominator);

			label = outputKnob.GetValue<Number>().ToStringShort();

			return true;
		}
	}
}
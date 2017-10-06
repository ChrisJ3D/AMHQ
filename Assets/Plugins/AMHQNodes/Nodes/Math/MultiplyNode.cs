using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Multiply")]
	public class MultiplyNode : Node 
	{
		public const string ID = "multiplyNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Multiply"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 80); } }
		public override string description { get { return "Multiplication (often denoted by the cross symbol \"×\", by a point \"⋅\", by juxtaposition, or, on computers, by an asterisk \"∗\") is one of the four elementary mathematical operations of arithmetic; with the others being addition, subtraction and division. The multiplication of whole numbers may be thought as a repeated addition; that is, the multiplication of two numbers is equivalent to adding as many copies of one of them, the multiplicand, as the value of the other one, the multiplier. Normally, the multiplier is written first and multiplicand second, (though this can vary by language.)"; } }

		[ValueConnectionKnob("Factor 1", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;
		[ValueConnectionKnob("Factor 2", Direction.In, "Number")]
		public ValueConnectionKnob bKnob;
		[ValueConnectionKnob("Product", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number factor1 = 0f;
		public Number factor2 = 0f;
		protected string label = "";
		
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
			factor1 = 1f;
			factor2 = 1f;

			if (aKnob.connected())
				factor1 = aKnob.GetValue<Number> ();
			if (bKnob.connected())
				factor2 = bKnob.GetValue<Number> ();

			outputKnob.SetValue<Number> (factor1 * factor2);

			label = outputKnob.GetValue<Number>().ToStringShort();

			return true;
		}
	}
}
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Subtract")]
	public class SubtractNode : Node 
	{
		public const string ID = "subtractNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Subtract"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 80); } }
		public override string description { get { return "Subtraction is a mathematical operation that represents the operation of removing objects from a collection. It is signified by the minus sign (−). Performing subtraction is one of the simplest numerical tasks. Subtraction of very small numbers is accessible to young children. In primary education, students are taught to subtract numbers in the decimal system, starting with single digits and progressively tackling more difficult problems."; } }

		[ValueConnectionKnob("Minuend", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;
		[ValueConnectionKnob("Subtrahend", Direction.In, "Number")]
		public ValueConnectionKnob bKnob;
		[ValueConnectionKnob("Difference", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number minuend = 0f;
		public Number subtrahend = 0f;
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
			minuend = 0f;
			subtrahend = 0f;
			
			if (aKnob.connected())
				minuend = aKnob.GetValue<Number> ();
			if (bKnob.connected())
				subtrahend = bKnob.GetValue<Number> ();

			outputKnob.SetValue<Number> (minuend - subtrahend);

			label = outputKnob.GetValue<Number>().ToStringShort();

			return true;
		}
	}
}
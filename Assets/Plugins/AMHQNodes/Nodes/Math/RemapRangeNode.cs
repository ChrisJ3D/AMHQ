using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Remap Range")]
	public class RemapRangeNode : Node 
	{
		public const string ID = "RemapRangeNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Remap Range"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (165, 120); } }
		public override string description { get { return "The Remap Range node takes an input value and remaps it to a range of your choosing. For example, if you want to remap a number in currently in the range of 0-64 to be 0-1 instead, you input 0 and 64 in the \"Old Min\" and \"Old Max\" knobs, and 0 and 1 in the \"New min\" and \"New Max\" knobs."; } }

		[ValueConnectionKnob("Input", Direction.In, "Number")]
		public ValueConnectionKnob inputKnob;

		[ValueConnectionKnob("Old Min", Direction.In, "Number")]
		public ValueConnectionKnob oldMinKnob;
		[ValueConnectionKnob("Old Max", Direction.In, "Number")]
		public ValueConnectionKnob oldMaxKnob;

		[ValueConnectionKnob("New Min", Direction.In, "Number")]
		public ValueConnectionKnob newMinKnob;
		[ValueConnectionKnob("New Max", Direction.In, "Number")]
		public ValueConnectionKnob newMaxKnob;

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		private Number input = new Number();
		private Number oldMin = new Number();
		private Number oldMax = new Number();
		private Number newMin = new Number();
		private Number newMax = new Number();
		
		protected string label = "";
		
		public override void NodeGUI () 
		{
			base.NodeGUI();
			
			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}
		
		public override bool Calculate () 
		{
			Number result = new Number();

			if (inputKnob.connected())
				input = inputKnob.GetValue<Number> ();
			if (oldMinKnob.connected())
				oldMin = oldMinKnob.GetValue<Number> ();
			if (oldMaxKnob.connected())
				oldMax = oldMaxKnob.GetValue<Number> ();
			if (newMinKnob.connected())
				newMin = newMinKnob.GetValue<Number> ();
			if (newMaxKnob.connected())
				newMax = newMaxKnob.GetValue<Number> ();

			result = newMin + (input - oldMin) * (newMax - newMin) / (oldMax - oldMin);

			outputKnob.SetValue<Number> (result);

			return true;
		}
	}
}
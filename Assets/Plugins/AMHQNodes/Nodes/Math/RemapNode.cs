using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Remap")]
	public class RemapNode : Node 
	{
		public const string ID = "RemapNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Remap"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 90); } }
		public override string description { get { return "The Remap Range node takes an input value, along with a range, and remaps it to a range of 0..1. If you want to set your own custom output range, use the \"Remap Range\" node."; } }

		[ValueConnectionKnob("Input", Direction.In, "Number")]
		public ValueConnectionKnob inputKnob;

		[ValueConnectionKnob("Old Min", Direction.In, "Number")]
		public ValueConnectionKnob oldMinKnob;
		[ValueConnectionKnob("Old Max", Direction.In, "Number")]
		public ValueConnectionKnob oldMaxKnob;

		[ValueConnectionKnob("", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		private Number input = new Number();
		private Number oldMin = new Number();
		private Number oldMax = new Number();
		
		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			foreach (ConnectionKnob input in inputKnobs)
				input.DisplayLayout ();

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
			Number result = new Number();

			input = 0f;
			oldMin = 0f;
			oldMax = 1f;

			if (inputKnob.connected())
				input = inputKnob.GetValue<Number> ();
			if (oldMinKnob.connected())
				oldMin = oldMinKnob.GetValue<Number> ();
			if (oldMaxKnob.connected())
				oldMax = oldMaxKnob.GetValue<Number> ();

			result = (input - oldMin) / (oldMax - oldMin);

			outputKnob.SetValue<Number> (result);

			label = outputKnob.GetValue<Number> ().ToStringShort();

			return true;
		}
	}
}
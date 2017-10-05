using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Constants/Vector")]
	public class VectorNode : Node 
	{
		public const string ID = "VectorNode";
		public override string GetID { get { return ID; } }
		public override string Title { get { return "Vector"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (200, 100); } }

		public Number value = new Number();

		[ValueConnectionKnob("X", Direction.In, "Number")]
		public ValueConnectionKnob xKnob;

		[ValueConnectionKnob("Y", Direction.In, "Number")]
		public ValueConnectionKnob yKnob;

		[ValueConnectionKnob("Z", Direction.In, "Number")]
		public ValueConnectionKnob zKnob;

		[ValueConnectionKnob("W", Direction.In, "Number")]
		public ValueConnectionKnob wKnob;

		[ValueConnectionKnob("Vector", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{
			// base.NodeGUI();
			value.x = RTEditorGUI.FloatField (value.x);
			value.y = RTEditorGUI.FloatField (value.y);
			value.z = RTEditorGUI.FloatField (value.z);
			value.w = RTEditorGUI.FloatField (value.w);
		}

		public override bool Calculate () 
		{
			// if (xKnob.connected()) {
			// 	value.x = xKnob.GetValue<Number>().x;
			// }

			// if (yKnob.connected()) {
			// 	value.y = yKnob.GetValue<Number>().y;
			// }

			// if (zKnob.connected()) {
			// 	value.z = zKnob.GetValue<Number>().z;
			// }

			// if (wKnob.connected()) {
			// 	value.w = wKnob.GetValue<Number>().w;
			// }

			outputKnob.SetValue<Number> (value);
			return true;
		}
	}
}
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Logic/Conditional")]
	public class ConditionalNode : Node 
	{
		public enum ConditionType { LessThan, LessThanOrEqual, Equal, GreaterThanOrEqual, Greater }
		public ConditionType method = ConditionType.Equal;

		public const string ID = "ConditionalNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Conditional"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (200, 200); } }

		public float Input1Val = 1f;
		public float Input2Val = 1f;

		[ValueConnectionKnob("A", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;

		[ValueConnectionKnob("B", Direction.In, "Number")]
		public ValueConnectionKnob bKnob;

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			if (aKnob.connected())
				aKnob.DisplayLayout();
			else
				Input1Val = RTEditorGUI.FloatField (GUIContent.none, Input1Val);
			
			// --
			if (bKnob.connected())
				bKnob.DisplayLayout();
			else
				Input2Val = RTEditorGUI.FloatField (GUIContent.none, Input2Val);

			GUILayout.EndVertical ();
			GUILayout.BeginVertical ();

			outputKnob.DisplayLayout ();

			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();

	#if UNITY_EDITOR
			method = (ConditionType)UnityEditor.EditorGUILayout.EnumPopup (new GUIContent ("Method", "The type of calculation performed on Input 1 and Input 2"), method);
	#else
			GUILayout.Label (new GUIContent ("Method: " + type.ToString (), "The type of calculation performed on Input 1 and Input 2"));
	#endif
		}

		public override bool Calculate () 
		{
			if (aKnob.connected())
				Input1Val = aKnob.GetValue<Number> ();
			if (bKnob.connected())
				Input2Val = bKnob.GetValue<Number> ();

			switch (method) 
			{
			case ConditionType.LessThan:
				if (outputKnob != null) {
					outputKnob.SetValue<Number> ((Input1Val < Input2Val) ? true : false);
				}
				break;
			case ConditionType.LessThanOrEqual:
				if (outputKnob != null) {
					outputKnob.SetValue<Number> ((Input1Val <= Input2Val) ? true : false);
				}
				break;
			case ConditionType.Equal:
				if (outputKnob != null) {
					outputKnob.SetValue<Number> ((Input1Val == Input2Val) ? true : false);
				}
				break;
			case ConditionType.GreaterThanOrEqual:
				if (outputKnob != null) {
					outputKnob.SetValue<Number> ((Input1Val >= Input2Val) ? true : false);
				}
				break;
			case ConditionType.Greater:
				if (outputKnob != null) {
					outputKnob.SetValue<Number> ((Input1Val > Input2Val) ? true : false);
				}
				break;
			}

			return true;
		}
	}
}

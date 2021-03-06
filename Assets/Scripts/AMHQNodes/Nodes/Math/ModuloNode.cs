﻿using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Modulo")]
	public class ModuloNode : Node 
	{
		public const string ID = "moduloNode";
		public override string GetID { get { return ID; } }
		public override string Title { get { return "Modulo"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 80); } }

		[ValueConnectionKnob("Dividend", Direction.In, "Number")]
		public ValueConnectionKnob dividendKnob;
		[ValueConnectionKnob("Mod Divisior", Direction.In, "Number")]
		public ValueConnectionKnob modDivisorKnob;
		[ValueConnectionKnob("Remainer", Direction.Out, "Number")]
		public ValueConnectionKnob remainderKnob;

		public Number dividend = new Number();
		public Number modDivisor = new Number();
		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			dividendKnob.DisplayLayout ();
			GUILayout.Space(5f);
			modDivisorKnob.DisplayLayout ();

			GUILayout.EndVertical ();
			GUILayout.BeginVertical ();
			
			remainderKnob.DisplayLayout (new GUIContent(label));
			
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}
		
		public override bool Calculate () 
		{
			dividend = 0f;
			modDivisor = 1f;

			if (dividendKnob.connected())
				dividend = dividendKnob.GetValue<Number> ();

			if (modDivisorKnob.connected())
				modDivisor = modDivisorKnob.GetValue<Number> ();

			remainderKnob.SetValue<Number> (dividend % modDivisor);

			label = remainderKnob.GetValue<Number> ().ToStringShort();

			return true;
		}
	}
}
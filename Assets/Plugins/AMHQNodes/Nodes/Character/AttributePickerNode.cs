using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Character/Attribute Picker")]
	public class AttributePickerNode : Node 
	{
		public const string ID = "AttributePickerNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Attribute Picker"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (120, 50); } }

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public CharacterAttribute attribute;
		public Number output = new Number();

		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();

			attribute = (CharacterAttribute)RTEditorGUI.EnumPopup (attribute);

			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			outputKnob.DisplayLayout();

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}
		
		public override bool Calculate () 
		{
			output = (int)attribute;
			outputKnob.SetValue<Number>(output);
			return true;
		}

		public enum CharacterAttribute {
			Stress = 0,
			Charisma = 1,
			Innovation = 2,
			Organisation = 3,
			Knowledge = 4,
			Eloquence = 5
		};
	}
}
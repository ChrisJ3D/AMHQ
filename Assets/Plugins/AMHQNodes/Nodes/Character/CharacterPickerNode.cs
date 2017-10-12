﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Character/Character Picker")]
	public class CharacterPickerNode : Node 
	{
		public const string ID = "CharacterPickerNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Character Picker"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (120, 50); } }

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public ChosenCharacter character;
		public Number output = new Number();

		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();

			character = (ChosenCharacter)RTEditorGUI.EnumPopup (character);

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
			output = (int)character;
			outputKnob.SetValue<Number>(output);
			return true;
		}

		public enum ChosenCharacter {
			Player = 0,
			Bob = 1,
			Jake = 2,
			Bonky = 3,
			Jef = 4
		};
	}
}
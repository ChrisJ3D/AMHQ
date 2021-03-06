﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

		[SerializeField]
		public int characterIndex;
		
		[SerializeField]
		public Number output = new Number();

		protected string label = "";
		List<string> characterAssets = new List<string>();
		
		public override void NodeGUI () 
		{
			#if UNITY_EDITOR
			GUILayout.BeginHorizontal();
			GUILayout.Space(5);
			GUILayout.BeginVertical();
			GUILayout.Space(5);
			characterIndex = EditorGUILayout.Popup(characterIndex, characterAssets.ToArray(), GUILayout.Width(105));

			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			
			outputKnob.DisplayLayout(new GUIContent(""));

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);

			Calculate();
			#endif
		}
		
		public override bool Calculate () 
		{
			if(NodeEditor.curNodeCanvas) {
				var currentCanvas = (AMHQCanvas)NodeEditor.curNodeCanvas;
				characterAssets = currentCanvas.GetCharactersInAssetFolder();
			}

			output = (int)characterIndex;
			outputKnob.SetValue<Number>(output);
			return true;
		}
	}
}
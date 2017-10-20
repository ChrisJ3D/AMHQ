using System;
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

		public int characterIndex;
		public Number output = new Number();

		protected string label = "";
		List<string> characterAssets = new List<string>();
		
		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();

			characterIndex = RTEditorGUI.Popup (characterIndex, characterAssets.ToArray());

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
			var currentCanvas = (AMHQCanvas)NodeEditor.curNodeCanvas;
			characterAssets = currentCanvas.GetCharactersInAssetFolder();

			output = (int)characterIndex;
			outputKnob.SetValue<Number>(output);
			return true;
		}
	}
}
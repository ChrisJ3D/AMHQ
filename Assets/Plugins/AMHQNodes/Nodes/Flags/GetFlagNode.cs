using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections.Generic;
using UnityEditor;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Flags/GetFlag")]
	public class GetFlagNode : Node 
	{
		public const string ID = "GetFlagNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Get Flag"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (120, 50); } }
		public override string description { get { return "This node takes a given item (hopefully from the Item Picker node) and checks if it's in the player's inventory. It will return a boolean value."; } }

		public int chosenFlagIndex;
		List<string> flagAssets = new List<string>();

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(5);
			GUILayout.BeginVertical();
			GUILayout.Space(5);

			chosenFlagIndex = RTEditorGUI.Popup (chosenFlagIndex, flagAssets.ToArray());

			GUILayout.EndVertical();
			
			GUILayout.BeginVertical();

			outputKnob.DisplayLayout(new GUIContent(""));

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}

		public override bool Calculate () 
		{
			if(NodeEditor.curNodeCanvas) {
				var currentCanvas = (AMHQCanvas)NodeEditor.curNodeCanvas;
				flagAssets = currentCanvas.GetFlagsInAssetsFolder();
			}

			outputKnob.SetValue<Number>(chosenFlagIndex);
			return true;
		}
	}
}

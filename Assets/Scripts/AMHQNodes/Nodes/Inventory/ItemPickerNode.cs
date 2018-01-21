using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.IO;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Inventory/Item Picker")]
	public class ItemPickerNode : Node 
	{
		public const string ID = "ItemPickerNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Item Picker"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (120, 50); } }

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public int chosenItemIndex;
		public Number output = new Number();

		protected string label = "";
		List<string> itemAssets = new List<string>();
		
		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(5);
			GUILayout.BeginVertical();
			GUILayout.Space(5);

			chosenItemIndex = RTEditorGUI.Popup (chosenItemIndex, itemAssets.ToArray());

			GUILayout.EndVertical();
			GUILayout.BeginVertical();

			outputKnob.DisplayLayout(new GUIContent(""));

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			if (GUI.changed) {
				NodeEditor.curNodeCanvas.OnNodeChange(this);
			}

			Calculate();
		}
		
		public override bool Calculate () 
		{
			if(NodeEditor.curNodeCanvas) {
				var currentCanvas = (AMHQCanvas)NodeEditor.curNodeCanvas;
				itemAssets = currentCanvas.GetItemsInAssetFolder();
			}

			output = chosenItemIndex;
			outputKnob.SetValue<Number>(output);
			return true;
		}
	}
}
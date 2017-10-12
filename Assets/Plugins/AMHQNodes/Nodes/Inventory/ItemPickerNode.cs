using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

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

		public Item chosenItem;
		public Number output = new Number();

		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();

			chosenItem = (Item)RTEditorGUI.EnumPopup (chosenItem);

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
			output = (int)chosenItem;
			outputKnob.SetValue<Number>(output);
			return true;
		}

		public enum Item {
			Box = 0,
			Log = 1,
			Insurance = 2,
			Gun = 3,
			Paint = 4
		};
	}
}
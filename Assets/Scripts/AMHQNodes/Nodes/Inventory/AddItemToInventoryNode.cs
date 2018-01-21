using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Inventory/Add Item")]
	public class AddItemToInventory : Node 
	{
		public const string ID = "AddItemToInventory";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Add Item to Inventory"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (180, 70); } }
		public override string description { get { return "This node takes a given item (hopefully from the Item Picker node) and adds it to the player's inventory. You can use the \"Allow Duplicates\" input to allow/disallow duplicates."; } }

		[ValueConnectionKnob("Item", Direction.In, "Number")]
		public ValueConnectionKnob itemKnob;

		[ValueConnectionKnob("Allow Duplicates", Direction.In, "Number")]
		public ValueConnectionKnob duplicatesKnob;

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			itemKnob.DisplayLayout();
			duplicatesKnob.DisplayLayout();

			GUILayout.EndVertical ();
			GUILayout.BeginVertical ();

			outputKnob.DisplayLayout ();

			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}

		public override bool Calculate () 
		{

			return true;
		}
	}
}

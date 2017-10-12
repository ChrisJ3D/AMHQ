using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Inventory/Is Item in Inventory")]
	public class CheckItemNode : Node 
	{
		public const string ID = "CheckItemNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Is Item in Inventory?"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (160, 50); } }
		public override string description { get { return "This node takes a given item (hopefully from the Item Picker node) and checks if it's in the player's inventory. It will return a boolean value."; } }

		public float Input1Val = 1f;

		[ValueConnectionKnob("Item", Direction.In, "Number")]
		public ValueConnectionKnob itemKnob;

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			itemKnob.DisplayLayout();

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

using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Inventory/Remove Item")]
	public class RemoveItemFromInventoryNode : Node 
	{
		public const string ID = "RemoveItemFromInventoryNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Remove Item"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (200, 90); } }
		public override string description { get { return "This node takes a given item (hopefully from the Item Picker node) and removes it to the player's inventory. You can use the \"Remove All\" input to remove every single instance of the item, if there are multiple."; } }

		[ConnectionKnob("Flow In", Direction.In, "Flow", NodeSide.Left, 10)]
		public ConnectionKnob flowIn;
		[ConnectionKnob("Flow Out", Direction.Out, "Flow", NodeSide.Right, 10)]
		public ConnectionKnob flowOut;

		[ValueConnectionKnob("Item", Direction.In, "Number")]
		public ValueConnectionKnob itemKnob;

		[ValueConnectionKnob("Remove All", Direction.In, "Number")]
		public ValueConnectionKnob removeAllKnob;

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{
			
			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			flowIn.DisplayLayout();
			itemKnob.DisplayLayout();
			removeAllKnob.DisplayLayout();

			GUILayout.EndVertical ();
			GUILayout.BeginVertical ();

			flowOut.DisplayLayout();
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

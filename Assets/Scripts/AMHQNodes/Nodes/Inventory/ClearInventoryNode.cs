using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Inventory/Clear Inventory")]
	public class ClearInventoryNode : Node 
	{
		public const string ID = "ClearInventoryNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Clear Inventory"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (200, 50); } }
		public override string description { get { return "This node will remove every single item from the player's inventory. Use with caution!!"; } }

		[ConnectionKnob("Flow In", Direction.In, "Flow", NodeSide.Left, 10)]
		public ConnectionKnob flowIn;
		[ConnectionKnob("Flow Out", Direction.Out, "Flow", NodeSide.Right, 10)]
		public ConnectionKnob flowOut;

		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			flowIn.DisplayLayout();

			GUILayout.EndVertical ();
			GUILayout.BeginVertical ();

			flowOut.DisplayLayout ();

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

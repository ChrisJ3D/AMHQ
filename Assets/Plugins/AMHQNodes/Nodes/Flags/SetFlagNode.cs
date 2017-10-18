using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Flags/SetFlag")]
	public class SetFlagNode : Node 
	{
		public const string ID = "SetFlagNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Set Flag"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (200, 70); } }
		public override string description { get { return "This node takes a given item (hopefully from the Item Picker node) and adds it to the player's inventory. You can use the \"Allow Duplicates\" input to allow/disallow duplicates."; } }

		[ValueConnectionKnob("Value", Direction.In, "Number")]
		public ValueConnectionKnob valueKnob;

		public Flag chosenFlag;

		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();

			valueKnob.DisplayLayout(new GUIContent(""));

			GUILayout.EndVertical();
			GUILayout.Space(5);
			GUILayout.BeginVertical();

			chosenFlag = (Flag)RTEditorGUI.EnumPopup (chosenFlag);

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}

		public override bool Calculate () 
		{

			return true;
		}
	}
}

using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Flags/GetFlag")]
	public class GetFlagNode : Node 
	{
		public const string ID = "GetFlagNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Get Flag"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (160, 80); } }
		public override string description { get { return "This node takes a given item (hopefully from the Item Picker node) and checks if it's in the player's inventory. It will return a boolean value."; } }

		public Flag chosenFlag;

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public override void NodeGUI () 
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(5);
			GUILayout.BeginVertical();

			chosenFlag = (Flag)RTEditorGUI.EnumPopup (chosenFlag);

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
			return true;
		}
	}

	public enum Flag {
			MarriedValen = 0,
			FreedTingle = 1,
			KilledTheAnimals = 2,
			SavedTheFrames = 3,
		};
}

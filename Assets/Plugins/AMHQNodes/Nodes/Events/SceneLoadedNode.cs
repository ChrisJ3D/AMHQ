using System;
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections.Generic;
using UnityEditor;

namespace NodeEditorFramework.Standard
{
	[System.Serializable]
	[Node (false, "Events/SceneLoaded")]
	public class SceneLoadedNode : BaseConversationNode 
	{
		public override string Title {get { return "Scene Loaded"; } }
		public override Vector2 MinSize { get { return new Vector2(200, 60); } }
		public override bool AutoLayout { get { return true; } }

		private const string Id = "sceneLoadedNode";
		public override string GetID { get { return Id; } }
		public override Type GetObjectType { get { return typeof (SceneLoadedNode); } }

		[ValueConnectionKnob("To Next", Direction.Out, "DialogueForward", NodeSide.Right, 30)]
		public ValueConnectionKnob toNextOUT;

		private Vector2 scroll;
		public int nodeIndex = 0;

		protected override void OnCreate ()
		{
			base.OnCreate ();
			CharacterName = "Character name";
			DialogLine = "This is the SceneLoadedNode, this line should hopefully not be rendered.";
			CharacterPotrait = null;
		}

		public override void NodeGUI()
		{
			GUILayout.Space(5);
		}

		public override BaseConversationNode GetDownsteamNode(int inputValue)
		{
			switch (inputValue)
			{
			case (int)EDialogInputValue.Next:
				if (IsNextAvailable ())
					return getTargetNode (toNextOUT);
				
				break;
			}
			return null;
		}

		public override bool IsBackAvailable()
		{
			return false;
		}

		public override bool IsNextAvailable()
		{
			return IsAvailable (toNextOUT);
		}
	}
}

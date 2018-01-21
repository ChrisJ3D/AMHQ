using System;
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using AMHQ;

[System.Serializable]
[Node (false, "Events/Start")]
public class SceneLoadedNode : BaseConversationNode 
{
	public override string Title {get { return "Start"; } }
	public override Vector2 MinSize { get { return new Vector2(100, 40); } }
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

	public override BaseConversationNode GetDownstreamNode(int inputValue)
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

	public override BaseConversationNode PassAhead(int inputValue)
	{
		return GetDownstreamNode(inputValue);
	}
}

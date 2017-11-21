using System;
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections.Generic;
using UnityEditor;
using AMHQ;
using UnityEngine.SceneManagement;

[System.Serializable]
[Node (false, "Events/LoadScene")]
public class LoadSceneNode : BaseConversationNode 
{
	public override string Title {get { return "Load Scene"; } }
	public override Vector2 MinSize { get { return new Vector2(200, 60); } }
	public override bool AutoLayout { get { return true; } }

	private const string Id = "LoadSceneNode";
	public override string GetID { get { return Id; } }
	public override Type GetObjectType { get { return typeof (LoadSceneNode); } }

	[ValueConnectionKnob("To Next", Direction.In, "DialogueForward", NodeSide.Left, 30)]
	public ValueConnectionKnob inputKnob;

	private Vector2 scroll;
	public int nodeIndex = 0;
	public string sceneName;

	protected override void OnCreate ()
	{
		base.OnCreate ();
		CharacterName = "Character name";
		DialogLine = "This is the LoadSceneNode, this line should hopefully not be rendered.";
		CharacterPotrait = null;
	}

	public override void NodeGUI()
	{
		base.NodeGUI();
		sceneName = EditorGUILayout.TextArea(sceneName, GUILayout.MinWidth(80));
	}

	// public override bool Calculate() {
	// 	SceneManager.LoadSceneAsync(sceneName);
	// 	SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
	// 	return true;
	// }

	public override BaseConversationNode GetDownstreamNode(int inputValue)
	{
		switch (inputValue)
		{
		case (int)EDialogInputValue.Back:
			if (IsNextAvailable ())
				return getTargetNode (inputKnob).PassAhead(inputValue);
			break;

		default:
			return null;
		}
		
		return null;
	}

	public override bool IsBackAvailable()
	{
		return IsAvailable(inputKnob);
	}

	public override bool IsNextAvailable()
	{
		return false;
	}
}

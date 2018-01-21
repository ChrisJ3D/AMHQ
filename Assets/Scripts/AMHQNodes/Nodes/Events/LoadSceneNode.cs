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
[Node (false, "Events/LoadScene")]
public class LoadSceneNode : BaseConversationNode 
{
	public override string Title {get { return "Load Scene"; } }
	public override Vector2 MinSize { get { return new Vector2(200, 45); } }
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
		#if UNITY_EDITOR
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		inputKnob.DisplayLayout(new GUIContent(""));

		GUILayout.EndVertical();
		
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		sceneName = EditorGUILayout.TextField(sceneName, GUILayout.Width(145), GUILayout.ExpandHeight(false));
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		
		if(GUILayout.Button("...")) {
			string path = UnityEditor.EditorUtility.OpenFilePanel("Choose Scene Graph", "/Resources/Graphs/", "asset");
			if (path != null && path != "") {
				sceneName = path.Substring(path.IndexOf("Graphs/") + 7);
				sceneName = sceneName.Split(".".ToCharArray())[0];
			}
		}
		

		GUILayout.EndVertical();

		GUILayout.Space(5);
		GUILayout.EndHorizontal();
		#endif
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

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[Node (false, "Events/Play Sound Effect")]
public class PlaySoundEffectNode : BaseConversationNode 
{
	public const string ID = "PlaySoundEffectNode";
	public override string GetID { get { return ID; } }
	public override string Title { get { return "Play Sound Effect"; } }
	public override Vector2 MinSize { get { return new Vector2(180, 60); } }
	public override bool AutoLayout { get { return true; } }
	public override Vector2 DefaultSize { get { return new Vector2 (120, 90); } }

	public override string description { get { return "The Branch node reroutes the level flow based on the incoming condition. The condition should be in a boolean format (true/false)."; } }
	public override Type GetObjectType { get { return typeof(PlaySoundEffectNode); } }

	[ValueConnectionKnob("", Direction.In, "DialogueForward", NodeSide.Left, 10)]
	public ValueConnectionKnob flowIn;

	[ValueConnectionKnob("", Direction.Out, "DialogueForward", NodeSide.Right, 10)]
	public ValueConnectionKnob flowOut;

	public AudioClip bgm = null;
	public bool loop = true;
	
	public override void NodeGUI () 
	{
		#if UNITY_EDITOR
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();

		flowIn.DisplayLayout(new GUIContent(""));

		GUILayout.EndVertical();

		bgm = (AudioClip)EditorGUILayout.ObjectField(new GUIContent(""), bgm, typeof(AudioClip), false, GUILayout.Width(160));

		GUILayout.BeginVertical();

		flowOut.DisplayLayout(new GUIContent(""));

		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();

		GUILayout.Space(10);

		GUILayout.Label("Loop");
		loop = EditorGUILayout.Toggle(loop);

		GUILayout.EndHorizontal();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
		
		#endif
	}
	
	public override bool Calculate () 
	{
		GameManager gameManager = FindObjectOfType<GameManager>();
		
		gameManager.PlaySoundEffect(bgm, loop);

		return GetDownstreamNode(-2);
	}

	public override bool IsBackAvailable() {
		return IsAvailable(flowIn);
	}

	public override bool IsNextAvailable() {
		return IsAvailable(flowOut);
	}

	public override BaseConversationNode PassAhead(int inputValue)
	{
		Calculate();
		
		return GetDownstreamNode(inputValue);
	}

	public override BaseConversationNode GetDownstreamNode(int inputValue)
	{
		if (IsNextAvailable()) {
			return getTargetNode (flowOut).PassAhead(inputValue);
		}

		return null;
	}
}
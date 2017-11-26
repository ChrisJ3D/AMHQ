using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[Node (false, "Events/Set Background Image")]
public class SetBackgroundImageNode : BaseConversationNode 
{
	public const string ID = "SetBackgroundImageNode";
	public override string GetID { get { return ID; } }
	public override string Title { get { return "Set Background Image"; } }
	public override Vector2 MinSize { get { return new Vector2(180, 80); } }
	public override bool AutoLayout { get { return true; } }
	public override Vector2 DefaultSize { get { return new Vector2 (120, 90); } }

	public override string description { get { return "The Branch node reroutes the level flow based on the incoming condition. The condition should be in a boolean format (true/false)."; } }
	public override Type GetObjectType { get { return typeof(SetBackgroundImageNode); } }

	[ValueConnectionKnob("", Direction.In, "DialogueForward", NodeSide.Left, 10)]
	public ValueConnectionKnob flowIn;

	[ValueConnectionKnob("", Direction.Out, "DialogueForward", NodeSide.Right, 10)]
	public ValueConnectionKnob flowOut;

	public Sprite bg = null;
	
	public override void NodeGUI () 
	{
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();

		flowIn.DisplayLayout(new GUIContent(""));

		GUILayout.EndVertical();

		bg = (Sprite)EditorGUILayout.ObjectField(new GUIContent(""), bg, typeof(Sprite), false, GUILayout.Width(75));
		
		GUILayout.BeginVertical();

		flowOut.DisplayLayout(new GUIContent(""));

		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}
	
	public override bool Calculate () 
	{
		GameManager gameManager = FindObjectOfType<GameManager>();
		
		gameManager.SetBackgroundImage(bg);

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
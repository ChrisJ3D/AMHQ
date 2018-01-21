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

[Node (false, "Events/Fade")]
public class FadeNode : BaseConversationNode 
{
	public const string ID = "FadeNode";
	public override string GetID { get { return ID; } }
	public override string Title { get { return "Fade"; } }
	public override Vector2 MinSize { get { return new Vector2(180, 60); } }
	public override bool AutoLayout { get { return true; } }
	public override Vector2 DefaultSize { get { return new Vector2 (120, 90); } }

	public override string description { get { return "The Branch node reroutes the level flow based on the incoming condition. The condition should be in a boolean format (true/false)."; } }
	public override Type GetObjectType { get { return typeof(FadeNode); } }

	[ValueConnectionKnob("", Direction.In, "DialogueForward", NodeSide.Left, 10)]
	public ValueConnectionKnob flowIn;

	[ValueConnectionKnob("", Direction.Out, "DialogueForward", NodeSide.Right, 10)]
	public ValueConnectionKnob flowOut;

	[SerializeField]
	private enum Mode {
		FadeIn = 0,
		FadeOut = 1
	};

	string[] options = new string[2];
	int[] values = new int[2];

	[SerializeField]
	private int mode = 0;

	[SerializeField]
	private float duration = 1f;

	protected override void OnCreate() {

	}
	
	public override void NodeGUI () 
	{
		#if UNITY_EDITOR
		options[0] = "In";
		options[1] = "Out";
		values[0] = 0;
		values[1] = 1;
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();

		flowIn.DisplayLayout(new GUIContent(""));

		GUILayout.EndVertical();

		// mode = EditorGUILayout.EnumPopup(new GUIContent(""), mode, GUILayout.Width(105));
		GUILayout.Label("Fade");
		mode = EditorGUILayout.IntPopup(mode, options, values);

		GUILayout.BeginVertical();

		flowOut.DisplayLayout(new GUIContent(""));

		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();

		GUILayout.Space(10);

		GUILayout.Label("Duration");
		duration = EditorGUILayout.FloatField(duration, GUILayout.Width(60));

		GUILayout.Space(10);

		GUILayout.EndHorizontal();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
		
		#endif
	}
	
	public override bool Calculate () 
	{
		GameManager gameManager = FindObjectOfType<GameManager>();
		
		gameManager.Fade((int)mode, duration);

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
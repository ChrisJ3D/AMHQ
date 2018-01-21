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
[Node (false, "Events/Load Hub")]
public class LoadHubNode : BaseConversationNode 
{
	public override string Title {get { return "Load Hub"; } }
	public override Vector2 MinSize { get { return new Vector2(110, 45); } }
	public override bool AutoLayout { get { return true; } }

	private const string Id = "LoadHubNode";
	public override string GetID { get { return Id; } }
	public override Type GetObjectType { get { return typeof (LoadHubNode); } }

	[ValueConnectionKnob("", Direction.In, "DialogueForward", NodeSide.Left, 30)]
	public ValueConnectionKnob inputKnob;

	private Vector2 scroll;
	public int nodeIndex = 0;
	public int selection;
	public string[] hubs;

	protected override void OnCreate ()
	{
		base.OnCreate ();
		hubs = new string[2];
		hubs[0] = "Home";
		hubs[1] = "Work";
	}

	public override void NodeGUI()
	{
		#if UNITY_EDITOR
		GUILayout.BeginHorizontal();

		inputKnob.DisplayLayout();
		
		selection = EditorGUILayout.Popup(selection, hubs, GUILayout.Width(80));

		GUILayout.Space(15);
		
		GUILayout.EndHorizontal();
		#endif
	}

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

using System;
using NodeEditorFramework;
using UnityEditor;
using UnityEngine;
using NodeEditorFramework.Utilities;

/// <summary>
/// This node has one entry and one exit, it is just to display something, then move on
/// </summary>
[Node(false, "Conversation/Dialogue", new Type[] { typeof(AMHQCanvas) })]
public class DialogueNode : BaseConversationNode
{
	public override string Title {get { return "Dialogue"; } }
	public override Vector2 MinSize { get { return new Vector2(350, 60); } }
	public override bool AutoLayout { get { return true; } }

	private const string Id = "dialogueNode";
	public override string GetID { get { return Id; } }
	public override Type GetObjectType { get { return typeof(DialogNode); } }

	//Previous Node Connections
	[ValueConnectionKnob("From Previous", Direction.In, "DialogueForward", NodeSide.Left, 30)]
	public ValueConnectionKnob fromPreviousIN;
	[ConnectionKnob("To Previous", Direction.Out, "DialogueBack", NodeSide.Left, 50)]
	public ConnectionKnob toPreviousOut;

	//Next Node to go to
	[ValueConnectionKnob("To Next", Direction.Out, "DialogueForward", NodeSide.Right, 30)]
	public ValueConnectionKnob toNextOUT;
	[ConnectionKnob("From Next",Direction.In, "DialogueBack", NodeSide.Right, 50)]
	public ConnectionKnob fromNextIN;

	[ValueConnectionKnob("Character", Direction.In, "Number", NodeSide.Left, 30)]
		public ValueConnectionKnob characterKnob;

	public CharacterPosition characterPosition = 0;
	private Vector2 scroll;

	protected override void OnCreate ()
	{
		CharacterName = "Character Name";
		DialogLine = "Insert dialogue text here";
		CharacterPotrait = null;

	}

	public override void NodeGUI()
	{
		GUILayout.BeginHorizontal();

		scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Height(100));
		EditorStyles.textField.wordWrap = true;
		DialogLine = EditorGUILayout.TextArea(DialogLine, GUILayout.ExpandHeight(true));
		EditorStyles.textField.wordWrap = false;
		EditorGUILayout.EndScrollView();
		GUILayout.EndHorizontal();

		characterKnob.DisplayLayout();
		GUILayout.Label(""+ speakerIndex);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Screen Position");
		GUILayout.Space(30);
		characterPosition = (CharacterPosition)RTEditorGUI.EnumPopup (new GUIContent(""),characterPosition);

		GUILayout.EndHorizontal();
		GetSpeaker();
	}

	public override BaseConversationNode Input(int inputValue)
	{
		switch (inputValue)
		{
		case (int)EDialogInputValue.Next:
			if (IsNextAvailable ())
				return getTargetNode (toNextOUT);
			break;
		case (int)EDialogInputValue.Back:
			if (IsBackAvailable ())
				return getTargetNode (fromPreviousIN);
			break;
		}
		return null;
	}

	public override void Compile() {
		
		speakerIndex = characterKnob.GetValue<Number>();
		Debug.Log("Compiling with speakerIndex at " + speakerIndex);
		Debug.Log("KnobValue is " + characterKnob.GetValue<Number>());
	}

	public override bool IsBackAvailable()
	{
		return IsAvailable (toPreviousOut);
	}

	public override bool IsNextAvailable()
	{
		return IsAvailable (toNextOUT);
	}

	public void GetSpeaker() {
		speakerIndex = characterKnob.GetValue<Number>();
	}

	public enum CharacterPosition {
		Left = 0,
		Middle = 1,
		Right = 2
	};
}

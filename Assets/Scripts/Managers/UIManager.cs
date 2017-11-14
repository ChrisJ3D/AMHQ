using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework.Standard;

public class UIManager : Singleton<UIManager> {

	public GameManager gameManager;
	public GameObject UI_DialogueBoxPrefab;
	public GameObject UI_QuestionBoxPrefab;

	[SerializeField]
	private RectTransform _canvasObject;

	private UI_DialogueBox _dialogueBox;
	private UI_QuestionBox _questionBox;
	private NodeManager _nodeManager;
	private CharacterManager _characterManager;

	public override void Initialize(MonoBehaviour parent) {
		gameManager = parent as GameManager;
		_nodeManager = gameManager.nodeManager;
		_characterManager = gameManager.characterManager;
	}

	public void SetDialogueBoxType(BaseConversationNode node) {
		_dialogueBox.ClearContents();

		if(node == null) {
			_dialogueBox.DialogueComplete();
		} else if (node is SceneLoadedNode) {
			okButton();
		} else if (node is DialogueNode) {
			UpdateDialogueBox((DialogueNode) node);
		} else if (node is QuestionNode) {
			InitializeQuestionBox((QuestionNode) node);
		}
	}

	public void InitializeDialogueBox() {
		_dialogueBox = GameObject.Instantiate(UI_DialogueBoxPrefab).GetComponent<UI_DialogueBox>();
		_dialogueBox.Construct(this);
		_dialogueBox.transform.SetParent(_canvasObject, false);

		SetDialogueBoxType(gameManager.nodeManager.startNode);
	}

	public void UpdateDialogueBox(DialogueNode node) {
		if (_dialogueBox == null) {
			InitializeDialogueBox();
			return;
		}

		_characterManager.ShowCharacter(node.speakerIndex);
		string speakerName = _characterManager.characterList[node.speakerIndex].firstName;
		string dialogueLine = node.DialogLine;
		_dialogueBox.SetAsDialogueNode(dialogueLine, speakerName, node.IsBackAvailable(), node.IsNextAvailable());
	}

	public void InitializeQuestionBox(QuestionNode node) {
		_questionBox = Instantiate(UI_QuestionBoxPrefab).GetComponent<UI_QuestionBox>();
		_questionBox.transform.SetParent(_canvasObject, false);

		_questionBox.CreateOptions(node.GetAllOptions(), optionSelected);
		//GrowMessageBox(node.GetAllOptions().Count);

		_dialogueBox.SetAsQuestionNode(node.DialogLine, node.IsBackAvailable());

		_characterManager.HideCharacter();
	}

	private void GrowQuestionBox(int count) {
		Vector2 size = GetComponent<RectTransform>().sizeDelta;
		size.y += (count * _questionBox.CellHeight());
		GetComponent<RectTransform>().sizeDelta = size;
	}

	public void okButton() {
		_nodeManager.StepForward();
		SetDialogueBoxType(_nodeManager.currentNode);
	}

	public void backButton() {
		_nodeManager.StepBackward();
		SetDialogueBoxType(_nodeManager.currentNode);
	}

	public void optionSelected(int option) {
		_nodeManager.OptionSelected(option);
		SetDialogueBoxType(_nodeManager.currentNode);
	}
}

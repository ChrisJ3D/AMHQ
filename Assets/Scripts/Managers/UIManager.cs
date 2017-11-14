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

	public override void Initialize(MonoBehaviour parent) {
		gameManager = parent as GameManager;
	}

	public void SetDialogueBoxType(BaseConversationNode node) {
		_dialogueBox.ClearContents();

		if(node == null) {
			DialogueComplete();
		} else if (node is SceneLoadedNode) {
			okButton();
		} else if (node is DialogueNode) {
			UpdateDialogueBox((DialogueNode) node);
		} else if (node is QuestionNode) {
			InitializeQuestionBox((QuestionNode) node);
		}
	}

	public void InitializeDialogueBox(BaseConversationNode node) {
		_dialogueBox = GameObject.Instantiate(UI_DialogueBoxPrefab).GetComponent<UI_DialogueBox>();
		_dialogueBox.Construct(this);
		_dialogueBox.transform.SetParent(_canvasObject, false);

		SetDialogueBoxType(node);
	}

	public void UpdateDialogueBox(DialogueNode node) {
		if (_dialogueBox == null) {
			InitializeDialogueBox(node);
			return;
		}

		gameManager.ShowCharacter(node.speakerIndex);
		string speakerName = gameManager.GetCharacter(node.speakerIndex).firstName;
		string dialogueLine = node.DialogLine;
		_dialogueBox.SetAsDialogueNode(dialogueLine, speakerName, node.IsBackAvailable(), node.IsNextAvailable());
	}

	public void InitializeQuestionBox(QuestionNode node) {
		_questionBox = Instantiate(UI_QuestionBoxPrefab).GetComponent<UI_QuestionBox>();
		_questionBox.transform.SetParent(_canvasObject, false);

		_questionBox.CreateOptions(node.GetAllOptions(), optionSelected);
		//GrowMessageBox(node.GetAllOptions().Count);

		_dialogueBox.SetAsQuestionNode(node.DialogLine, node.IsBackAvailable());

		gameManager.HideCharacters();
	}

	private void GrowQuestionBox(int count) {
		Vector2 size = GetComponent<RectTransform>().sizeDelta;
		size.y += (count * _questionBox.CellHeight());
		GetComponent<RectTransform>().sizeDelta = size;
	}

	public void okButton() {
		gameManager.StepForward();
		SetDialogueBoxType(gameManager.GetCurrentNode());
	}

	public void backButton() {
		gameManager.StepBackward();
		SetDialogueBoxType(gameManager.GetCurrentNode());
	}

	public void optionSelected(int option) {
		gameManager.SelectOption(option);
		SetDialogueBoxType(gameManager.GetCurrentNode());
	}

	public void DialogueComplete() {
		DestroyObject(_dialogueBox);
	}
}

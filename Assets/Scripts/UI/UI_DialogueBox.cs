using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeEditorFramework.Standard;
using System;

public class UI_DialogueBox : MonoBehaviour {

	private NodeManager _nodeManager;
	public CharacterManager characterManager;
	public UIManager uiManager;

	[SerializeField]
	private GameObject _backButton;
	[SerializeField]
	private GameObject _okButton;
	[SerializeField]
	private UI_QuestionHolder _optionsHolder;
	[SerializeField]
	private GameObject _dialogueLineBox;

	private RectTransform _canvasObject;

	public void Construct(NodeManager nodeManager, RectTransform canvasObject) {
		_nodeManager = nodeManager;
		_canvasObject = canvasObject;
		_backButton.SetActive(false);
	}

	//	Signals from the button gameobject
	public void okButton() {
		_nodeManager.okButton();
	}

	public void backButton() {
		_nodeManager.backButton();
	}

	private void OptionSelected(int option) {
		_nodeManager.OptionSelected(option);
	}

	//	Check the type of node being processed and call functions accordingly
	public void SetData(BaseConversationNode node) {
		ClearContents();

		if(node == null) {
			DialogueComplete();
		} else if (node is SceneLoadedNode) {
			SetAsStartNode((SceneLoadedNode) node);
		} else if (node is DialogueNode) {
			SetAsDialogueNode((DialogueNode) node);
		} else if (node is QuestionNode) {
			SetAsQuestionNode((QuestionNode) node);
		}
	}

	private void SetAsStartNode(SceneLoadedNode node) {
		_nodeManager.okButton();
	}

	//	This is the golden function that grabs all the data from the node and inserts it into the UI
	private void SetAsDialogueNode(DialogueNode node) {
		Character speaker = uiManager.gameManager.characterManager.characterList[node.speakerIndex];

		uiManager.gameManager.characterManager.ShowCharacter(node.speakerIndex);
		
		this.GetComponentsInChildren<Text>()[0].text = speaker.firstName;
		this._dialogueLineBox.GetComponent<Text>().text = node.DialogLine;
	}
	
	private void SetAsQuestionNode(QuestionNode node) {
		_backButton.SetActive(node.IsBackAvailable());
		_okButton.SetActive(false);
		uiManager.gameManager.characterManager.HideCharacter();

		this._dialogueLineBox.GetComponent<Text>().text = node.DialogLine;

		UI_QuestionHolder questionHolder = Instantiate(_optionsHolder).GetComponent<UI_QuestionHolder>();
		questionHolder.transform.SetParent(_canvasObject,false);
		questionHolder.CreateOptions(node.GetAllOptions(), OptionSelected);
		//GrowMessageBox(node.GetAllOptions().Count);
	}

	private void GrowMessageBox(int count) {
		Vector2 size = GetComponent<RectTransform>().sizeDelta;
		size.y += (count * _optionsHolder.CellHeight());
		GetComponent<RectTransform>().sizeDelta = size;
	}

	private void ClearContents() {
		this.GetComponentsInChildren<Text>()[0].text = "";
		this._dialogueLineBox.GetComponent<Text>().text = "";
	}

	private void DialogueComplete() {
		DestroyObject(gameObject);
	}
	
}

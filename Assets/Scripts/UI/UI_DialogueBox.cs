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
	private OptionsHandler _optionsHolder;
	[SerializeField]
	private GameObject _dialogueLineBox;

	public Character speaker;

	public string dialogueLine;

	public bool isPreviousAvailable;
	public bool isForwardAvailable;

	public int previousNodeIndex;
	public int forwardNodeIndex;

	public void Construct(NodeManager nodeManager) {
		_nodeManager = nodeManager;
		_backButton.SetActive(false);
	}

	//	Signals from the button gameobject
	public void okButton() {
		_nodeManager.okButton();
	}

	public void backButton() {
		_nodeManager.backButton();
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
		}
	}

	private void SetAsStartNode(SceneLoadedNode node) {
		_nodeManager.okButton();
	}

	//	This is the golden function that grabs all the data from the node and inserts it into the UI
	private void SetAsDialogueNode(DialogueNode node) {
		dialogueLine = node.DialogLine;

		Character speaker = uiManager.gameManager.characterManager.characterList[node.speakerIndex];

		uiManager.gameManager.characterManager.ShowCharacter(node.speakerIndex);
		
		this.GetComponentsInChildren<Text>()[0].text = speaker.firstName;
		this._dialogueLineBox.GetComponent<Text>().text = dialogueLine;
	}

	private void SetAsDialogueNode2(Character speaker) {

		speaker.GetComponent<Image>().enabled = true;
		
		this.GetComponentsInChildren<Text>()[0].text = speaker.firstName;
		this._dialogueLineBox.GetComponent<Text>().text = dialogueLine;
	}

	private void ClearContents() {
		this.GetComponentsInChildren<Text>()[0].text = "";
		this._dialogueLineBox.GetComponent<Text>().text = "";

	}

	private void DialogueComplete() {
		DestroyObject(gameObject);
	}
}

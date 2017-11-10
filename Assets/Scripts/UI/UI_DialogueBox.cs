using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeEditorFramework.Standard;
using System;

public class UI_DialogueBox : MonoBehaviour {

	private int _nodeID;
	private NodeManager _nodeManager;
	public CharacterManager characterManager;
	public UIManager uiManager;

	[SerializeField]
	private GameObject _backButton;
	[SerializeField]
	private GameObject _okButton;
	[SerializeField]
	private OptionsHandler _optionsHolder;

	public Character speaker;

	public string dialogueLine;

	public bool isPreviousAvailable;
	public bool isForwardAvailable;

	public int previousNodeIndex;
	public int forwardNodeIndex;

	public void Construct(int nodeID, NodeManager nodeManager) {
		_nodeID = nodeID;
		_nodeManager = nodeManager;
		_backButton.SetActive(false);
		//_okButton.GetComponentInChildren<GUIText>().text = "OKAY";

		characterManager = GameObject.Find("System").GetComponent<CharacterManager>();
	}

	public void Construct2(int nodeID) {

	}

	//	Signals from the button gameobject
	public void okButton() {
		_nodeManager.okButton(_nodeID);
	}

	public void backButton() {
		_nodeManager.backButton(_nodeID);
	}

	//	Check the type of node being processed and call functions accordingly
	public void SetData(BaseConversationNode node) {
		ResetMessageBox();

		if(node == null) {
			DialogueComplete();
		} else if (node is SceneLoadedNode) {
			SetAsStartNode((SceneLoadedNode) node);
		} else if (node is DialogueNode) {
			SetAsDialogueNode((DialogueNode) node);
		}
	}

	private void SetAsStartNode(SceneLoadedNode node) {
		_backButton.SetActive(node.IsBackAvailable());
		_okButton.SetActive(true);		
	}

	//	This is the golden function that grabs all the data from the node and inserts it into the UI
	private void SetAsDialogueNode(DialogueNode node) {

		// _speakerName = characterManager.characterList[node.GetSpeaker()].firstName;

		string _speakerName = "Orien";

		GameObject character = null;

		if (_speakerName != "") {
			character = GameObject.Find(_speakerName); 
			}

		character.GetComponent<Image>().enabled = true;
		this.GetComponentInChildren<Text>().text = node.DialogLine;
	}

	private void SetAsDialogueNode2(Character speaker) {

		speaker.GetComponent<Image>().enabled = true;
		this.GetComponentsInChildren<Text>()[0].text = speaker.firstName;
		this.GetComponentsInChildren<Text>()[1].text = dialogueLine;
	}

	private void ResetMessageBox() {
		//	_optionsHolder.ClearList();
	}

	private void DialogueComplete() {
		_nodeManager.RemoveDialogueBox(_nodeID);
		DestroyObject(gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeEditorFramework.Standard;
using System;

public class UI_DialogueBox : MonoBehaviour {

	private int _nodeID;
	private NodeManager _nodeManager;

	private GameObject _backButton;
	private GameObject _okButton;
	private OptionsHandler _optionsHolder;

	private Image _speakerSprite;
	private string _speakerName;

	public CharacterManager characterManager;


	public void Construct(int nodeID, NodeManager nodeManager) {
		_nodeID = nodeID;
		_nodeManager = nodeManager;
		_backButton.SetActive(false);
		_okButton.GetComponentInChildren<GUIText>().text = "OKAY";
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
		}
		else if (node is SceneLoadedNode) {
			SetAsStartNode((SceneLoadedNode) node);
		}
		else if (node is DialogueNode) {
			SetAsDialogueNode((DialogueNode) node);
		}

	}

	private void SetAsStartNode(SceneLoadedNode node) {
		_backButton.SetActive(node.IsBackAvailable());
		_okButton.SetActive(true);
		
	}

	private void SetAsDialogueNode(DialogueNode node) {

		_speakerName = characterManager.characterList[node.GetSpeaker()].firstName;

		if (_speakerName != "") {
			GameObject.Find(_speakerName).SetActive(true); 
			}

	}



	private void ResetMessageBox() {
		_optionsHolder.ClearList();
	}

	private void DialogueComplete() {
		_nodeManager.RemoveDialogueBox(_nodeID);
		DestroyObject(gameObject);
	}
}

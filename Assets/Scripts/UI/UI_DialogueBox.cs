using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AMHQ {
	public class UI_DialogueBox : MonoBehaviour {

		[SerializeField]
		private GameObject _backButton;
		[SerializeField]
		private GameObject _okButton;
		[SerializeField]
		private GameObject _dialogueLineBox;

		public GameObject nameField;

		private UIManager _uiManager;

<<<<<<< HEAD
		public void Construct(UIManager parent) {
			_uiManager = parent;
			_backButton.SetActive(false);
		}

		//	Signals from the button gameobject
		public void okButton() {
			_uiManager.okButton();
		}

		public void backButton() {
			_uiManager.backButton();
		}

		public void SetAsDialogueNode(string dialogueLine, string speakerName, bool isBackAvailable, bool isNextAvailable) {
			nameField.GetComponent<Text>().text = speakerName;
			_dialogueLineBox.GetComponent<Text>().text = dialogueLine;
			
			_backButton.SetActive(isBackAvailable);
			_okButton.SetActive(isNextAvailable);
		}
		
		public void SetAsQuestionNode(string dialogueLine, bool isBackAvailable) {
			_dialogueLineBox.GetComponent<Text>().text = dialogueLine;

			_backButton.SetActive(isBackAvailable);
			_okButton.SetActive(false);
		}
=======
	public UIManager uiManager;

	public void Construct(int nodeID, NodeManager nodeManager) {
		_nodeID = nodeID;
		_nodeManager = nodeManager;
		_backButton.SetActive(false);
		//_okButton.GetComponentInChildren<GUIText>().text = "OKAY";
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

		_speakerName = "Orien";

		GameObject character = null;

		if (_speakerName != "") {
			character = GameObject.Find(_speakerName); 
			}

		character.GetComponent<Image>().enabled = true;
		this.GetComponentInChildren<Text>().text = node.DialogLine;
	}

	private void ResetMessageBox() {
		//	_optionsHolder.ClearList();
	}
>>>>>>> origin/master

		public void ClearContents() {
			nameField.GetComponent<Text>().text = "";
			_dialogueLineBox.GetComponent<Text>().text = "";
		}
	}
}
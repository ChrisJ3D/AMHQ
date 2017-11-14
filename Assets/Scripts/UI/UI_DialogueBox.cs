using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DialogueBox : MonoBehaviour {

	[SerializeField]
	private GameObject _backButton;
	[SerializeField]
	private GameObject _okButton;
	[SerializeField]
	private GameObject _dialogueLineBox;

	private UIManager _uiManager;

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
		this.GetComponentsInChildren<Text>()[0].text = speakerName;
		_dialogueLineBox.GetComponent<Text>().text = dialogueLine;
		
		_backButton.SetActive(isBackAvailable);
		_okButton.SetActive(isNextAvailable);
	}
	
	public void SetAsQuestionNode(string dialogueLine, bool isBackAvailable) {
		_dialogueLineBox.GetComponent<Text>().text = dialogueLine;

		_backButton.SetActive(isBackAvailable);
		_okButton.SetActive(false);
	}

	public void ClearContents() {
		this.GetComponentsInChildren<Text>()[0].text = "";
		_dialogueLineBox.GetComponent<Text>().text = "";
	}
}

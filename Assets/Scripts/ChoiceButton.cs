using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour {

	public string option;
	public DialogueManager box;

	public void SetText(string newText) {
		this.GetComponentInChildren<UnityEngine.UI.Text>().text = newText;
	}

	public void SetOption(string newOption) {
		this.option = newOption;
	}

	public void ParseOption() {
		string command = option.Split(',')[0];
		string commandModifier = option.Split(',')[1];
		box.playerTalking = false;

		if(command == "line") {
			box.lineNum = int.Parse(commandModifier);
			box.ClearButtons();
			box.ShowDialogue();
		} else if (command == "scene") {
			UnityEngine.SceneManagement.SceneManager.LoadScene("Scene" + commandModifier);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

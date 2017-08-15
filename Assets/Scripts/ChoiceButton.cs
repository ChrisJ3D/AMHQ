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
		

		if(command == "line") {
			box.isChoosing = false;
			box.lineNum = int.Parse(commandModifier);
			box.ClearButtons();
			box.ShowDialogue();
		} else if (command == "scene") {
			UnityEngine.SceneManagement.SceneManager.LoadScene("Scene" + commandModifier);
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	DialogueParser parser;

	public string dialogue, characterName;
	public int lineNum;
	public float textSpeed = 0.01f;
	int pose;
	string position;
	string[] options;
	public bool playerTalking;
	List<Button> buttons = new List<Button>();

	public Text dialogueBox;
	public Text nameBox;
	public GameObject choiceBox;

	bool isTyping;
	public bool isChoosing;

	void Start () {
		dialogue = "";
		characterName = "";
		pose = 0;
		position = "L";
		playerTalking = false;
		isTyping = false;
		parser = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
		lineNum = 0;
	}
	
	public void OnClick () {
		if(!isTyping) {
			ShowDialogue();
			lineNum++;
			UpdateUI();
		}
	}

	public void ShowDialogue() {
		ResetImages();
		ParseLine();
	}

	void ResetImages() {
		if (characterName != "") {
			GameObject character = GameObject.Find(characterName);
			//Image currentSprite = character.GetComponent<Image>();
			//currentSprite.sprite = null;
		}
	}

	void ParseLine() {
		if (parser.GetName (lineNum) != "Player") {
			playerTalking = false;
			characterName = parser.GetName(lineNum);
			dialogue = parser.GetContent(lineNum);
			pose = parser.GetPose(lineNum);
			position = parser.GetPosition(lineNum);
			DisplayImages();
		} else {
			playerTalking = true;
			characterName = "";
			dialogue = "";
			pose = 0;
			position = "";
			options = parser.GetOptions(lineNum);
			CreateButtons();
		}		
	}

	void DisplayImages() {
		if (characterName != "") {
			GameObject character = GameObject.Find(characterName);

			SetSpritePositions(character);
			SpriteRenderer currentSprite = character.GetComponent<SpriteRenderer>();
			currentSprite.sprite = character.GetComponent<Character>().characterPoses[pose];
		}
	}

	void SetSpritePositions(GameObject spriteObject) {
		Vector3 Left = new Vector3(-6,0,0);
		Vector3 Right = new Vector3(6,0,0);
		Vector3 Middle = new Vector3(0,0,0);

		if (position == "L") {
			spriteObject.transform.position = Left;
		} else if (position == "R") {
			spriteObject.transform.position = Right;
		} else {
			spriteObject.transform.position = Middle;
		}
		//spriteObject.transform.position = new Vector3(spriteObject.transform.position.x, spriteObject.transform.position.y, 0);
	}

	void CreateButtons() {

		for (int i = 0; i < options.Length; i++) {
			GameObject button = (GameObject)Instantiate(choiceBox);
			Button b = button.GetComponent<Button>();
			ChoiceButton cb = button.GetComponent<ChoiceButton>();

			cb.SetText(options[i].Split(':')[0]);
			cb.option = options[i].Split(':')[1];
			cb.box = this;

			b.transform.SetParent(this.transform);
			b.transform.localPosition = new Vector3(0,-25 + (i*200));
			b.transform.localScale = new Vector3(1,1,1);
			buttons.Add(b);
		}
	}

	public void UpdateUI() {
		nameBox.text = characterName;
		StartCoroutine(TypeText());
	}

	IEnumerator TypeText() {
		dialogueBox.text = "";
		isTyping = true;

		foreach (char letter in dialogue.ToCharArray()) {
			dialogueBox.text += letter;
			yield return 0;
			yield return new WaitForSeconds(textSpeed);
		}
		isTyping = false;
	}

	public void ClearButtons() {
		print("FUNCTION CALL: ClearButtons");
		foreach(Button b in buttons) {
			Destroy(b.gameObject);
		}
		buttons.Clear();
	}
}

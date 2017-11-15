using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AMHQ {
	public class DialogueManager : Singleton<DialogueManager> {

		ScriptParser parser;

		public string dialogue, characterName;
		public int lineNum;
		public float textSpeed = 0.01f;
		// int pose;
		string position;
		string[] options;
		public bool isChoosing;
		List<Button> buttons = new List<Button>();

		public Text dialogueBox;
		public Text nameBox;
		public GameObject choiceBox;

		bool isTyping;

		void Start () {
			dialogue = "";
			characterName = "";
			// pose = 0;
			position = "L";
			isChoosing = false;
			isTyping = false;
			parser = GameObject.Find("ScriptParser").GetComponent<ScriptParser>();
			lineNum = 0;
		}
		
		public void OnClick () {
			//	When the player attempts to further the dialogue, we need to check that we're not
			//	in the middle of typing text or choosing
			if(!isChoosing) {
				if(isTyping) {
					TypeTextImmediately();
				} else {
					ShowDialogue();
					lineNum++;
					UpdateUI();
				}
			}
		}

		public void ShowDialogue() {
			ResetImages();
			ParseLine();
		}

		void ResetImages() {
			if (characterName != "") {
				// GameObject character = GameObject.Find(characterName);
				// Image currentSprite = character.GetComponent<Image>();
				// currentSprite.sprite = null;
			}
		}

		void ParseLine() {
			if (parser.GetName (lineNum) != "Player") {
				isChoosing = false;
				characterName = parser.GetName(lineNum);
				dialogue = parser.GetContent(lineNum);
				// pose = parser.GetPose(lineNum);
				position = parser.GetPosition(lineNum);
				DisplayImages();
			} else {
				isChoosing = true;
				characterName = "";
				dialogue = "";
				// pose = 0;
				position = "";
				options = parser.GetOptions(lineNum);
				CreateButtons();
			}
		}

		void DisplayImages() {
			if (characterName != "") {
				GameObject character = GameObject.Find(characterName);

				SetSpritePositions(character);
				// SpriteRenderer currentSprite = character.GetComponent<SpriteRenderer>();
				// currentSprite.sprite = character.GetComponent<Character>().characterPoses[pose];
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

		public void UpdateUI() {
			nameBox.text = characterName;
			StartCoroutine("TypeText");
		}

		IEnumerator TypeText() {
			//	TypeText() is a purely graphical function to make text appear in a typing fashion
			dialogueBox.text = "";
			isTyping = true;

			foreach (char letter in dialogue.ToCharArray()) {
				dialogueBox.text += letter;
				yield return 0;
				yield return new WaitForSeconds(textSpeed);
			}
			isTyping = false;
		}

		void TypeTextImmediately() {
			StopCoroutine("TypeText");
			isTyping = false;
			dialogueBox.text = dialogue;
		}

		void CreateButtons() {
			//	CreateButtons() creates dialogue options and displays them on screen.

			for (int i = 0; i < options.Length; i++) {
				GameObject button = (GameObject)Instantiate(choiceBox);
				Button b = button.GetComponent<Button>();

				b.transform.SetParent(this.transform);
				b.transform.localPosition = new Vector3(0,-25 + (i*200));
				b.transform.localScale = new Vector3(1,1,1);
				buttons.Add(b);
			}
		}

		public void ClearButtons() {
			//	ClearButtons() is to be called when a dialogue choice has been made, and proceeds to
			//	destroy all choiceButton objects.

			foreach(Button b in buttons) {
				Destroy(b.gameObject);
			}
			buttons.Clear();
		}
	}
}
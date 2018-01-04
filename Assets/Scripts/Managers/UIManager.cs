using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeEditorFramework.Standard;

namespace AMHQ {
	public class UIManager : Singleton<UIManager> {

		public GameManager gameManager;
		public GameObject UI_DialogueBoxPrefab;
		public GameObject UI_QuestionBoxPrefab;

		public RectTransform _canvasObject;

		public UI_DialogueBox _dialogueBox;
		public UI_QuestionBox _questionBox;
		public GameObject BG;
		public GameObject fadeScreen;
		
		public GameObject homeHub;
		public GameObject workHub;

		public override void Initialize(MonoBehaviour parent) {
			gameManager = parent as GameManager;
		}

		public void OnSceneLoad() {
			_dialogueBox.gameObject.SetActive(false);
			_questionBox.gameObject.SetActive(false);
		}

		public void SetDialogueBoxType(BaseConversationNode node) {
			_dialogueBox.ClearContents();

			if(node == null) {
				DialogueComplete();
			} else if (node is SceneLoadedNode) {
				okButton();
			} else if (node is DialogueNode) {
				UpdateDialogueBox((DialogueNode) node);
			} else if (node is QuestionNode) {
				InitializeQuestionBox((QuestionNode) node);
			} else if (node is LoadSceneNode) {
				LoadScene((LoadSceneNode) node);
			} else if (node is LoadHubNode) {
				LoadHub((LoadHubNode) node);
			}
		}

		public void InitializeDialogueBox(BaseConversationNode node) {
			_dialogueBox.gameObject.SetActive(true);
			_dialogueBox.Construct(this);

			SetDialogueBoxType(node);
		}

		public void UpdateDialogueBox(DialogueNode node) {
			if (_dialogueBox == null) {
				InitializeDialogueBox(node);
				return;
			}

			gameManager.SetCharacterPosition(node.speakerIndex, node.GetCharacterPosition());
			gameManager.ShowCharacter(node.speakerIndex);
			
			string speakerName = gameManager.GetCharacter(node.speakerIndex).firstName;
			string dialogueLine = ProcessString(node.DialogLine);
			_dialogueBox.SetAsDialogueNode(dialogueLine, speakerName, node.IsBackAvailable(), node.IsNextAvailable());
		}

		public void InitializeQuestionBox(QuestionNode node) {
			_questionBox.gameObject.SetActive(true);
			_questionBox.CreateOptions(node.GetAllOptions(), optionSelected);
			//GrowMessageBox(node.GetAllOptions().Count);

			_dialogueBox.SetAsQuestionNode(node.DialogLine, node.IsBackAvailable());

			gameManager.HideCharacters();
		}

		private void GrowQuestionBox(int count) {
			Vector2 size = GetComponent<RectTransform>().sizeDelta;
			size.y += (count * _questionBox.CellHeight());
			GetComponent<RectTransform>().sizeDelta = size;
		}

		public string ProcessString(string line) {
			string newLine = line;

			if (line.Contains("{name}")) {
				newLine = line.Replace("{name}", gameManager.GetPlayerName());
			}

			return newLine;
		}

		public void okButton() {
			gameManager.StepForward();
			SetDialogueBoxType(gameManager.GetCurrentNode());
		}

		public void backButton() {
			gameManager.StepBackward();
			SetDialogueBoxType(gameManager.GetCurrentNode());
		}

		public void optionSelected(int option) {
			gameManager.SelectOption(option);
			SetDialogueBoxType(gameManager.GetCurrentNode());
		}

		private void DialogueComplete() {
			_dialogueBox.gameObject.SetActive(false);
			_questionBox.gameObject.SetActive(false);
		}

		private void LoadScene(LoadSceneNode node) {
			DialogueComplete();
			gameManager.LoadScene(node.sceneName);
		}

		private void LoadHub(LoadHubNode node) {
			DialogueComplete();
			gameManager.LoadHub(node.selection);
		}

		public void ShowHub(string hub) {
			Debug.Log("Showing hub " + hub);
			switch (hub) {
				case "work":
				homeHub.SetActive(false);
				workHub.SetActive(true);
				break;

				case "home":
				workHub.SetActive(false);
				homeHub.SetActive(true);
				break;
			}
		}

		public void SetBackgroundImage (Sprite image) {
			BG.GetComponent<Image>().sprite = image;
		}

		public void Fade(int mode, float duration) {
			switch (mode) {
				case 0:
					Debug.Log("Fading in");
					StartCoroutine(Fade(fadeScreen.GetComponent<Image>(),Color.black, Color.clear, duration));
					break;
				
				case 1:
				Debug.Log("Fading out");
					StartCoroutine(Fade(fadeScreen.GetComponent<Image>(),Color.clear, Color.black, duration));
					break;
			}
			
		}

		IEnumerator Fade(Image image, Color start, Color end, float duration) {

			for (float t = 0f; t < duration; t += Time.deltaTime) {
				float normalizedTime = t/duration;
				
				image.color = Color.Lerp(start, end, normalizedTime);
				yield return null;
				}

				image.color = end; //without this, the value will end at something like 0.9992367
			}

	}
}
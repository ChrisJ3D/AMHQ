using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework.Standard;

namespace AMHQ {
	public class UIManager : Singleton<UIManager> {

		public GameManager gameManager;
		public GameObject UI_DialogueBoxPrefab;
		public GameObject UI_QuestionBoxPrefab;

		public RectTransform _canvasObject;

		public UI_DialogueBox _dialogueBox;
		public UI_QuestionBox _questionBox;

		public override void Initialize(MonoBehaviour parent) {
			gameManager = parent as GameManager;
		}

		public void OnSceneLoad() {
			Debug.Log("UIManager: OnSceneLoad");
			_canvasObject = FindObjectOfType<Canvas>().transform as RectTransform;
			_dialogueBox = null;
			_questionBox = null;
		}

		public void SetDialogueBoxType(BaseConversationNode node) {
			Debug.Log("Setting Dialogue Box Type");
			_dialogueBox.ClearContents();

			if(node == null) {
				Debug.Log("Setting to null");
				DialogueComplete();
			} else if (node is SceneLoadedNode) {
				Debug.Log("Setting to SceneLoadedNode");
				okButton();
			} else if (node is DialogueNode) {
				Debug.Log("Setting to DialogueNode");
				UpdateDialogueBox((DialogueNode) node);
			} else if (node is QuestionNode) {
				Debug.Log("Setting to Question");
				InitializeQuestionBox((QuestionNode) node);
			} else if (node is LoadSceneNode) {
				Debug.Log("Setting to LoadScene");
				LoadScene((LoadSceneNode) node);
			}
		}

		public void InitializeDialogueBox(BaseConversationNode node) {
			Debug.Log("Initializing dialogue box");
			_dialogueBox = GameObject.Instantiate(UI_DialogueBoxPrefab).GetComponent<UI_DialogueBox>();
			_dialogueBox.Construct(this);
			_dialogueBox.transform.SetParent(_canvasObject, false);

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
			_questionBox = Instantiate(UI_QuestionBoxPrefab).GetComponent<UI_QuestionBox>();
			_questionBox.transform.SetParent(_canvasObject, false);

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
			DestroyObject(_dialogueBox);
			DestroyObject(_questionBox);
		}

		private void LoadScene(LoadSceneNode node) {
			DialogueComplete();
			gameManager.LoadScene(node.sceneName);
		}
	}
}
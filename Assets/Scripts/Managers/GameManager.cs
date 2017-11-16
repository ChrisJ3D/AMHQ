using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NodeEditorFramework;

namespace AMHQ {
	public class GameManager : Singleton<GameManager> {

		public CharacterManager characterManager;
		private InventoryManager inventoryManager;
		public NodeManager nodeManager;
		public PlayerManager playerManager;
		private TimeManager timeManager;
		public UIManager uiManager;

		public string currentScene;

		public override void Awake() {
			base.Awake();

			currentScene = SceneManager.GetActiveScene().name;

			characterManager = (CharacterManager)GetComponent("CharacterManager");
			inventoryManager = (InventoryManager)GetComponent("InventoryManager");
			nodeManager = (NodeManager)GetComponent("NodeManager");
			playerManager = (PlayerManager)GetComponent("PlayerManager");
			timeManager = (TimeManager)GetComponent("TimeManager");
			uiManager = (UIManager)GetComponent("UIManager");

			characterManager.Initialize(this);
			inventoryManager.Initialize(this);
			nodeManager.Initialize(this);
			playerManager.Initialize(this);
			timeManager.Initialize(this);
			uiManager.Initialize(this);
		}

		void Start () {
			//	Start reading from node graph
			uiManager.InitializeDialogueBox(nodeManager.startNode);
		}

		public void LoadScene(string sceneName) {
			SceneManager.LoadScene(sceneName);
		}

	//	CharacterManager functions

		public Character GetCharacter(int index) {
			return characterManager.characterList[index];
		}

		public void ShowCharacter(int index) {
			characterManager.ShowCharacter(index);
		}

		public void HideCharacters() {
			characterManager.HideCharacter();
		}

		public void SetCharacterPosition(int characterIndex, CharacterPosition position) {
			characterManager.SetCharacterPosition(characterIndex, position);
		}

	//	NodeManager functions

		public BaseConversationNode GetCurrentNode() {
			return nodeManager.currentNode;
		}

		public void StepForward() {
			nodeManager.StepForward();
		}

		public void StepBackward() {
			nodeManager.StepBackward();
		}

		public void SelectOption(int option) {
			nodeManager.OptionSelected(option);
		}

	//	PlayerManager functions

		public string GetPlayerName() {
			return playerManager.playerName;
		}
	}
}
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

			OnSceneLoad();
		}

		void Start () {
			//	Start reading from node graph
			
		}

		public void LoadScene(string sceneName) {
			SceneManager.LoadScene(sceneName);
			OnSceneLoad();
		}

		public void OnSceneLoad() {
			Debug.Log("OnSceneLoad");
			currentScene = SceneManager.GetActiveScene().name;
			nodeManager.OnSceneLoad();
			uiManager.OnSceneLoad();
			uiManager.InitializeDialogueBox(nodeManager.startNode);
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

		public Dictionary<CharacterAttributeType, Number> GetPlayerAttributes() {
			return playerManager.attributes;
		}

		public void SetPlayerAttribute(CharacterAttributeType attribute, Number value) {
			playerManager.SetAttribute(attribute, value);
		}

		public void AdjustPlayerAttribute(CharacterAttributeType attribute, Number value) {
			
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NodeEditorFramework;
using System;

namespace AMHQ {
	public class GameManager : Singleton<GameManager> {

		public CharacterManager characterManager;
		private InventoryManager inventoryManager;
		public NodeManager nodeManager;
		public PlayerManager playerManager;
		private TimeManager timeManager;
		public UIManager uiManager;

		public string startingScene;
		public string currentScene;

		public GameObject BGMPlayer;
		public GameObject SFXPlayer;
		
		public override void Awake() {
			base.Awake();

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
			currentScene = startingScene;
			OnSceneLoad();
		}

		public void LoadScene(string sceneName) {
			// SceneManager.LoadScene(sceneName);
			currentScene = sceneName;
			OnSceneLoad();
		}

		public void OnSceneLoad() {
			nodeManager.OnSceneLoad(currentScene);
			uiManager.OnSceneLoad();
			uiManager.InitializeDialogueBox(nodeManager.startNode);
		}

        public void LoadHub(int index) {
			switch (index) {
				case 0:
				uiManager.ShowHub("home");
				break;

				case 1:
				uiManager.ShowHub("work");
				break;
			}
		}

		internal void CheckAfterWorkEvents()
        {
            //	foreach flag in workFlags
			//	flag.launchEvent
			uiManager.ShowHub("home");
        }

	#region	CharacterManager functions

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

	#endregion
	#region	NodeManager functions

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
		
	#endregion
	#region	PlayerManager functions

		public string GetPlayerName() {
			return playerManager.firstName;
		}

		public Dictionary<CharacterAttributeType, Number> GetPlayerAttributes() {
			return playerManager.attributes;
		}

		public Number GetAttribute(CharacterAttributeType attribute) {
			return playerManager.GetAttribute(attribute);
		}


		public void SetPlayerAttribute(CharacterAttributeType attribute, Number value) {
			playerManager.SetAttribute(attribute, value);
		}

		public void AdjustPlayerAttribute(CharacterAttributeType attribute, Number value) {
			playerManager.AdjustAttribute(attribute, value);
		}
	#endregion
	#region UIManager functions
	
		public void SetBackgroundImage(Sprite image) {
			if (image == null){
				return;
			}

			uiManager.SetBackgroundImage(image);
		}

		public void Fade(int mode, float duration) {
			uiManager.Fade(mode, duration);
		}

		public int GetRemaingActions()
        {
            return uiManager.workHub.GetComponent<UI_WorkHub>().actionsPerDay;
        }

	#endregion

		public void SetBackgroundMusic(AudioClip clip, bool loop) {
			AudioSource source = BGMPlayer.GetComponent<AudioSource>();
			source.clip = clip;
			source.loop = loop;
			source.Play();
		}

		public void PlaySoundEffect(AudioClip clip, bool loop) {
			AudioSource source = SFXPlayer.GetComponent<AudioSource>();
			source.clip = clip;
			source.loop = loop;
			source.Play();
		}


	}
}
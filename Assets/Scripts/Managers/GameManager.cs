using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NodeEditorFramework;

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

	// Use this for initialization
	void Start () {
		//	Start reading from node graph
		nodeManager.StartDialogue();
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void PassNodeToUI() {
		
	}

	//	NodeManager functions
}
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

	public override void Awake() {
		base.Awake();

		characterManager = (CharacterManager)GetComponent("CharacterManager");
		inventoryManager = (InventoryManager)GetComponent("InventoryManager");
		nodeManager = (NodeManager)GetComponent("NodeManager");
		playerManager = (PlayerManager)GetComponent("PlayerManager");
		timeManager = (TimeManager)GetComponent("TimeManager");
		uiManager = (UIManager)GetComponent("UIManager");

		characterManager.gameManager = this;
		inventoryManager.gameManager = this;
		nodeManager.gameManager = this;
		playerManager.gameManager = this;
		timeManager.gameManager = this;
		uiManager.gameManager = this;
	}

	// Use this for initialization
	void Start () {
		//	Start reading from node graph
		nodeManager.ShowDialogueByID(1, true);
	}

	public void LoadScene(string sceneName) {
		var currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(sceneName);
	}

	public void PassNodeToUI() {
		
	}

	//	NodeManager functions

	public BaseConversationNode GetNodeByID (int nodeID) {
		return nodeManager.GetNodeByID(nodeID);
	}

	public void FetchNodeData(int nodeID, int inputValue) {
		nodeManager.FetchNodeData(nodeID, inputValue);
	}
}
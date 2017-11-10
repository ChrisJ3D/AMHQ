using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NodeEditorFramework;

public class GameManager : Singleton<GameManager> {

	public CharacterManager characterManager;
	private TimeManager timeManager;
	public UIManager uiManager;
	public NodeManager nodeManager;
	private InventoryManager inventoryManager;
	public PlayerManager playerManager;

	public override void Awake() {
		base.Awake();

		inventoryManager = (InventoryManager)GetComponent("InventoryManager");
		nodeManager = (NodeManager)GetComponent("NodeManager");
		characterManager = (CharacterManager)GetComponent("CharacterManager");
		uiManager = (UIManager)GetComponent("UIManager");
		
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
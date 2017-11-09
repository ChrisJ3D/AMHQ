using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NodeEditorFramework;

public class GameManager : Singleton<GameManager> {

	public GameObject CharacterManager;
	private TimeManager _timeManager;
	public GameObject UIManager;
	public NodeManager _nodeManager;
	private InventoryManager _inventoryManager;

	public override void Awake() {
		base.Awake();

		_inventoryManager = (InventoryManager)GetComponent("InventoryManager");
		_nodeManager = (NodeManager)GetComponent("NodeManager");
	}

	// Use this for initialization
	void Start () {
		//	Start reading from node graph
		_nodeManager.ShowDialogueByID(1, true);
	}

	public void LoadScene(string sceneName) {
		var currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(sceneName);

	}
}
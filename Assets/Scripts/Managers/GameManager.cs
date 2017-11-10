using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NodeEditorFramework;

public class GameManager : Singleton<GameManager> {

	private CharacterManager _characterManager;
	private TimeManager _timeManager;
	public UIManager _UIManager;
	private NodeManager _nodeManager;
	private InventoryManager _inventoryManager;
	private PlayerManager _playerManager;

	public override void Awake() {
		base.Awake();

		_inventoryManager = (InventoryManager)GetComponent("InventoryManager");
		_nodeManager = (NodeManager)GetComponent("NodeManager");
		_characterManager = (CharacterManager)GetComponent("CharacterManager");
		_playerManager = (PlayerManager)GetComponent("PlayerManager");
		_UIManager = (UIManager)GetComponent("UIManager");

		_inventoryManager.gameManager = this;
		_nodeManager.gameManager = this;
		_characterManager.gameManager = this;
		_playerManager.gameManager = this;
		_UIManager.gameManager = this;
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
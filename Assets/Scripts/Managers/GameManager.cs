using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NodeEditorFramework;

public class GameManager : Singleton<GameManager> {

	public GameObject CharacterManager;
	private TimeManager _timeManager;
	public GameObject UIManager;
	public GameManager NodeManager;
	private InventoryManager _inventoryManager;

	// Use this for initialization
	void Start () {
		_inventoryManager = (InventoryManager)GetComponent("InventoryManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadScene(string sceneName) {
		var currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(sceneName);

	}
}
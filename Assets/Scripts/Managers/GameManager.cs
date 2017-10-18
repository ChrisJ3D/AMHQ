using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject CharacterManager;
	public GameObject TimeManager;
	public GameObject UIManager;
	public GameObject NodeManager;
	public InventoryManager _inventoryManager;

	void DontDestroyOnLoad() {

	}

	// Use this for initialization
	void Start () {
		_inventoryManager = (InventoryManager)GetComponent("InventoryManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

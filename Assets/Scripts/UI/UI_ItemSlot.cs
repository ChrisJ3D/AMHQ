using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour {


	public GameObject[] itemSlot;
	InventoryManager inventory;

	// Use this for initialization
	void Start () {
		inventory = FindObjectOfType<Player>().GetComponent<InventoryManager>();
	}
	
	public void RefreshItemSlot() {
		int x = 0;

		foreach (Item i in inventory.items) {
			if (i) {
				itemSlot[x].transform.GetChild(2).GetComponent<Text>().text = i.name;
				x++;
				}
		}
	}
}

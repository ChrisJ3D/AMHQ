using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public const int numItemSlots = 6;
	public Image[] itemImages = new Image[numItemSlots];
	public Item[] items = new Item[numItemSlots];

	public float Funds = 1000.0f;

	UI_ItemSlot inventory_ui;

	void Start() {
		inventory_ui = FindObjectOfType<UI_ItemSlot>();

	}

	public void AddItem(Item itemToAdd) {
		for(int i = 0; i < items.Length; i++) {
			if (items[i] == null) {
				items[i] = itemToAdd;
				inventory_ui.RefreshItemSlot();
			//	itemImages[i].sprite = itemToAdd.sprite;
			//	itemImages[i].enabled = true;
				return;
			}
		}
	}

	public void RemoveItem(Item itemToRemove) {
		for(int i = 0; i < items.Length; i++) {
			if (items[i] == itemToRemove) {
				items[i] = null;
				itemImages[i].sprite = null;
				itemImages[i].enabled = false;
				return;
			}
		}
	}

	public bool HasItemByName(string Name) {
		bool hasItem = false;
		foreach (Item i in items) {
			if (i) {
				if (i.name == Name) {
					hasItem = true;
					return hasItem;
				}
			}
		} 
		return hasItem;
	}

	public bool HasItemByBrand(string Brand) {
		return false;
	}

	public bool HasItemByType(string Type) {
		return false;
	}


}

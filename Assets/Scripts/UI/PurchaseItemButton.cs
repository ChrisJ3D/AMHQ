using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PurchaseItemButton : MonoBehaviour {

	public Item item;
	InventoryManager playerInventory;
	
	public void OnClick() {
		
		float funds = playerInventory.Funds;

		if (funds >= item.cost) {
			playerInventory.AddItem(item);
			funds = funds - item.cost;
		}
		
		playerInventory.Funds = funds;
		print("funds are now " + funds.ToString());
	}
	// Use this for initialization
	void Start () {
		playerInventory = FindObjectOfType<Player>().GetComponent<InventoryManager>();
	}
}

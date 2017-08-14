using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public Sprite sprite = null;
	public float cost;
	public ItemType type;
	public Brand brand;
	
	public enum ItemType {
		Top,
		Bottom,
		Hat,
		Accessory,
		Gift,
		Special
	};

	public enum Brand {
		None,
		PrettyInPink,
		Starlite,
		FrillyLuxurious,
		UltraTratt,
		BokstavligStövel
	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

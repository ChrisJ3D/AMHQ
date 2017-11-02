using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	[Header("Sprites")]
	public Sprite[] characterPoses = null; 

	public Sprite[] portraits = null;

	[Header("Initial Stats")]
	public AffectionLevel StartingAffection;

	public float currentAffection;
	public int index;
	
	public enum AffectionLevel {
		Hostile, 
		Disintrested,
		Neutral,
		Friendly,
		Interested,
		Affectionate
		};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

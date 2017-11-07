using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	[Header("Character")]
	public string firstName;
	public string lastName;

	[Header("Initial Stats")]
	public AffectionLevel StartingAffection;

	public float currentAffection;

	[Header("Sprites")]
	public Sprite[] characterPoses = null; 
	public Sprite[] portraits = null;

	[HideInInspector]
	public int index;
	[HideInInspector]
	public int characterIndex;
	[HideInInspector]
	public CharacterManager characterManager;
	
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

	void OnAwake() {
		characterManager = FindObjectOfType<CharacterManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

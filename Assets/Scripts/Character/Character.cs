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
	public Sprite[] characterSprites = null; 
	public Sprite[] portraits = null;
	public int activeSprite = 0;

	[HideInInspector]
	public int index;
	[HideInInspector]
	public int characterIndex;
	[HideInInspector]
	public CharacterManager characterManager;

	private Image _imageComponent;
	
	public enum AffectionLevel {
		Hostile, 
		Disintrested,
		Neutral,
		Friendly,
		Interested,
		Affectionate
		};

	void Awake() {
		_imageComponent = this.GetComponent<Image>();
	}

	public void Show () {
		Show(activeSprite);
	}

	public void Show(int pose) {
		activeSprite = pose;
		_imageComponent.sprite =  characterSprites[activeSprite];
		_imageComponent.enabled = true;
	}

	public void Hide() {
		this.GetComponent<Image>().enabled = false;
	}
}

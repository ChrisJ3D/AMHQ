using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AMHQ {
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

		void Awake() {
			_imageComponent = this.GetComponent<Image>();
		}

		public void Show () {
			Show(activeSprite);
		}

		public void Show(int pose) {
			bool enable = true;
			activeSprite = pose;
			try {
				_imageComponent.sprite =  characterSprites[activeSprite];
			}
			catch {
				enable = false;
			}
			_imageComponent.enabled = enable;
		}

		public void SetPositionLeft() {
			if (transform.localPosition.x == -250f) {
				return;
			} else 
			if (transform.localPosition.x == 0f) {
				transform.Translate(-250f, 0f, 0f);
			} else 
			if (transform.localPosition.x == 250f) {
				transform.Translate(-500f, 0f, 0f);
			}
		}

		public void SetPositionMiddle() {
			
			if (transform.localPosition.x == -250f) {
				transform.Translate(250f, 0f, 0f);
			} else 
			if (transform.localPosition.x == 0f) {
				return;
			} else 
			if (transform.localPosition.x == 250f) {
				transform.Translate(-250f, 0f, 0f);
			}
		}

		public void SetPositionRight() {
			if (transform.localPosition.x == -250f) {
				transform.Translate(500f, 0f, 0f);
			} else 
			if (transform.localPosition.x == 0f) {
				transform.Translate(250f, 0f, 0f);
			} else 
			if (transform.localPosition.x == 250f) {
				return;
			}
		}

		public void Hide() {
			_imageComponent.enabled = false;
		}
	}
}
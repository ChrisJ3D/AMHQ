using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AMHQ;

namespace AMHQ {
	public class UI_Hub : MonoBehaviour {

		protected UIManager uiManager;
		protected GameManager gameManager;

		public Sprite backgroundImage;
		public AudioClip backgroundMusic;

		public GameObject actionsPanel;
		public GameObject StatusPanel;
		public GameObject attributesPanel;
		public GameObject fundsPanel;
		public GameObject DatePanel;

		public int actionsPerDay;

		protected virtual void Start () {
			uiManager = FindObjectOfType<UIManager>();
			gameManager = FindObjectOfType<GameManager>();
		}

		protected virtual void Activate() {

		}

		protected virtual void Deactivate() {
			
		}

		protected virtual void CheckActions() {
			actionsPerDay--;
			if (actionsPerDay <= 0) {
				DayComplete();
			}
		}

		protected virtual void DayComplete() {
			
		}
	}
}

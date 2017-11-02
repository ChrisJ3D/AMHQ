using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	public bool isTimedScreen = false;
	public float screenTime = 3f;
	public GameObject targetTimedScreen;

	private float startTime = 0f;


	// Use this for initialization
	void Start () {
		SetupCanvas();
	}

	void OnEnable() {
		SetupCanvas();
	}
	
	// Update is called once per frame
	void Update () {
		if (isTimedScreen) {
			while (Time.time < startTime + screenTime) {
				return;
			}

			if (targetTimedScreen) {
				gameObject.SetActive(false);
				targetTimedScreen.SetActive(true);
			}
		}
	}

	void SetupCanvas() {
		startTime = Time.time;
	}
}

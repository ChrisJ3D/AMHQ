using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour {

	public float TextSpeed = 0.1f;

	public string message;
	public Text textComponent;

	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text>();
	}

	public void AnimateText() {
		message = textComponent.text;
		textComponent.text = "";
		StartCoroutine(TypeText());
	}

	IEnumerator TypeText() {
		foreach (char letter in message.ToCharArray()) {
			textComponent.text += letter;
			yield return 0;
			yield return new WaitForSeconds(TextSpeed);
		}
	}
}

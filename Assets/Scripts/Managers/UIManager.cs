using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

	public GameObject UI_DialogueBoxPrefab;
	private Dictionary<int, UI_DialogueBox> _dialogueBoxes;

	[SerializeField]
	private RectTransform _canvasObject;

	public GameManager gameManager;

	public override void Initialize(MonoBehaviour parent) {
		gameManager = parent as GameManager;
	}

	public void PopulateDialogueBox(int nodeID, DialogueNode node) {

	}
}

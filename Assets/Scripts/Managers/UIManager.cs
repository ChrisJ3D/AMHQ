using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

	public GameObject UI_DialogueBoxPrefab;
	private Dictionary<int, UI_DialogueBox> _dialogueBoxes;

	[SerializeField]
	private RectTransform _canvasObject;

	public GameManager gameManager;

	public void PopulateDialogueBox(int nodeID, DialogueNode node) {
	UI_DialogueBox dialogueBox = GameObject.Instantiate(UI_DialogueBoxPrefab).GetComponent<UI_DialogueBox>();
		dialogueBox.Construct(gameManager.nodeManager);
		dialogueBox.transform.SetParent(_canvasObject, false);
//		dialogueBox.SetData(gameManager.GetNodeByID(nodeID));
		dialogueBox.speaker = gameManager.characterManager.FindCharacterByIndex(node.speakerIndex);
		_dialogueBoxes.Add(nodeID, dialogueBox);
	}

}

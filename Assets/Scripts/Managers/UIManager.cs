using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

	public GameManager gameManager;

	[SerializeField]
	private GameObject UI_DialogueBoxPrefab;

	public Dictionary<int, UI_DialogueBox> _dialogueBoxes;

	[SerializeField]
	private RectTransform _canvasObject;

	public override void Awake() {
		base.Awake();

		_dialogueBoxes = new Dictionary<int, UI_DialogueBox>();
	}
	
	public void InstantiateDialogueBox(int nodeID, RectTransform canvas, NodeManager nodeManager) {
		UI_DialogueBox dialogueBox = GameObject.Instantiate(UI_DialogueBoxPrefab).GetComponent<UI_DialogueBox>();
		dialogueBox.Construct(nodeID, nodeManager);
		dialogueBox.transform.SetParent(canvas, false);
		dialogueBox.SetData(nodeManager.GetNodeByID(nodeID));
		nodeManager._dialogueBoxes.Add(nodeID, dialogueBox);
	}
}

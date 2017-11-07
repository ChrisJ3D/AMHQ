using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DialogueBox : MonoBehaviour {

	private int _nodeID;
	private DialogueManager _dialogueManager;

	public void Construct(int nodeID, DialogueManager dialogueManager) {
		_nodeID = nodeID;
		_dialogueManager = dialogueManager;
	}
}

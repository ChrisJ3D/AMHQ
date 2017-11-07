using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : Singleton<NodeManager> {

	private Dictionary<int, AMHQCanvas> _nodeTracker;
	public AMHQCanvas nodeCanvas;

	[SerializeField]
	private GameObject dialoguePrefab;

	private Dictionary<int, UI_DialogueBox> _dialogueBoxes;

	public override void Awake() {
		base.Awake();

		//	Create some dictionaries to keep track of our nodes, and another one to keep track of our UI Dialogueboxes where we'll put all the text.
		_dialogueBoxes = new Dictionary<int, UI_DialogueBox>();
		_nodeTracker = new Dictionary<int, AMHQCanvas>();
		_nodeTracker.Clear();

		//	Traverse the node canvas and add all nodes to our dictionary.
		if (nodeCanvas) {
			foreach (int id in nodeCanvas.GetAllDialogId()) {
				_nodeTracker.Add(id, nodeCanvas);
			}
		}
		else {
			foreach (AMHQCanvas canvas in Resources.LoadAll<AMHQCanvas>("Saves/")) {
				foreach (int id in canvas.GetAllDialogId()) {
					_nodeTracker.Add(id, canvas);
				}
			}
		}
	}

	public void ShowDialogueByID(int nodeID, bool goBackToBeginning) {
		
	}

	private BaseConversationNode GetNodeByID(int nodeID) {
		AMHQCanvas canvas;
		if(_nodeTracker.TryGetValue(nodeID, out canvas)) {
			return nodeCanvas.GetDialog(nodeID);
		}

		else {
			Debug.LogError("NODEMANAGER: Unable to find node with requested ID: " + nodeID);
			return null;
		}
	}
}

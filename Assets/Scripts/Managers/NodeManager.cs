using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

public class NodeManager : Singleton<NodeManager> {

	private Dictionary<int, AMHQCanvas> _nodeTracker;
	
	[SerializeField]
	private GameObject UI_DialogueBoxPrefab;
	private Dictionary<int, UI_DialogueBox> _dialogueBoxes;

	[SerializeField]
	private RectTransform _canvasObject;
	
	public AMHQCanvas nodeCanvas;

	public override void Awake() {
		base.Awake();

		//	Create some dictionaries to keep track of our nodes, and another one to keep track of our UI Dialogueboxes where we'll put all the text.
		_dialogueBoxes = new Dictionary<int, UI_DialogueBox>();
		_nodeTracker = new Dictionary<int, AMHQCanvas>();
		_nodeTracker.Clear();
	}

	public void Start() {
		//	Traverse the node canvas and add all nodes to our dictionary.
		//	First we check if there's a canvas stored in our scene

		nodeCanvas = GetCanvasFromScene();
		if (nodeCanvas) {
			foreach (int id in nodeCanvas.GetAllDialogId()) {
				_nodeTracker.Add(id, nodeCanvas);
			}
		} else {
			//	If there was no canvas stored in the scene, we look in Resources/Saves
			foreach (AMHQCanvas canvas in Resources.LoadAll<AMHQCanvas>("Saves/")) {
				foreach (int id in canvas.GetAllDialogId()) {
					_nodeTracker.Add(id, canvas);
				}
			}
		}
	}

	public void ShowDialogueByID(int nodeID, bool goBackToBeginning) {
		if (_dialogueBoxes.ContainsKey(nodeID)) {
			return;
		}

		AMHQCanvas nodeCanvas;
		if (_nodeTracker.TryGetValue(nodeID, out nodeCanvas)) {
			nodeCanvas.ActivateDialog(nodeID, goBackToBeginning);
		} else {
			Debug.LogError("NodeManager: Could not find node with ID " + nodeID);
		}

		UI_DialogueBox dialogueBox = GameObject.Instantiate(UI_DialogueBoxPrefab).GetComponent<UI_DialogueBox>();
		dialogueBox.Construct(nodeID, this);
		dialogueBox.transform.SetParent(_canvasObject, false);
		dialogueBox.SetData(GetNodeByID(nodeID));
		_dialogueBoxes.Add(nodeID, dialogueBox);
		
	}

	private BaseConversationNode GetNodeByID(int nodeID) {
		AMHQCanvas canvas;
		if(_nodeTracker.TryGetValue(nodeID, out canvas)) {
			return nodeCanvas.GetDialog(nodeID);
		} else {
			Debug.LogError("NODEMANAGER: Unable to find node with requested ID: " + nodeID);
			return null;
		}
	}

	public void okButton(int nodeID) {
		FetchNodeData(nodeID, (int)EnumDialogInputValue.Next);
		_dialogueBoxes[nodeID].SetData(GetNodeByID(nodeID));
	}

	public void backButton(int nodeID) {
		FetchNodeData(nodeID, (int)EnumDialogInputValue.Back);
		_dialogueBoxes[nodeID].SetData(GetNodeByID(nodeID));
	}

	//	Formerly known as "GiveInputToDialog"
	private void FetchNodeData(int nodeID, int inputValue) {
		AMHQCanvas nodeCanvas;

		if (_nodeTracker.TryGetValue(nodeID, out nodeCanvas)) {
			nodeCanvas.InputToDialog(nodeID, inputValue);
		} else {
			Debug.LogError("NodeManager: Cannot find node with ID " + nodeID);
		}
	}

	public void RemoveDialogueBox(int nodeID) {
		_dialogueBoxes.Remove(nodeID);
	}

	public AMHQCanvas GetCanvasFromScene() {
		AMHQCanvas canvas = null;

		GameObject canvasObject = GameObject.Find("NodeEditor_SceneSaveHolder");
		if (canvasObject) {
			canvas = (AMHQCanvas)canvasObject.GetComponent<NodeCanvasSceneSave>().savedNodeCanvas;
		}

		return canvas;
	}
}

public enum EnumDialogInputValue
	{
		Next = -2,
		Back = -1,
	}
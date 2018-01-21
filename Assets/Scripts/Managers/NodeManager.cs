using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using UnityEngine.SceneManagement;

namespace AMHQ {
	public class NodeManager : Singleton<NodeManager> {

<<<<<<< HEAD
		public GameManager gameManager;
		public BaseConversationNode startNode {get { return nodeCanvas.startNode; } }
		public BaseConversationNode currentNode {get { return nodeCanvas.currentNode; } }
=======
	public GameManager gameManager;
	private Dictionary<int, AMHQCanvas> _nodeTracker;
	
	[SerializeField]
	private GameObject UI_DialogueBoxPrefab;
	public Dictionary<int, UI_DialogueBox> _dialogueBoxes;
>>>>>>> origin/master

		private AMHQCanvas nodeCanvas = null;

<<<<<<< HEAD
		public override void Initialize(MonoBehaviour parent) {
			gameManager = parent as GameManager;
=======
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
			_nodeTracker.Clear();
			//	If there was no canvas stored in the scene, we look in Resources/Saves

			//	TODO: Right now it loads all canvases it can find, make it just look for the canvas with the same name as the currently opened scene
			foreach (AMHQCanvas canvas in Resources.LoadAll<AMHQCanvas>("Saves/")) {
				foreach (int id in canvas.GetAllDialogId()) {
					_nodeTracker.Add(id, canvas);
					}

				// if (canvas.Name == SceneManager.GetActiveScene().name) {
				// foreach (int id in canvas.GetAllDialogId()) {
				// 	_nodeTracker.Add(id, canvas);
				// 	}
				// }
			}
>>>>>>> origin/master
		}

		public void OnSceneLoad(string currentScene) {
			LoadCanvasByName(currentScene);
			nodeCanvas.GetSceneLoadedNode();
		}

		public void StepForward() {
			nodeCanvas.TraverseNodes((int)EnumDialogInputValue.Next);
		}

<<<<<<< HEAD
		public void StepBackward() {
			nodeCanvas.TraverseNodes((int)EnumDialogInputValue.Back);
=======
		gameManager._UIManager.InstantiateDialogueBox(nodeID, _canvasObject, this);		
	}

	public BaseConversationNode GetNodeByID(int nodeID) {
		AMHQCanvas canvas;
		if(_nodeTracker.TryGetValue(nodeID, out canvas)) {
			return nodeCanvas.GetDialog(nodeID);
		} else {
			Debug.LogError("NODEMANAGER: Unable to find node with requested ID: " + nodeID);
			return null;
>>>>>>> origin/master
		}

		public void OptionSelected(int option) {
			nodeCanvas.TraverseNodes(option);
		}

<<<<<<< HEAD
		public void LoadCanvasByName(string canvasName) {
			if(!gameManager) {
				Debug.LogWarning("GameManager not set!!");
				return;
			}
			nodeCanvas = Resources.Load("Graphs/" + canvasName, typeof(AMHQCanvas)) as AMHQCanvas;
=======
	public void RemoveDialogueBox(int nodeID) {
		_dialogueBoxes.Remove(nodeID);
	}

	public AMHQCanvas GetCanvasFromScene() {
		AMHQCanvas canvas = null;

		GameObject gameObject = GameObject.Find("NodeEditor_SceneSaveHolder");
		if (gameObject) {
			NodeCanvasSceneSave saveComponent = (NodeCanvasSceneSave)gameObject.GetComponent<NodeCanvasSceneSave>();

			if (saveComponent) {
				canvas = (AMHQCanvas)saveComponent.savedNodeCanvas;
			}
>>>>>>> origin/master
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

namespace AMHQ {
	public class NodeManager : Singleton<NodeManager> {

		public GameManager gameManager;
		public BaseConversationNode startNode {get { return nodeCanvas.startNode; } }
		public BaseConversationNode currentNode {get { return nodeCanvas.currentNode; } }
		
		[SerializeField]
		private GameObject UI_DialogueBoxPrefab;

		private UI_DialogueBox _dialogueBox;
		private AMHQCanvas nodeCanvas = null;

		public override void Initialize(MonoBehaviour parent) {
			gameManager = parent as GameManager;
		}

		public void OnSceneLoad(string currentScene) {
			LoadCanvasByName(currentScene);
			nodeCanvas.GetSceneLoadedNode();
		}

		public void StepForward() {
			nodeCanvas.TraverseNodes((int)EnumDialogInputValue.Next);
		}

		public void StepBackward() {
			nodeCanvas.TraverseNodes((int)EnumDialogInputValue.Back);
		}

		public void OptionSelected(int option) {
			nodeCanvas.TraverseNodes(option);
		}

		public void GetCanvasFromScene() {
			nodeCanvas = null;

			GameObject gameObject = GameObject.Find("NodeEditor_SceneSaveHolder");
			if (gameObject) {
				NodeCanvasSceneSave saveComponent = gameObject.GetComponent<NodeCanvasSceneSave>();

				if (saveComponent) {
				nodeCanvas = (AMHQCanvas)saveComponent.savedNodeCanvas;
				} else {
					Debug.LogWarning("SceneSaveHolder was missing component, loading from file");
					GetCanvasFromFile();
				}
			} else {
				Debug.Log("No canvas in scene found, loading from file");
				GetCanvasFromFile();
			}
		}

		public void GetCanvasFromFile() {
			if(!gameManager) {
				Debug.LogWarning("GameManager not set!!");
				return;
			}
			nodeCanvas = Resources.Load("Saves/" + gameManager.currentScene + "/LevelGraph", typeof(AMHQCanvas)) as AMHQCanvas;
		}

		public void LoadCanvasByName(string canvasName) {
			if(!gameManager) {
				Debug.LogWarning("GameManager not set!!");
				return;
			}
			Debug.Log("Attempting to load Saves/" + canvasName);
			nodeCanvas = Resources.Load("Saves/" + canvasName, typeof(AMHQCanvas)) as AMHQCanvas;
		}
	}
}
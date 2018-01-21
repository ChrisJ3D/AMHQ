using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

namespace AMHQ {
	public class NodeManager : Singleton<NodeManager> {

		public GameManager gameManager;
		public BaseConversationNode startNode {get { return nodeCanvas.startNode; } }
		public BaseConversationNode currentNode {get { return nodeCanvas.currentNode; } }

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

		public void LoadCanvasByName(string canvasName) {
			if(!gameManager) {
				Debug.LogWarning("GameManager not set!!");
				return;
			}
			nodeCanvas = Resources.Load("Graphs/" + canvasName, typeof(AMHQCanvas)) as AMHQCanvas;
		}
	}
}
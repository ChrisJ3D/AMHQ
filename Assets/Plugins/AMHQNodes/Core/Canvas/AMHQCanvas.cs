using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using UnityEngine;
using System.IO;

[NodeCanvasType("AMHQ Canvas")]
public class AMHQCanvas : NodeCanvas
{

	public override string canvasName { get { return "AMHQ Scene"; } }
	public string Name = "AMHQ";

	private Dictionary<int, BaseConversationNode> _lstActiveDialogs = new Dictionary<int, BaseConversationNode>();

	public SceneLoadedNode startNode = null;
	public BaseConversationNode currentNode = null;

	public SceneLoadedNode GetStartNode(int nodeIndex) {
		return (SceneLoadedNode)this.nodes.FirstOrDefault (x => x is SceneLoadedNode && ((SceneLoadedNode)x).nodeIndex == nodeIndex);
	}

	public bool HasDialogWithId(int nodeIndexToLoad)
	{
		GetSceneLoadedNode();
		return startNode != default(Node) && startNode != default(SceneLoadedNode);
	}

	public IEnumerable<int> GetAllDialogId()
	{
		foreach (Node node in this.nodes) {
			if (node is SceneLoadedNode) {
				yield return ((SceneLoadedNode)node).nodeIndex;
			}
		}
	}

	public BaseConversationNode GetDialog(int nodeIndexToLoad)
	{
		GetSceneLoadedNode();
		return startNode;

		// BaseConversationNode node;
		// if (!_lstActiveDialogs.TryGetValue(nodeIndexToLoad, out node))
		// {
		// 	ActivateDialog(nodeIndexToLoad, false);
		// }
		// return _lstActiveDialogs[nodeIndexToLoad];
	}

	public void InputToDialog(int nodeIndexToLoad, int inputValue)
	{
		if (startNode) {
			TraverseNodes(inputValue);
		} else {
			Debug.Log("startNode is null");
		}

		// BaseConversationNode node;
		// if (_lstActiveDialogs.TryGetValue(nodeIndexToLoad, out node))
		// {
		// 	node = node.GetDownsteamNode(inputValue);
		// 	if(node != null)
		// 		node = node.PassAhead(inputValue);
		// 	_lstActiveDialogs[nodeIndexToLoad] = node;
		// }
	}

	public void GetSceneLoadedNode() {
		startNode = (SceneLoadedNode)this.nodes.FirstOrDefault (x => x is SceneLoadedNode);
	}

	public void TraverseNodes(int steps) {
		BaseConversationNode targetNode;

		if (currentNode == null) {
			currentNode = startNode;
		}
		
		if (startNode != null) {
			targetNode = (BaseConversationNode)currentNode.GetDownsteamNode(steps);
			if (targetNode != null) {
				currentNode = targetNode.PassAhead(steps);
			}
		}
	}

	public List<string> GetItemsInAssetFolder() {

		List<string> itemsFolderEntries = new List<string>();

		string itemFolderPath = "/Prefabs/Items";
		string dataPath = Application.dataPath;
	
		string[] itemPaths = Directory.GetFiles(dataPath + itemFolderPath, searchOption: SearchOption.AllDirectories,
		searchPattern: "*.prefab");
			
		for(int i = 0; i < itemPaths.Length; i++) {
				string assetName = itemPaths[i].Substring(dataPath.Length+itemFolderPath.Length+1);
				assetName = assetName.Substring(0,assetName.Length-7);
				itemsFolderEntries.Add(assetName);
			}

		return itemsFolderEntries;
	}

	public List<string> GetCharactersInAssetFolder() {

		List<string> characterAssets = new List<string>();

		string itemFolderPath = "/Prefabs/Characters";
		string dataPath = Application.dataPath;
	
		string[] itemPaths = Directory.GetFiles(dataPath + itemFolderPath, searchOption: SearchOption.AllDirectories,
		searchPattern: "*.prefab");
			
		for(int i = 0; i < itemPaths.Length; i++) {
				string assetName = itemPaths[i].Substring(dataPath.Length+itemFolderPath.Length+1);
				assetName = assetName.Substring(0,assetName.Length-7);
				characterAssets.Add(assetName);
			}

		return characterAssets;
	}

	public List<string> GetFlagsInAssetsFolder() {

		List<string> flagAssets = new List<string>();

		string itemFolderPath = "/Prefabs/Flags";
		string dataPath = Application.dataPath;
	
		string[] itemPaths = Directory.GetFiles(dataPath + itemFolderPath, searchOption: SearchOption.AllDirectories,
		searchPattern: "*.prefab");
			
		for(int i = 0; i < itemPaths.Length; i++) {
				string assetName = itemPaths[i].Substring(dataPath.Length+itemFolderPath.Length+1);
				assetName = assetName.Substring(0,assetName.Length-7);
				flagAssets.Add(assetName);
			}

		return flagAssets;
	}
}

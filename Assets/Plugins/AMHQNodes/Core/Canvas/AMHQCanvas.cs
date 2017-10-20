using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using UnityEngine;
using System.IO;

[NodeCanvasType("AMHQ Canvas")]
public class AMHQCanvas : CalculationCanvasType
{

	public override string canvasName { get { return "AMHQ Scene"; } }
	public string Name = "AMHQ";

	private Dictionary<int, BaseDialogNode> _lstActiveDialogs = new Dictionary<int, BaseDialogNode>();

	public List<string> itemsFolderEntries;

	public DialogStartNode getDialogStartNode(int dialogID) {
		return (DialogStartNode)this.nodes.FirstOrDefault (x => x is DialogStartNode
			                                               && ((DialogStartNode)x).DialogID == dialogID);
	}

	public bool HasDialogWithId(int dialogIdToLoad)
	{
		DialogStartNode node = getDialogStartNode(dialogIdToLoad);
		return node != default(Node) && node != default(DialogStartNode);
	}

	public IEnumerable<int> GetAllDialogId()
	{
		foreach (Node node in this.nodes) {
			if (node is DialogStartNode) {
				yield return ((DialogStartNode)node).DialogID;
			}
		}
	}
		
	public void ActivateDialog(int dialogIdToLoad, bool goBackToBeginning)
	{
		BaseDialogNode node;
		if (!_lstActiveDialogs.TryGetValue(dialogIdToLoad, out node))
		{
			node = getDialogStartNode (dialogIdToLoad);
			_lstActiveDialogs.Add(dialogIdToLoad, node);
		}
		else
		{
			if (goBackToBeginning && !(node is DialogStartNode))
			{
				_lstActiveDialogs [dialogIdToLoad] = getDialogStartNode (dialogIdToLoad);
			}
		}
	}

	public BaseDialogNode GetDialog(int dialogIdToLoad)
	{
		BaseDialogNode node;
		if (!_lstActiveDialogs.TryGetValue(dialogIdToLoad, out node))
		{
			ActivateDialog(dialogIdToLoad, false);
		}
		return _lstActiveDialogs[dialogIdToLoad];
	}

	public void InputToDialog(int dialogIdToLoad, int inputValue)
	{
		BaseDialogNode node;
		if (_lstActiveDialogs.TryGetValue(dialogIdToLoad, out node))
		{
			node = node.Input(inputValue);
			if(node != null)
				node = node.PassAhead(inputValue);
			_lstActiveDialogs[dialogIdToLoad] = node;
		}
	}

	public List<string> GetItemsInAssetFolder() {

		itemsFolderEntries = new List<string>();

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
}

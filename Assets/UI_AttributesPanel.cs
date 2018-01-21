using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AMHQ;

public class UI_AttributesPanel : MonoBehaviour {

	public GameObject stressLabel;
	public GameObject charismaLabel;
	public GameObject innovationLabel;
	public GameObject eloquenceLabel;
	public GameObject knowledgeLabel;
	public GameObject organizationLabel;
	public GameObject remainingActionsLabel;

	public GameManager gameManager;

	/// <summary>
	/// Fetches all the current attribute values from the player and updates the UI
	/// </summary>
	public void Refresh() {
		if (gameManager == null) {
			gameManager = FindObjectOfType<GameManager>();
		}

		stressLabel.GetComponent<Text>().text = "Stress: " + gameManager.GetAttribute(CharacterAttributeType.Stress).ToStringShort();

		charismaLabel.GetComponent<Text>().text = "Charisma: " + gameManager.GetAttribute(CharacterAttributeType.Charisma).ToStringShort();

		innovationLabel.GetComponent<Text>().text = "Innovation: " + gameManager.GetAttribute(CharacterAttributeType.Innovation).ToStringShort();

		eloquenceLabel.GetComponent<Text>().text = "Eloquence: " + gameManager.GetAttribute(CharacterAttributeType.Eloquence).ToStringShort();

		knowledgeLabel.GetComponent<Text>().text = "Knowledge: " + gameManager.GetAttribute(CharacterAttributeType.Knowledge).ToStringShort();

		organizationLabel.GetComponent<Text>().text = "Organization: " + gameManager.GetAttribute(CharacterAttributeType.Organisation).ToStringShort();

		remainingActionsLabel.GetComponent<Text>().text = "Remaining Actions: " + GetComponentInParent<UI_WorkHub>().actionsPerDay;
	}

	private int RemaingActions() {
		int actions = 0;
		if (GetComponentInParent<UI_WorkHub>() == null) {
			actions = GetComponentInParent<UI_HomeHub>().actionsPerDay;
		}
		return actions;
	}
}

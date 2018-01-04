using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AMHQ;

public class UI_WorkHub : UI_Hub {

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}

	void OnEnable() {
		attributesPanel.GetComponent<UI_AttributesPanel>().Refresh();
	}

	public void WorkOnProjectButton() {
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Knowledge, 20);
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Stress, 10);
		attributesPanel.GetComponent<UI_AttributesPanel>().Refresh();
		base.CheckActions();
	}

	public void CorrespondWithClientsButton() {
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Eloquence, 20);
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Stress, 10);
		attributesPanel.GetComponent<UI_AttributesPanel>().Refresh();
		base.CheckActions();
	}

	public void DoResearchButton() {
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Organisation, 20);
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Stress, 10);
		attributesPanel.GetComponent<UI_AttributesPanel>().Refresh();
		base.CheckActions();
	}

	public void CheckOnCoworkersButton() {
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Charisma, 20);
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Stress, 10);
		attributesPanel.GetComponent<UI_AttributesPanel>().Refresh();
		base.CheckActions();
	}

	public void PrototypeIdeasButton() {
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Innovation, 20);
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Stress, 10);
		attributesPanel.GetComponent<UI_AttributesPanel>().Refresh();
		base.CheckActions();
	}

	public void TakeItEasyButton() {
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Stress, -20);
		attributesPanel.GetComponent<UI_AttributesPanel>().Refresh();
		base.CheckActions();
	}

	protected override void DayComplete() {
		gameManager.CheckAfterWorkEvents();
	}
}

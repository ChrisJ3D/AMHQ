using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AMHQ;

public class UI_WorkHub : UI_Hub {

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}

	public void WorkOnProjectButton() {

	}

	public void CorrespondWithClientsButton() {
		gameManager.AdjustPlayerAttribute(CharacterAttributeType.Charisma, 20);
		attributesPanel.GetComponent<UI_AttributesPanel>().Refresh();
	}

	public void DoResearchButton() {

	}

	public void CheckOnCoworkersButton() {

	}

	public void PrototypeIdeasButton() {

	}

	public void TakeItEasyButton() {
		
	}
}

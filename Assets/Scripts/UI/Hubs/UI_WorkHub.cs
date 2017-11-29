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
		CheckActions();
	}

	public void CorrespondWithClientsButton() {
		CheckActions();
		gameManager.SetPlayerAttribute(CharacterAttributeType.Charisma, 20);

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

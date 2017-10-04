using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Command : ScriptableObject {

	public enum COMMAND_TYPE {
		DIALOGUE,
		LOADSCENE,
		LOADSAVE,
		SAVE,
		SETBG,
		SETBGM,
		CHECKSTAT,
		CHECKDATE,
		CHECKWEEKDAY,
		ADJUSTSTAT,
		ADVANCETIME,
		PLAYVFX,
		PLAYSFX,
	}

	public COMMAND_TYPE command;
	public string[] args;

	public Command(string Command, string[] Args) {
		command = (COMMAND_TYPE)Enum.Parse(typeof(COMMAND_TYPE), Command);
		args = Args;
	}

	public virtual void Execute() {

	}
}

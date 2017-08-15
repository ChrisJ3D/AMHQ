using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class ScriptParser : MonoBehaviour {

	struct ScriptLine {
		public string name;
		public string content;
		public int pose;
		public string position;
		public string[] options;

		public ScriptLine(string Name, string Content, int Pose, string Position) {
			name = Name;
			content = Content;
			pose = Pose;
			position = Position;
			options = new string[0];
		}

	}

	List<ScriptLine> lines;

	void Start () {
		string file = "Assets/Dialogue/";
		string sceneNum = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name;

		//sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
		file += sceneNum;
		file += ".txt";

		lines = new List<ScriptLine>();

		LoadDialogue(file);
	}

	void LoadDialogue(string fileName) {
		string line;
		StreamReader r = new StreamReader (fileName);

		using (r) {
			do {
				line = r.ReadLine();
				if (line != null) {
					string[] lineData = line.Split('|');
					if (lineData[0] == "Player") {
						ScriptLine lineEntry = new ScriptLine(lineData[0], "", 0, "");
						lineEntry.options = new string[lineData.Length - 1];
						for (int i = 1; i < lineData.Length; i++) {
							lineEntry.options[i-1] = lineData[i];
						}
						lines.Add(lineEntry);
					} else {
						ScriptLine lineEntry = new ScriptLine(lineData[0], lineData[1], int.Parse(lineData[2]), lineData[3]);
						lines.Add(lineEntry);
					}
				}
			}
			while (line != null);
			r.Close();
		}
	}

	void ParseVFX() {

	}

	void ParseSFX() {

	}

	void ParseBG() {

	}

	void ParseBGM() {

	}

	public string GetPosition(int lineNumber) {
		if (lineNumber < lines.Count) {
			return lines[lineNumber].position;
		}
		return "";
	}

	public string GetName(int lineNumber) {
		if (lineNumber < lines.Count) {
			return lines[lineNumber].name;
		}
		return "";
	}

	public string GetContent(int lineNumber) {
		if (lineNumber < lines.Count) {
			return lines[lineNumber].content;
		}
		return "";
	}

	public int GetPose(int lineNumber) {
		if (lineNumber < lines.Count) {
			return lines[lineNumber].pose;
		}
		return 0;
	}

	public string[] GetOptions(int lineNumber) {
		if (lineNumber < lines.Count) {
			return lines[lineNumber].options;
		}
		return new string[0];
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour {
	
	public int duration = 1;
	Player player;
	TimeManager timeManager;

	[Header("Attribute Adjustments")]
	public int stress = 0;
	public int organisation = 0;
	public int charisma = 0;
	public int knowledge = 0;
	public int innovation = 0;
	public int eloquence = 0;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
		timeManager = FindObjectOfType<TimeManager>();
	}
	
	public void OnClick() {
		player.stress.value += stress;
		player.organisation.value += organisation;
		player.charisma.value += charisma;
		player.knowledge.value += knowledge;
		player.innovation.value += innovation;
		player.eloquence.value += eloquence;

		print("Stress: " + player.stress.value.ToString());

		timeManager.time.AddHours(2);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour {

	int season;
	public int actionsPerDay = 3;
	int remainingActions;

	public System.DateTime time = new System.DateTime();

	// Use this for initialization
	void Start () {
		remainingActions = actionsPerDay;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpendAction() {
		if (remainingActions > 0) {
			remainingActions--;
		} else {
			print("No actions remaining!!");
		}
	}

	public void ResetDailyActions() {
		remainingActions = actionsPerDay;
	}

	public int GetSeason() {
	//	Returns the current season as an int where 0 = Winter, 1 = Spring, 2 = Summer, 3 = Fall
	//	Remember that date.Month starts at 1, not 0!!

		int currentSeason = 0;

		if (time.Month == 12 || time.Month == 1 || time.Month == 2){
			currentSeason = 0;
		}

		if(time.Month == 3 || time.Month == 4 || time.Month == 5) {
			currentSeason = 1;
		}

		if(time.Month == 6 || time.Month == 7 || time.Month == 8) {
			currentSeason = 2;
		}

		if(time.Month == 9 || time.Month == 10 || time.Month == 11) {
			currentSeason = 3;
		}

		return currentSeason;
	}

	public DayOfWeek GetWeekday() {
		return time.DayOfWeek;
	}
}

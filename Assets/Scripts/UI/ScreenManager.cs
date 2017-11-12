using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

	public GameObject optionsScreen;
	public GameObject loadGameScreen;
	public GameObject newGameScreen;

	public void SwitchScreens(int targetID) {

		switch(targetID) {
			case 0:
				if(optionsScreen) {
					optionsScreen.SetActive(true);	
				}
				break;

			case 1:
				if (loadGameScreen) {
					loadGameScreen.SetActive(true);
				}
				break;

			case 2:
				if(newGameScreen) {
					newGameScreen.SetActive(true);
				}
				break;

		}
	}
}

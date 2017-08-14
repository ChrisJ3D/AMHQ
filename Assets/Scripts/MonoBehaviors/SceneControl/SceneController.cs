using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour {

	public event Action BeforeSceneUnload;
	public event Action AfterSceneLoad;

	public CanvasGroup faderCanvasGroup;
	public float fadeDuration = 1f;
	private bool isFading;

	public string startingSceneName = "SecurityRoom";
	public string initialStartingPositionName = "DoorToMarket";
	public SaveData playerSaveData;

	// Use this for initialization
	private IEnumerator Start () {
		faderCanvasGroup.alpha = 1f;
		//playerSaveData.Save();

		yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));

		StartCoroutine(Fade(0f));
	}

	public void FadeAndLoadScene (SceneReaction sceneReaction) {
		if(!isFading) {
			StartCoroutine(FadeAndSwitchScenes(sceneReaction.sceneName));
		}
	}

	private IEnumerator FadeAndSwitchScenes(string sceneName) {
		yield return StartCoroutine(Fade(1f));
		if(BeforeSceneUnload != null) {
			BeforeSceneUnload();
		}
		yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

		yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

		if(AfterSceneLoad != null) {
			AfterSceneLoad();
		}
		yield return StartCoroutine(Fade(0f));
	}

	private IEnumerator LoadSceneAndSetActive(string sceneName) {
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

		Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		SceneManager.SetActiveScene(newlyLoadedScene);
	}

	private IEnumerator Fade(float finalAlpha) {
		isFading = true;
		faderCanvasGroup.blocksRaycasts = true;

		float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

		while(!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha)) {
			faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
			yield return null;
		}

		isFading = false;
		faderCanvasGroup.blocksRaycasts = false;
	}
}
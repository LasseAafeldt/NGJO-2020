using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtons : MonoBehaviour {
	public int scenesCount = 8;
	public GameObject creditsPanel;

	public void Play() {
		SceneManager.LoadScene(2);
	}

	public void ShowCredits() {
		creditsPanel.SetActive(true);
	}

	public void HideCredits() {
		creditsPanel.SetActive(false);
	}

	public void Quit() {
		Application.Quit();
	}
}

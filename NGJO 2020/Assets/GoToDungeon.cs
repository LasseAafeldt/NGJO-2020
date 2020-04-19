using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDungeon : MonoBehaviour {
	public int scenesCount = 8;

	public void Play() {
		SceneManager.LoadScene("Room" + Random.Range(1, scenesCount + 1));
	}
}

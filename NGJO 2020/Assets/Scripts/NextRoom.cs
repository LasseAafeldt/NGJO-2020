using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoom : MonoBehaviour {
	public int scenesCount = 2;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.GetComponent<PlayerMovement>()) {
			string s;
			do {
				s = "Room" + Random.Range(1, scenesCount + 1);
			} while (s == SceneManager.GetActiveScene().name);
			SceneManager.LoadScene(s);
		}
	}
}

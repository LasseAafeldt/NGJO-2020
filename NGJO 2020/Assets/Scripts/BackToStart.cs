using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour {
	public void LoadStart() {
		Destroy(Inventory.instance.gameObject);
		SceneManager.LoadScene(0);
	}
}

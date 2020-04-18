using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {
	public Vector2[] targets;
	public bool loop = true;
	public Enemy[] enemyPrefabs;

	void Awake() {
		Enemy e = GameObject.Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
		e.path = this;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(targets[0], 0.25f);
		if (targets != null && targets.Length >= 2) {
			for (var i = 0; i < targets.Length - 1; i++)
				Gizmos.DrawLine(targets[i], targets[i + 1]);
			if (loop) {
				Gizmos.DrawLine(targets[0], targets[targets.Length - 1]);
			}
		}
	}
}

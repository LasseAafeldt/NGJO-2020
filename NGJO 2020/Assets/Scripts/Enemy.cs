using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour {
	public Vector2[] targets;
	public bool loop = true;
	public float speed = 3;
	public float stepDelay = 0;
	public float terminalDelay = 1;

	bool moving = true;
	int currentTarget = 1;
	int nextTarget = 1;

	void Start() {
		transform.position = targets[0];
	}

	void Update() {
		if (moving) {
			transform.position = Vector2.MoveTowards(transform.position, targets[currentTarget], speed * Time.deltaTime);
			if (Vector2.Distance(transform.position, targets[currentTarget]) < speed * Time.deltaTime) {
				if (currentTarget >= targets.Length - 1) {
					if (loop) {
						currentTarget = 0;
						StartCoroutine(Wait(stepDelay));
						return;
					} else {
						nextTarget = -nextTarget;
						StartCoroutine(Wait(terminalDelay));
					}
				} else if (currentTarget <= 0 && !loop) {
					nextTarget = -nextTarget;
					StartCoroutine(Wait(terminalDelay));
				}
				currentTarget += nextTarget;
				StartCoroutine(Wait(stepDelay));
			}
		}
	}
	IEnumerator Wait(float delay) {
		if (delay > 0 && moving) {
			moving = false;
			yield return new WaitForSeconds(delay);
			moving = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.GetComponent<PlayerMovement>()) {
			collision.GetComponent<PlayerMovement>().Respawn();
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		if (targets != null && targets.Length >= 2) {
			for (var i = 0; i < targets.Length - 1; i++)
				Gizmos.DrawLine(targets[i], targets[i + 1]);
			if (loop) {
				Gizmos.DrawLine(targets[0], targets[targets.Length - 1]);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour {
	public EnemyPath path;

	public float speed = 3;
	public float stepDelay = 0;
	public float terminalDelay = 1;

	bool moving = true;
	int currentTarget = 1;
	int nextTarget = 1;

	void Start() {
		transform.position = path.targets[0];
	}

	void Update() {
		if (moving) {
			transform.position = Vector2.MoveTowards(transform.position, path.targets[currentTarget], speed * Time.deltaTime);
			if (Vector2.Distance(transform.position, path.targets[currentTarget]) < speed * Time.deltaTime) {
				if (currentTarget >= path.targets.Length - 1) {
					if (path.loop) {
						currentTarget = 0;
						StartCoroutine(Wait(stepDelay));
						return;
					} else {
						nextTarget = -nextTarget;
						StartCoroutine(Wait(terminalDelay));
					}
				} else if (currentTarget <= 0 && !path.loop) {
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
}

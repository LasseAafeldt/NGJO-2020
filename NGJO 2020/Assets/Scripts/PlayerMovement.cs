using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] private float moveSpeed = 2f;
	[SerializeField] private Rigidbody2D rb;

	InputMaster inputActions;
	Animator animator;
	SpriteRenderer spriteRenderer;

	//input variables
	Vector2 movementInput;
	Vector2 startPosition;


	private void Awake() {
		inputActions = new InputMaster();
		inputActions.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
		startPosition = transform.position;
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate() {
		DesiredMove();
	}

	public bool facingRight = true;
	void DesiredMove() {
		Debug.Log("movement input: " + movementInput);
		rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
		animator.SetFloat("X", movementInput.x);
		animator.SetFloat("Y", movementInput.y);

		float h = movementInput.x;
		if (h > 0 && !facingRight || h < 0 && facingRight) {
			facingRight = !facingRight;
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}
	}

	private void OnEnable() {
		inputActions.Enable();
	}

	private void OnDisable() {
		inputActions.Disable();
	}

	public void Respawn() {
		transform.position = startPosition;
	}
}

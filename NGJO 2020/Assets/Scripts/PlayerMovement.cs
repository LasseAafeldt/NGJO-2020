using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] private float moveSpeed = 2f;
	[SerializeField] private Rigidbody2D rb;
	[Space]
	public Vector3 killCamPosition;
	public float killCamMaxMove = 0.1f;
	public float killCamDepth = 1.3f;
	public float killCamDepthSpeed = 10;
	public Vector3 killCamRotation;
	public float killCamMaxRotate = 0.1f;
	public float killCamDuration = 2;

	InputMaster inputActions;
	Animator animator;
	SpriteRenderer spriteRenderer;

	//input variables
	Vector2 movementInput;
	Vector2 startPosition;

	bool respawn;

	private void Awake() {
		inputActions = new InputMaster();
		inputActions.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
		startPosition = transform.position;
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update() {
		if (respawn) {
			Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, transform.position + killCamPosition, killCamMaxMove * Time.deltaTime);
			Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, Quaternion.Euler(killCamRotation), killCamMaxRotate * Time.deltaTime);
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, killCamDepth, killCamDepthSpeed * Time.deltaTime);
		}
	}

	private void FixedUpdate() {
		if (!respawn) {
			DesiredMove();
		}
	}

	bool facingRight = true;
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
		StartCoroutine(RespawnCoroutine());
	}
	IEnumerator RespawnCoroutine() {
		if (!respawn) {
			respawn = true;
			animator.SetBool("Respawn", true);
			yield return new WaitForSeconds(killCamDuration);
			transform.position = startPosition;
			Camera.main.transform.position = new Vector3(0, 0, -10);
			Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
			Camera.main.orthographicSize = 5.4f;
			respawn = false;
			animator.SetBool("Respawn", false);
		}
	}
}

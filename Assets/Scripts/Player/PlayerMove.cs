using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	// Speed and gravity of player
	public float moveSpeed;
	public float playerRotateSpeed;
	public float cameraRotateSpeed;
	public float jumpSpeed;
	public float gravity;

	// Movement vector
	private Vector3 move;

	// Rotate vector
	private Vector3 playerRotate;
	private Vector3 cameraRotate;

	// Camera rotate range
	private float cameraRotateRange = 60.0f;

	// Rigidbody and Character Controller
	private CharacterController cc;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		// Lock cursor
		Screen.lockCursor = true;

		// Get components
		cc = GetComponent<CharacterController> ();
		rb = GetComponent<Rigidbody> ();

		// Initialize gravity
		gravity = rb.mass;

		// Initialize vectors
		move = Vector3.zero;
		playerRotate = Vector3.zero;
		cameraRotate = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		float horizontalRotate = Input.GetAxis ("Mouse X");
		float verticalRotate = cameraRotate.x - Input.GetAxis ("Mouse Y")  * cameraRotateSpeed;
		verticalRotate = Mathf.Clamp (verticalRotate, -cameraRotateRange, cameraRotateRange);

		playerRotate.x = 0.0F;
		playerRotate.y = horizontalRotate;
		playerRotate.z = 0.0F;

		cameraRotate.x = verticalRotate;
		cameraRotate.y = 0.0F;
		cameraRotate.z = 0.0F;

		transform.Rotate (playerRotate * playerRotateSpeed);
		Camera.main.transform.localRotation = Quaternion.Euler (cameraRotate);

		// Movement
		float verticalMove = Input.GetAxis ("Vertical");
		float horizontalMove = Input.GetAxis ("Horizontal");

		move.x = horizontalMove;
		if (Input.GetKeyDown("space")) {
			move.y = jumpSpeed;
		} else {
			move.y -= gravity;
		}
		move.z = verticalMove;

		move = transform.rotation * move;

		cc.Move (move * moveSpeed * Time.deltaTime);
	}
}

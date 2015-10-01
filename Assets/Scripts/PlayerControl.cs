using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float Walk_Speed;
	public float Run_Speed;
	public float Jump_Power;

	//Non Public Variable
	BoxCollider2D myCollider;
	Rigidbody2D myRigidbody;

	bool canJumpAgain;
	bool isGrounded;
	bool inLadder;
	bool isVisible;

	float tarRot;

	// Use this for initialization
	void Awake () {
		myCollider = GetComponent<BoxCollider2D> ();
		myRigidbody = GetComponent<Rigidbody2D> ();

		Jump_Power = Jump_Power * 7.5f;
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();

		//If Player Falling Down...
		if (transform.position.y < -10) {
			transform.position = new Vector2(0, 0);
		}
	}

	void Movement ()
	{
		Vector3 input = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, 0);
		
		//if (tarRot != input.x && input.x != 0) {
		//	tarRot = input.x;
		//}
		
		Vector3 motion = input;
		motion *= (Mathf.Abs (input.x) == 1)? .5f : 1;
		motion *= (Input.GetButton ("Run")) ? Run_Speed : Walk_Speed;

		//if (tarRot == -1) {
		//	transform.eulerAngles = new Vector2 (0, 180);
		//} else if (tarRot == 1){
		//	transform.eulerAngles = Vector2.zero;
		//}

		transform.Translate(motion * Time.deltaTime);

		//Jump Section

		if (Input.GetButtonDown("Jump") && isGrounded) {
			myRigidbody.AddForce(Vector2.up * Jump_Power);
		}

		if (Input.GetButtonDown("Jump") && !isGrounded && canJumpAgain) {
			myRigidbody.velocity = Vector2.zero;
			myRigidbody.AddForce(Vector2.up * Jump_Power);
			canJumpAgain = false;
		}

		//Climb Section

		if (inLadder) {

			Vector3 climbInput = new Vector3(0, Input.GetAxisRaw("Climb"), 0);

			Vector3 climbMotion = climbInput;
			climbMotion *= (Mathf.Abs(climbInput.y) == 1)?.5f:1f;
			climbMotion *= Walk_Speed;

			myRigidbody.isKinematic = true;

			transform.Translate(climbMotion * Time.deltaTime);
		}

		//Move Down Platform

		if (isGrounded) {
			if(Input.GetKeyDown(KeyCode.DownArrow)) {
				StartCoroutine(Invisible());
			}
		}

	}

	IEnumerator Invisible ()
	{
		myCollider.isTrigger = true;
		yield return new WaitForSeconds (.5f);
		myCollider.isTrigger = false;
		yield return false;
	}

	void OnCollisionStay2D (Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			isGrounded = true;
			canJumpAgain = true;
			myRigidbody.freezeRotation = true;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
		}

		if (col.gameObject.tag == "Platformer_Side") {
			myRigidbody.freezeRotation = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Platformer_Move") {
			transform.parent = col.gameObject.transform;
		}
	}

	void OnCollisionExit2D (Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			isGrounded = false;
			myCollider.isTrigger = false;
		}

		transform.parent = null;
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.gameObject.tag == "Ladder") {
			inLadder = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "Ladder") {
			inLadder = false;
			myRigidbody.isKinematic = false;
		}
	}
	
}

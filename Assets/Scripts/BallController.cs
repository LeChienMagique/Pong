using System;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BallController: MonoBehaviour {
	private Rigidbody rb;

	public float SpeedMultiplier;
	public float Speed;

	private Vector3 velocity;

	void Start() {
		rb       = GetComponent<Rigidbody>();
		velocity = (Vector3.right) * Speed;
		// rb.velocity = velocity;
		rb.AddForce(velocity, ForceMode.VelocityChange);
	}

	void FixedUpdate() {
		if (Input.GetKey(KeyCode.R)) {
			ResetPosition(1);
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Player")) {
			BoxCollider paddleBColl = collision.gameObject.GetComponent<BoxCollider>();
			Bounds      bounds      = paddleBColl.bounds;
			float       centerZ     = bounds.center.z;
			float       size        = bounds.size.z;
			float       ballZ       = transform.position.z;

			ballZ -= centerZ;

			float ratio = (ballZ / (size / 2));
			ratio = Math.Clamp(ratio, -1, 1);

			Quaternion rotation = Quaternion.Euler(0, -45f * ratio * (transform.position.x > 0 ? -1 : 1), 0);
			rb.AddForce(rb.velocity * SpeedMultiplier, ForceMode.VelocityChange);
			rb.velocity = rotation * rb.velocity;
		}
	}

	public void ResetPosition(int scoringPlayer) {
		transform.position = new Vector3(0, 0, 0);
		rb.velocity        = Vector3.right * (Speed * (scoringPlayer == 1 ? 1 : -1));
	}
}
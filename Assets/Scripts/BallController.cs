using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BallController: MonoBehaviour {
	private Rigidbody rb;

	public float SpeedIncrease;
	public float Speed;

	private Vector3 velocity;
	private float   rotationToggle = -1f;

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

			Quaternion rotation = Quaternion.Euler(0, -45f * ratio, 0);
			// rotationToggle *= -1;
			rb.AddForce(rb.velocity * SpeedIncrease, ForceMode.VelocityChange);
			rb.velocity = rotation * rb.velocity;
		}
	}

	public void ResetPosition(int scoringPlayer) {
		transform.position = new Vector3(0, 0, 0);
		rb.velocity        = Vector3.right * (Speed * (scoringPlayer == 1 ? 1 : -1));
	}
}
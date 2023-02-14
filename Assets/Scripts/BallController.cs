using System;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BallController: MonoBehaviour {
	private Rigidbody   rb;
	private AudioSource audioSource;

	public  float SpeedMultiplier;
	private float initialSpeed = 25f;

	public int LastPlayerTouched = 1;

	void Start() {
		audioSource = GetComponent<AudioSource>();
		rb          = GetComponent<Rigidbody>();
		rb.AddForce(Vector3.right * initialSpeed, ForceMode.VelocityChange);
	}

	private void OnCollisionEnter(Collision collision) {
		if (!collision.gameObject.CompareTag("Player")) return;

		audioSource.Play();

		LastPlayerTouched = LastPlayerTouched == 1 ? 2 : 1;
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

	public void ResetPosition(int scoringPlayer) {
		transform.position = new Vector3(0, 0, 0);
		rb.velocity        = Vector3.right * (initialSpeed * (scoringPlayer == 1 ? 1 : -1));
	}

	public void SpeedUp(float percentage) {
		rb.AddForce(rb.velocity * percentage, ForceMode.VelocityChange);
	}
}
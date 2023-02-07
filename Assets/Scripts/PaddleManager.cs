using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager: MonoBehaviour {
	public  GameObject  PaddleLeft;  // player 1
	public  GameObject  PaddleRight; // player 2
	private BoxCollider paddleLeftColl;
	private BoxCollider paddleRightColl;

	public  GameObject  BorderTop;
	public  GameObject  BorderBottom;
	private BoxCollider borderTopColl;
	private BoxCollider borderBottomColl;

	public float PaddleSpeed = 5f;

	void Start() {
		paddleLeftColl   = PaddleLeft.GetComponent<BoxCollider>();
		paddleRightColl  = PaddleRight.GetComponent<BoxCollider>();
		borderTopColl    = BorderTop.GetComponent<BoxCollider>();
		borderBottomColl = BorderBottom.GetComponent<BoxCollider>();
	}

	void FixedUpdate() {
		float player1 = Input.GetAxisRaw("Player1");
		float player2 = Input.GetAxisRaw("Player2");

		float p1Force = player1 * PaddleSpeed * Time.deltaTime;
		float p2Force = player2 * PaddleSpeed * Time.deltaTime;
		UpdatePaddlePos(PaddleLeft, paddleLeftColl, p1Force);
		UpdatePaddlePos(PaddleRight, paddleRightColl, p2Force);
	}

	void UpdatePaddlePos(GameObject paddle, BoxCollider paddleBColl, float force) {
		Bounds  bounds    = paddleBColl.bounds;
		float   minZ      = borderBottomColl.bounds.max.z + bounds.extents.z;
		float   maxZ      = borderTopColl.bounds.min.z - bounds.extents.z;
		Vector3 paddlePos = paddle.transform.position;
		paddle.transform.position = new Vector3(paddlePos.x, paddlePos.y, Math.Clamp(paddlePos.z + force, minZ, maxZ));
	}
}
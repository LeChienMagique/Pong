using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

public enum PowerUpType {
	PADDLE_SPEED_UP,
	PADDLE_SPEED_DOWN,
	PADDLE_EXTEND,
	PADDLE_SHRINK
}

public class PowerUp: MonoBehaviour {
	public  GameObject    PaddleManagerGO;
	private PaddleManager paddleManager;

	public PowerUpType PowerUpType;

	public void Start() {
		paddleManager = PaddleManagerGO.GetComponent<PaddleManager>();
		PowerUpType   = (PowerUpType) Random.Range(0, 4);
		Color color;
		switch (PowerUpType) {
			case PowerUpType.PADDLE_EXTEND:
			case PowerUpType.PADDLE_SPEED_UP:
				color = new Color(.4f, 1f, .3f);
				break;
			case PowerUpType.PADDLE_SHRINK:
			case PowerUpType.PADDLE_SPEED_DOWN:
				color = new Color(1f, .2f, .3f);
				break;
			default:
				color = new Color(1f, 1f, 1f);
				break;
		}
		Renderer rend = gameObject.GetComponent<Renderer>();
		rend.material.color = color;
	}

	private void PaddleChangeSpeed(BallController ballController, float percentage) {
		if (ballController.LastPlayerTouched == 1)
			paddleManager.LeftPaddleSpeed *= percentage;
		else
			paddleManager.RightPaddleSpeed *= percentage;
	}

	private void ScalePaddle(BallController ballController, float percentage) {
		Transform paddleTransform =
			ballController.LastPlayerTouched == 1 ? paddleManager.PaddleLeft.transform : paddleManager.PaddleRight.transform;
		Vector3 scale = paddleTransform.localScale;
		scale.z                    *= percentage;
		paddleTransform.localScale =  scale;
	}

	public void OnTriggerEnter(Collider other) {
		BallController ballController = other.gameObject.GetComponent<BallController>();
		switch (PowerUpType) {
			case PowerUpType.PADDLE_SPEED_UP:
				PaddleChangeSpeed(ballController, 1.25f);
				break;
			case PowerUpType.PADDLE_SPEED_DOWN:
				PaddleChangeSpeed(ballController, 0.75f);
				break;
			case PowerUpType.PADDLE_EXTEND:
				ScalePaddle(ballController, 1.25f);
				break;

			case PowerUpType.PADDLE_SHRINK:
				ScalePaddle(ballController, 0.75f);
				break;
		}

		Destroy(gameObject);
	}
}
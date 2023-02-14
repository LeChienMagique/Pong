using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner: MonoBehaviour {
	public float Period;

	private float stopwatch;

	public GameObject PowerUpPrefab;
	public GameObject PaddleManagerGO;

	private List<GameObject> powerUps;

	private void Start() {
		powerUps = new List<GameObject>();
	}

	private Vector3 GetRandomPosition() =>
		// x = -30 - 30
		// z = -19 - 19
		new(Random.Range(-30f, 30f), 0, Random.Range(-19f, 19f));

	void FixedUpdate() {
		stopwatch += Time.fixedDeltaTime;
		if (stopwatch > Period) {
			stopwatch = 0;
			GameObject powerUp = Instantiate(PowerUpPrefab, GetRandomPosition(), Quaternion.identity);
			powerUp.GetComponent<PowerUp>().PaddleManagerGO = PaddleManagerGO;
		}
	}

	public void RemovePowerUps() {
		powerUps.ForEach(Destroy);
		powerUps = new List<GameObject>();
	}
}
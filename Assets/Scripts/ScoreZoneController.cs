using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreZoneController: MonoBehaviour {
	public GameObject ScoreController;
	public int        PlayerNumber;

	private ScoreManager scoreManager;

	private void Start() {
		scoreManager = ScoreController.GetComponent<ScoreManager>();
	}

	private void OnTriggerEnter(Collider other) {
		scoreManager.AddPoint(PlayerNumber);
	}
}
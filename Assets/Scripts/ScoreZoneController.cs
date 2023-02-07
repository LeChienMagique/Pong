using UnityEngine;

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
using System;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = System.Drawing.Color;

public class ScoreManager: MonoBehaviour {
	public  GameObject     Ball;
	private BallController ballController;
	public  GameObject     PaddleManager;
	private PaddleManager  paddleManager;

	private int player1Score;
	private int player2Score;

	public  GameObject     PowerUpSpawner;
	private PowerUpSpawner powerUpSpawner;

	public  GameObject      ScoreText;
	private TextMeshProUGUI scoreText;

	private void Start() {
		ballController = Ball.GetComponent<BallController>();
		powerUpSpawner = PowerUpSpawner.GetComponent<PowerUpSpawner>();
		scoreText      = ScoreText.GetComponent<TextMeshProUGUI>();
		paddleManager  = PaddleManager.GetComponent<PaddleManager>();
	}

	private string PlayerIDToString(int playerId) => playerId == 1 ? "Left" : "Right";

	public void AddPoint(int player) {
		if (player == 1)
			player1Score++;
		else if (player == 2)
			player2Score++;

		if (player1Score == 11 || player2Score == 11) {
			Debug.Log($"Game over, {PlayerIDToString(player)} paddle wins!");
			player1Score = 0;
			player2Score = 0;
		}
		else {
			Debug.Log($"{PlayerIDToString(player)} paddle scored, score is now: {player1Score} - {player2Score}");
		}
		ResetBall(player);
		UpdateScoreText(player);
		KillPowerUps();
		ResetPaddles();
	}

	private string ColorToHex(Color color) => "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

	private void UpdateScoreText(int player) {
		Color p1Color = Color.FromArgb(0, 255, 255 - (int) Math.Round((float) player1Score / 11 * 255));
		Color p2Color = Color.FromArgb(0, 255, 255 - (int) Math.Round((float) player2Score / 11 * 255));
		switch (player) {
			case 1:
				scoreText.text = $"<u><b><color={ColorToHex(p1Color)}>{player1Score}</color></b></u> - " +
				                 $"<color={ColorToHex(p2Color)}>{player2Score}</color>";
				break;
			case 2:
				scoreText.text = $"<color={ColorToHex(p1Color)}>{player1Score}</color> - " +
				                 $"<u><b><color={ColorToHex(p2Color)}>{player2Score}</color></b></u>";
				break;
		}
	}

	private void ResetPaddles() {
		paddleManager.PaddleLeft.transform.localScale  = new Vector3(1, 1, 10);
		paddleManager.PaddleRight.transform.localScale = new Vector3(1, 1, 10);
		paddleManager.PaddleLeft.transform.position    = new Vector3(-35, 0, 0);
		paddleManager.PaddleRight.transform.position   = new Vector3(35, 0, 0);
		paddleManager.LeftPaddleSpeed                  = 50f;
		paddleManager.RightPaddleSpeed                 = 50f;
	}

	private void ResetBall(int scoringPlayer) {
		ballController.ResetPosition(scoringPlayer);
	}

	private void KillPowerUps() {
		powerUpSpawner.RemovePowerUps();
	}
}
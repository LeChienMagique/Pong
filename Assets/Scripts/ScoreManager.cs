using UnityEngine;

public class ScoreManager: MonoBehaviour {
	public  GameObject     Ball;
	private BallController ballController;

	private int player1Score;
	private int player2Score;

	private void Start() {
		ballController = Ball.GetComponent<BallController>();
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
	}

	private void ResetBall(int scoringPlayer) {
		ballController.ResetPosition(scoringPlayer);
	}
}
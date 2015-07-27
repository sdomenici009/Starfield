using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	protected GameManager  gameManager;
	protected ScoreManager scoreManager;

	protected virtual void Awake () {
		gameManager  = GameManager.instance;
		scoreManager = ScoreManager.instance;
	}
}

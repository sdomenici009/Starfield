using UnityEngine;
using System.Collections;

public class Game_Controller : MonoBehaviour {
	
	void Start () {
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
	}
	
	void Update () {
	}

	IEnumerator EndGameScreen()
	{
		yield return new WaitForSeconds(1f);

		ResetGame();
	}

	public void ResetGame()
	{
		Application.LoadLevel(0);
	}
}

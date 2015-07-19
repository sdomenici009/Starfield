using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameState startMenu = new StartMenu();
	public GameState startMenu2 = new StartMenu();

	private GameState currentState;
	public GameState CurrentState
	{
		get { return currentState; }
	}

	void Start () {
		currentState = startMenu;
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
	}
	
	void Update () {
		currentState.Execute();
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

	public void StateTransition(GameState newState)
	{
		currentState.EndState();
		currentState = newState;
		currentState.StartState();
	}
}

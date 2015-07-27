using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	
	public static GameManager instance
	{
		get
		{
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<GameManager>();
			return _instance;
		}
	}
	
	public GameState startMenu = new StartMenuGameState();
	public GameState startMenu2 = new StartMenuGameState();

	private GameState currentState;
	public GameState CurrentState
	{
		get { return currentState; }
	}

	[SerializeField]
	private Player player;
	public Player Player
	{
		get { return player; }
	}

	private Level currentLevel;
	private List<Level> levels = new List<Level>();

	void Start () {
		for(int i=0; i < transform.childCount; i++)
		{
			levels.Add(transform.GetChild(i).GetComponent<Level>());
		}

		if(levels.Count > 0) currentLevel = levels[0];
		else Debug.LogError("Where did you put the levels, ya dope?");

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

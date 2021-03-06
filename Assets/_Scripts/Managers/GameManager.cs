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

	[SerializeField]
	private GameObject homeCanvas;

	[SerializeField]
	private GameObject levelTransitionCanvas;

	public GameState home = new StartMenuGameState();
	public GameState inGame = new InGameGameState();

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

	private List<Level> levels = new List<Level>();
	private Level currentLevel;
	private int levelIndex = 0;

	public bool betweenLevels = true;

	void Start () {
		for(int i=0; i < transform.childCount; i++)
		{
			levels.Add(transform.GetChild(i).GetComponent<Level>());
		}

		if(levels.Count > 0) currentLevel = levels[0];
		else Debug.LogError("Where did you put the levels, ya dope?");

		//currentLevel.OnLevelStart();

		currentState = home;
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
	}

	public void StartCurrentLevel()
	{
		currentLevel.OnLevelStart();
	}

	void Update () {
		currentState.Execute();

		if(betweenLevels && !player.cursor.gameObject.activeInHierarchy)
		{
			player.cursor.gameObject.SetActive(true);
		}

		if(!betweenLevels && player.cursor.gameObject.activeInHierarchy)
		{
			player.cursor.gameObject.SetActive(false);
		}

		if(currentState == inGame)
		{
			if(homeCanvas.activeInHierarchy) homeCanvas.SetActive(false);

			if(levels.Count > levelIndex && !betweenLevels)
			{
				currentLevel.currentWave.waveTimer -= Time.deltaTime;
				
				if(currentLevel.currentWave.enemies.Count == 0 ||
				   currentLevel.currentWave.waveTimer <= 0)
				{
					currentLevel.currentWave.OnWaveEnd();
					
					currentLevel.waveIndex++;
					if(currentLevel.waves.Count > currentLevel.waveIndex)
					{
						currentLevel.currentWave = currentLevel.waves[currentLevel.waveIndex];
						currentLevel.currentWave.OnWaveStart();
					}
					else
					{
						betweenLevels = true;
					}
				}
			}
			else
			{
				if(!levelTransitionCanvas.activeInHierarchy)
				{
					currentLevel.OnLevelEnd();
					levelTransitionCanvas.SetActive(true);
				}
			}
		}
	}

	public void RetryLevel()
	{
		currentLevel.OnLevelStart();
		betweenLevels = false;
	}

	public void NextLevel()
	{
		levelIndex++;
		currentLevel = levels[levelIndex];
		currentLevel.OnLevelStart();
		betweenLevels = false;
	}

	public void ReturnHome()
	{
		homeCanvas.SetActive(true);
		StateTransition(home);
		betweenLevels = false;
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

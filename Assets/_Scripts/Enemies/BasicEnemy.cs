using UnityEngine;
using System.Collections;

public class BasicEnemy : Enemy {

	//Transform target;
	//float randomPositionInitialTimer = 1.25f;
	//float randomPositionTimer;
	//Vector3 randomPositionInFrontOfPlayer;

	//bool inRange = false;

	private EnemyState currentState;

	private HuntingEnemyState hunting;
	private AttackingEnemyState attacking;
	private LeavingEnemyState leaving;

	private Player player;

	void Start () {
		player = GameManager.instance.Player;

		hunting = new HuntingEnemyState(this, player);
		attacking = new AttackingEnemyState(this, player);
		leaving = new LeavingEnemyState(this, player);

		currentState = hunting;
		currentState.StartState();
	}
	
	void Update () {
		currentState.Execute();

		if(currentState == hunting && Mathf.Abs(transform.position.z - player.transform.position.z) < 5)
		{
			StateTransition(attacking);
		}

		if(parentWave.waveTimer < 0)
		{
			StateTransition(leaving);
		}

		if(health <= 0)
		{
			scoreManager.Add(5);
			Destroy(gameObject);
		}
	}

	public void StateTransition(EnemyState newState)
	{
		currentState.EndState();
		currentState = newState;
		currentState.StartState();
	}
}

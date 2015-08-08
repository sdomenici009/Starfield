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
		//target = gameManager.Player.transform;
		//randomPositionTimer = randomPositionInitialTimer;
		//randomPositionInFrontOfPlayer = target.position + new Vector3(Random.Range(-1.75f, 1.75f), Random.Range(-1.75f, 1.75f), 3f);
	}
	
	void Update () {
		transform.LookAt(player.transform);

		currentState.Execute();

		if(currentState == hunting && Mathf.Abs(transform.position.z - player.transform.position.z) < 5)
		{
			StateTransition(attacking);
		}

		/*
		randomPositionTimer -= Time.deltaTime;

		transform.LookAt(target);

		if(Mathf.Abs(transform.position.z - target.position.z) < 10)
		{
			if(!inRange)
			{
				randomPositionTimer = randomPositionInitialTimer;
				rigidbody.AddForce((transform.position - target.position)*speed);
				inRange = true;
			}

			if(randomPositionTimer < 0)
			{
				randomPositionInFrontOfPlayer = target.position + new Vector3(Random.Range(-1.75f, 1.75f), Random.Range(-1.75f, 1.75f), 3f);
				randomPositionTimer = randomPositionInitialTimer;

				rigidbody.AddForce((randomPositionInFrontOfPlayer - transform.position)*speed);
			}
		}
		else
		{
			if(randomPositionTimer < 0)
			{
				rigidbody.AddForce((target.position - transform.position)*speed);
				randomPositionTimer = 7.5f;
			}
		}*/

		if(health < 0)
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

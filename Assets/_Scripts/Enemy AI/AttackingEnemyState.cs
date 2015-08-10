using UnityEngine;
using System.Collections;

public class AttackingEnemyState : EnemyState {

	private Enemy enemy;
	private Rigidbody enemyRigidbody;
	private Player player;

	private float initialMoveTimer = 3.5f;
	private float moveTimer;
	private Vector3 targetPosition;

	private float randomMoveDelay;
	
	public AttackingEnemyState(Enemy enemy, Player player)
	{
		this.enemy = enemy;
		enemyRigidbody = enemy.GetComponent<Rigidbody>();
		this.player = player;
		randomMoveDelay = Random.Range(1f, 2f);
	}
	
	public override void StartState() 
	{
		moveTimer = initialMoveTimer;
		targetPosition = player.transform.position + new Vector3(Random.Range(-1.75f, 1.75f), Random.Range(-1.75f, 1.25f), 2f);
	}
	
	public override void Execute() 
	{
		enemy.transform.LookAt(player.transform);

		moveTimer -= Time.deltaTime;

		if(moveTimer < initialMoveTimer)
		{
			Vector3 targetDirection = new Vector3(Mathf.Clamp(targetPosition.x - enemy.transform.position.x, -1f, 1f),
			                                      Mathf.Clamp(targetPosition.y - enemy.transform.position.y, -1f, 1f),
			                                      Mathf.Clamp(targetPosition.z - enemy.transform.position.z, -1f, 1f));

			enemyRigidbody.AddForce((targetDirection)*enemy.Speed/3f, ForceMode.Acceleration);
		}

		if(moveTimer < 0)
		{
			targetPosition = player.transform.position + new Vector3(Random.Range(-1.75f, 1.75f), Random.Range(-1.75f, 1.25f), 2f);

			if(Random.Range(0, 2) == 0)
			{
				moveTimer = initialMoveTimer + randomMoveDelay;
				randomMoveDelay = Random.Range(2f, 3f);
			}
			else
			{	
				GameObject.Instantiate(enemy.Projectile, enemy.transform.position + enemy.transform.forward*.7f, Quaternion.identity);
				moveTimer = initialMoveTimer;
			}
		}
	}
	
	public override void EndState() {}
}

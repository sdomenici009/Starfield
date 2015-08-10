using UnityEngine;
using System.Collections;

public class LeavingEnemyState : EnemyState {

	private Enemy enemy;
	private Rigidbody enemyRigidbody;
	private Player player;
	
	public LeavingEnemyState(Enemy enemy, Player player)
	{
		this.enemy = enemy;
		enemyRigidbody = enemy.GetComponent<Rigidbody>();
		this.player = player;
	}
	
	public override void StartState() {}
	
	public override void Execute() 
	{
		enemyRigidbody.AddForce((enemy.transform.position - new Vector3(0,0,20)).normalized*enemy.Speed, ForceMode.Acceleration);
	}
	
	public override void EndState() {}
}

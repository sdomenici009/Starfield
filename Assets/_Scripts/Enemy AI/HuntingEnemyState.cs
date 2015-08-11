using UnityEngine;
using System.Collections;

public class HuntingEnemyState : EnemyState {

	private Enemy enemy;
	private Rigidbody enemyRigidbody;
	private Player player;

	public HuntingEnemyState(Enemy enemy, Player player)
	{
		this.enemy = enemy;
		enemyRigidbody = enemy.GetComponent<Rigidbody>();
		this.player = player;
	}
	
	public override void StartState() 
	{

	}
	
	public override void Execute() 
	{
		enemy.transform.LookAt(player.transform);
		enemyRigidbody.AddForce((player.transform.position - enemy.transform.position).normalized*enemy.Speed, ForceMode.Acceleration);
	}
	
	public override void EndState() 
	{
		enemyRigidbody.AddForce(-enemyRigidbody.velocity/1.5f, ForceMode.VelocityChange); //= Vector3.zero;
		//enemyRigidbody.angularVelocity = Vector3.zero; 
	}
}

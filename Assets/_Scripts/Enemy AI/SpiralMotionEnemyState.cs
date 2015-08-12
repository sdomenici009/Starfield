using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiralMotionEnemyState : EnemyState {
	
	private Enemy enemy;
	private Rigidbody enemyRigidbody;
	private Player player;

	List<Vector3> targetPoints = new List<Vector3>();
	private int currentTarget = 0;

	public SpiralMotionEnemyState(Enemy enemy, Player player)
	{
		this.enemy = enemy;
		enemyRigidbody = enemy.GetComponent<Rigidbody>();
		this.player = player;
	}
	
	public override void StartState() 
	{
		for(int i=0; i < ((int)Mathf.Abs(enemy.transform.position.z - player.transform.position.z - 300)); i++)
		{
			targetPoints.Add(enemy.transform.position - new Vector3(Mathf.Cos(i*.25f*Mathf.PI)*.75f, Mathf.Sin(i*Mathf.PI*.25f)*.75f, i+1));
		}

		if(currentTarget < targetPoints.Count)
		{
			enemyRigidbody.AddForce((targetPoints[currentTarget] - enemy.transform.position).normalized*enemy.Speed);
		}
	}
	
	public override void Execute() 
	{
		if(currentTarget < targetPoints.Count)
		{
			enemyRigidbody.AddForce((targetPoints[currentTarget] - enemy.transform.position).normalized*enemy.Speed);

			if(enemy.transform.position.z < targetPoints[currentTarget].z)
			{
				currentTarget++;
			}
		}
	}
	
	public override void EndState() {}
}
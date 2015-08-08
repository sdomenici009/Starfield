using UnityEngine;
using System.Collections;

public class LeavingEnemyState : EnemyState {

	private Enemy enemy;
	private Player player;
	
	public LeavingEnemyState(Enemy enemy, Player player)
	{
		this.enemy = enemy;
		this.player = player;
	}
	
	public override void StartState() {}
	
	public override void Execute() {}
	
	public override void EndState() {}
}

using UnityEngine;
using System.Collections;

public class NeutralEnemy : Enemy {
	
	private EnemyState currentState;
	
	private Player player;

	private SpiralMotionEnemyState spiral;
	
	void Start () {
		player = GameManager.instance.Player;
		spiral = new SpiralMotionEnemyState(this, player);

		currentState = spiral;
		currentState.StartState();
	}
	
	void Update () {
		currentState.Execute();
	}
	
	public void StateTransition(EnemyState newState)
	{
		currentState.EndState();
		currentState = newState;
		currentState.StartState();
	}
}

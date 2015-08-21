using UnityEngine;
using System.Collections;

public class FancyEnemy : Enemy {

	private EnemyState currentState;
	
	private HuntingEnemyState hunting;
	private AttackingEnemyWithShieldState attacking;
	private LeavingEnemyState leaving;
	
	private Player player;

	[SerializeField]
	private GameObject shield;
	public GameObject Shield {
		get {
			return shield;
		}
	}
	
	void Start () {
		player = GameManager.instance.Player;
		
		hunting = new HuntingEnemyState(this, player);
		attacking = new AttackingEnemyWithShieldState(this, player);
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
	}
	
	public void StateTransition(EnemyState newState)
	{
		currentState.EndState();
		currentState = newState;
		currentState.StartState();
	}

	protected override void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "PlayerProjectile")
		{
			if(!shield.activeInHierarchy)
			{
				health--;
				
				Destroy(collision.gameObject);
				
				if(!dead && health <= 0)
				{
					for(int i=0; i < 10; i++)
					{
						onDeathParticleSystem.transform.position = transform.position + Random.onUnitSphere*.125f;
						onDeathParticleSystem.Emit(30);
					}
					
					dead = true;
					if(parentWave != null)
						parentWave.enemies.Remove(this.gameObject);
					scoreManager.Add(scoreValue);
					Destroy(gameObject);
				}
			}
		}
	}
}

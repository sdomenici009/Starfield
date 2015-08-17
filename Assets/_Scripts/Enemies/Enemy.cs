using UnityEngine;
using System.Collections;

public class Enemy : Actor {

	public Wave parentWave;

	protected ParticleSystem onDeathParticleSystem;

	[SerializeField]
	protected GameObject projectile;
	public GameObject Projectile
	{
		get { return projectile; }
	}

	[SerializeField]
	protected float lifetime;
	
	[SerializeField]
	protected float speed;
	public float Speed
	{
		get { return speed; }
	}

	[SerializeField]
	protected int health;

	[SerializeField]
	protected int scoreValue;

	protected Rigidbody rigidbody;

	protected override void Awake () {
		base.Awake();
		onDeathParticleSystem = GameObject.Find("EnemyDeath").GetComponent<ParticleSystem>();

		rigidbody = GetComponent<Rigidbody>();
	}

	[SerializeField]
	private int collisionDamage;
	public int CollisionDamage
	{
		get { return collisionDamage; }
	}

	bool dead = false;
	
	void Update () {
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "PlayerProjectile")
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

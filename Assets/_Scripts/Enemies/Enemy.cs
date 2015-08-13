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
		rigidbody = GetComponent<Rigidbody>();
	}

	bool dead = false;
	
	void Update () {
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "PlayerProjectile")
		{
			health--;

			if(!dead && health <= 0)
			{
				dead = true;
				parentWave.enemies.Remove(this.gameObject);
				scoreManager.Add(scoreValue);
				Destroy(gameObject);
			}
		}
	}
}

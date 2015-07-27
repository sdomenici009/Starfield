using UnityEngine;
using System.Collections;

public class Enemy : Actor {

	protected ParticleSystem onDeathParticleSystem;
	
	[SerializeField]
	protected float lifetime;
	
	[SerializeField]
	protected float speed;

	[SerializeField]
	protected int health;

	[SerializeField]
	protected int scoreValue;

	protected Rigidbody rigidbody;

	protected override void Awake () {
		base.Awake();
		rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "PlayerProjectile")
		{
			health--;
		}
	}
}

using UnityEngine;
using System.Collections;

public class PowerUp : Actor {

	bool captured = false;

	ParticleSystem onDeathParticleSystem;
	Rigidbody rigidbody;

	protected override void Awake () {
		base.Awake();
		onDeathParticleSystem = GameObject.Find("EnemyDeath").GetComponent<ParticleSystem>();
		rigidbody = GetComponent<Rigidbody>();
	}

	void Start()
	{
		rigidbody.AddForce(0, 0, Random.Range(-3.5f, -10.5f)*120f);
	}

	public void Update()
	{
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "PlayerProjectile")
		{
			Destroy(collision.gameObject);

			if(!captured)
			{
				for(int i=0; i < 10; i++)
				{
					onDeathParticleSystem.transform.position = transform.position + Random.onUnitSphere*.125f;
					onDeathParticleSystem.Emit(30);
				}

				captured = true;

				Destroy(gameObject);
			}
		}

		if(collision.collider.tag == "Player")
		{
			captured = true;
			Destroy (gameObject);
		}
	}
}

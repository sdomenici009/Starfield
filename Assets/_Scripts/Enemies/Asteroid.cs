using UnityEngine;
using System.Collections;

public class Asteroid : Enemy {

	protected override void Awake () {
		base.Awake();

		rigidbody.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));

		health = Random.Range(1, 4);
	}
	
	void Update () {
		if(transform.position.z < -25f)
			Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "PlayerProjectile")
		{
			health--;
		}

		if(health < 0)
		{
			for(int i=0; i < 10; i++)
			{
				onDeathParticleSystem.transform.position = transform.position + Random.onUnitSphere*.125f;
				onDeathParticleSystem.Emit(30);
			}

			scoreManager.Add(1);

			Destroy(gameObject);
		}
	}
}

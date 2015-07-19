using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public int health = 10;

	ParticleSystem asteroidDeath;
	Rigidbody rigidbody;
	Ship ship;

	void Start () {
		asteroidDeath = GameObject.Find("AsteroidDeath").GetComponent<ParticleSystem>();
		ship = GameObject.Find("Ship").GetComponent<Ship>();
		rigidbody = GetComponent<Rigidbody>();

		rigidbody.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
	}
	
	void Update () {
		if(transform.position.z < -10f)
			Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Lazer")
		{
			health--;
		}

		if(health < 0)
		{
			for(int i=0; i < 10; i++)
			{
				asteroidDeath.transform.position = transform.position + Random.onUnitSphere*.125f;
				asteroidDeath.Emit(30);
			}

			ship.score++;
			ship.scoreText.text = ship.score.ToString("D3");
			Destroy(gameObject);
		}
	}
}

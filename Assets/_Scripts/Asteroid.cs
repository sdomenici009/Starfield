using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public int health = 10;

	ParticleSystem asteroidDeath;
	Rigidbody rigidbody;
	Ship_Controller ship;

	// Use this for initialization
	void Start () {
		asteroidDeath = GameObject.Find("AsteroidDeath").GetComponent<ParticleSystem>();
		ship = GameObject.Find("Ship").GetComponent<Ship_Controller>();
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
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
			for(int i=0; i < rigidbody.mass/8; i++)
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

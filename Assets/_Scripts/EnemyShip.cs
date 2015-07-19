using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

	Transform target;

	int health = 5;

	float randomPositionInitialTimer = 7.5f;
	float randomPositionTimer;
	Vector3 randomPositionInFrontOfPlayer;

	void Start () {
		target = GameObject.Find("Ship").transform;
		randomPositionTimer = randomPositionInitialTimer;
		randomPositionInFrontOfPlayer = target.position + new Vector3(Random.Range(-1.1f, 1.1f), Random.Range(-1.1f, 1.1f), 4f);
	}
	
	void Update () {
		randomPositionTimer -= Time.deltaTime;

		if(randomPositionTimer < 0)
		{
			randomPositionInFrontOfPlayer = target.position + new Vector3(Random.Range(-1.1f, 1.1f), Random.Range(-1.1f, 1.1f), 4f);
			randomPositionTimer = randomPositionInitialTimer;
		}

		transform.LookAt(target);

		if(Vector3.Distance(transform.position, target.position) > 1f)
		{
			transform.GetComponent<Rigidbody>().drag = .25f;

			transform.GetComponent<Rigidbody>().AddForce((randomPositionInFrontOfPlayer - transform.position)/5f);
		}
		else
		{
			transform.GetComponent<Rigidbody>().drag = 1;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Lazer")
		{
			health--;
		}
		
		if(health < 0)
		{
			target.GetComponent<Ship>().score += 5;
			target.GetComponent<Ship>().scoreText.text = target.GetComponent<Ship>().score.ToString("D3");
			Destroy(gameObject);
		}
	}
}

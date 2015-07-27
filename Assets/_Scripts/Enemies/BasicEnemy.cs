using UnityEngine;
using System.Collections;

public class BasicEnemy : Enemy {

	Transform target;
	float randomPositionInitialTimer = 7.5f;
	float randomPositionTimer;
	Vector3 randomPositionInFrontOfPlayer;
	
	void Start () {
		target = gameManager.Player.transform;
		randomPositionTimer = randomPositionInitialTimer;
		randomPositionInFrontOfPlayer = target.position + new Vector3(Random.Range(-1.75f, 1.75f), Random.Range(-1.75f, 1.75f), 3f);
	}
	
	void Update () {
		randomPositionTimer -= Time.deltaTime;

		transform.LookAt(target);

		if(randomPositionTimer < 0)
		{
			randomPositionInFrontOfPlayer = target.position + new Vector3(Random.Range(-1.75f, 1.75f), Random.Range(-1.75f, 1.75f), 3f);
			randomPositionTimer = randomPositionInitialTimer;

			rigidbody.AddForce((randomPositionInFrontOfPlayer - transform.position)*speed*500f);
		}

		/*

		if(Vector3.Distance(transform.position, target.position) > 1f)
		{
			rigidbody.drag = .25f;

			rigidbody.AddForce((randomPositionInFrontOfPlayer - transform.position)*speed*1.5f);
		}
		else
		{
			if(randomPositionTimer < 0)
			{
				randomPositionInFrontOfPlayer = target.position + new Vector3(Random.Range(-1.75f, 1.75f), Random.Range(-1.75f, 1.75f), 3f);
				randomPositionTimer = randomPositionInitialTimer;

				Debug.Log ("WOMBO");
				
				rigidbody.AddForce((randomPositionInFrontOfPlayer - transform.position)*speed*250f);
			}

			rigidbody.drag = .5f;
		}

		*/

		if(health < 0)
		{
			scoreManager.Add(5);
			Destroy(gameObject);
		}
	}
}

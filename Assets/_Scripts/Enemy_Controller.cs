using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Controller : MonoBehaviour {

	[SerializeField]
	private List<GameObject> enemyPrefab;

	[SerializeField]
	private float initialEnemySpawnTimer;
	private float enemySpawnTimer;

	[SerializeField]
	private float initialIncreaseSpawnRate;
	private float increaseSpawnRateTimer;

	GameObject ship;

	void Start () {
		ship = GameObject.Find("Ship");
		enemySpawnTimer = initialEnemySpawnTimer;

		for(int i = (int)ship.transform.position.z; i < transform.position.z; i++)
		{
			for(int j=0; j < 10; j++)
			{
				SpawnAsteroid(new Vector3(0, 0, i));
			}
		}
	}
	
	void Update () {
		increaseSpawnRateTimer -= Time.deltaTime;

		if(increaseSpawnRateTimer < 0)
		{
			increaseSpawnRateTimer = initialIncreaseSpawnRate;
		}

		enemySpawnTimer -= Time.deltaTime;

		if(enemySpawnTimer < 0 && ship != null)
		{
			GameObject enemy = SpawnAsteroid(transform.position);
			enemy.transform.localScale = Vector3.zero;
			iTween.ScaleTo(enemy, iTween.Hash("scale", new Vector3(0.05f, 0.05f, 0.05f), "time", 10f, "easetype", "easeinexpo"));

			enemySpawnTimer = initialEnemySpawnTimer;
		}
	}

	GameObject SpawnAsteroid(Vector3 spawnPosition)
	{
		GameObject enemy = (GameObject)Instantiate(enemyPrefab[Random.Range(0, 4)], spawnPosition + Random.onUnitSphere*2f, Random.rotation);
		enemy.GetComponent<Rigidbody>().mass = 40;
		enemy.GetComponent<Asteroid>().health = Random.Range(1, 4);
		enemy.GetComponent<Rigidbody>().AddForce(0, 0, Random.Range(-35, -105)*40f);
		return enemy;
	}
}

﻿using UnityEngine;
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

	// Use this for initialization
	void Start () {
		ship = GameObject.Find("Ship");
		enemySpawnTimer = initialEnemySpawnTimer;

		for(int i=0; i < 10; i++)
		{
			GameObject enemy = (GameObject)Instantiate(enemyPrefab[Random.Range(0, 4)], transform.position + Random.onUnitSphere*2f, Random.rotation);
			//float randomScalar = Random.Range(0.01f, 0.05f);
			//enemy.transform.localScale = Vector3.one*.05f;//*randomScalar;
			enemy.GetComponent<Rigidbody>().mass = 100*.05f;// * randomScalar/.05f;
			enemy.GetComponent<Asteroid>().health = (int)(enemy.GetComponent<Rigidbody>().mass/20);
			enemy.GetComponent<Rigidbody>().AddForce(0, 0, Random.Range(-35, -105)*enemy.GetComponent<Rigidbody>().mass);
		}
	}
	
	// Update is called once per frame
	void Update () {
		increaseSpawnRateTimer -= Time.deltaTime;

		if(increaseSpawnRateTimer < 0)
		{
			if(initialEnemySpawnTimer >= .1f)
				//initialEnemySpawnTimer -= .05f;

			increaseSpawnRateTimer = initialIncreaseSpawnRate;
		}

		enemySpawnTimer -= Time.deltaTime;

		if(enemySpawnTimer < 0 && ship != null)
		{
			GameObject enemy = (GameObject)Instantiate(enemyPrefab[Random.Range(0, 4)], transform.position + Random.onUnitSphere*2f, Random.rotation);
			//float randomScalar = Random.Range(0.01f, 0.05f);
			//enemy.transform.localScale = Vector3.one*.05f;//*randomScalar;
			enemy.GetComponent<Rigidbody>().mass = 100*.05f;// * randomScalar/.05f;
			enemy.GetComponent<Asteroid>().health = (int)(enemy.GetComponent<Rigidbody>().mass/20);
			enemy.GetComponent<Rigidbody>().AddForce(0, 0, Random.Range(-35, -105)*enemy.GetComponent<Rigidbody>().mass);

			enemySpawnTimer = initialEnemySpawnTimer;
		}
	}
}

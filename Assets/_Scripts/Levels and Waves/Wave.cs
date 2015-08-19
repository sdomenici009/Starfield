using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {

	[SerializeField]
	private float waveDelay;

	[SerializeField]
	private float initialWaveTimer;

	public float waveTimer;

	public List<Enemy> enemyPrefabs;

	[HideInInspector]
	public List<GameObject> enemies = new List<GameObject>();

	public List<PowerUp> powerUpPrefabs;

	[HideInInspector]
	public List<GameObject> powerUps = new List<GameObject>();
	
	public void OnWaveStart()
	{
		waveTimer = initialWaveTimer;

		if(enemies.Count > 0)
		{
			for(int i=0; i < enemies.Count; i++)
			{
				Destroy (enemies[i]);
			}
		}

		enemies.Clear();

		if(enemies.Count > 0)
		{
			for(int i=0; i < powerUps.Count; i++)
			{
				Destroy (powerUps[i]);
			}
		}

		powerUps.Clear();

		Debug.Log ("OnWaveStart");
		for(int i=0; i < enemyPrefabs.Count; i++)
		{
			GameObject enemy = (GameObject)Instantiate(enemyPrefabs[i].gameObject, new Vector3(0, 0, 35 + i*2f), Quaternion.identity);
			enemies.Add(enemy);
			enemy.GetComponent<Enemy>().parentWave = this;
		}

		for(int i=0; i < powerUpPrefabs.Count; i++)
		{
			GameObject powerUp = (GameObject)Instantiate(powerUpPrefabs[i].gameObject, new Vector3(0, 0, 35 + i*2f), Quaternion.identity);
			powerUps.Add(powerUp);
		}
	}

	public void OnWaveEnd()
	{
		Debug.Log ("OnWaveEnd");
	}
}

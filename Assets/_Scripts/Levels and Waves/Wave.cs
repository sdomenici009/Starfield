using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {

	[SerializeField]
	private float waveDelay;

	public float waveTimer;

	public List<Enemy> enemyPrefabs = new List<Enemy>();
	public List<Enemy> enemies = new List<Enemy>();
	public List<PowerUp> powerUps = new List<PowerUp>();
	
	public void OnWaveStart()
	{
		Debug.Log ("OnWaveStart");
		for(int i=0; i < enemyPrefabs.Count; i++)
		{
			GameObject enemy = (GameObject)Instantiate(enemyPrefabs[i].gameObject, new Vector3(0, 0, 35 + i*2f), Quaternion.identity);
			enemies.Add(enemy.GetComponent<Enemy>());
			enemy.GetComponent<Enemy>().parentWave = this;
		}
	}

	public void OnWaveEnd()
	{
		Debug.Log ("OnWaveEnd");
	}
}

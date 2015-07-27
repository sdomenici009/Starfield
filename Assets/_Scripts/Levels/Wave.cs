using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {

	[SerializeField]
	private float waveDelay;

	[SerializeField]
	private float maximumWaveTime;

	private bool waveCompleted = false;

	public List<Enemy> enemies = new List<Enemy>();
	public List<PowerUp> powerUps = new List<PowerUp>();

	public void OnWaveStart()
	{
		for(int i=0; i < enemies.Count; i++)
		{
			GameObject enemy = (GameObject)Instantiate(enemies[i].gameObject, new Vector3(0, 0, 35), Quaternion.identity);
		}
	}

	public void OnWaveEnd()
	{
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {

	[SerializeField]
	private float waveDelay;

	public float maximumWaveTime;
	
	public List<Enemy> enemies = new List<Enemy>();
	public List<PowerUp> powerUps = new List<PowerUp>();
	
	public void OnWaveStart()
	{
		Debug.Log ("OnWaveStart");
		for(int i=0; i < enemies.Count; i++)
		{
			GameObject enemy = (GameObject)Instantiate(enemies[i].gameObject, new Vector3(0, 0, 35), Quaternion.identity);
			enemy.GetComponent<Enemy>().parentWave = this;
		}
	}

	public void OnWaveEnd()
	{
		Debug.Log ("OnWaveEnd");
	}
}

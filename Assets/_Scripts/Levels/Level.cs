using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

	[HideInInspector]
	public List<Wave> waves = new List<Wave>();

	[HideInInspector]
	public Wave currentWave;

	[HideInInspector]
	public int waveIndex = 0;
	
	public void OnLevelStart()
	{
		Debug.Log("OnLevelStart");

		for(int i=0; i < transform.childCount; i++)
		{
			waves.Add(transform.GetChild(i).GetComponent<Wave>());
		}
		
		if(waves.Count > 0) currentWave = waves[0];
		else Debug.LogError("Where did you put the waves, ya dope?");
		
		currentWave.OnWaveStart();
	}

	public void OnLevelEnd()
	{
		Debug.Log ("OnLevelEnd");
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	
	private List<Wave> waves = new List<Wave>();
	private Wave currentWave;

	void Start () {
		for(int i=0; i < transform.childCount; i++)
		{
			waves.Add(transform.GetChild(i).GetComponent<Wave>());
		}

		if(waves.Count > 0) currentWave = waves[0];
		else Debug.LogError("Where did you put the waves, ya dope?");

		currentWave.OnWaveStart();
	}
	
	void Update () {
	
	}
}

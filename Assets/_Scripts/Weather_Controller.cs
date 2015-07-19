using UnityEngine;
using System.Collections;

public class Weather_Controller : MonoBehaviour {

	[SerializeField]
	private ParticleSystem ps;

	void Start () {
	
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
		{
			ps.enableEmission = !ps.enableEmission;
		}
	}
}

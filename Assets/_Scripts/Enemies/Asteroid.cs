using UnityEngine;
using System.Collections;

public class Asteroid : Enemy {

	protected override void Awake () {
		base.Awake();

		rigidbody.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));

		health = Random.Range(1, 4);
	}
	
	void Update () {
		if(transform.position.z < -25f)
			Destroy(gameObject);
	}
}

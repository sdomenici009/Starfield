using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {

	private ParticleSystem bulletExplosionParticleSystem;

	void Start () {
		bulletExplosionParticleSystem = GameObject.Find("BulletPS").GetComponent<ParticleSystem>();
	}
	
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Meteor")
		{
			Destroy(gameObject);
			for(int i=0; i < Random.Range(20, 40); i++)
			{
				bulletExplosionParticleSystem.Emit(collision.contacts[0].point + 
				                                   Random.onUnitSphere*.02f, collision.collider.attachedRigidbody.velocity +
				                                   new Vector3(Random.Range(-.01f, .01f), Random.Range(-.01f, .01f), Random.Range(-.01f, 0)), Random.Range(0.01f, 0.075f), .4f, Color.white);
			}
		}
	}
}

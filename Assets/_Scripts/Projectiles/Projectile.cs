using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private ParticleSystem onCollisionParticleSystem;

	[SerializeField]
	private AudioClip triggered;

	[SerializeField]
	private float lifetime;

	[SerializeField]
	private float speed;

	private Rigidbody rigidbody;

	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		onCollisionParticleSystem = GameObject.Find("BulletPS").GetComponent<ParticleSystem>();
	}

	public void Initialize(Vector3 audioPoint, Transform head)
	{
		rigidbody.AddForce(head.forward*speed);
		transform.rotation = head.rotation;
		AudioSource.PlayClipAtPoint(triggered, audioPoint, .75f);
		Destroy(gameObject, lifetime);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Meteor")
		{
			Destroy(gameObject);
			int rand = Random.Range(20, 40);
			for(int i=0; i < rand; i++)
			{
				onCollisionParticleSystem.Emit(collision.contacts[0].point + 
				                                   Random.onUnitSphere*.02f, collision.collider.attachedRigidbody.velocity +
				                                   new Vector3(Random.Range(-.01f, .01f), Random.Range(-.01f, .01f), Random.Range(-.01f, 0)), Random.Range(0.01f, 0.075f), .4f, Color.white);
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	[SerializeField]
	private float speed;

	[SerializeField]
	private int health;

	private Player player;

	private Rigidbody rigidbody;

	[SerializeField]
	private int damage;
	public int Damage
	{
		get { return damage; }
	}

	void Awake () {
		player = GameManager.instance.Player;
		rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		rigidbody.AddForce((player.transform.position - transform.position).normalized*speed, ForceMode.Acceleration);
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "PlayerProjectile")
		{
			health--;
			
			if(health < 0)
			{
				Destroy(gameObject);
			}
		}
	}
}

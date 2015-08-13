using UnityEngine;
using System.Collections;

public class VRButton : MonoBehaviour {

	protected GameManager gameManager;

	void Start()
	{
		gameManager = GameManager.instance;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "PlayerProjectile")
		{
			OnHit();
		}
	}

	public virtual void OnHit() {}
}

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
		if(gameManager.CurrentState == gameManager.startMenu)
		{
			if(collision.collider.tag == "Lazer")
			{
				OnHit();
			}
		}
	}

	public virtual void OnHit() {}
}

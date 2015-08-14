using UnityEngine;
using System.Collections;

public class VRButton : MonoBehaviour {

	protected GameManager gameManager;

	private float initialClickDelayTimer = 1f;
	private float clickDelayTimer = 0;

	void Start()
	{
		gameManager = GameManager.instance;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(clickDelayTimer <= 0)
		{
			if(collision.collider.tag == "PlayerProjectile")
			{
				OnHit();
				clickDelayTimer = initialClickDelayTimer;
			}
		}
	}

	public virtual void OnHit() {}
}

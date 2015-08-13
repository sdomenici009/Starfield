using UnityEngine;
using System.Collections;

public class HomeButton : VRButton {
	
	public override void OnHit ()
	{
		gameManager.ReturnHome();
		transform.root.gameObject.SetActive(false);
	}
}

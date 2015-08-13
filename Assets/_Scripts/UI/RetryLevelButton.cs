using UnityEngine;
using System.Collections;

public class RetryLevelButton : VRButton {
	
	public override void OnHit ()
	{
		gameManager.RetryLevel();
		transform.root.gameObject.SetActive(false);
	}
}

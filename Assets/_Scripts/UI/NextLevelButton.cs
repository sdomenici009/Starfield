using UnityEngine;
using System.Collections;

public class NextLevelButton : VRButton {
	
	public override void OnHit ()
	{
		gameManager.NextLevel();
		transform.root.gameObject.SetActive(false);
	}
}

using UnityEngine;
using System.Collections;

public class PlayButton : VRButton {

	public override void OnHit ()
	{
		gameManager.StateTransition(gameManager.inGame);
		gameManager.betweenLevels = false;
	}
}

using UnityEngine;
using System.Collections;

public class InGameGameState : GameState {
	
	public InGameGameState()
	{
		
	}
	
	public override void StartState() {
		GameManager.instance.StartCurrentLevel();
	}
	
	public override void Execute() 
	{
	}
	
	public override void EndState() {}
}

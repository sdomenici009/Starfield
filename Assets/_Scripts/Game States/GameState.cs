using UnityEngine;
using System.Collections;

public class GameState {

	public GameState () {}
	public virtual void StartState() {}
	public virtual void Execute() {}
	public virtual void EndState() {}
}

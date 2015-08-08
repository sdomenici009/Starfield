using UnityEngine;
using System.Collections;

public class EnemyState {

	public EnemyState () {}
	public virtual void StartState() {}
	public virtual void Execute() {}
	public virtual void EndState() {}
}

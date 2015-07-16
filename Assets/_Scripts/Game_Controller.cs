using UnityEngine;
using System.Collections;

public class Game_Controller : MonoBehaviour {

	[SerializeField]
	private GameObject resetPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator EndGameScreen()
	{
		yield return new WaitForSeconds(1f);
		
		resetPanel.SetActive(true);
	}

	public void ResetGame()
	{
		Application.LoadLevel(0);
	}
}

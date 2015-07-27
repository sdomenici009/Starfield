using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private static ScoreManager _instance;
	
	public static ScoreManager instance
	{
		get
		{
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<ScoreManager>();
			return _instance;
		}
	}

	[SerializeField]
	private Text scoreText;

	private int score = 0;

	public void Add(int value)
	{
		score += value;
		scoreText.text = score.ToString("D3");
	}

}

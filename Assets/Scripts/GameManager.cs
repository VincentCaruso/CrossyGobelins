using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	RowGenerator RowGenerator;

	[SerializeField]
	TextMeshProUGUI Textfield;

	[SerializeField]
	GameObject GameOver;

	int currentScore = 0;


	// Start is called before the first frame update
	void Start()
	{
		GameOver.SetActive(false);
		Textfield.text = "Score : " + currentScore.ToString("000000");
	}

	public void GoUp()
	{
		AddPoints(100);
		RowGenerator.GoForward();
	}

	void AddPoints(int numPoints)
	{
		currentScore += numPoints;
		Textfield.text = "Score : " + currentScore.ToString("000000");
	}

	public void Die()
	{
		GameOver.SetActive(true);
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Artemis;

public class UpdateScore : MonoBehaviour
{
	public bool HighScore;
	public bool CurrentScore;
	public bool Velocity = false;

	public TextMeshProUGUI text;
	private DataManager dm;

	void Start()
	{
		dm = FindObjectOfType<DataManager>();
	}
	void Update() {
		if (HighScore)
		{
			text.text = "High Score: " + dm.GetScore(1);
		}
		if (CurrentScore)
		{
			text.text = "Current Score: " + dm.GetScore(2);
			Debug.Log(DataManager.CurrentScore);
		}
		if (Velocity)
		{
			text.text = "Velocity: " + DataManager.Velocity + " m/s";
		}
	}
}

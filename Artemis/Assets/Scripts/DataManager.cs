using UnityEngine;

namespace Artemis{

	/// <summary>
	/// This class contains and handles data which will be important for the functioning of the game
	/// </summary>
	public class DataManager : MonoBehaviour
	{
		public int DefaultHealth;
		public int DefaultScore;
		public int DefaultOxygen;
		public bool Debugging;


		[HideInInspector]
		public static int CurrentHealth;

		[HideInInspector]
		public static int CurrentScore;

		[HideInInspector]
		public static int CurrentOxygen;

		[HideInInspector]
		public static int HighScore;

		public void Start()
		{
			DontDestroyOnLoad(gameObject);
			ResetAll();

			if (Debugging)
				Debug.Log("Start was called in DataManager.cs");
		}


		/// <summary>
		/// This method makes DataManager set all variables to their default values
		/// </summary>
		public void ResetAll()
		{
			if (Debugging)
				Debug.Log("ResetAll was called in DataManager.cs");

			CurrentHealth = DefaultHealth;
			CurrentScore = DefaultScore;
			CurrentOxygen = DefaultOxygen;
			if (PlayerPrefs.HasKey("HighScore"))
			{
				HighScore = PlayerPrefs.GetInt("HighScore");
			}
			else
			{
				HighScore = 0;
			}
		}
		/// <summary>
		///		This function handles updating saved data on scores
		/// </summary>
		/// <param name="Score">This is the new score obtained after finishing the level</param>
		/// <param name="ForceHighScore">Forces highscore if true, behaves normally otherwise</param>
		public void SubmitScore(int Score, bool ForceHighScore)
		{
			if (Debugging)
				Debug.Log("SubmitScore was called in DataManager.cs");

			PlayerPrefs.SetInt("Last Score", Score);
			if (PlayerPrefs.HasKey("HighScore"))
			{
				HighScore = PlayerPrefs.GetInt("HighScore");
			}
			else
			{
				HighScore = 0;
			}
			if (!ForceHighScore) {
				if (Score > HighScore)
				{
					PlayerPrefs.SetInt("HighScore", Score);
				}
			}
			if (ForceHighScore)
			{
				PlayerPrefs.SetInt("HighScore", Score);
			}
		}
		/// <summary>
		///		This function handles updating saved data on scores
		/// </summary>
		/// <param name="Score">This is the new score obtained after finishing the level</param>
		public void SubmitScore(int Score)
		{
			if (Debugging)
				Debug.Log("SubmitScore was called in DataManager.cs");

			PlayerPrefs.SetInt("Last Score", Score);
			if (PlayerPrefs.HasKey("HighScore"))
			{
				HighScore = PlayerPrefs.GetInt("HighScore");
			}
			else
			{
				HighScore = 0;
			}

			if (Score > HighScore)
			{
				PlayerPrefs.SetInt("HighScore", Score);
			}
		}
		/// <summary>
		/// This function handles updating saved data on scores, no input required
		/// </summary>
		public void SubmitScore()
		{
			if (Debugging)
				Debug.Log("SubmitScore was called in DataManager.cs");

			int Score;
			Score = CurrentScore;

			PlayerPrefs.SetInt("Last Score", Score);
			if (PlayerPrefs.HasKey("HighScore"))
			{
				HighScore = PlayerPrefs.GetInt("HighScore");
			}
			else
			{
				HighScore = 0;
			}

			if (Score > HighScore)
			{
				PlayerPrefs.SetInt("HighScore", Score);
			}
		}
	}
}
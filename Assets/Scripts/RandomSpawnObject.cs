using UnityEngine;
namespace Artemis
{
	/// <summary>
	/// Handles the spawning of an object at multiple locations throughout the map, makes sure each spawn is random
	/// </summary>
	public class RandomSpawnObject : MonoBehaviour
	{
		[Tooltip("How likely is this object to spawn at an individual location at a time")]
		public float ProbabilityPercentage;
		public GameObject SpawnPrefab;
		public Transform[] SpawnLocations;
		[Tooltip("How long till the next spawn is checked (seconds)")]
		[Range(0, 10)]
		public float CheckPeriod;

		[Tooltip("Keep this on if debugging features are needed")]
		public bool Debugging = false;
		[Tooltip("Keep this off if you want to bypass scripts' assumptions and error-avoidance mechanisms to have full control over its behavior")]
		public bool SafeMode = true;

		private float currentRandom;
		private float localTime;

		public void Start()
		{
			if (SafeMode)
			{
				if (SpawnLocations[0] == null)
				{
					SpawnLocations[0] = transform;
				}
			}
		}

		public void Update()
		{
			if (localTime > CheckPeriod)
			{
				Random.InitState(System.DateTime.Now.Millisecond);
				currentRandom = Random.Range(0, 20);
				foreach (Transform location in SpawnLocations)
				{
					currentRandom += Random.Range(0.0f, 10000.0f);
					Random.InitState(Mathf.RoundToInt(currentRandom));
					if (Random.Range(0.0f, 100.0f) < ProbabilityPercentage)
					{
						Instantiate(SpawnPrefab, location);
					}
				}
				localTime = 0;
			}
			localTime += Time.deltaTime;
		}



		/// <summary>
		/// Increases the probability of the object spawning
		/// </summary>
		/// <param name="P">How likely the object is to spawn</param>
		public void IncreaseProbability(float P)
		{
			ProbabilityPercentage += P;
		}

		/// <summary>
		/// Increase the probability of the object spawning by one percent
		/// </summary>
		public void IncreaseProbability()
		{
			ProbabilityPercentage += 1;
		}
	}
}

using System;
using UnityEngine;

namespace Artemis
{
	/// <summary>Makes a light component flicker, pretty self-explanatory</summary>
	/// <author>Hamza Bin Aamir, 2020</author>
	[DisallowMultipleComponent] // We dont want our light flicker scripts messing with each other now do we?
	public class LightFlicker : MonoBehaviour
	{
		/// <p> Public Variables </p>
		/// <list>
		///		<li> Minimum Intensity (Floating Point) </li>
		///		<li> Maximum Intensity (Floating Point) </li>
		///		<li> Rate (Floating Point) </li>
		///		<li> Smoothing (Floating Point) </li>
		///		<li> light (Light) </li>
		///		<li> Skybox (Skybox) </li>
		///		<li> Debugging (Boolean) </li>
		///		<li> SafeMode (Boolean) </li>
		/// </list>

		[Tooltip("The lowest the intensity value can go")]
		[Range(0, 1)]
		public float MinIntensity;

		[Tooltip("If you want to modify the skybox with this light too, add materials. Otherwise leave it empty.")]
		public Material[] skybox;

		[Tooltip("The highest the intensity value can go")]
		[Range(0, 5)]
		public float MaxIntensity = 1f;

		[Tooltip("How often should the light change its target? (set negative value if random)")]
		[Range(-1, 2)]
		public float Rate = 1f;

		[Tooltip("How smooth should the effect be?")]
		[Range(0, 1)]
		public float Smoothing = 0.5f;

		[Tooltip("What light should this module affect?")]
		public Light lightToFlicker;



		[Tooltip("Keep this on if debugging features are needed")]
		public bool Debugging = false;

		[Tooltip("Keep this off if you want to bypass scripts' assumptions and error-avoidance mechanisms to have full control over its behavior")]
		public bool SafeMode = true;


		/// <p> Private Variables </p>
		/// <list>
		///		<li> Local Time (Floating Point) </li>
		///		<li> Target Intensity (Floating Point) </li>
		/// </list>

		private float localTime;
		private float NormalizedRate;
		private float targetIntensity;

		public void Start()
		{
			// We dont want the program to be running with a NullReferenceException incase the designer forgot
			if (SafeMode)
			{
				if (lightToFlicker == null)
				{
					Debug.LogWarning("You did not attach a light to LightFlicker.cs in " + gameObject +
						" The issue was resolved automatically but please specify a light to avoid unintended behavior");
					lightToFlicker = gameObject.GetComponent<Light>();
					if (lightToFlicker == null)
					{
						Debug.LogError("There was no light attached to the GameObject " + gameObject +
							" at all, The issue was resolved automatically but please specify a light to avoid unintended behavior");
						lightToFlicker = FindObjectOfType<Light>();
					}
				}
			}

			if (Rate < 0)
			{
				NormalizedRate = UnityEngine.Random.Range(1, 3);
			}
			else
			{
				NormalizedRate = Rate;
			}

			if (Debugging)
				Debug.Log("Start function ran in LightFlicker.cs attached to " + gameObject + " sucessfully");
		}
		public void Update()
		{
			UnityEngine.Random.InitState(DateTime.Now.Millisecond); //This is a necessity for the program to generate random numbers as expected

			// Uncomment this to see what the random number generator is upto
			// Debug.Log(UnityEngine.Random.Range(MinIntensity, MaxIntensity)); 

			
			if (localTime >= NormalizedRate)
			{
				targetIntensity = GetTargetIntensity();
				localTime = 0;
			}

			lightToFlicker.intensity = Mathf.Lerp(targetIntensity, lightToFlicker.intensity, Smoothing);


			//Modifies the skybox to reflect changes too
			if (skybox != null)
			{
				foreach (Material skyboxes in skybox)
				{
					skyboxes.SetFloat("_Exposure", lightToFlicker.intensity / 2);
				}
			}
			// This is important for the script to manage its own countdown systems
			localTime += Time.deltaTime;
		}

		/// <summary>
		/// Seperate method "GetTargetIntensity()" allows us to increase clarity in the update function
		/// </summary>
		/// <returns>A random float within the acceptable range</returns>
		public float GetTargetIntensity()
		{
			float t;

			t = UnityEngine.Random.Range(MinIntensity, MaxIntensity);

			//This section handles managing the rate
			if (Rate < 0)
			{
				NormalizedRate = UnityEngine.Random.Range(1, 3);
			}
			else
			{
				NormalizedRate = Rate;
			}

			return t;
		}
	}
}


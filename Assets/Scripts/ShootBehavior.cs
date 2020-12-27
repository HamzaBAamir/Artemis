using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

namespace Artemis
{
	/// <summary>
	///  This module handles being shot by a ShootManager.cs  Monobehavior
	/// </summary>
	public class ShootBehavior : MonoBehaviour
	{
		/// <p> Public Variables </p>
		/// <list>
		///		<li> shootType (ShootType) </li>
		///		<li> sceneToLoad (Integer) </li>
		///		<li> Debugging (Boolean) </li>
		///		<li> SafeMode (Boolean) </li>
		/// </list>
		public enum ShootType { UILoad, UIQuit, Death, Damaged };

		[Tooltip("What kind of functionality do you want this module to perform?")]
		public ShootType shootType;

		[Tooltip("What scene do you want to load? Leave empty if unecessary, check scene manager to identify a number")]
		public int sceneToLoad;

		[Tooltip("Keep this on if debugging features are needed")]
		public bool Debugging = false;

		[Tooltip("Keep this off if you want to bypass scripts' assumptions and error-avoidance mechanisms to have full control over its behavior")]
		public bool SafeMode = true;

		/// <p> Private Variables </p>
		/// <list>
		///		<li> HandleShot (Integer) </li>
		/// </list>
		private bool HandleShot;

		public void Start()
		{
			// We dont want to try to load an invalid scene incase the designer made an error
			if (SafeMode)
			{
				if (sceneToLoad > SceneManager.sceneCount || sceneToLoad < 0)
				{
					Debug.LogWarning("ShootBehavior.cs attached to " + gameObject + "ran with an invalid value for sceneToLoad \n"
						+ "the issue has automatically been resolved but please attach a valid sceneToLoad to avoid unintended behavior");
					sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
				}
			}
			if (Debugging)
				Debug.Log("Start function ran in ShootBehavior.cs attached to " + gameObject + " sucessfully");
		}

		public void Update()
		{
			if (HandleShot)
			{
				Debug.LogWarning("ShootBehavior.cs can only handle shots of type ShootType.UILoad at the moment, please make sure that was your intention "
					+ "and add functionality if necessary");

				// Load Scene
				if (shootType == ShootType.UILoad)
				{
					if (Debugging)
						Debug.Log("We are currently performing a UI Load Shoot type at " + gameObject);

					//SendHapticFeedback();

					SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
				}


				// Reset shot checking
				HandleShot = false;
			}
		}

		/// <summary>
		/// This public method is going to be externally called for us to begin the shooting behaviors
		/// </summary>
		public void Shot()
		{
			if (Debugging)
				Debug.Log("ShootBehavior.Shot() was called on " + gameObject);

			HandleShot = true;

			if (Debugging)
				Debug.Log("The value of HandleShot in " + gameObject + " was set to " + HandleShot);
		}

		public void SendHapticFeedback()
		{

			SteamVR_Behaviour_Pose[] sp;
			SteamVR_Behaviour_Skeleton[] ss;
			ss = FindObjectsOfType<SteamVR_Behaviour_Skeleton>();
			foreach(SteamVR_Behaviour_Skeleton s in ss)
			{
					s.hapticSignal.Execute(0f, 0.1f, 160, 0.5f, s.inputSource);
			}
			sp = FindObjectsOfType<SteamVR_Behaviour_Pose>();
			foreach (SteamVR_Behaviour_Pose s in sp)
			{
				s.hapticSignal.Execute(0f, 0.1f, 160, 0.5f, s.inputSource);
			}
		}
	}
}
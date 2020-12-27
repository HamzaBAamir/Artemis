using UnityEngine;

namespace Artemis
{
	/// <p>
	/// Useful enumeration for handling dimensions with Unity
	/// </p>
	public enum Dimension{x, y, z, xb, yb, zb};

	/// <summary>
	///		Makes the UI track the camera and "follow" it at a slower pace.
	/// </summary>
	public class UITrackCamera : MonoBehaviour
	{

		/// <p> Public Variables </p>
		/// <list>
		///		<li> Transform (Camera) </li>
		///		<li> Track Rate (Floating Point) </li>
		///		<li> Align Dimension (Dimension) </li>
		///		<li> Debugging (Boolean) </li>
		///		<li> SafeMode (Boolean) </li>
		/// </list>

		[Tooltip("Which camera should this UI element track?")]
		public Transform cameraToTrack;

		[Tooltip("The higher this is, the faster the UI element will align with the camera")]
		[Range(0, 0.3f)]
		public float TrackRate;

		[Tooltip("Which dimension should the object use to 'look' at the camera")]
		public Dimension AlignDimension;

		[Tooltip("Keep this on if debugging features are needed")]
		public bool Debugging = false;

		[Tooltip("Keep this off if you want to bypass scripts' assumptions and error-avoidance mechanisms to have full control over its behavior")]
		public bool SafeMode = true;




		public void Start()
		{
			//We dont want the program to run with a NullReferenceException incase the designer forgot. </p>
			if (SafeMode)
			{
				if (cameraToTrack == null)
				{
					Debug.LogWarning("The UITrackCamera.cs MonoBehavior attached to " + gameObject + " ran with an empty cameraToTrack variable" +
						"Please attach a transform for it to work as expected every time. Don't worry, the issue has automatically been resolved");
					cameraToTrack = FindObjectOfType<Camera>().transform;
				}
			}
			if (Debugging)
				Debug.Log("Start function ran in UITrackCamera.cs attached to " + gameObject + " sucessfully");
		}

		public void Update()
		{

			transform.position = Vector3.Lerp(transform.position, cameraToTrack.position, TrackRate);

			AlignToCamera(AlignDimension);
		}

		/// <p>
		///  Seperate method AlignToCamera to avoid confusion in update
		/// </p>
		public void AlignToCamera(Dimension d)
		{
			if (Debugging)
			{
				Debug.Log("UITrackCamera.AlignToCamera was called by " + gameObject + " with parameter " + d);
			}
			if (d == Dimension.x)
			{
				transform.LookAt(cameraToTrack.parent, Vector3.right);
			}
			if (d == Dimension.y)
			{
				transform.LookAt(cameraToTrack.parent, Vector3.up);
			}
			if (d == Dimension.z)
			{
				transform.LookAt(cameraToTrack.parent, Vector3.forward);
			}
			if (d == Dimension.xb)
			{
				transform.LookAt(cameraToTrack.parent, Vector3.left);
			}
			if (d == Dimension.yb)
			{
				transform.LookAt(cameraToTrack.parent, Vector3.down);
			}
			if (d == Dimension.zb)
			{
				transform.LookAt(cameraToTrack.parent, Vector3.back);
			}
		}
	}
}
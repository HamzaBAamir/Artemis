using UnityEngine;

public class PlayerMove : MonoBehaviour
{

	public Rigidbody VRRig;
	public Transform RightController;
	public Transform LeftController;

	private bool ApplyForceR;
	private bool ApplyForceL;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (ApplyForceR)
		{
			VRRig.AddForceAtPosition(Vector3.Scale(RightController.forward, new Vector3(0.1f, 0.1f, 0.1f)), RightController.position, ForceMode.VelocityChange);
			Debug.Log(Vector3.Scale(RightController.forward, new Vector3(0.1f, 0.1f, 0.1f)));
			ApplyForceR = false;
		}
		if (ApplyForceL)
		{
			VRRig.AddForceAtPosition(Vector3.Scale(LeftController.forward, new Vector3(0.1f, 0.1f, 0.1f)), LeftController.position, ForceMode.VelocityChange);
			Debug.Log(Vector3.Scale(LeftController.forward, new Vector3(0.1f, 0.1f, 0.1f)));
			ApplyForceL = false;
		}
	}

	public void MovePlayerR()
	{
		ApplyForceR = true;
	}
	public void MovePlayerL()
	{
		ApplyForceR = true;
	}
}

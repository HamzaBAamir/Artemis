using UnityEngine;

public class PlayerCreateEMP : MonoBehaviour
{
	public GameObject EMP;

	private bool HandleSpawn;
	private bool HandleRelease;
	private bool Spawned;
	private GameObject EMPInstance;
    // Start is called before the first frame update
    void Start()
    {
		HandleRelease = false;
		HandleSpawn = false;
		Spawned = false;
	}

    // Update is called once per frame
    void Update()
    {
        if(HandleSpawn & !Spawned)
		{
			EMPInstance = Instantiate(EMP, transform.position, transform.rotation);
			if(EMPInstance.GetComponent<FixedJoint>() != null)
			EMPInstance.GetComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
			Spawned = true;
		}
		if(HandleRelease)
		{
			Debug.Log(EMPInstance);
			if (EMPInstance.GetComponent<FixedJoint>() != null)
				EMPInstance.GetComponent<FixedJoint>().connectedBody = null;
			if (EMPInstance.GetComponent<Rigidbody>() != null)
				EMPInstance.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward, transform.position, ForceMode.Impulse);
			HandleRelease = false;
			HandleSpawn = false;
			Spawned = false;
		}
    }

	public void Spawn()
	{
		HandleSpawn = true;
	}

	public void Release()
	{
		HandleRelease = true;
	}
}

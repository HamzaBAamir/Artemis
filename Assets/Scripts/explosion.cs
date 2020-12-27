using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
	private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
		rb = gameObject.GetComponent<Rigidbody>();
		
			rb.AddExplosionForce(40, new Vector3(transform.localPosition.x , transform.localPosition.y - Random.Range(1f, 7f), transform.localPosition.z - Random.Range(0.5f, 4f)), 20);
    }

}

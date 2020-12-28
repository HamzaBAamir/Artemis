using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{
	public GameObject EMP;
	public float threshold = 5f;
	private EnemyManager[] Enemy;

    // Update is called once per frame
    void Update()
    {
		Enemy = FindObjectsOfType<EnemyManager>();
		foreach (EnemyManager enemy in Enemy)
		{
			if(Vector3.Distance(EMP.transform.position, enemy.transform.position) < threshold)
			{
				enemy.Die();
			}
		}
    }
}

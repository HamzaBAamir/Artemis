using UnityEngine;
using UnityEngine.SceneManagement;
using Artemis;

public class EnemyManager : MonoBehaviour
{
	public GameObject Effect;
	public int DeathType;
	public Material DeathMat;

	private bool Death = false;
	private bool Died = false;
	private MeshRenderer currentMat;

	public void Update()
	{
		if (Death && DeathType == 1 && !Died)
		{
			Instantiate(Effect, transform.position, transform.rotation);
			if (FindObjectOfType<DataManager>() != null)
			{
				FindObjectOfType<DataManager>().AddScore(10);
			}
			//gameObject.SetActive(false);

			currentMat = gameObject.GetComponent<MeshRenderer>();

			currentMat.material = DeathMat;

			currentMat = gameObject.GetComponentInChildren<MeshRenderer>();

			currentMat.material = DeathMat;

			Died = true;
		}
		if(Death && DeathType == 2 && !Died)
		{
			SceneManager.LoadScene(2, LoadSceneMode.Single);
			Died = true;
		}
	}

	public void Die()
	{
		Death = true;
	}
}

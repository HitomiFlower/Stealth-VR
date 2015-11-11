using UnityEngine;

public class LaserSwitchDeactivation : MonoBehaviour
{
	public GameObject laser;
	public Material unlockedMat;

	private GameObject player;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player)
		{
			if (Input.GetButton("Switch"))
			{
				LaserDeactivation();
			}
		}
	}

	private void LaserDeactivation()
	{
		laser.SetActive(false);

		Renderer screen = transform.Find("prop_switchUnit_screen").GetComponent<Renderer>();
		screen.material = unlockedMat;
		GetComponent<AudioSource>().Play();
	}
}
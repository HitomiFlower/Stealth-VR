using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour 
{
	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameConstroller).GetComponent<LastPlayerSighting> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if(GetComponent<Renderer>().enabled)
		{
			if(other.gameObject == player)
			{
				lastPlayerSighting.position = player.transform.position;
			}
		}
	}
}

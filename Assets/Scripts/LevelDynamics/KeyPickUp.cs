using UnityEngine;
using System.Collections;

public class KeyPickUp : MonoBehaviour 
{
	public AudioClip grabClip;

	private GameObject player;
	private PlayerInventory playerInventory;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory = player.GetComponent<PlayerInventory> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == player)
		{
			AudioSource.PlayClipAtPoint(grabClip, transform.position);
			playerInventory.hasKey = true;
			Destroy (gameObject);
		}
	}
}

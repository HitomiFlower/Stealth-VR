using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
	public AudioClip grabClip;

	private GameObject player;
	private PlayerInventory playerInventory;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
		playerInventory = player.GetComponent<PlayerInventory>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			AudioSource.PlayClipAtPoint(grabClip, transform.position);
			playerInventory.hasKey = true;
			Destroy(gameObject);
		}
	}
}
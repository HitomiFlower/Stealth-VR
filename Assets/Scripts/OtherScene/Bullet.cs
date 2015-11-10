using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float life = 2.0f;

	// Use this for initialization
	private void Start()
	{
		Destroy(gameObject, life);
	}
}
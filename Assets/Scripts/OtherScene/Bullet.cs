using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float life = 2.0f;
	// Use this for initialization
	void Start ()
	{
		Destroy (gameObject, life);
	}

}

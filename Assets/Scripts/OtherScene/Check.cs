using UnityEngine;
using System.Collections;

public class Check : MonoBehaviour {
	

	private float previousX;
	private bool isMoved;

	void Star()
	{
		previousX = transform.position.x;
		isMoved = false;
	}
	// Update is called once per frame
	void Update () 
	{
		if(Mathf.Abs(transform.position.x - previousX) > 0.3 && isMoved == false )
		{
			++(GameObject.Find("Main Camera").GetComponent<Shoot>().KillCount);
			isMoved = true;
			Destroy (gameObject,3.0f);
		}
	}
	
}

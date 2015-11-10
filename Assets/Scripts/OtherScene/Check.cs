using UnityEngine;

public class Check : MonoBehaviour
{
	private float previousX;
	private bool isMoved;

	private void Star()
	{
		previousX = transform.position.x;
		isMoved = false;
	}

	// Update is called once per frame
	private void Update()
	{
		if (Mathf.Abs(transform.position.x - previousX) > 0.3 && isMoved == false)
		{
			++(GameObject.Find("Main Camera").GetComponent<Shoot>().KillCount);
			isMoved = true;
			Destroy(gameObject, 3.0f);
		}
	}
}
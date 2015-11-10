using UnityEngine;

public class Menu : MonoBehaviour
{
	// Use this for initialization
	private void Start()
	{
	}

	// Update is called once per frame
	private void Update()
	{
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(220, 300, 60, 30), "Exit"))
		{
			Application.Quit();
		}
		if (GUI.Button(new Rect(350, 300, 60, 30), "Start"))
		{
			Application.LoadLevel("Shoot");
		}
	}
}
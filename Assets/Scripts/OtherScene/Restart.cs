using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if(GUI.Button (new Rect(250,220,60,30), "Exit"))
		{
			Application.Quit();
		}
		if(GUI.Button (new Rect(340,220,60,30), "Restart"))
		{
			Application.LoadLevel("Shoot");
		}

		if (GUI.Button (new Rect (10, 160, 100, 50), "Play"))
		{
			GetComponent<AudioSource>().Play ();
		}
		if(GUI.Button (new Rect(10,220,100,50), "Pause"))
		{
			GetComponent<AudioSource>().Pause();
		}
		if(GUI.Button (new Rect(10, 280, 100, 50), "Stop"))
		{
			GetComponent<AudioSource>().Stop ();
		}
	}
}

﻿using UnityEngine;

public class LaserBlinking : MonoBehaviour
{
	public float onTime;
	public float offTime;

	private float timer;

	private void Update()
	{
		timer += Time.deltaTime;

		if (GetComponent<Renderer>().enabled && timer >= onTime)
		{
			SwitchBeam();
		}

		if (!GetComponent<Renderer>().enabled && timer >= offTime)
		{
			SwitchBeam();
		}
	}

	private void SwitchBeam()
	{
		timer = 0f;

		GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
		GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
	}
}
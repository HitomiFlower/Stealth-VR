using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour 
{
	public float timeToDoorsClose = 2f;
	public float timeToLiftStart = 3f;
	public float timeToEndLevel = 6f;
	public float liftSpeed = 3f;

	private GameObject player;
	private Animator playerAnim;
	private HashIDs hash;
	private CameraMovement cameraMovement;
	private SceneFadeInOut sceneFadeInOut;
	private LiftDoorTracking liftDoorTracking;
	private bool playerInLift;
	private float timer;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerAnim = player.GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameConstroller).GetComponent<HashIDs> ();
		cameraMovement = Camera.main.gameObject.GetComponent<CameraMovement> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<SceneFadeInOut> ();
		liftDoorTracking = GetComponent<LiftDoorTracking> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == player)
		{
			playerInLift = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == player)
		{
			playerInLift = false;
			timer = 0f;
		}
	}

	void Update()
	{
		if(playerInLift)
		{
			ListActivation();
		}

		if(timer < timeToDoorsClose)
		{
			liftDoorTracking.DoorFollowing();
		}
		else
		{
			liftDoorTracking.DoorClose();
		}
	}

	void ListActivation()
	{
		timer += Time.deltaTime;
		
		if(timer >= timeToLiftStart)
		{
			playerAnim.SetFloat(hash.speedFloat, 0f);
			player.transform.parent = transform;
			cameraMovement.enabled = false;
			
			transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);
			
			if(!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
			
			if(timer >= timeToEndLevel)
			{
				sceneFadeInOut.EndScene();
			}
		}
	}
}

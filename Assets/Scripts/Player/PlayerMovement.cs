using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public AudioClip shoutingClip;
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;

	private Animator anim;
	private HashIDs hash;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameConstroller).GetComponent<HashIDs> ();

		anim.SetLayerWeight (1, 1f);
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		bool sneak = Input.GetButton ("Sneak");

		MovementManagement (h, v, sneak);
	}

	void Update()
	{
		bool shout = Input.GetButton ("Attract");
		anim.SetBool (hash.shoutingBool, shout);
		AudioManagement (shout);
	}

	void MovementManagement(float horizontal, float vertical, bool sneaking)
	{
		anim.SetBool (hash.sneakingBool, sneaking);

		if(horizontal != 0f || vertical != 0f)
		{
			Rotating (horizontal, vertical);
			anim.SetFloat(hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
		}
		else
		{
			anim.SetFloat (hash.speedFloat, 0f);
		}
	}

	void Rotating(float horizontal, float vertical)
	{
		Vector3 targetDirection = new Vector3 (horizontal, 0f, vertical);
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
		GetComponent<Rigidbody>().MoveRotation (newRotation);
	}

	void AudioManagement(bool shout)
	{
		if(anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.locomotionState)
		{
			if(!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play ();
			}
		}
		else
		{
			GetComponent<AudioSource>().Stop ();
		}

		if(shout)
		{
			AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
		}
	}
}

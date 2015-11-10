using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour 
{
	public float deadZone = 5f;

	private Transform player;
	private Animator anim;
	private EnemySight enemySight;
	private NavMeshAgent nav;
	private HashIDs hash;
	private AnimatorSetup animSetup;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player).transform;
		anim = GetComponent<Animator> ();
		enemySight = GetComponent<EnemySight> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameConstroller).GetComponent<HashIDs> ();
		nav = GetComponent<NavMeshAgent> ();

		nav.updateRotation = false;
		animSetup = new AnimatorSetup (anim, hash);

		anim.SetLayerWeight (1, 1f);
		anim.SetLayerWeight (2, 1f);

		deadZone *= Mathf.Deg2Rad;
	}

	void Update()
	{
		NavAnimSetup ();
	}

	void OnAnimatorMove()
	{
		nav.velocity = anim.deltaPosition / Time.deltaTime;
		transform.rotation = anim.rootRotation;
	}

	void NavAnimSetup()
	{
		float speed;
		float angle;

		if(enemySight.playerInSight)
		{
			speed = 0f;

			angle = FindAngle (transform.position, player.position - transform.position, transform.up);
		}
		else
		{
			speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
			angle = FindAngle (transform.forward, nav.desiredVelocity, transform.up);

			if(Mathf.Abs (angle) < deadZone)
			{
				transform.LookAt(transform.position + nav.desiredVelocity);
				angle = 0f;
			}
		}

		animSetup.Setup (speed, angle);
	}


	float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
		if(toVector == Vector3.zero)
			return 0f;

		float angle = Vector3.Angle (fromVector, toVector);
		Vector3 normal = Vector3.Cross (fromVector, toVector);
		angle *= Mathf.Sign (Vector3.Dot (normal, upVector));
		angle *= Mathf.Deg2Rad;

		return angle;
	}
}

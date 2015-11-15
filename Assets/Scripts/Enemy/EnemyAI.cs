using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public float patrolSpeed = 2f;
	public float chaseSpeed = 5f;
	public float chaseWaitTime = 5f;
	public float patrolWaitTime = 1f;
	public Transform[] patrolWayPoints;

	private EnemySight enemySight;
	private NavMeshAgent nav;
	private Transform player;
	private PlayerHealth playerHealth;
	private LastPlayerSighting lastPlayerSighting;

	private float patrolTimer;
	private float chaseTimer;
	private int wayPointIndex;

	private enum StatusCode {Patrol, Chasing, Shoot}
	StatusCode statusCode = StatusCode.Patrol;

	private void Awake()
	{
		enemySight = GetComponent<EnemySight>();
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameConstroller).GetComponent<LastPlayerSighting>();
	}

	private void Update()
	{
		if (enemySight.playerInSight && playerHealth.health > 0f)
		{
			Shooting();
		}
		else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f)
		{
			Chasing();
		}
		else
		{
			Patrolling();
		}
	}

	private void Shooting()
	{
		if (statusCode != StatusCode.Shoot)
		{
			statusCode = StatusCode.Shoot;
		}
		//Unity 4が新しいパスを設定すれば行けるけど
		//Unity 5が元のパスをResetするかResumeするをしないといけない
		nav.Stop();
	}

	private void Chasing()
	{
		if (statusCode != StatusCode.Chasing)
		{
			statusCode = StatusCode.Chasing;
			nav.Resume();
		}
		Vector3 sightingPosDelta = enemySight.personalLastSighting - transform.position;
		if (sightingPosDelta.sqrMagnitude > 4f)
		{
			nav.destination = enemySight.personalLastSighting;
		}

		nav.speed = chaseSpeed;

		if (nav.remainingDistance < nav.stoppingDistance)
		{
			chaseTimer += Time.deltaTime;

			if (chaseTimer >= chaseWaitTime)
			{
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				chaseTimer = 0f;
			}
		}
		else
		{
			chaseTimer = 0f;
		}
	}

	private void Patrolling()
	{
		if (statusCode != StatusCode.Patrol)
		{
			statusCode = StatusCode.Patrol;
			nav.ResetPath();
		}
		nav.speed = patrolSpeed;

		if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
		{
			patrolTimer += Time.deltaTime;

			if (patrolTimer > patrolWaitTime)
			{
				if (wayPointIndex == patrolWayPoints.Length - 1)
				{
					wayPointIndex = 0;
				}
				else
				{
					wayPointIndex++;
				}

				patrolTimer = 0f;
			}
		}
		else
		{
			patrolTimer = 0f;
		}

		nav.destination = patrolWayPoints[wayPointIndex].position;
	}
}
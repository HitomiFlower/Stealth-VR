using UnityEngine;

public class DoneWayPointGizmo : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position, "wayPoint.png", true);
	}
}
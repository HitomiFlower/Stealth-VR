using UnityEngine;

public class guiTextSet : MonoBehaviour
{
	// Use this for initialization
	private void Start()
	{
		GetComponent<GUIText>().text = "\nArrow Keys: Move\nLeft Shift: Sneak\nZ: Use Switch\nX: Attract Attention";
	}
}
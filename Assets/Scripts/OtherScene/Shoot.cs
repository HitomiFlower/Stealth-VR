using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public float speed = 5f;
	public int xSpeed = 60;
	public int ySpeed = 60;
	public GameObject bullet;
	
	private int bulletCount = 0;
	public int KillCount { get; set; }
	// Use this for initialization
	void Start () 
	{
		KillCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * speed;
		float y = Input.GetAxis ("Jump") * Time.deltaTime * speed;
		transform.Translate (x, y, z);

		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Rotate(0, -ySpeed*Time.deltaTime,0,Space.Self);
		}
		if(Input.GetKey (KeyCode.D))
		{
			transform.Rotate (0,ySpeed*Time.deltaTime, 0, Space.Self);
		}
		transform.Rotate (Input.GetAxis ("Mouse ScrollWheel") * xSpeed * Time.deltaTime, 0, 0, Space.Self);


		GameObject obj;

		GUIText text = GameObject.FindObjectOfType(typeof(GUIText)) as GUIText;
		if(Input.GetButtonDown("Fire1"))
		{
			obj = (GameObject)Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
			Vector3 fwd = transform.TransformDirection (Vector3.forward);

			obj.GetComponent<Rigidbody>().AddForce (fwd * 2800);
			bulletCount++;
		}
		text.text = string.Format("Bullet:{0} Score:{1}", bulletCount, KillCount);
		if(KillCount > 20)
		{
			text.text = "Mission Complete";
			text.fontSize = 24;
			text.transform.position = new Vector3(0.36f, 0.7f);
			gameObject.GetComponent<Shoot>().enabled = false;
			GameObject.Find("Ground").GetComponent<Restart>().enabled = true;
		}
	}
}

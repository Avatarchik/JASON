using UnityEngine;
using System.Collections;

public class Rolling : MonoBehaviour {
	public bool flipped;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = Vector3.zero;
		
		dir.x = -Input.acceleration.y* 0.5f;
		dir.y = -Input.acceleration.z* 3f;
		//transform.position.x += dir.x;
		if(flipped){
			transform.position = new Vector3(dir.y,-dir.x,transform.position.z);
		}else{
		transform.position = new Vector3(dir.y,-dir.x,transform.position.z);
		}
	}
}

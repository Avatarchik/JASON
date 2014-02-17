using UnityEngine;
using System.Collections;

public class PlayerCamera:MonoBehaviour {
	public Transform target;
	public float distance = 3.0f;
	public float distancex = 3.0f;
	public float height = 3.0f;
	public float damping = 5.0f;
	public Camera cam;
	private bool targetFound = false;
	private Vector3 initialPosition;
	public int camDistance;
	public float camDamping;
	void Start(){
		initialPosition = cam.transform.position;
	}
	void Update () 
	{
		if(target != null){
			targetFound = true;
		}else{
			targetFound = false;
		}
		if(targetFound){
			Vector3 wantedPosition;
			wantedPosition = target.transform.position + new Vector3(distancex, height, distance);
			transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,60 + camDistance,Time.deltaTime * camDamping);
		}
	}
}
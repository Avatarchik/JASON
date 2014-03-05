using UnityEngine;
using System.Collections;

public class PlayerCamera:MonoBehaviour {
	[SerializeField] private Transform target;

	[SerializeField] private float distance;
	[SerializeField] private float distanceX;
	[SerializeField] private float height;
	[SerializeField] private float damping;
	[SerializeField] private float cameraDamping;
	[SerializeField] private float cameraBuffer;

	[SerializeField] private Camera playerCamera;

	[SerializeField] private int cameraDistance;

	private bool targetFound;

	void Update() {
		if(target != null) {
			Vector3 wantedPosition = target.transform.position + new Vector3(distanceX + cameraBuffer, height, distance);
			if(transform.position.x < (target.transform.position.x + distanceX + cameraBuffer)){
				cameraBuffer = 2;
			}else if(transform.position.x > (target.transform.position.x + distanceX + cameraBuffer)){
				cameraBuffer = -2;
			}
			if((playerCamera.fieldOfView == 60 + cameraDistance)){
				cameraBuffer = 0;
			}
				//Debug.Log("position: " + transform.position.x + "    wanted: " + (target.transform.position.x + distanceX));
			transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);
			playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60 + cameraDistance, Time.deltaTime * cameraDamping);
		}
	}
	public IEnumerator CameraShake(){
		Vector3 origin = transform.position;
		Handheld.Vibrate();
		transform.Translate(new Vector3(Random.Range(-2,3),0,0));
		yield return new WaitForSeconds(0.05f);
		transform.position = origin;
		transform.Translate(new Vector3(Random.Range(-2,3),0,0));
		yield return new WaitForSeconds(0.05f);
	    transform.position = origin;
	}
	public int CameraDistance { set { cameraDistance = value; } }
}
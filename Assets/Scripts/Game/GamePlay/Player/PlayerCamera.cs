using UnityEngine;
using System.Collections;

public class PlayerCamera:MonoBehaviour {
	[SerializeField] private Transform target;

	[SerializeField] private Vector3 targetPosition;
	
	[SerializeField] private float cameraDamping;
	[SerializeField] private float cameraBuffer;
	[SerializeField] private float fovDamping;

	[SerializeField] private int fovDistance;

	private bool targetFound;

	void Update() {
		if(target != null) {
			Vector3 wantedPosition = target.transform.position + new Vector3(targetPosition.x + cameraBuffer, targetPosition.y, targetPosition.z);

			if((Camera.main.fieldOfView == 60 + fovDistance))
				cameraBuffer = 0;

			transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * fovDamping);
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60 + fovDistance, Time.deltaTime * cameraDamping);
		}
	}

	/** Shake the camera */
	public IEnumerator CameraShake(){
		Vector3 origin = transform.position;
		//Handheld.Vibrate();
		transform.Translate(new Vector3(Random.Range(-2,3),0,0));
		yield return new WaitForSeconds(0.05f);
		transform.position = origin;
		transform.Translate(new Vector3(Random.Range(-2,3),0,0));
		yield return new WaitForSeconds(0.05f);
	    transform.position = origin;
	}

	public Transform Target {
		set { target = value; }
		get { return target; }
	}

	public Vector3 TargetPosition {
		set { targetPosition = value; }
		get { return targetPosition; }
	}

	public int CameraDistance { set { fovDistance = value; } }
}
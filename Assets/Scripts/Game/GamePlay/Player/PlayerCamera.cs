using UnityEngine;
using System.Collections;

public class PlayerCamera:MonoBehaviour {
	[SerializeField] private Transform target;

	[SerializeField] private float distance;
	[SerializeField] private float distanceX;
	[SerializeField] private float height;
	[SerializeField] private float damping;
	[SerializeField] private float cameraDamping;

	[SerializeField] private Camera playerCamera;

	[SerializeField] private int cameraDistance;

	private bool targetFound;

	void Update() {
		if(target != null) {
			Vector3 wantedPosition = target.transform.position + new Vector3(distanceX, height, distance);

			transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);
			playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60 + cameraDistance, Time.deltaTime * cameraDamping);
		}
	}

	public int CameraDistance { set { cameraDistance = value; } }
}
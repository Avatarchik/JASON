using UnityEngine;
using System.Collections;
using System;

public class CameraManager:Singleton<CameraManager> {
	[SerializeField] private Texture2D cameraEventBorder;

	[SerializeField] private Transform target;

	[SerializeField] private Vector3 targetPosition;

	[SerializeField] private float fovDamping;
	[SerializeField] private float damping;
	[SerializeField] private float buffer;

	[SerializeField] private int fovDistance;
	[SerializeField] private int borderSize;

	private bool cameraEventActive;

	void Update() {
		if(target == null)
			return;

		Vector3 wantedPosition = target.transform.position + new Vector3(targetPosition.x + buffer, targetPosition.y, targetPosition.z);

		if(Camera.main.fieldOfView == 60 + fovDamping)
			buffer = 0;

		transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * fovDamping);
		Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60 + fovDistance, Time.deltaTime * damping);
	}

	void OnGUI() {
		if(!cameraEventActive)
			return;

		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height / borderSize), cameraEventBorder);
		GUI.DrawTexture(new Rect(0, Screen.height - (Screen.height / borderSize), Screen.width, Screen.height / borderSize), cameraEventBorder);
	}

	/** <summary>Handle a camera event</summary>
	 * <param name="eventTarget">The target the camera should focus on</param>
	 * <param name="duration">The duration of the event</param>
	 * <param name="callback">The callback event, must implement an <code>OnEvent</code> method</param> */
	public IEnumerator CameraEvent(Transform eventTarget, float duration, Action<string> callback) {
		if(cameraEventActive)
			yield break;

		PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		playerMovement.TargetPosition = playerMovement.transform.position;

		Transform oldTarget = target;
		target = eventTarget;
		cameraEventActive = true;

		yield return new WaitForSeconds(duration / 2);

		callback("");

		yield return new WaitForSeconds(duration / 2);

		target = oldTarget;
		cameraEventActive = false;
	}

	/** <returns>Whether or not the camera event is currently active</returns> */
	public bool CameraEventActive {
		get { return cameraEventActive; }
	}
}

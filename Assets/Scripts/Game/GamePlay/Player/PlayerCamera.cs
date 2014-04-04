using UnityEngine;
using System.Collections;

public class PlayerCamera:MonoBehaviour {
	[SerializeField] private Transform target;

	[SerializeField] private Vector3 targetPosition;
	
	[SerializeField] private float cameraDamping;
	[SerializeField] private float cameraBuffer;
	[SerializeField] private float fovDamping;

	[SerializeField] private int fovDistance;

	[SerializeField] private Texture2D cameraEventTexture;

	private bool targetFound;

	void Update() {
		if(target == null)
			return;

		Vector3 wantedPosition = target.transform.position + new Vector3(targetPosition.x + cameraBuffer, targetPosition.y, targetPosition.z);

		if((Camera.main.fieldOfView == 60 + fovDistance))
			cameraBuffer = 0;

		transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * fovDamping);

		Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60 + fovDistance, Time.deltaTime * cameraDamping);

	}

	/** Shake the camera */
	public IEnumerator CameraShake(){
		Vector3 origin = transform.position;
		
		if(Application.platform == RuntimePlatform.Android)
			Handheld.Vibrate();

		transform.Translate(new Vector3(Random.Range(-2,3),0,0));
		yield return new WaitForSeconds(0.05f);
		transform.position = origin;
		transform.Translate(new Vector3(Random.Range(-2,3),0,0));
		yield return new WaitForSeconds(0.05f);
	    transform.position = origin;
	}

	/** Set and/or get the target of the camera */
	public Transform Target {
		set { target = value; }
		get { return target; }
	}

	/** Set and/or get the target position of the camera */
	public Vector3 TargetPosition {
		set { targetPosition = value; }
		get { return targetPosition; }
	}

	/** Set the distance of the camera */
	public int CameraDistance {
		set { fovDistance = value; }
	}

	/** Get the camera event texture */
	public Texture2D CameraEventTexture {
		get { return cameraEventTexture; }
	}
}
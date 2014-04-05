using UnityEngine;
using System.Collections;

public class MultipleHandler:MonoBehaviour {
	[SerializeField] private Old_Trigger[] triggers;

	private PlayerCamera playerCamera;

	private Transform oldTarget;
	private Transform cameraEventTarget;
	
	private int numEnabled;

	private bool puzzleDone;
	
	void Start() {
		playerCamera = Camera.main.gameObject.GetComponent<PlayerCamera>();
		cameraEventTarget = transform.FindChild("Camera Focus");
	}

	void Update() {
		if(puzzleDone)
			return;

		numEnabled = 0;

		foreach(Old_Trigger trigger in triggers)
			if(trigger.IsActive)
				numEnabled++;

		if(numEnabled >= triggers.Length) {
			puzzleDone = true;
			StartCoroutine(playerCamera.CameraEvent(cameraEventTarget, new Door[] {GetComponent<Door>()}));
		}
	}
}

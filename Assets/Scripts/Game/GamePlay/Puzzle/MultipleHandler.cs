using UnityEngine;
using System.Collections;

public class MultipleHandler:MonoBehaviour {
	[SerializeField] private Trigger[] triggers;

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

		foreach(Trigger trigger in triggers)
			if(trigger.IsActive)
				numEnabled++;

		if(!puzzleDone && numEnabled >= triggers.Length) {
			puzzleDone = true;
			StartCoroutine(playerCamera.CameraEvent(cameraEventTarget, GetComponents<Door>()));
		}
	}
}

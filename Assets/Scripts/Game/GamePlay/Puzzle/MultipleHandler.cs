using UnityEngine;
using System.Collections;

public class MultipleHandler:MonoBehaviour {
	[SerializeField] private Trigger[] triggers;

	private PlayerCamera playerCamera;

	private Transform oldTarget;
	private Transform eventTarget;
	
	private int numEnabled;

	private bool puzzleDone;
	
	void Start() {
		playerCamera = Camera.main.gameObject.GetComponent<PlayerCamera>();
		eventTarget = transform.FindChild("Camera Focus");
	}

	void Update() {
		if(puzzleDone)
			return;

		numEnabled = 0;

		foreach(Trigger trigger in triggers)
			if(trigger.IsActive)
				numEnabled++;

		if(numEnabled >= triggers.Length) {
			puzzleDone = true;
			StartCoroutine(CameraEvent(Door.DoorState.Open));
		}
	}

	private IEnumerator CameraEvent(Door.DoorState state) {
		oldTarget = playerCamera.Target;
		playerCamera.Target = eventTarget;

		yield return new WaitForSeconds(1.5f);

		if(state == Door.DoorState.Open) {
			GetComponent<Door>().Open();
		} else if(state == Door.DoorState.Closed) {
			GetComponent<Door>().Close();
		}

		yield return new WaitForSeconds(1.5f);

		playerCamera.Target = oldTarget;
	}
}

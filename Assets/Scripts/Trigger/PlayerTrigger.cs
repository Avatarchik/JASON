using UnityEngine;
using System.Collections;

public class PlayerTrigger:Trigger, ITrigger {
	[SerializeField] private Transform pressArea;

	[SerializeField] private DoorTrigger[] doors;

	void OnTriggerEnter(Collider col) {
		if(!col.CompareTag("Player") || (triggerType == TriggerType.Once && triggeredOnce))
			return;

		StartCoroutine(CameraManager.Instance.CameraEvent(cameraEventTarget, 3, CameraEventCallback));

		pressArea.Translate(new Vector3(0, 0, -0.05f));

		triggeredOnce = true;
		triggered = true;
	}

	void OnTriggerExit(Collider col) {
		if(!triggered && !col.CompareTag("Player"))
			return;

		pressArea.Translate(new Vector3(0, 0, 0.05f));

		triggered = false;
	}

	private void CameraEventCallback(string message) {
		foreach(DoorTrigger door in doors)
			door.Open();
	}

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.Player;
	}
}

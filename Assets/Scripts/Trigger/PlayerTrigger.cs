using UnityEngine;
using System.Collections;

public class PlayerTrigger:Trigger, ITrigger {
	[SerializeField] private Transform pressArea;

	[SerializeField] private DoorTrigger[] doors;

	void OnTriggerEnter(Collider col) {
		if(!col.CompareTag("Player"))
			return;

		triggered = true;
		pressArea.Translate(new Vector3(0, 0, -0.05f));

		if(triggerType == TriggerType.Once && triggeredOnce)
			return;

		StartCoroutine(CameraManager.Instance.CameraEvent(cameraEventTarget, 0.25f, 3, delegate(string s) {
			foreach(DoorTrigger door in doors)
				door.Open();
		}));

		triggeredOnce = true;
		
	}

	void OnTriggerExit(Collider col) {
		if(!triggered && !col.CompareTag("Player"))
			return;

		pressArea.Translate(new Vector3(0, 0, 0.05f));

		triggered = false;
	}

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.Player;
	}
}

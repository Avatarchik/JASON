using UnityEngine;
using System.Collections;

public class ArrowTrigger:Trigger, ITrigger {
	[SerializeField] private DoorTrigger[] doors;

	void OnCollisionEnter(Collision col) {
		if(!col.collider.CompareTag("Arrow") || (triggerType == TriggerType.Once && triggeredOnce))
			return;

		StartCoroutine(CameraManager.Instance.CameraEvent(cameraEventTarget, 0.25f, 3, delegate(string s) {
			foreach(DoorTrigger door in doors)
				door.Open();
		}));

		triggeredOnce = true;
		triggered = true;
	}

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.Arrow;
	}
}

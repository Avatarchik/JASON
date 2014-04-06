using UnityEngine;
using System.Collections;

public class ArrowTrigger:Trigger, ITrigger {
	[SerializeField] private DoorTrigger[] doors;

	void OnCollisionEnter(Collision col) {
		if(!col.collider.CompareTag("Arrow") || (triggerType == TriggerType.Once && triggeredOnce))
			return;

		foreach(DoorTrigger door in doors)
			door.Open();

		triggeredOnce = true;
		triggered = true;
	}

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.Arrow;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerTrigger:Trigger, ITrigger {
	[SerializeField] private Transform pressArea;

	void OnTriggerEnter(Collider col) {
		if(!col.CompareTag("Player") || (triggerType == TriggerType.Once && triggeredOnce))
			return;

		// TODO Open connected doors

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

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.Player;
	}
}

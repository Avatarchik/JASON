using UnityEngine;
using System.Collections;

public class TimedArrowTriggerSlave:Trigger, ITrigger {
	[SerializeField] private float duration;
	
	void OnCollisionEnter(Collision col) {
		if(!col.collider.CompareTag("Arrow") || (triggerType == TriggerType.Once && triggeredOnce))
			return;

		StartCoroutine("Trigger");
	}

	/** <summary>Activate the trigger, and deactivate it after the specified delay</summary> */
	private IEnumerator Trigger() {
		triggeredOnce = true;
		triggered = true;

		yield return new WaitForSeconds(duration);

		triggered = false;
	}

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.Arrow;
	}
}

using UnityEngine;
using System.Collections;

public class BrazierTrigger:Trigger, ITrigger {
	[SerializeField] private Transform brazierPosition;

	void OnTriggerEnter(Collider col) {
		if(!col.CompareTag("Interactable Object") || (triggerType == TriggerType.Once && triggeredOnce) || (col.GetComponent(typeof(IInteractable)) as IInteractable).GetInteractableType() != InteractableType.Brazier)
			return;
		
		col.transform.position = brazierPosition.position;
		col.transform.rotation = brazierPosition.rotation;

		col.GetComponent<Brazier>().Lock(true);

		// TODO Open connected doors

		triggeredOnce = true;
		triggered = true;
	}

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.Brazier;
	}
}

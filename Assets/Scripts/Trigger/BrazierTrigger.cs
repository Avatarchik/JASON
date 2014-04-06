using UnityEngine;
using System.Collections;

public class BrazierTrigger:Trigger, ITrigger {
	[SerializeField] private Transform brazierPosition;

	[SerializeField] private DoorTrigger[] doors;

	void OnTriggerEnter(Collider col) {
		if(!col.CompareTag("Interactable Object") || (triggerType == TriggerType.Once && triggeredOnce) || (col.GetComponent(typeof(IInteractable)) as IInteractable).GetInteractableType() != InteractableType.Brazier)
			return;
		
		col.transform.position = brazierPosition.position;
		col.transform.rotation = brazierPosition.rotation;

		col.GetComponent<Brazier>().Lock(true);

		foreach(DoorTrigger door in doors)
			door.Open();

		triggeredOnce = true;
		triggered = true;
	}

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.Brazier;
	}
}

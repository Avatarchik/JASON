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

		StartCoroutine(CameraManager.Instance.CameraEvent(cameraEventTarget, 0.25f, 3, delegate(string s) {
			foreach(DoorTrigger door in doors)
				door.Open();
		}));

		triggeredOnce = true;
		triggered = true;
	}

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.Brazier;
	}
}

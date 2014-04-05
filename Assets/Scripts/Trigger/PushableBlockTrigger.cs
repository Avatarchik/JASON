using UnityEngine;
using System.Collections;

public class PushableBlockTrigger:Trigger, ITrigger {
	[SerializeField] private Transform pushableBlockPosition;
	[SerializeField] private Transform pressArea;

	void OnTriggerEnter(Collider col) {
		if(!col.CompareTag("Interactable Object") || (triggerType == TriggerType.Once && triggeredOnce) || (col.GetComponent(typeof(IInteractable)) as IInteractable).GetInteractableType() != InteractableType.PushableBlock)
			return;

		if(triggerType == TriggerType.Once) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Drop(false);

			col.transform.position = pushableBlockPosition.position;
			col.transform.rotation = pushableBlockPosition.rotation;

			col.GetComponent<PushableBlock>().Lock(true);
		}

		pressArea.Translate(new Vector3(0, 0, -0.1f));

		// TODO Open connected doors

		triggeredOnce = true;
		triggered = true;
	}

	void OnTriggerExit(Collider col) {
		if(!triggered || !col.CompareTag("Interactable Object") || (col.GetComponent(typeof(IInteractable)) as IInteractable).GetInteractableType() != InteractableType.PushableBlock)
			return;

		pressArea.Translate(new Vector3(0, 0, 0.1f));

		triggered = false;
	}

	public TriggerActivator GetTriggerActivator() {
		return TriggerActivator.PushableBlock;
	}
}

using UnityEngine;
using System.Collections;

public class PushableBlockTrigger:Trigger, ITrigger {
	[SerializeField] private Transform pushableBlockPosition;
	[SerializeField] private Transform pressArea;

	[SerializeField] private DoorTrigger[] doors;

	void OnTriggerEnter(Collider col) {
		if(!col.CompareTag("Interactable Object") || (col.GetComponent(typeof(IInteractable)) as IInteractable).GetInteractableType() != InteractableType.PushableBlock)
			return;

		triggered = true;
		pressArea.Translate(new Vector3(0, 0, -0.1f));

		if(triggerType == TriggerType.Once && triggeredOnce)
			return;

		if(triggerType == TriggerType.Once) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Drop(false);

			col.transform.position = pushableBlockPosition.position;
			col.transform.rotation = pushableBlockPosition.rotation;

			col.GetComponent<PushableBlock>().Lock(true);
		}

		StartCoroutine(CameraManager.Instance.CameraEvent(cameraEventTarget, 3, delegate(string s) {
			foreach(DoorTrigger door in doors)
				door.Open();
		}));

		triggeredOnce = true;
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

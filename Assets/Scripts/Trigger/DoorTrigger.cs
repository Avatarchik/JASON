using UnityEngine;
using System.Collections;

public class DoorTrigger:MonoBehaviour {
	public enum DoorType {
		Trigger,
		Key,
		BossKey
	}

	[SerializeField] private DoorType doorType;

	[SerializeField] private Renderer[]	doorLock;

	void OnCollisionEnter(Collision col) {
		if(!col.collider.CompareTag("Player") || col.collider.GetComponent<Player>().Interactable == null)
			return;
		
		IInteractable interactable = col.collider.GetComponent<Player>().Interactable;

		if(interactable.GetInteractableType() == InteractableType.Key) {
			Key key = interactable as Key;
			
			if((key.Type == Key.KeyType.Normal && doorType == DoorType.Key) || (key.Type == Key.KeyType.Boss && doorType == DoorType.BossKey))
				Open();
		}
	}

	/** <summary>Open the door</summary> */
	public void Open() {
		AudioManager.Instance.SetAudio(AudioManager.AudioFiles.DoorOpen, true);

		if(doorLock != null)
			foreach(Renderer renderer in doorLock)
				renderer.enabled = false;

		collider.enabled = false;

		animation.enabled = true;
		animation.Play();
	}

	/** <returns>The type of the door</returns> */
	public DoorType Type {
		get { return doorType; }
	}
}

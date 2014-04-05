using UnityEngine;

public class Key:MonoBehaviour, IInteractable {
	public enum KeyType {
		Normal,
		Boss
	}

	[SerializeField] private KeyType keyType;

	private Transform targetPosition;

	private bool locked;
	private bool thrown;
	
	void FixedUpdate() {
		if(locked)
			return;

		if(targetPosition != null) {
			transform.position = targetPosition.position;
			transform.rotation = targetPosition.rotation;
		} 
	
		if(thrown) {
			// TODO Handle thrown logic
		}
	}

	public void Pickup(Transform position) {
		this.targetPosition = position;
	}

	public void Drop() {
		targetPosition = null;
	}

	public void Throw() {
		thrown = true;

		targetPosition = null;
	}

	public void Lock(bool locked) {
		this.locked = locked;

		Drop();
	}
	
	public InteractableType GetInteractableType() {
		return InteractableType.Key;
	}
}

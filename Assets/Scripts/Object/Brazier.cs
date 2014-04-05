using UnityEngine;
using System.Collections;

public class Brazier:MonoBehaviour, IInteractable {
	public enum KeyType {
		Normal,
		Boss
	}

	[SerializeField]
	private KeyType keyType;

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

		collider.enabled = false;
	}

	public void Drop() {
		targetPosition = null;

		transform.position = new Vector3(transform.position.x, 0.05f, transform.position.z);
	}

	public void Throw() {
		thrown = true;
		collider.enabled = true;

		targetPosition = null;
	}

	public void Lock(bool locked) {
		this.locked = locked;

		Drop();
	}

	public InteractableType GetInteractableType() {
		return InteractableType.Brazier;
	}
}

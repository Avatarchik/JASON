using UnityEngine;
using System.Collections;

public class PushableBlock:MonoBehaviour, IInteractable {
	private Transform targetPosition;

	private bool locked;

	void FixedUpdate() {
		if(!rigidbody.isKinematic)
			rigidbody.velocity = Vector3.zero;

		if(locked)
			return;

		if(targetPosition != null && transform.position != targetPosition.position) {
			rigidbody.isKinematic = false;
			rigidbody.AddForce(-(transform.position - targetPosition.position) * (Time.deltaTime * 35000));
		} else {
			rigidbody.isKinematic = true;
		}
	}

	public void Pickup(Transform position) {
		this.targetPosition = position;
	}

	public void Drop() {
		targetPosition = null;
	}

	public void Throw(Vector3 forward, Vector3 up) {
		Drop();
	}

	public void Lock(bool locked) {
		this.locked = locked;

		Drop();
	}

	public InteractableType GetInteractableType() {
		return InteractableType.PushableBlock;
	}

	public bool IsLocked() {
		return locked;
	}
}

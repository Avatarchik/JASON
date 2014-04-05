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
		rigidbody.velocity = Vector3.zero;

		if(locked)
			return;

		if(targetPosition != null) {
			transform.position = targetPosition.position;
			transform.rotation = targetPosition.rotation;
		}

		if(thrown)
			rigidbody.AddForce(Vector3.forward * Time.deltaTime * 5000);
	}

	void OnCollisionEnter(Collision col) {
		if(col.collider.CompareTag("Player"))
			return;

		if(col.collider.CompareTag("Brazier Switch")) {
			// TODO Brazier switch
		} else {
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
			transform.rotation = new Quaternion(-0.7f, 0.0f, 0.0f, 0.7f);
		}

		thrown = false;
	}

	public void Pickup(Transform position) {
		this.targetPosition = position;

		collider.enabled = false;
	}

	public void Drop() {
		targetPosition = null;

		collider.enabled = true;
	}

	public void Throw() {
		Drop();

		thrown = true;
	}

	public void Lock(bool locked) {
		this.locked = locked;

		Drop();
	}

	public InteractableType GetInteractableType() {
		return InteractableType.Brazier;
	}
}

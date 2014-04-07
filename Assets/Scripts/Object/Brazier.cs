using UnityEngine;
using System.Collections;

public class Brazier:MonoBehaviour, IInteractable {
	public enum KeyType {
		Normal,
		Boss
	}

	[SerializeField] private KeyType keyType;

	[SerializeField] private float speed;

	private Transform targetPosition;

	private bool locked;
	private bool thrown;

	void FixedUpdate() {
		if(rigidbody.isKinematic)
			return;

		rigidbody.velocity = Vector3.zero;

		if(locked)
			return;

		if(targetPosition != null) {
			transform.position = targetPosition.position;
			transform.rotation = targetPosition.rotation;
		}

		if(thrown)
			rigidbody.AddForce(transform.forward * (Time.deltaTime * speed));
	}

	void OnCollisionEnter(Collision col) {
		if(col.collider.CompareTag("Player"))
			return;

		if(!col.collider.CompareTag("Trigger")) {
			if(col.collider.GetComponent(typeof(ITrigger)) as ITrigger != null && (col.collider.GetComponent(typeof(ITrigger)) as ITrigger).GetTriggerActivator() != TriggerActivator.Brazier) {
				transform.position = new Vector3(transform.position.x, 0, transform.position.z);
				transform.rotation = new Quaternion(-0.7f, 0.0f, 0.0f, 0.7f);
			}
		}

		thrown = false;
		rigidbody.isKinematic = true;
	}

	public void Pickup(Transform position) {
		this.targetPosition = position;

		collider.enabled = false;
		rigidbody.isKinematic = false;
	}

	public void Drop() {
		targetPosition = null;

		collider.enabled = true;
	}

	public void Throw(Vector3 forward) {
		Drop();

		transform.forward = forward;
		thrown = true;
	}

	public void Lock(bool locked) {
		this.locked = locked;

		if(rigidbody != null)
			rigidbody.isKinematic = true;

		Drop();
	}

	public InteractableType GetInteractableType() {
		return InteractableType.Brazier;
	}

	public bool IsLocked() {
		return locked;
	}
}

using UnityEngine;

public class Key:MonoBehaviour, IInteractable {
	public enum KeyType {
		Normal,
		Boss
	}

	[SerializeField] private KeyType keyType;

	private Transform targetPosition;
	
	void FixedUpdate() {
		if(targetPosition != null) {
			transform.position = targetPosition.position;
			transform.rotation = targetPosition.rotation;
		} 
	}

	public void Pickup(Transform position) {
		this.targetPosition = position;

		collider.enabled = false;
	}

	public void Drop() {
		targetPosition = null;

		transform.position = new Vector3(transform.position.x, 0.05f, transform.position.z);
		transform.rotation = new Quaternion(0.5f, -0.5f, -0.5f, -0.5f);
	}

	public void Throw() {
		Drop();
	}

	public void Lock(bool locked) { }
	
	public InteractableType GetInteractableType() {
		return InteractableType.Key;
	}

	public bool IsLocked() {
		return false;
	}

	/** <retrurns>The type of the key</returns> */
	public KeyType Type {
		get { return keyType; }
	}
}

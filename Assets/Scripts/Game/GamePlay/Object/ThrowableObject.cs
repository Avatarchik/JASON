using UnityEngine;
using System.Collections;

public class ThrowableObject:MonoBehaviour {
	public enum ObjectType {
		Normal,
		Key
	}

	[SerializeField] private ObjectType type;

	[SerializeField] private bool destroyable;

	private bool isThrown;
	private bool isPickedUp;

	void FixedUpdate() {
		if(isThrown){
			transform.Translate(Vector3.forward * 0.8f);
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
			collider.enabled = true;
		}
	}

	void OnCollisionEnter(Collision collision) {
		switch(collision.gameObject.tag) {
		case "Door":
			HandleDoorCollision(collision.gameObject.GetComponent<Door>());
			break;
		}
	}

	/** Attach the object to an object */
	public void AttachTo(Transform other) {
		transform.position = new Vector3(other.position.x,other.position.y + 3, other.position.z);
		transform.rotation = other.transform.rotation;
	}

	/** Handle a collision with a door */
	private void HandleDoorCollision(Door door) {
		if(type == ObjectType.Key && door.Type == Door.DoorType.Key) {
			door.Open();

			Destroy(gameObject);
		}

		isThrown = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	public bool Thrown {
		set { isThrown = value; }
		get { return isThrown; }
	}
}
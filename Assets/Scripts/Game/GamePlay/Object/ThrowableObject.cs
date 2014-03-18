using UnityEngine;
using System.Collections;

public class ThrowableObject:InteractableObject {
	public enum ObjectType {
		Normal,
		Key,
		BossKey
	}

	[SerializeField] private ObjectType type;

	[SerializeField] private bool destroyable;

	private bool isThrown;
	private bool isPickedUp;

	protected override void FixedUpdate() {
		if(isThrown) {
			transform.Translate(Vector3.forward * 0.8f);
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
			collider.enabled = true;
		}
	}

	void OnCollisionEnter(Collision collision) {
		rigidbody.constraints = RigidbodyConstraints.None;
		isThrown = false;

		switch(collision.gameObject.tag) {
		case "Door":
			HandleDoorCollision(collision.gameObject.GetComponent<Door>());
			break;
		}
	}

	/** Handle a collision with a door */
	private void HandleDoorCollision(Door door) {
		if(type == ObjectType.Key && door.Type == Door.DoorType.Key) {
			door.Open();

			Destroy(gameObject);
		} else if(type == ObjectType.BossKey && door.Type == Door.DoorType.BossDoor) {
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
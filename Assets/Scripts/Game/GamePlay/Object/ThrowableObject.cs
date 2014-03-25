using UnityEngine;
using System.Collections;

public class ThrowableObject:InteractableObject {
	public enum ObjectType {
		Normal,
		Key,
		BossKey,
		FireItem
	}

	[SerializeField] private ObjectType type;

	[SerializeField] private bool destroyable;

	private bool isThrown;
	private bool isPickedUp;

	private Vector3 respawnPosition;

	void Start() {
		respawnPosition = transform.position;
	}

	protected override void FixedUpdate() {
		if(transform.position.y <= -20){
			transform.position = respawnPosition;
			rigidbody.velocity = Vector3.zero;
		}

		if(isThrown) {
			rigidbody.AddForce(Vector3.forward);
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			collider.enabled = true;
		}
	}

	void OnCollisionEnter(Collision collision) {
		isThrown = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;

		switch(collision.gameObject.tag) {
		case "Door":
			HandleDoorCollision(collision.gameObject.GetComponent<Door>());
			break;
		}
	}

	/** Handle a collision with a door */
	public void HandleDoorCollision(Door door) {
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

	public ObjectType Type {
		set { type = value; }
		get { return type; }
	}

	public bool Destroyable {
		set { destroyable = value; }
		get { return destroyable; }
	}

	public bool Thrown {
		set { isThrown = value; }
		get { return isThrown; }
	}
}
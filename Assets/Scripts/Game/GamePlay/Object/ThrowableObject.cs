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

	public void Pickup() {
		rigidbody.constraints = RigidbodyConstraints.None;
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

	/** Handle collision with the Ffire boss */
	public void HandleBossCollision(MadOvenMain boss) {
		Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		Destroy(gameObject);
		player.PlayerCamera.TargetPosition = new Vector3(-10, 15, player.PlayerCamera.TargetPosition.z);
		player.TargetPosition = player.transform.position;
		player.CurrentBoss = boss;
		boss.HealthPoints --;
		player.InBossRoom = true;
		boss.StartAttack();
	}

	/** Get the type of the object */
	public ObjectType Type {
		get { return type; }
	}

	/** Set whether the object should be destroyable */
	public bool Destroyable {
		set { destroyable = value; }
		get { return destroyable; }
	}

	/** Set wheter the object is thrown */
	public bool Thrown {
		set { isThrown = value; }
		get { return isThrown; }
	}
}
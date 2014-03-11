using UnityEngine;
using System.Collections;

public class ThrowableObject : MonoBehaviour {
	public enum ObjectType{
		Normal,
		Key
	}
	public ObjectType type;
	public bool hasThrown;
	public bool pickedUp;

	public bool destroyable;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	if(hasThrown){
			transform.Translate(Vector3.forward * 0.8f);
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
			collider.enabled = true;
		}
	}

	public void AttachToPlayer(Transform player){
		transform.position = new Vector3(player.position.x,player.position.y + 3, player.position.z);
		transform.rotation = player.transform.rotation;
	}

	void OnCollisionEnter(Collision coll){
		Debug.Log("Collided " + coll.gameObject.name);
		if(coll.gameObject.name != "Player"){
			if(coll.gameObject.name == "Door"){
				Door hitDoor = coll.gameObject.GetComponent<Door>();
				if(type == ObjectType.Key && hitDoor.type == Door.DoorType.key){
				hitDoor.OpenDoor();
				Destroy (this.gameObject);
				}
			}
			hasThrown = false;
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		}
	}
}

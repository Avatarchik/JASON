using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public enum DoorType{
		normal,
		key
	}
	public DoorType type;
	public GameObject door;
	// Use this for initialization
	void Start () {
	
	}

	public void OpenDoor(){
		renderer.enabled = false;
		collider.enabled = false;
		Destroy(door);
	}
	public void CloseDoor(){
		renderer.enabled = true;
		collider.enabled = true;
	}
}

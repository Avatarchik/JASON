using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public enum DoorType{
		normal,
		key
	}
	public DoorType type;
	// Use this for initialization
	void Start () {
	
	}

	public void OpenDoor(){
		renderer.enabled = false;
		collider.enabled = false;
	}
	public void CloseDoor(){
		renderer.enabled = true;
		collider.enabled = true;
	}
}

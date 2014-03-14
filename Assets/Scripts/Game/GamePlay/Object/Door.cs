using UnityEngine;
using System.Collections;

public class Door:MonoBehaviour {
	public enum DoorType {
		Normal,
		Key
	}
	
	[SerializeField] private DoorType type;

	public void Open() {
		renderer.enabled = false;
		collider.enabled = false;
		
		Destroy(transform.GetChild(0).gameObject);
	}
	
	public void Close() {
		renderer.enabled = true;
		collider.enabled = true;
	}
	
	/** Get the type of the door */
	public DoorType Type {
		get { return type; }
	}
}

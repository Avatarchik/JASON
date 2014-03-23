using UnityEngine;
using System.Collections;

public class Door:MonoBehaviour {
	public enum DoorType {
		Other,
		Key,
		BossDoor
	}
	
	[SerializeField] private DoorType type;
	[SerializeField] private GameObject lockArt;
	public void Open() {
		animation.enabled = true;
		if(type == DoorType.Key){
			lockArt.renderer.enabled = false;
		}
		animation.Play();
		collider.enabled = false;

		//Destroy(transform.GetChild(0).gameObject);
	}
	
	public void Close() {

		collider.enabled = true;
	}
	
	/** Get the type of the door */
	public DoorType Type {
		get { return type; }
	}
}

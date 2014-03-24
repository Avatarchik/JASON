using UnityEngine;
using System.Collections;

public class Door:MonoBehaviour {
	public enum DoorType {
		Other,
		Key,
		BossDoor
	}
	
	[SerializeField] private DoorType type;

	private GameObject lockGo;

	void Start() {
		Transform lockTransform = transform.FindChild ("Lock");

		if(lockTransform != null)
			lockGo = lockTransform.gameObject;
	}

	public void Open() {
		if(lockGo != null)
			lockGo.renderer.enabled = false;

		collider.enabled = false;

		if(animation != null) {
			animation.enabled = true;
			animation.Play();
		} else {
			Destroy(gameObject);
		}
	}
	
	public void Close() {
		collider.enabled = true;
	}
	
	/** Get the type of the door */
	public DoorType Type {
		get { return type; }
	}
}

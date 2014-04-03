using UnityEngine;
using System.Collections;

public class Door:MonoBehaviour {
	public enum DoorState {
		Open,
		Closed
	}

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

	/** Open the door */
	public void Open() {
		if(lockGo != null)
			lockGo.renderer.enabled = false;

		collider.enabled = false;

		if(animation != null) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.DoorOpen,true);
			animation.enabled = true;
			animation.Play();
		} else {
			Destroy(gameObject);
		}
	}
	
	/** Close the door */
	public void Close() {
		collider.enabled = true;
	}
	
	/** Get the type of the door */
	public DoorType Type {
		get { return type; }
	}
}

using UnityEngine;
using System.Collections;

public class Trigger:MonoBehaviour {
	enum TriggerType {
		Player,
		Block,
		Arrow,
		TimedArrow,
		FireItem
	}

	[SerializeField] private bool isToggle;

	[SerializeField] private TriggerType type;
	
	[SerializeField] private Door[] connectedDoors;

	private PlayerCamera playerCamera;

	private Transform oldTarget;
	private Transform eventTarget;

	private bool isTriggered;
	private bool arrowEnabled;
	private bool cameraEventActive;
	
	void Start() {
		playerCamera = Camera.main.gameObject.GetComponent<PlayerCamera>();

		eventTarget = transform.FindChild("Camera Focus");
	}

	void OnCollisionEnter(Collision collision) {
		if(cameraEventActive)
			return;

		switch(collision.gameObject.tag) {
		case "Player":
			OnPlayerCollision();
			break;
		case "PushableObject":
			OnBlockCollision();
			break;
		case "Arrow":
			OnArrowCollision();
			break;
		case "FireDungeonItem":
			OnFireDungeonItemCollision();
			break;				
		}
	}

	void OnCollisionExit(Collision collision) {
		if(isToggle || cameraEventActive)
			return;

		switch(collision.gameObject.tag) {
		case "Player":
			OnPlayerCollisionExit();
			break;
		case "PushableObject":
			OnBlockCollisionExit();
			break;
		}
	}

	private void OnPlayerCollision() {
		if(type != TriggerType.Player)
			return;

		StartCoroutine(CameraEvent(Door.DoorState.Open));
	}

	private void OnPlayerCollisionExit() {
		if(type != TriggerType.Player)
			return;

		StartCoroutine(CameraEvent(Door.DoorState.Closed));
	}

	private void OnBlockCollision() {
		if(type != TriggerType.Block)
			return;

		Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		player.AttachedPushable.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
		player.Drop();

		StartCoroutine(CameraEvent(Door.DoorState.Open));
	}

	private void OnBlockCollisionExit() {
		if(type != TriggerType.Block)
			return;

		StartCoroutine(CameraEvent(Door.DoorState.Closed));
	}

	private void OnFireDungeonItemCollision() {
		if(type != TriggerType.FireItem)
			return;

		StartCoroutine(CameraEvent(Door.DoorState.Open));
	}

	private void OnArrowCollision() {
		if(type == TriggerType.Arrow) {
			StartCoroutine(CameraEvent(Door.DoorState.Open));
		} else if(type == TriggerType.TimedArrow) {
			StartCoroutine(TimedArrow());
		}
	}
	
	private IEnumerator TimedArrow() {
		arrowEnabled = true;
		
		yield return new WaitForSeconds(1);

		arrowEnabled = false;
	}

	private IEnumerator CameraEvent(Door.DoorState state) {
		cameraEventActive = true;

		oldTarget = playerCamera.Target;
		playerCamera.Target = eventTarget;

		yield return new WaitForSeconds(1.5f);
		
		foreach(Door door in connectedDoors) {
			if(state == Door.DoorState.Open) {
				door.Open();
			} else if(state == Door.DoorState.Closed) {
				door.Close();
			}
		}

		yield return new WaitForSeconds(1.5f);

		playerCamera.Target = oldTarget;

		cameraEventActive = true;
	}

	public bool ArrowEnabled {
		get { return arrowEnabled; }
	}
}

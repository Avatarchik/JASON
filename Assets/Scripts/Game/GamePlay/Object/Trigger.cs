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
	[SerializeField] private bool canOnlyTriggerOnce;

	[SerializeField] private TriggerType type;
	
	[SerializeField] private Door[] connectedDoors;

	private PlayerCamera playerCamera;

	private Transform eventTarget;

	private bool isTriggered;
	private bool isActive;
	
	void Start() {
		playerCamera = Camera.main.gameObject.GetComponent<PlayerCamera>();

		eventTarget = transform.FindChild("Camera Focus");
	}

	void OnGUI() {
		
	}

	void OnCollisionEnter(Collision collision) {
		if(playerCamera.CameraEventActive)
			return;

		if(canOnlyTriggerOnce)
			if(isTriggered)
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

	/** Handle collision with the player */
	private void OnPlayerCollision() {
		if(type != TriggerType.Player)
			return;

		if(canOnlyTriggerOnce)
			isTriggered = true;
		AudioManager.Instance.SetAudio(AudioManager.AudioFiles.DoorOpen,true);
		StartCoroutine(playerCamera.CameraEvent(eventTarget, connectedDoors));
	}

	/** Handle collision with a block */
	private void OnBlockCollision() {
		if(type != TriggerType.Block)
			return;

		Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		if(canOnlyTriggerOnce) {
			player.AttachedPushable.Locked = true;
			isTriggered = true;
		}

		player.AttachedPushable.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
		player.TargetPosition = player.transform.position;
		player.Drop();

		StartCoroutine(playerCamera.CameraEvent(eventTarget, connectedDoors));
	}

	/** Handle when a fire item enters collision */
	private void OnFireDungeonItemCollision() {
		if(type != TriggerType.FireItem)
			return;

		if(canOnlyTriggerOnce)
			isTriggered = true;

		StartCoroutine(playerCamera.CameraEvent(eventTarget, connectedDoors));
	}

	/** Handle when an arrow enters collision */
	private void OnArrowCollision() {
		if(type == TriggerType.Arrow) {
			if(canOnlyTriggerOnce)
				isTriggered = true;

			StartCoroutine(playerCamera.CameraEvent(eventTarget, connectedDoors));
		} else if(type == TriggerType.TimedArrow) {
			if(canOnlyTriggerOnce)
				isTriggered = true;

			StartCoroutine(TimedArrow());
		}
	}
	
	/** Timed arrow delay */
	private IEnumerator TimedArrow() {
		isActive = true;
		
		yield return new WaitForSeconds(1);

		isActive = false;
	}

	/** Get whether the timed arrow delay is currently active */
	public bool IsActive {
		get { return isActive; }
	}
}

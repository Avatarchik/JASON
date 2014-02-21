using UnityEngine;
using System;
using System.Collections;

public class Player:MonoBehaviour {
	[SerializeField] internal PlayerData data;

	[SerializeField] internal PlayerCamera playerCamera;
	[SerializeField] internal GameObject playerModel;
	[SerializeField] internal Animator playerAnimation;
	
	[SerializeField] private SUISpriteButton shieldButton;	
	
	private bool isDefending;

	private PlayerCombat playerCombat;
	private Inventory inventory;
	
	private Vector3 targetPosition;

	void Start() {
		playerCombat = GetComponent<PlayerCombat>();
		inventory = GetComponent<Inventory>();
	
		targetPosition = transform.position;
	}

	void OnGUI() {
		if(GUI.Button(new Rect(0, 0, 200, 200), "SHIELD"))
			isDefending = !isDefending;
	}

	void Update() {	
		if(isDefending) {
			targetPosition = transform.position;
			playerCombat.Defend(true);
		} else {
			playerCombat.Defend(false);
		}

		rigidbody.velocity = Vector3.zero;
		
		CheckForTouch();

		if(targetPosition != transform.position) {
			playerCamera.CameraDistance = 10;
			float step = data.speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

			Vector3 lookPos = targetPosition - playerModel.transform.position;
			Quaternion rotation = Quaternion.identity;

			if(lookPos != Vector3.zero)
				rotation = Quaternion.LookRotation(lookPos);

			rotation.x = 0;
			rotation.z = 0;

			if(transform.position != targetPosition)
				playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, rotation, 30);
		} else {
			playerCamera.CameraDistance = -5;
			playerAnimation.SetBool("IsRunning", false);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.CompareTag("Item")) {
			inventory.PickupEquipable(collider.GetComponent<ItemEquipable>());
		}
	}

	private IEnumerator Delay(){
		yield return new WaitForSeconds(0.3f);

		playerAnimation.SetBool("GettingHit", false);
	}

	private void CheckForTouch() {
		if(Input.GetMouseButtonDown(0))
			CheckTouchPosition(Camera.main.ScreenPointToRay(Input.mousePosition));
		
		if(Input.touchCount == 0)
			return;
		
		Touch touch = Input.GetTouch(0);
		if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
			CheckTouchPosition(Camera.main.ScreenPointToRay(touch.position));
		}
	}
	
	private void CheckTouchPosition(Ray screenRay) {
		RaycastHit hit;
		Physics.Raycast(screenRay, out hit, 100);

		if(hit.collider != null) {
			if(hit.transform.tag != "Player")
				playerCombat.Defend(false);

			switch(hit.transform.tag) {
			case "Floor":
				playerAnimation.SetBool("IsRunning", true);
				Move(hit.point);
				break;
			case "Player":
				Move(transform.position);
				playerCombat.Defend(true);
				break;
			case "Enemy":
				playerCombat.StartAttack(hit.transform);
				break;
			case "DestroyableObject":
				playerCombat.StartAttack(hit.transform);
				break;
			case "Test":
				Application.LoadLevel(Application.loadedLevelName);
				break;
			}
		}
	}
	
	private void Move(Vector3 position) {
		playerCombat.Target = null;
		targetPosition = new Vector3(position.x, 1, position.z);
	}

	public void getDamage(int amount) {
		playerAnimation.SetBool("GettingHit",true);
		data.health -= amount;
		StartCoroutine("Delay");
	}
	
	public PlayerData Data { get { return data; } }
	
	public Vector3 TargetPosition {
		get { return targetPosition; }
		set { targetPosition = value; }
	}
	
	[Serializable]
	public class PlayerData {
		public int health;
		public int speed;
		public int attackDamage;

		public int chargeMultiplier;
		
		public float attackDelay;
	}
}

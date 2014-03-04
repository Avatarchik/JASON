using UnityEngine;
using System;
using System.Collections;

public class Player:MonoBehaviour {
	[SerializeField] private PlayerData playerData;

	[SerializeField] private GameObject playerModel;
	[SerializeField] private Animator playerAnimation;

	private PlayerCamera playerCamera;
	private PlayerCombat playerCombat;

	private Vector3 targetPosition;

	private bool defending;
	private bool hit;

	void Start() {
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>();
		playerCombat = GetComponent<PlayerCombat>();

		targetPosition = transform.position;
	}

	void FixedUpdate() {
		rigidbody.velocity = Vector3.zero;

		CheckForInput();

		if(Vector3.Distance(transform.position, targetPosition) > 2) {
			playerCamera.CameraDistance = 10;
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerData.speed * Time.deltaTime); 

			Vector3 lookPosition = targetPosition - playerModel.transform.position;
			Quaternion lookRotation = Quaternion.identity;

			if(lookPosition != Vector3.zero)
				lookRotation = Quaternion.LookRotation(lookPosition);

			lookRotation.x = 0;
			lookRotation.z = 0;

			if(transform.position != targetPosition)
				playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, lookRotation, 30);
		} else {
			playerCamera.CameraDistance = -5;
			playerAnimation.SetBool("IsRunning", false);
		}
	}

	void OnGUI() {
		if(GUI.Button(new Rect(0, 0, 100, 50), new GUIContent("Defend"))) {
			playerCombat.Defend(!playerCombat.Defending);
			
			if(playerCombat.Defending)
				targetPosition = transform.position;
		}
	}

	private void CheckForInput() {
		RaycastHit hit;
		Ray ray;

		if(Input.touchCount == 0) {
			if(Input.GetMouseButtonDown(0)) {
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Physics.Raycast(ray, out hit, 100);

				HandleInput(hit);
			}
		} else if(Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);

			ray = Camera.main.ScreenPointToRay(touch.position);
			Physics.Raycast(ray, out hit, 100);

			HandleInput(hit);
		}
	}

	private void HandleInput(RaycastHit hit) {	
		if(hit.collider == null)
			return;

		Debug.Log (hit.transform.tag);

		switch(hit.transform.tag) {
		case "Floor":
			if(!playerCombat.Defending) {
				playerAnimation.SetBool("IsRunning", true);
				Move(hit.point);
			}

			break;
		case "Enemy":
		case "Destructable":
			if(!playerCombat.Defending)
				playerCombat.Attack(hit.transform.gameObject, hit.transform.tag);
			break;
		}
	}

	void OnTriggerEnter(Collider collider) {
		switch(collider.tag) {
		case "ItemEquipable":
			playerData.inventory.PickupEquipable(collider.GetComponent<ItemEquipable>());
			break;
		case "ItemWeapon":
			playerData.inventory.PickupWeapon(collider.GetComponent<ItemWeapon>());
			break;
		case "ItemPower":
			playerData.inventory.PickupPower(collider.GetComponent<ItemPower>());
			break;
		case "ItemSpecial":
			playerData.inventory.PickupSpecial(collider.GetComponent<ItemSpecial>());
			break;
		}
	}

	public void Damage(int amount) {
		playerAnimation.SetBool("GettingHit", true);
		playerData.Health -= amount;

		hit = true;

		StartCoroutine("DamageDelay");
	}

	private void Move(Vector3 position) {
		playerCombat.TargetEnemy = null;
		playerCombat.TargetDestructable = null;
		targetPosition = new Vector3(position.x, 1, position.z);
	}

	private IEnumerator DamageDelay(){
		yield return new WaitForSeconds(playerData.damageDelay);

		hit = false;

		playerAnimation.SetBool("GettingHit", false);
	}

	public bool Hit { get { return hit; } }
	
	public PlayerData PlayerData { get { return playerData; } }

	public PlayerCombat PlayerCombat { get { return playerCombat; } }

	public Animator PlayerAnimation { get { return playerAnimation; } }
	
	public Vector3 TargetPosition {
		set { targetPosition = value; }
		get { return targetPosition; }
	}
}

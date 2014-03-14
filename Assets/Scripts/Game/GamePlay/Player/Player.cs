using UnityEngine;
using System;
using System.Collections;

public class Player:MonoBehaviour {
	[SerializeField] private GameObject playerModel;
	[SerializeField] private Animator playerAnimation;
	
	private PlayerCamera playerCamera;
	private PlayerCombat playerCombat;

	private Vector3 targetPosition;

	private ThrowableObject currentObject;
	private PushableBlock block;
	public GameObject pushablePosition;

	[HideInInspector]public bool hit;
	private int mask = ~(1 << 8);

	private bool dataInstanceFound;

	void Start() {
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>();
		playerCombat = GetComponent<PlayerCombat>();
		targetPosition = transform.position;
	}

	void FixedUpdate() {
		if(!dataInstanceFound) {
			if(GameObject.FindGameObjectWithTag("Global Manager") == null) {
				return;
			} else {
				dataInstanceFound = true;
			}
		}

		rigidbody.velocity = Vector3.zero;

		if(currentObject != null){
			currentObject.AttachTo(playerModel.transform);
			currentObject.collider.enabled = false;
		}
		if(pushablePosition != null && block != null){
			//block.transform.position = pushablePosition.transform.position;
			block.rigidbody.AddForce((pushablePosition.transform.position - block.transform.position) * 1551 * Time.smoothDeltaTime);
			Debug.Log("ASD");
		}

		CheckForInput();

		if(Vector3.Distance(transform.position, targetPosition) > 2) {
			playerAnimation.SetBool("IsRunning", true);

			playerCamera.CameraDistance = 10;
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, PlayerData.Instance.RunSpeed * Time.deltaTime); 

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
	void CheckForObjects(){
		Collider[] hits = Physics.OverlapSphere(transform.position, 3);
		
		for(int i = 0; i < hits.Length; i++) {
			if(hits[i].tag == "ThrowableObject"){
				if(currentObject == null){
					currentObject = hits[i].GetComponent<ThrowableObject>();
					Debug.Log("Grab Object");
				}
			}
			if(hits[i].tag == "PushableObject"){
				block = hits[i].GetComponent<PushableBlock>();
				//hits[i].transform.parent = this.transform;
				pushablePosition.transform.position = block.transform.position;
			}
		}
	}
	
	public void Pickup() {
		if(currentObject != null) {
			currentObject.Thrown = true;
			currentObject = null;
		} else if(block != null) {
			block.transform.parent = null;
			block = null;
		} else {
			CheckForObjects();
		}
	}

	private void CheckForInput() {
		RaycastHit hit;
		Ray ray;

		if(Input.touchCount == 0) {
			if(Input.GetMouseButtonDown(0)) {
				if(Input.mousePosition.x <= 176 && Input.mousePosition.y <= 50)
					return;
					
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Physics.Raycast(ray, out hit, 100, mask);

				HandleInput(hit);
			}
		} else {
			Touch touch = Input.GetTouch(0);
			
			if(touch.position.x <= 176 && touch.position.y <= 50)
				return;

			ray = Camera.main.ScreenPointToRay(touch.position);
			Physics.Raycast(ray, out hit, 100, mask);

			HandleInput(hit);
		}
	}

	private void HandleInput(RaycastHit hit) {
		if(hit.collider == null)
			return;

		switch(hit.transform.tag) {
			case "Floor":
				if(!playerCombat.Defending)
					Move(hit.point);
				break;
			case "Enemy":
			case "Destructable":
			if(!playerCombat.Defending)
				playerCombat.Attack(hit.transform.gameObject, hit.transform.tag);

			break;
			default:
			if(!playerCombat.Defending)
				Move(hit.point);
			break;
		}
	}

	void OnTriggerEnter(Collider collider) {
		/*switch(collider.tag) {
		case "Item Equipable":
			playerData.inventory.PickupEquipable(collider.GetComponent<ItemEquipable>());
			break;
		case "Item Weapon":
			playerData.inventory.PickupWeapon(collider.GetComponent<ItemWeapon>());
			break;
		case "Item Power":
			playerData.inventory.PickupPower(collider.GetComponent<ItemPower>());
			break;
		case "Item Special":
			playerData.inventory.PickupSpecial(collider.GetComponent<ItemSpecial>());
			break;
		}*/
	}

	public void Damage(int amount, float stunTime) {
		playerAnimation.SetBool("GettingHit", true);
		PlayerData.Instance.Health -= amount;

		hit = true;

		StartCoroutine(DamageDelay(stunTime));
	}

	private void Move(Vector3 position) {
		playerCombat.TargetEnemy = null;
		playerCombat.TargetDestructable = null;
		targetPosition = new Vector3(position.x, 1, position.z);
	}

	private IEnumerator DamageDelay(float stunTime){
		yield return new WaitForSeconds(stunTime);

		hit = false;

		playerAnimation.SetBool("GettingHit", false);
	}

	public bool Hit { get { return hit; } }

	public PlayerCombat PlayerCombat { get { return playerCombat; } }

	public Animator PlayerAnimation { get { return playerAnimation; } }
	
	public Vector3 TargetPosition {
		set { targetPosition = value; }
		get { return targetPosition; }
	}
}

using UnityEngine;
using System;
using System.Collections;

public class Player:MonoBehaviour {
	[SerializeField] private PlayerData playerData;

	[SerializeField] private GameObject playerModel;
	[SerializeField] private Animator playerAnimation;

	//public Inventory playerInventory;
	private PlayerCamera playerCamera;
	private PlayerCombat playerCombat;

	private Vector3 targetPosition;

	private bool defending;

	private ThrowableObject currentObject;
	private PushableBlock block;
	public GameObject pushablePosition;
	[HideInInspector]public bool hit;
	int mask = ~(1 << 8);

	void Start() {
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>();
		playerCombat = GetComponent<PlayerCombat>();
		playerData = PlayerData.Instance;
		targetPosition = transform.position;
	}

	void FixedUpdate() {
		if(currentObject != null){
			currentObject.AttachToPlayer(playerModel.transform);
			currentObject.collider.enabled = false;
		}
		if(pushablePosition != null && block != null){
			block.transform.position = pushablePosition.transform.position;
		}
		rigidbody.velocity = Vector3.zero;
		CheckForInput();

		if(Vector3.Distance(transform.position, targetPosition) > 2) {
			playerAnimation.SetBool("IsRunning", true);

			playerCamera.CameraDistance = 10;
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerData.speed * Time.deltaTime); 

			Vector3 lookPosition = targetPosition - playerModel.transform.position;
			Quaternion lookRotation = Quaternion.identity;

			if(lookPosition != Vector3.zero)
				lookRotation = Quaternion.LookRotation(lookPosition);

			lookRotation.x = 0;
			lookRotation.z = 0;

			if(transform.position != targetPosition){
				if(block == null){
				playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, lookRotation, 30);
				}
			}
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
	void OnGUI() {
		if(GUI.Button(new Rect(500, 300, 300, 150), new GUIContent("Pickup"))) {
			if(currentObject!= null){
				currentObject.hasThrown = true;
				currentObject = null;
			}else if(block != null){
				//pushablePosition = null;
				block.transform.parent = null;
				block = null;

			}else{
			CheckForObjects();
			}
		}
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
				Physics.Raycast(ray, out hit, 100,mask);

				HandleInput(hit);
			}
		} else if(Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);

			ray = Camera.main.ScreenPointToRay(touch.position);
			Physics.Raycast(ray, out hit, 100,mask);

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

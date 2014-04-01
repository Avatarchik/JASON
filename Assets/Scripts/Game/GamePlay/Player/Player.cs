using UnityEngine;
using System;
using System.Collections;

public class Player:MonoBehaviour {
	[SerializeField] private GameObject scrollingCombatText;
    [SerializeField] private GameObject selectionParticles;
	[SerializeField] private Renderer[] shield;
	[SerializeField] private Renderer sword;
	private Boss currentBoss;

	private Animator playerAnimation;
	private PlayerCamera playerCamera;
	private PlayerCombat playerCombat;

	private Transform pushablePosition;
	[SerializeField] private Transform throwablePosition;
	
	private Vector3 targetPosition;
	
	private PushableObject attachedPushable;
	private ThrowableObject attachedThrowable;

    private GameObject selectionParticle;
	private GameObject pickupWhenReady;

	private float previousX;
	
	private int mask = ~(1 << 8);
	
	private bool isHit;
	private bool isInBossRoom;

	private Vector3 movement;
	private Vector3 prevpos;
	private Vector3 newpos;
	private Vector3 fwd;

	private Vector3 side;
	void Start() {
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>();
		playerCombat = GetComponent<PlayerCombat>();
		playerAnimation = GetComponentInChildren<Animator>();

		targetPosition = transform.position;

		pushablePosition = transform.FindChild("Pushable Position");

		previousX = transform.position.x;
	}
	
	void Update() {
		AnimationHandling();

		if(Input.GetKeyDown(KeyCode.T)) {
			GameObject.Find("SGUI Manager").GetComponent<SGUIManager>().RemoveAll();
			PlayerData.Instance.Reset();
			Application.LoadLevel("Fire Dungeon");
		}

		HandlePickedUpObject();

		switch(Application.platform) {
		case RuntimePlatform.Android:
			HandleTouchInput();
			break;
		default:
			HandleMouseInput();
			break;
		}
	}
	
	void FixedUpdate() {
		rigidbody.velocity = Vector3.zero;
		if(Vector3.Distance(transform.position, targetPosition) > 0.5f) {
			playerAnimation.SetBool("IsRunning", true);

			if(!isInBossRoom)
				playerCamera.CameraDistance = 10;

			transform.position = Vector3.MoveTowards(transform.position, targetPosition, PlayerData.Instance.RunSpeed * Time.deltaTime); 
			
			if(attachedPushable == null) {
				Vector3 lookPosition = targetPosition - transform.position;
				Quaternion lookRotation = Quaternion.identity;
				
				if(lookPosition != Vector3.zero)
					lookRotation = Quaternion.LookRotation(lookPosition);
				
				lookRotation.x = 0;
				lookRotation.z = 0;
				
				if(transform.position != targetPosition)
					transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 30);
			}
		} else {
			if(!isInBossRoom)
				playerCamera.CameraDistance = -5;

			playerAnimation.SetBool("IsRunning", false);

			Destroy(selectionParticle);
		}

		if(transform.position.x < previousX)
			playerCamera.CameraDistance = 10;

		previousX = transform.position.x;
	}
	void LateUpdate ()
	{
		prevpos = transform.position;
		fwd = transform.forward;
		side = transform.right;
	}
	void AnimationHandling(){
		newpos = transform.position;
		movement = (newpos - prevpos);
		if(attachedPushable != null || attachedThrowable != null){
			shield[0].enabled = false;
			shield[1].enabled = false;
			sword.enabled = false;
		}else{
			shield[0].enabled = true;
			shield[1].enabled = true;
			sword.enabled = true;
		}
		if(attachedPushable != null){
			playerAnimation.SetBool("IsMovingBlock",true);
			float forward = Vector3.Dot(fwd, movement);
			float backwards = -forward;
			float rightside = Vector3.Dot(side, movement);
			float leftside = Mathf.Abs(rightside);

			Debug.Log("Priority    F = " + forward + " B = " + backwards + " R = " + rightside + " L = " + leftside);
			float[] floatlist = new float[] {forward,backwards,rightside,leftside};
			float highest = Mathf.Max(floatlist);
			Debug.Log(highest);

			if(forward == highest){
				playerAnimation.SetInteger("MoveDirection",1);
			}else if(backwards == highest){
				playerAnimation.SetInteger("MoveDirection",2);
			}else if(rightside == highest){
				playerAnimation.SetInteger("MoveDirection",4);
			}else if(leftside == highest){
				playerAnimation.SetInteger("MoveDirection",3);
			}

			if(forward == 0 && backwards == 0 && leftside == 0 && rightside == 0){
				playerAnimation.SetInteger("MoveDirection",0);
			}
		}else{
			playerAnimation.SetBool("IsMovingBlock",false);
		}
		if(attachedThrowable != null){
			playerAnimation.SetBool("IsHolding", true);
		}else{
			playerAnimation.SetBool("IsHolding", false);
		}
	}
	void OnCollisionEnter(Collision collision) {
		HandleCollision(collision);
	}

	void OnCollisionStay(Collision collision) {
		HandleCollision(collision);
	}

	void OnTriggerEnter(Collider collider) {
		BasicTutorial tutorial = BasicTutorial.Instance;
		
		if(!tutorial.Started)
			return;

		switch(collider.tag) {
		case "Tutorial Arrows":
			if(tutorial.Stage == BasicTutorial.TutorialStage.Traps)
				tutorial.StartStage();
			break;
		case "Tutorial Block Pickup":
			if(tutorial.Stage == BasicTutorial.TutorialStage.BlockPickup)
				tutorial.StartStage();
			break;
		case "Tutorial Block Drop":
			if(tutorial.Stage == BasicTutorial.TutorialStage.BlockDrop && attachedPushable != null)
				tutorial.StartStage();
			break;
		case "Tutorial Player Trigger":
			if(tutorial.Stage == BasicTutorial.TutorialStage.PlayerTrigger)
				tutorial.StartStage();
			break;
		case "Tutorial Block Trigger":
			if(tutorial.Stage == BasicTutorial.TutorialStage.BlockTrigger && attachedPushable != null)
				tutorial.StartStage();
			break;
		case "Tutorial Key":
			if(tutorial.Stage == BasicTutorial.TutorialStage.Key)
				tutorial.StartStage();
			break;
		case "Tutorial Key Door":
			if(tutorial.Stage == BasicTutorial.TutorialStage.KeyDoor && attachedThrowable != null)
				tutorial.StartStage();
			break;
		}
	}

	private void HandleCollision(Collision collision) {
		if(pickupWhenReady != null) {
			if(collision.gameObject.CompareTag(pickupWhenReady.tag)) {
				switch(collision.gameObject.tag) {
				case "ThrowableObject":
					attachedThrowable = pickupWhenReady.GetComponent<ThrowableObject>();

					attachedThrowable.collider.enabled = false;

					targetPosition = transform.position;
					break;
				case "PushableObject":
					attachedPushable = pickupWhenReady.GetComponent<PushableObject>();

					if(attachedPushable.Locked) {
						Drop();
						break;
					}
										
					transform.LookAt(attachedPushable.transform.position);
					transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

					targetPosition = transform.position;

					BasicTutorial tutorial = BasicTutorial.Instance;

					if(tutorial.Started && tutorial.Stage == BasicTutorial.TutorialStage.BlockPath)
						tutorial.StartStage();

					break;
				}

				pickupWhenReady = null;
			}
		}

		if(attachedThrowable != null) {
			if(collision.gameObject.CompareTag("Door"))
				attachedThrowable.HandleDoorCollision(collision.gameObject.GetComponent<Door>());
		}
	}

	/** Pickup an object */
	public void Pickup(GameObject obj) {
		if(attachedPushable != null || attachedThrowable != null)
			return;

		Move(obj.transform.position);

		pickupWhenReady = obj;
	}

	/** Drop the picked up object */
	public void Drop() {
		if(attachedPushable != null) {
			attachedPushable.rigidbody.isKinematic = true;
			targetPosition = transform.position;
		}

		if(attachedThrowable != null) {
			attachedThrowable.collider.enabled = true;
			attachedThrowable.transform.position = new Vector3(attachedThrowable.transform.position.x, 0.58f, attachedThrowable.transform.position.z);
		}
		
		attachedThrowable = null;
		attachedPushable = null;
	}

	/** Throw the picked up object */
	public void ThrowObject() {
		if(attachedThrowable == null)
			return;

		attachedThrowable.Thrown = true;
		Drop();
	}

	/** Damage the player */
	public void Damage(int amount, float stunTime, bool crit) {
		playerAnimation.SetBool("GettingHit", true);
		PlayerData.Instance.Health -= amount;
		
		DisplayCombatText(amount.ToString(), Color.yellow, crit);
		
		isHit = true;

		if(PlayerData.Instance.Health <= 0) {
			PlayerData.Instance.Reset();

			if(!isInBossRoom) {
				Application.LoadLevel(Application.loadedLevel);
			} else {
				currentBoss.Reset();
				transform.position = GameObject.Find("Player Spawn").transform.position;
				targetPosition = transform.position;
			}
		}
		
		StartCoroutine(DamageDelay(stunTime));
	}
	
	private void DisplayCombatText(string text, Color color, bool crit) {
		Vector3 position = transform.position;
		position.y += 3.5f;
		
		TextMesh popup = (Instantiate(Resources.Load("Prefabs/Misc/Scrolling Combat Text"), position, Quaternion.identity) as GameObject).GetComponent<TextMesh>();
		
		if(crit) {
			Vector3 ls = popup.transform.localScale;
			
			ls.x *= 2;
			ls.y *= 2;
			ls.z *= 2;
			
			popup.transform.localScale = ls;
		}
		
		popup.transform.parent = this.transform;
		
		popup.text = text;
		popup.color = color;
	}

	/** Handle the picked up object */
	private void HandlePickedUpObject() {
		if(attachedThrowable != null) {
			attachedThrowable.transform.position = throwablePosition.position;
			attachedThrowable.transform.rotation = throwablePosition.rotation;
		} 
		
		if(attachedPushable != null) {
			attachedPushable.rigidbody.isKinematic = attachedPushable.transform.position.Equals(pushablePosition.position) ? true : false;
			attachedPushable.rigidbody.velocity = -(attachedPushable.transform.position - pushablePosition.position).normalized * 10;

			if(Vector3.Distance(transform.position, attachedPushable.transform.position) > 5)
				Drop();
		}
	}

	private void HandleTouchInput() {
		foreach(Touch touch in Input.touches) {
			if(touch.phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				RaycastHit hit;

				Physics.Raycast(ray, out hit, 100, mask);

				HandleInput(hit);
			}
		}
	}

	private void HandleMouseInput() {
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			Physics.Raycast(ray, out hit, 100, mask);

			HandleInput(hit);
		}
	}

	private void HandleInput(RaycastHit hit) {
		if(hit.collider == null || SGUIManager.Instance.AnyButtonClicked)
			return;
		
		if(playerCombat.Defending)
			return;

		switch(hit.transform.tag) {
		case "Enemy":
		case "Boss":
			if(!playerCombat.InCombat) {
				playerCombat.WeaponCollisionArea.collider.enabled = true;
				playerCombat.StartAttack(hit.transform.gameObject);
			}
			break;
		case "ThrowableObject":
		case "PushableObject":
			BasicTutorial tutorial = BasicTutorial.Instance;

			if(tutorial.Started && tutorial.Stage == BasicTutorial.TutorialStage.BlockPickup)
				tutorial.StartStage();

			playerCombat.WeaponCollisionArea.collider.enabled = false;

			if(attachedPushable != null) {
				Drop();
			} else {
				Pickup(hit.collider.gameObject);
			}
			break;
		default:
			playerCombat.WeaponCollisionArea.collider.enabled = false;
			Move(hit.point);
			break;
		}
	}

	/** Move the player */
	private void Move(Vector3 position) {
		BasicTutorial tutorial = BasicTutorial.Instance;

		if(tutorial.Started && tutorial.Stage == BasicTutorial.TutorialStage.KeyDoor && tutorial.Labels[(int)tutorial.Stage - 1].FinishedWriting)
			tutorial.StopTutorial();

		if(tutorial.Started && tutorial.Labels[(int)tutorial.Stage - 1].FinishedWriting)
			tutorial.NextStage();
		
		playerCombat.Target = null;
		targetPosition = new Vector3(position.x, 1, position.z);

        if(selectionParticle != null)
            Destroy(selectionParticle);

        selectionParticle = Instantiate(selectionParticles, targetPosition, Quaternion.identity) as GameObject;
	}

	/** The damage delay of the player */
	private IEnumerator DamageDelay(float stunTime){
		yield return new WaitForSeconds(stunTime);
		
		isHit = false;
		
		playerAnimation.SetBool("GettingHit", false);
	}

	/** Set and/or get the current boss */
	public Boss CurrentBoss {
		set { currentBoss = value; }
		get { return currentBoss; }
	}

	/** Get the animator component of the player */
	public Animator PlayerAnimation {
		get { return playerAnimation; }
	}

	/** Get the camera component of the player */
	public PlayerCamera PlayerCamera {
		get { return playerCamera; }
	}

	/** Get the combat component of the player */
	public PlayerCombat PlayerCombat {
		get { return playerCombat; } 
	}
	
	/** Get the pushable position */
	public Transform PushablePosition {
		get { return pushablePosition; }
	}

	/** Get the throwable position */
	public Transform ThrowablePosition {
		get { return throwablePosition; }
	}

	/** Set and/or get the target position */
	public Vector3 TargetPosition {
		set { targetPosition = value; }
		get { return targetPosition; }
	}

	/** Set and/or get the attached pushable object */
	public PushableObject AttachedPushable {
		set { attachedPushable = value; }
		get { return attachedPushable; }
	}

	/** Set and/or get the attached throwable object */
	public ThrowableObject AttachedThrowable {
		set { attachedThrowable = value; }
		get { return attachedThrowable; }
	}
	
	/** Get wheter or not the player is hit */
	public bool Hit { 
		get { return isHit; }
	}

	/** Set and/or get wheter or not the player is in a boss room */
	public bool InBossRoom {
		set { isInBossRoom = value; }
		get { return isHit; }
	}
}

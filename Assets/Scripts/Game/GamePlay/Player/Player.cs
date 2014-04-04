using UnityEngine;
using System;
using System.Collections;

public class Player:MonoBehaviour {
	[SerializeField] private GameObject scrollingCombatText;
    [SerializeField] private GameObject selectionParticles;
	[SerializeField] private Renderer[] shield;
	[SerializeField] private Renderer sword;

	[SerializeField] private Transform throwablePosition;

	private Boss currentBoss;

	private Animator playerAnimation;

	private PlayerCamera playerCamera;
	private PlayerCombat playerCombat;

	private Transform pushablePosition;
	
	private Vector3 targetPosition;

	private Vector3 movement;
	private Vector3 prevpos;
	private Vector3 newpos;
	private Vector3 fwd;
	private Vector3 side;

	private Vector2 lastTouchPosition;
	
	private PushableObject attachedPushable;
	private ThrowableObject attachedThrowable;

    private GameObject selectionParticle;
	private GameObject pickupWhenReady;

	private float previousX;
	private float lastTouchTime;
	
	private int mask = ~(1 << 8);
	
	private bool isHit;
	private bool isInBossRoom;
	private bool sprinting;
	
	void Start() {
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>();
		playerCombat = GetComponent<PlayerCombat>();
		playerAnimation = GetComponentInChildren<Animator>();

		targetPosition = transform.position;

		pushablePosition = transform.FindChild("Pushable Position");

		previousX = transform.position.x;
	}
	
	void Update() {
		HandleAnimations();
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

			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.FootSteps, true);

			if(!isInBossRoom)
				playerCamera.CameraDistance = 10;

			int speed = (sprinting && !playerCombat.Defending) ? PlayerData.Instance.RunSpeed : PlayerData.Instance.WalkSpeed;

			if(playerCombat.Defending)
				speed /= 2;

			transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); 
			
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
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.FootSteps, false);

			if(!isInBossRoom)
				playerCamera.CameraDistance = -5;

			playerAnimation.SetBool("IsRunning", false);

			Destroy(selectionParticle);
		}

		if(transform.position.x < previousX)
			playerCamera.CameraDistance = 10;

		previousX = transform.position.x;
	}

	void LateUpdate() {
		prevpos = transform.position;
		fwd = transform.forward;
		side = transform.right;
	}
	
	void OnCollisionEnter(Collision collision) {
		HandleCollision(collision);
	}

	void OnCollisionStay(Collision collision) {
		HandleCollision(collision);
	}

	void OnTriggerEnter(Collider collider) {
		BasicTutorial tutorial = BasicTutorial.Instance;

		if(collider.CompareTag("Fire Rooster"))
			Damage(1, 0, false);

		if(tutorial.Started) {
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
			case "Tutorial Boss":
				if(tutorial.Stage == BasicTutorial.TutorialStage.Boss)
					tutorial.StartStage();
				break;
			}
		}
	}

	/** Handle collisions */
	private void HandleCollision(Collision collision) {

		if(collision.gameObject.name == "FireAttack")
			Damage(0.1f, 1, false);
		
		if(collision.gameObject.name == "Hamer")
			Damage(0.1f, 1, false);
		
		if(collision.gameObject.name == "Spatel")
			Damage(0.1f, 1, false);

		if(pickupWhenReady != null) {
			if(collision.gameObject.CompareTag(pickupWhenReady.tag)) {
				switch(collision.gameObject.tag) {
				case "Key":
				case "FireItem":
					attachedThrowable = pickupWhenReady.GetComponent<ThrowableObject>();

					attachedThrowable.collider.enabled = false;
					attachedThrowable.Pickup();

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

		if(collision.gameObject.tag == "Boss"){
			if(attachedThrowable != null){
				if(attachedThrowable.Type == ThrowableObject.ObjectType.FireItem){
					MadOvenMain baas = collision.gameObject.GetComponent<MadOvenMain>();
					baas.StartAttack();
					Destroy(attachedThrowable.gameObject);
					baas.HealthPoints -= 1;
					attachedThrowable = null;
					AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BossMusic,true);
					AudioManager.Instance.SetAudio(AudioManager.AudioFiles.NormalMusic,false);
				}
			}
		}

		if(attachedThrowable != null) {
			if(AttachedThrowable.CompareTag("Key"))
				if(collision.gameObject.CompareTag("Door"))
					attachedThrowable.HandleDoorCollision(collision.gameObject.GetComponent<Door>());

			if(AttachedThrowable.CompareTag("FireItem"))
				if(collision.gameObject.CompareTag("Main Body"))
					attachedThrowable.HandleBossCollision(collision.gameObject.GetComponent<MadOvenMain>());
		}
	}

	private void HandleAnimations() {
		newpos = transform.position;
		movement = (newpos - prevpos);

		shield[0].enabled = attachedPushable == null && attachedThrowable == null;
		shield[1].enabled = attachedPushable == null && attachedThrowable == null;
		sword.enabled = attachedPushable == null && attachedThrowable == null;

		if(attachedPushable != null) {
			playerAnimation.SetBool("IsMovingBlock", true);

			float forward = Vector3.Dot(fwd, movement);
			float back = -forward;
			float right = Vector3.Dot(side, movement);
			float left = Mathf.Abs(right);

			float highest = Mathf.Max(new float[] { forward, back, right, left });

			if(forward == highest) {
				playerAnimation.SetInteger("MoveDirection", 1);
				AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, true);
			} else if(back == highest) {
				AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, true);
				playerAnimation.SetInteger("MoveDirection", 2);
			} else if(right == highest) {
				AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, true);
				playerAnimation.SetInteger("MoveDirection", 4);
			} else if(left == highest) {
				AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, true);
				playerAnimation.SetInteger("MoveDirection", 3);
			}

			if(forward == 0 && back == 0 && right == 0 && left == 0) {
				AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, false);
				playerAnimation.SetInteger("MoveDirection", 0);
			}
		} else {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, false);
			playerAnimation.SetBool("IsMovingBlock", false);
		}

		playerAnimation.SetBool("IsHolding", attachedThrowable != null ? true : false);
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
	public void Damage(float amount, float stunTime, bool crit) {
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
	
	/** Display combat text */
	private void DisplayCombatText(string text, Color color, bool crit) {
		Vector3 position = transform.position;
		position.y += 3.5f;
		
		TextMesh popup = (Instantiate(scrollingCombatText, position, Quaternion.identity) as GameObject).GetComponent<TextMesh>();
		
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

	/** Handle touch input */
	private void HandleTouchInput() {
		foreach(Touch touch in Input.touches) {
			if(touch.phase == TouchPhase.Began) {
				if((Time.time - lastTouchTime) < 0.5f) {
					Ray ray = Camera.main.ScreenPointToRay(touch.position);
					RaycastHit hit;

					Physics.Raycast(ray, out hit, 100, mask);

					HandleInput(hit, true);
				} else {
					if(attachedPushable == null && attachedThrowable == null) {
						Ray ray = Camera.main.ScreenPointToRay(touch.position);
						RaycastHit hit;

						Physics.Raycast(ray, out hit, 100, mask);

						HandleInput(hit, false);
					} else {
						ThrowObject();
					}
				}

				lastTouchTime = Time.time;
			}
		}
	}

	/** Handle mouse input */
	private void HandleMouseInput() {
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			Physics.Raycast(ray, out hit, 100, mask);

			HandleInput(hit, false);

			lastTouchTime = Time.time;
		} else if(Input.GetMouseButtonDown(1)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			Physics.Raycast(ray, out hit, 100, mask);

			HandleInput(hit, true);

			lastTouchTime = Time.time;
		} else if(Input.GetMouseButton(0)) {
			if((Time.time - lastTouchTime) > 0.5f) {
				if(attachedThrowable != null) {
					ThrowObject();
				} else {
					playerCombat.Defend(!playerCombat.Defending);
				}

				lastTouchTime = Time.time;
			}
		}
	}

	/** Handle input */
	private void HandleInput(RaycastHit hit, bool doubleTap) {
		if(hit.collider == null || SGUIManager.Instance.AnyButtonClicked || playerCamera.CameraEventActive)
			return;

		if(!doubleTap) {
			sprinting = false;

			switch(hit.transform.tag) {
			case "Enemy":
			case "Boss":
				if(!playerCombat.InCombat) {
					playerCombat.WeaponCollisionArea.collider.enabled = true;
					playerCombat.StartAttack(hit.transform.gameObject);
				}
				break;
			case "Key":
			case "FireItem":
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
		} else {
			playerCombat.WeaponCollisionArea.collider.enabled = false;
			sprinting = true;
			Move(hit.point);
		}
	}

	/** Move the player */
	private void Move(Vector3 position) {
		BasicTutorial tutorial = BasicTutorial.Instance;

		if(tutorial.Started && tutorial.Stage == BasicTutorial.TutorialStage.Boss && tutorial.Labels[(int)tutorial.Stage - 1].FinishedWriting)
			tutorial.StopTutorial();

		if(tutorial.Started && tutorial.Labels[(int)tutorial.Stage - 1].FinishedWriting)
			tutorial.NextStage();
		
		playerCombat.Target = null;
		targetPosition = new Vector3(position.x, 1, position.z);

        if(selectionParticle != null)
            Destroy(selectionParticle);

        selectionParticle = Instantiate(selectionParticles, targetPosition, Quaternion.identity) as GameObject;
		selectionParticle.transform.Rotate(new Vector3(270, 0, 0));
	}

	/** The damage delay of the player */
	private IEnumerator DamageDelay(float stunTime) {
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

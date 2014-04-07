using UnityEngine;
using System.Collections;

public class PlayerMovement:MonoBehaviour {
	private Player player;

	private GameObject pickupWhenColliding;

	private Vector3 previousPosition;
	private Vector3 targetPosition;
	private Vector3 moveDirection;
	
	private float lastClickTime;

	private int layerMask = ~(1 << 8);

	private bool clicked;

	void Start() {
		player = GetComponent<Player>();

		targetPosition = transform.position;
	}

	void Update() {
		CheckForInput();
	}

	void FixedUpdate() {
		rigidbody.velocity = Vector3.zero;
		
		if(player.Interactable != null && player.Interactable.GetInteractableType() == InteractableType.PushableBlock) {
			float distance = Vector3.Distance(transform.position, (player.Interactable as PushableBlock).transform.position);

			if(distance > 3.0f) {
				transform.position = previousPosition;
			} else if(distance < 2.99f) {
				previousPosition = transform.position;
			}
		}
		
		if(Vector3.Distance(transform.position, targetPosition) > 0.1f) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.FootSteps, true);
			player.Animator.SetBool("IsRunning", true);

			moveDirection = -(transform.position - targetPosition);
			rigidbody.AddForce(-(transform.position - targetPosition).normalized * player.EntityData.WalkSpeed);

			if(player.Interactable == null || player.Interactable.GetInteractableType() != InteractableType.PushableBlock)
				if((transform.position - targetPosition) != Vector3.zero)
					transform.rotation = Utils.RotateTowards(transform.position, transform.rotation, targetPosition);
					
		} else {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.FootSteps, false);
			player.Animator.SetBool("IsRunning", false);

			moveDirection = Vector3.zero;
		}
	}

	void OnCollisionEnter(Collision col) {
		HandleCollision(col);	
	}

	void OnCollisionStay(Collision col) {
		HandleCollision(col);
	}
		
	/** <summary>Move the player to the specified position</summary>
	 * <param name="position">The position to move to</param> */
	private void Move(Vector3 position) {
		targetPosition = new Vector3(position.x, 0.667f, position.z);
	}
	
	/** <summary>Handle <code>OnCollisionEnter</code> and <code>OnCollisionStay</code> calls</summary>
	 * <param name="col">The collision</param>  */
	private void HandleCollision(Collision col) {
		if(pickupWhenColliding != null && col.collider.gameObject.Equals(pickupWhenColliding)) {
			player.Pickup(col.transform);

			pickupWhenColliding = null;
		}
	}

	/** <summary>Handle short clicks/touches</summary>
	 * <param name="hit">The raycast hit</param> */
	private void HandleClick(RaycastHit hit) {
		if(hit.collider == null || CameraManager.Instance.CameraEventActive)
			return;

		switch(hit.transform.tag) {
		case "Enemy":
			Debug.Log("Enemy clicked");
			break;
		case "Interactable Object":
			if(player.Interactable == null) {
				pickupWhenColliding = hit.transform.gameObject;
				Move(hit.transform.position);
			} else {
				if(player.Interactable == hit.transform.GetComponent(typeof(IInteractable)) as IInteractable) {
					player.Drop(false);
				} else {
					player.Drop(false);
					pickupWhenColliding = hit.transform.gameObject;
					Move(hit.transform.position);
				}
			}
			break;
		default:
			Move(hit.point);
			break;
		}
	}

	/** <summary>Handle long clicks/touches</summary>
	 * <param name="hit">The raycast hit</param> */
	private void HandleLongClick(RaycastHit hit) {
		if(hit.collider == null)
			return;

		transform.rotation = Utils.RotateTowards(transform.position, transform.rotation, hit.point);

		if(player.Interactable != null) {
			player.Drop(true);
		}
	}

	/** <returns>The move direction</returns> */
	public Vector3 MoveDirection {
		get { return moveDirection; }
	}

	/** <summary>Set or get the target position</summary> */
	public Vector3 TargetPosition {
		set { targetPosition = value; }
		get { return targetPosition; }
	}

#if UNITY_EDITOR || !UNITY_ANDROID
	/** <summary>Check for input each update</summary> */
	private void CheckForInput() {
		if(Input.GetMouseButton(0) && Input.GetMouseButtonDown(0)) {
			clicked = true;
			lastClickTime = Time.time;
		} else if(Input.GetMouseButton(0)) {
			if((Time.time - lastClickTime) > 0.5f)
				HandleLongClick(RaycastUtils.PositionToRaycastHit(Input.mousePosition, layerMask));
		} else {
			if(clicked) {
				clicked = false;
				HandleClick(RaycastUtils.PositionToRaycastHit(Input.mousePosition, layerMask));
			}
		}
	}
#elif UNITY_ANDROID
	/** <summary>Check for input each update</summary> */
	private void CheckForInput() {
		Debug.Log("Android");

		if(Input.touchCount <= 0)
			return;

		foreach(Touch touch in Input.touches) {
			if(touch.phase == TouchPhase.Began) {
				Debug.Log("Touch began");
			} else if(touch.phase == TouchPhase.Moved) {
				Debug.Log("Touch moved");
			} else if(touch.phase == TouchPhase.Stationary) {
				Debug.Log("Touch stationary");
			}
		}
	}
#endif
}

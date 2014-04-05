using UnityEngine;
using System.Collections;

public class PlayerMovement:MonoBehaviour {
	private Player player;

	private GameObject pickupWhenColliding;

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

		if(Vector3.Distance(transform.position, targetPosition) > 0.1f) {
			moveDirection = -(transform.position - targetPosition).normalized;

			rigidbody.AddForce(-(transform.position - targetPosition).normalized * player.EntityData.WalkSpeed);
		} else {
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
		if(hit.collider == null)
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
		case "Movable Point":
			Move(hit.point);
			break;
		}
	}

	/** <summary>Handle long clicks/touches</summary>
	 * <param name="hit">The raycast hit</param> */
	private void HandleLongClick(RaycastHit hit) {
		if(hit.collider == null)
			return;

		if(player.Interactable != null) {
			player.Drop(true);
		}
	}

	public Vector3 MoveDirection {
		get { return moveDirection; }
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

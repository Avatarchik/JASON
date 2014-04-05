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
		}
	}

	void OnCollisionEnter(Collision col) {
		if(pickupWhenColliding != null && col.collider.gameObject.Equals(pickupWhenColliding)) {
			Pickup(col.transform);
		}
	}

	private void Move(Vector3 position) {
		targetPosition = new Vector3(position.x, 0.667f, position.z);
	}

	private void Pickup(Transform target) {
		Debug.Log("pickup!");
	}

	private void HandleClick(RaycastHit hit) {
		if(hit.collider == null)
			return;

		switch(hit.transform.tag) {
		case "Enemy":
			Debug.Log("Enemy clicked");
			break;
		case "Interactable Object":
			pickupWhenColliding = hit.transform.gameObject;
			Move(hit.transform.position);
			break;
		case "Movable Point":
			Move(hit.transform.position);
			break;
		}
	}

	private void HandleLongClick(RaycastHit hit) {
		if(hit.collider == null)
			return;
	}

#if UNITY_EDITOR || !UNITY_ANDROID
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

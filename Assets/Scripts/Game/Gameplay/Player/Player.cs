using UnityEngine;
using System;
using System.Collections;

public class Player:MonoBehaviour {	
	[SerializeField]
	private PlayerData data;
	private PlayerCombat playerCombat;
	
	private Vector3 targetPosition;
	
	void Start() {
		playerCombat = GetComponent<PlayerCombat>();
	
		targetPosition = transform.position;
	}
	
	void Update() {	
		CheckForTouch();
		
		if(targetPosition != transform.position) {
			float step = data.speed * Time.deltaTime;
			
			transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Room")
			other.GetComponent<Room>().Show();
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Room")
			other.GetComponent<Room>().Hide();
	}
	
	private void CheckForTouch() {
		if(Input.GetMouseButtonDown(0)) {
			CheckTouchPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
		
		if(Input.touchCount == 0)
			return;
		
		for(int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch(i);
		
			if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				CheckTouchPosition(Camera.main.ScreenToWorldPoint(touch.position));
			}
		}	
	}
	
	private void CheckTouchPosition(Vector2 position) {
		RaycastHit2D hit = Physics2D.Raycast(position, -Vector2.up);
		
		if(hit) {
			switch(hit.transform.tag) {
			case "Player":
				playerCombat.Defend();
				break;
			case "Enemy":
				playerCombat.StartAttack(hit.transform);
				break;
			default:
				Move(position);
				break;
			}
		} else {
			Move(position);
		}
	}
	
	private void Move(Vector2 position) {
		Debug.Log ("Move");
		playerCombat.Target = null;
	
		targetPosition = position;
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
		
		public float attackDelay;
	}
}

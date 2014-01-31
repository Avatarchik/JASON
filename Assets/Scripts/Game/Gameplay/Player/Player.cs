using UnityEngine;
using System;

public class Player:MonoBehaviour {
	[Serializable]
	public class PlayerData {
		public int health;
		public int speed;
	}
	
	[SerializeField] private PlayerData playerData;
	
	public GameObject touchPoint;	
	public Vector2 fingerPosition;
	public string hitName;
	
	private PlayerCombat playerCombat;
	
	void Start() {
		playerCombat = GetComponent<PlayerCombat>();
	}
	
	void Update() {
		CheckForTouch();
	}
	
	void FixedUpdate() {
		rigidbody2D.velocity = Vector2.zero;
	}
	
	private void CheckForTouch() {
		if(Input.touchCount == 0) {
			hitName = "Idle";
			return;
		}
		
		for(int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch(i);
		
			if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				CheckTouchPosition(Camera.main.ScreenToWorldPoint(touch.position));
			}
		}	
	}
	
	private void CheckTouchPosition(Vector2 position) {
		RaycastHit2D hit = Physics2D.Raycast(position, position);
		
		if(hit) {
			Debug.Log(hit.transform.tag);
		
			switch(hit.transform.tag) {
			case "Player":
				playerCombat.Defend();
				break;
			case "Enemy":
				playerCombat.Attack();
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
		float step = 5 * Time.deltaTime;
		
		transform.position = Vector2.MoveTowards(transform.position, position, step);
		
		hitName = "Walking";
	}

	void OnGUI (){
		GUI.Label(new Rect(0,0,100,100), "" + transform.position.x + " : " + transform.position.y); 
		GUI.Label(new Rect(0,50,100,100), "" + hitName); 
	}
}

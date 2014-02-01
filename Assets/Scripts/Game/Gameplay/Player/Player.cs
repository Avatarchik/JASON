using UnityEngine;
using System;
using System.Collections;

public class Player:MonoBehaviour {	
	[Serializable]
	public class PlayerData {
		public int health;
		public int speed;
		public int attackDamage;
		
		public float attackDelay;
	}

	[SerializeField] private PlayerData playerData;
	
	private Vector3 targetPosition;
	
	private Transform attackTarget;
	
	private bool canAttack;
	private bool attacking;
	
	void Start() {
		targetPosition = transform.position;
	}
	
	void Update() {
		canAttack = false;
	
		CheckForTouch();
		
		if(attackTarget != null) {
			if(Vector3.Distance(transform.position, attackTarget.position) <= 1) {
				targetPosition = transform.position;
				
				canAttack = true;
			} else {
				targetPosition = attackTarget.position;
			}
		}
		
		if(targetPosition != transform.position) {
			float step = playerData.speed * Time.deltaTime;
			
			transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
		}
		
		if(canAttack && !attacking)
			StartCoroutine("Attack");
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
				Defend();
				break;
			case "Enemy":
				InitAttack(hit.transform);
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
		attackTarget = null;
	
		targetPosition = position;
	}
	
	private void Defend() {
		attackTarget = null;
	
		Debug.Log("defend");
	}
	
	private void InitAttack(Transform target) {
		attackTarget = target;
		targetPosition = attackTarget.position;
	}
	
	private IEnumerator Attack() {
		attacking = true;
	
		yield return new WaitForSeconds(playerData.attackDelay);
		
		if(attackTarget != null)
			attackTarget.GetComponent<Enemy>().TakeDamage(playerData.attackDamage);
		
		attacking = false;
	}
}

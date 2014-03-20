using UnityEngine;
using System.Collections;
using System;

public class Bull:Enemy {
	public enum State {
		Idle,
		Charging,
		Attacking,
		Stunned,
		Dead
	}
	
	private Vector3 playerPosition;
	
	private State state;
	private State lastState;
	
	private int rigidbodyForce;
	
	private bool stoppingCharge;
	
	void Update() {
		float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
		
		if(data.Health <= 0) {
			lastState = state;
			state = State.Dead;
			return;
		}
	
		if(state != State.Attacking && state != State.Stunned && state != State.Charging && distanceToPlayer < data.AttackRange) {
			lastState = state;
			state = State.Attacking;
			StartCoroutine(Attacking());
		} else if(state == State.Attacking && distanceToPlayer > data.AttackRange) {
			lastState = state;
			state = State.Idle;
		}
	}
	
	protected override void FixedUpdate() {
		rigidbody.velocity = Vector3.zero;
		Debug.Log(state + " " + lastState);
		switch(state) {
		case State.Charging:
			Charge();
			break;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		switch(collision.gameObject.tag) {
		case "Boss Walls":
			if(state != State.Charging)
				break;
				
			lastState = state;
			state = State.Stunned;
			StartCoroutine(Stunned());
			break;
		case "Player":
			if(state == State.Charging) {
				lastState = state;
				state = State.Attacking;
				player.GetComponent<Player>().Damage(data.AttackDamage * 4, data.StunTime * 2);
			} else if(lastState != State.Charging) {
				player.GetComponent<Player>().Damage(data.AttackDamage * 2, data.StunTime);
			}
			break;	
		}
	}
	
	public void StartAttack() {
		state = State.Idle;
		lastState = State.Idle;
			
		StartCoroutine(Attack());
	}
	
	public override void Damage(int amount) {	
		if(state == State.Stunned)
			data.Health -= amount;
			
		Debug.Log (data.Health);
	}

	private IEnumerator Attack() {
		while(true) {
			if(state == State.Idle && lastState != State.Attacking) {
				state = State.Charging;
				playerPosition = player.transform.position - transform.position;
				LookAt(playerPosition);
			} else if(state == State.Idle && lastState == State.Attacking) {
				lastState = state;
				yield return new WaitForSeconds(2);
			}
			
			yield return new WaitForSeconds(1);
		}
	}
	
	private void Charge() {
		if(!stoppingCharge && Vector3.Distance(transform.position, playerPosition) < 3)
			stoppingCharge = true;
	
		if(!stoppingCharge) {
			rigidbodyForce = 100;
		} else {
			rigidbodyForce -= 20;
		}
		
		rigidbody.AddForce(playerPosition.normalized * (100 * data.RunSpeed));
	}
	
	private IEnumerator Stunned() {
		yield return new WaitForSeconds(3);
		
		lastState = State.Stunned;
		state = State.Idle;
	}
	
	private IEnumerator Attacking() {
		while(state == State.Attacking) {
			yield return new WaitForSeconds(data.AttackDelay);
			
			player.GetComponent<Player>().Damage(data.AttackDamage, data.StunTime);
		}
	}
	
	private void LookAt(Vector3 position) {
		Vector3 localTarget = transform.InverseTransformPoint(position);
		
		float angle = (float)Math.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
		
		Vector3 eurlerAngleVelocity = new Vector3(0, angle, 0);
		Quaternion deltaRotation = Quaternion.Euler(eurlerAngleVelocity * Time.deltaTime);
		
		rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
	}
}

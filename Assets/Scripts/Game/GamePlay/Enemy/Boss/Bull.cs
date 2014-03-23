using UnityEngine;
using System.Collections;
using System;

public class Bull:Boss {
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

	private StoreTransform startTransform;
	private EnemyData startData;

	protected override void Start() {
		base.Start();

		startTransform = transform.SaveWorld();
		startData = data;
	}
	
	protected override void Update() {
		base.Update ();

		if(data.Health <= 0) {
			lastState = state;
			state = State.Dead;
			Debug.Log("DEAD");
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
		base.FixedUpdate();

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
				player.GetComponent<Player>().Damage(data.AttackDamage * 4, data.StunTime * 2, true);
			} else if(lastState != State.Charging) {
				player.GetComponent<Player>().Damage(data.AttackDamage * 2, data.StunTime, false);
			}
			break;	
		}
	}

	public override void Reset() {
		transform.LoadWorld(startTransform);
		data = startData;
	}
	
	public void StartAttack() {
		state = State.Idle;
		lastState = State.Idle;
			
		StartCoroutine(Attack());
	}
	
	public override void Damage(int amount) {
		if(state == State.Dead)
			return;
	
		if(state == State.Stunned) {
			data.Health -= amount;
			DisplayCombatText(amount.ToString(), Color.red, 0.7f);
		} else {
			DisplayCombatText("Blocked", Color.gray, 0.7f);
		}
	}

	private IEnumerator Attack() {
		while(true) {
			if(state == State.Idle && lastState != State.Attacking) {
				state = State.Charging;
				playerPosition = player.transform.position - transform.position;
				transform.LookAt(player.transform.position);
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
		DisplayCombatText("Stunned", Color.yellow, 0.7f);
	
		yield return new WaitForSeconds(3);
		
		lastState = State.Stunned;
		state = State.Idle;
	}
	
	private IEnumerator Attacking() {
		while(state == State.Attacking) {
			yield return new WaitForSeconds(data.AttackDelay);
			
			player.GetComponent<Player>().Damage(data.AttackDamage, data.StunTime, false);
		}
	}
}

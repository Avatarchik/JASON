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
	public Animator animator;
	private Vector3 playerPosition;
	
	private State state;
	private State lastState;

	private StoreTransform startTransform;

	protected override void Start() {
		base.Start();

		startTransform = transform.SaveWorld();
	}

	private IEnumerator SwitchLevel() {
		GameObject.Find("SGUI Manager").GetComponent<SGUIManager>().RemoveAll();

		yield return new WaitForSeconds(3);
		
		PlayerData.Instance.Reset();

		Application.LoadLevel("Boss Door");
	}

	protected override void Update() {
		base.Update ();

		if(data.Health <= 0) {
			lastState = state;
			state = State.Dead;

			StartCoroutine("SwitchLevel");
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
			StartCoroutine("IsCharging");
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
				player.GetComponent<Player>().Damage(data.AttackDamage * 4, data.StunTime * 2, true);
			} else if(lastState != State.Charging) {
				player.GetComponent<Player>().Damage(data.AttackDamage * 2, data.StunTime, false);
			}
			break;	
		}
	}

	public override void Reset() {
		transform.LoadWorld(startTransform);
		data.Reset();
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

	private IEnumerator IsCharging() {
		animator.SetBool("StartCharge",true);
		AudioManager.Instance.SetAudio(AudioManager.AudioFiles.Bull3,true);
		yield return new WaitForSeconds(1.5f);

		animator.SetBool("IsCharging",true);
		
		animator.SetBool("StartCharge",false);
		
		rigidbody.AddForce(playerPosition.normalized * (100 * data.RunSpeed));
	}
	private IEnumerator Stunned() {
		StopCoroutine("IsCharging");
		animator.SetBool("IsCharging",false);
		animator.SetBool("StartCharge",false);
		animator.SetBool("WallHit",true);
		animator.SetBool("IsStunned",true);
		DisplayCombatText("Stunned", Color.yellow, 0.7f);

		yield return new WaitForSeconds(3);
	
		animator.SetBool("IsStunned",false);
		animator.SetBool("WallHit",false);

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

using UnityEngine;
using System.Collections;
using System;

public class Enemy:MonoBehaviour {
	[SerializeField] protected GameObject scrollingCombatText;

	[SerializeField] protected EnemyData data;

	protected GameObject player;

	protected float distanceToPlayer;
	
	protected bool hasMoved;

	protected bool isDead;
	protected bool isChasingPlayer;

	protected virtual void Start() {
		player = GameObject.FindGameObjectWithTag("Player");

		if(player == null)
			throw new System.NullReferenceException("No Game Object found in the scene with the 'Player' tag");
			
		data.Reset();
	}

	protected virtual void Update() {
		distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
		isChasingPlayer = distanceToPlayer <= data.ChaseRange;
	}

	protected virtual void FixedUpdate() {
		if(rigidbody != null){
		rigidbody.velocity = Vector3.zero;
		}
	}

	/** Damage the enemy */
	public virtual void Damage(int amount) {
		data.Health -= amount;
		
		if(data.Health <= 0) {
			DisplayCombatText(amount.ToString(), Color.red, 1);
			OnKilled();
		} else {
			DisplayCombatText(amount.ToString(), Color.yellow, 1);
		}
	}
	
	protected void DisplayCombatText(string text, Color color, float size) {
		Vector3 position = transform.position;
		position.y += 3.5f;
		
		TextMesh popup = (Instantiate(Resources.Load("Prefabs/Misc/Scrolling Combat Text"), position, Quaternion.identity) as GameObject).GetComponent<TextMesh>();
		
		popup.transform.localScale = new Vector3(size, size, size);		
		popup.transform.parent = this.transform;
		
		popup.text = text;
		popup.color = color;
	}

	/** Called when the enemy has been killed */
	protected virtual void OnKilled() {
		isDead = true;
	}

	/** Get the distance to the player */
	public float DistanceToPlayer {
		get { return distanceToPlayer; }
	}

	/** Get wheter or not the enemy is chasing the player */
	public bool ChasingPlayer {
		get { return isChasingPlayer; }
	}

	/** Get wheter or not the enemy has moved since the last update */
	public bool HasMoved {
		get { return hasMoved; }
	}

	/** Get wheter or not the enemy is dead */
	public bool IsDead {
		get { return isDead; }
	}
}
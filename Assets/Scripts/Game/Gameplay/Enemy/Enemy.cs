using UnityEngine;
using System.Collections;
using System;

public class Enemy:MonoBehaviour {	
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
			
		data.Init();
	}

	protected virtual void FixedUpdate() {
		rigidbody.velocity = Vector3.zero;
	
		distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
		isChasingPlayer = distanceToPlayer <= data.ChaseRange;
	}

	/** Damage the enemy */
	public virtual void Damage(int amount) {
		data.Health -= amount;
		
		if(data.Health <= 0)
			OnKilled();
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
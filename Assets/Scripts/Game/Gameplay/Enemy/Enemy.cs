using UnityEngine;
using System.Collections;
using System;

public class Enemy:MonoBehaviour {
	[Serializable]
	public class EnemyData {
		public int health;
		public int speed;
		public int attackDamage;
		
		public float attackDelay;
		public float range;
	}
	
	[SerializeField] protected EnemyData data;

	protected Transform player;
	protected float distanceToPlayer;

	protected bool isDead;
	protected bool playerFound;

	protected virtual void Start() {
		player = GameObject.Find("Player").transform;
	}

	protected virtual void FixedUpdate() {
		rigidbody.velocity = Vector3.zero;
		distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

		playerFound = (distanceToPlayer < data.range) ? true : false;
	}

	public void Damage(int amt) {
		data.health -= amt;
		
		if(data.health <= 0)
			Die();
	}

	public virtual void Die() { }

	public bool IsDead { get { return isDead; } }
}

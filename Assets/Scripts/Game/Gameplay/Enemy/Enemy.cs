using UnityEngine;
using System.Collections;
using System;

public class Enemy:MonoBehaviour {
	[Serializable]
	public class EnemyData {
		public int maxHealth;
		public int health;
		public int speed;
		public int attackDamage;
		
		public float attackDelay;
		public float attackRange;

		public float chaseRange;
	}
	
	[SerializeField] public EnemyData data;

	protected GameObject player;
	protected float distanceToPlayer;

	protected bool dead;
	protected bool moved;

	protected bool playerFound;

	protected virtual void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	protected virtual void FixedUpdate() {
		rigidbody.velocity = Vector3.zero;
		distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

		playerFound = (distanceToPlayer < data.chaseRange) ? true : false;
	}

	public void Damage(int amt) {
		data.health -= amt;
		
		if(data.health <= 0)
			Die();
	}

	public virtual void Die() { Debug.Log("Override me"); }

	public bool Dead { get { return dead; } }

	public bool Moved { get { return moved; } }
}
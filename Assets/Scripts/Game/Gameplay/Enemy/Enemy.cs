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
	}
	
	[SerializeField] protected EnemyData data;
	
	protected Transform player;

	protected float distance;

	protected bool foundPlayer;
	protected bool dead;

	protected virtual void Start() {
		player = GameObject.Find("Player").transform;
	}

	protected virtual void FixedUpdate() {
		rigidbody.velocity = Vector3.zero;

		distance = Vector3.Distance(this.transform.position, player.transform.position);

		foundPlayer = (distance < 10) ? true : false;
	}

	public void Damage(int amt) {
		data.health -= amt;
		
		if(data.health <= 0)
			Die();
	}

	public virtual void Die() { }

	public bool IsDead { get { return dead; } }
}
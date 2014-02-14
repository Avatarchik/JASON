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
	
	[SerializeField] protected EnemyData enemyData;
	public bool foundPlayer;
	public Transform player;
	public float distance;
	public bool isDead;
	public void Start() {
		player = GameObject.Find("Player").transform;
	}
	public void FixedUpdate () {
		rigidbody.velocity = new Vector3(0,0,0);
		distance = Vector3.Distance(this.transform.position, player.transform.position);
		if(distance < 10){
			foundPlayer = true;
		}else{
			foundPlayer = false;
		}
	}
	public void TakeDamage(int amt) {
		enemyData.health -= amt;
		
		if(enemyData.health <= 0)
			Die();
	}
	void OnTriggerEnter(Collider coll){
		if(coll.name == "WeaponCollision"){
			Debug.Log("Collision");
			int dmg = player.GetComponent<Player>().Data.attackDamage;
			TakeDamage(dmg);
		}
	}
	public virtual void Die() {
		Debug.Log("You need to overide me");
	}
}

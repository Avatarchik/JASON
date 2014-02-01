using UnityEngine;
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

	void Start() {
	
	}
	
	void Update() {
	
	}
	
	public void TakeDamage(int amt) {
		enemyData.health -= amt;
		
		if(enemyData.health <= 0)
			Die();
	}
	
	private void Die() {
		Destroy(gameObject);
	}
}

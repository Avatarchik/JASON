using UnityEngine;
using System;

[Serializable]
public class EnemyData {
	[SerializeField] private int initialHealth;
	[SerializeField] private int initialRunSpeed;
	[SerializeField] private int initialAttackDamage;
	
	[SerializeField] private float initialAttackDelay;
	[SerializeField] private float initialAttackRange;
	[SerializeField] private float initialChaseRange;
	[SerializeField] private float initialStunTime;
	
	private int health;
	private int runSpeed;
	private int attackDamage;
	
	private float attackDelay;
	private float attackRange;
	private float chaseRange;
	private float stunTime;
	
	public void Init() {
		health = initialHealth;
		runSpeed = initialRunSpeed;
		attackDamage = initialAttackDamage;
		
		attackDelay = initialAttackDelay;
		attackRange = initialAttackRange;
	}
	
	// Set and/or get the current health of the enemy */
	public int Health {
		set { health = value; }
		get { return health; }
	}
	
	// Set and/or get the current speed of the enemy */
	public int RunSpeed {
		set { runSpeed = value; }
		get { return runSpeed; }
	}
	
	// Set and/or get the current attack damage of the enemy */
	public int AttackDamage {
		set { attackDamage = value; }
		get { return attackDamage; }
	}
	
	// Set and/or get the current attack delay of the enemy */
	public float AttackDelay {
		set { attackDelay = value; }
		get { return attackDelay; }
	}
	
	// Set and/or get the current attack range of the enemy */
	public float AttackRange {
		set { attackRange = value; }
		get { return attackRange; }
	}
	
	// Set and/or get the current chase range of the enemy */
	public float ChaseRange {
		set { chaseRange = value; }
		get { return chaseRange; }
	}
	
	// Set and/or get the current chase range of the enemy */
	public float StunTime {
		set { stunTime = value; }
		get { return stunTime; }
	}
	
	// Get the initial health of the enemy */
	public int InitialHealth {
		get { return initialHealth; }
	}
	
	// Get the initial speed of the enemy */
	public int InitialRunSpeed {
		get { return initialRunSpeed; }
	}
	
	// Get the initial attack damage of the enemy */
	public int InitialAttackDamage {
		get { return initialAttackDamage; }
	}
	
	// Get the initial attack delay of the enemy */
	public float InitialAttackDelay {
		get { return initialAttackDelay; }
	}
	
	// Get the initial attack range of the enemy */
	public float InitialAttackRange {
		get { return initialAttackRange; }
	}
	
	// Get the initial chase range of the enemy */
	public float InitialChaseRange {
		get { return initialChaseRange; }
	}
	
	// Get the initial chase range of the enemy */
	public float InitialStunTime {
		get { return initialStunTime; }
	}
}

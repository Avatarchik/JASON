using UnityEngine;

public class PlayerData:Singleton<PlayerData> {
	[SerializeField] private int initialRunSpeed;
	[SerializeField] private int initialHealth;
	[SerializeField] private int initialAttackDamage;

	[SerializeField] private float initialAttackDelay;
	[SerializeField] private float initialAttackRange;

	private int health;
	private int runSpeed;
	private int attackDamage;

	private float attackDelay;
	private float attackRange;

	void Start() {
		Reset();
	}

	public void Reset() {
		health = initialHealth;
		runSpeed = initialRunSpeed;
		attackDamage = initialAttackDamage;
		
		attackDelay = initialAttackDelay;
		attackRange = initialAttackRange;
	}

	/** Set and/or get the current run speed of the player */
	public int RunSpeed {
		set { runSpeed = value; }
		get { return runSpeed; }
	}

	/** Set and/or get the current health of the player */
	public int Health { 
		set { health = value; }
		get { return health; }
	}

	/** Set and/or get the current attack damage of the player */
	public int AttackDamage {
		set { attackDamage = value; }
		get { return attackDamage; }
	}

	/** Set and/or get the current attack delay of the player */
	public float AttackDelay {
		set { attackDelay = value; }
		get { return attackDelay; }
	}

	/** Set and/or get the current attack range of the player */
	public float AttackRange {
		set { attackRange = value; }
		get { return attackRange; }
	}
	
	/** Get the initial health of the player */
	public int InitialHealth {
		get { return initialHealth; }
	}

	/** Get the initial run speed of the player */
	public int InitialRunSpeed {
		get { return initialRunSpeed; }
	}

	/** Get the initial attack damage of the player */
	public int InitialAttackDamage {
		get { return initialAttackDamage; }
	}
	
	/** Get the initial attack delay of the player */
	public float InitialAttackDelay {
		get { return initialAttackDelay; }
	}

	/** Get the initial attack range of the player */
	public float InitialAttackRange {
		get { return initialAttackRange; }
	}
}

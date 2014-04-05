using UnityEngine;

public class PlayerData:Singleton<PlayerData> {
	[SerializeField] private int initialWalkSpeed;
	[SerializeField] private int initialRunSpeed;
	[SerializeField] private int initialHealth;
	[SerializeField] private int initialAttackDamage;

	[SerializeField] private float initialAttackDelay;

	private float health;
	private int walkSpeed;
	private int runSpeed;
	private int attackDamage;

	private float attackDelay;
	private float attackRange;

	void Start() {
		Reset();
	}

	/** Reset the player data to the defaults */
	public void Reset() {
		health = initialHealth;
		walkSpeed = initialWalkSpeed;
		runSpeed = initialRunSpeed;
		attackDamage = initialAttackDamage;
		
		attackDelay = initialAttackDelay;
	}

	/** Set and/or get the current walk speed of the player */
	public int WalkSpeed {
		set { walkSpeed = value; }
		get { return walkSpeed; }
	}

	/** Set and/or get the current walk speed of the player */
	public int RunSpeed {
		set { runSpeed = value; }
		get { return runSpeed; }
	}

	/** Set and/or get the current health of the player */
	public float Health { 
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
	public float InitialHealth {
		get { return initialHealth; }
	}

	/** Get the initial walk speed of the player */
	public int InitialWalkSpeed {
		get { return initialWalkSpeed; }
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
}

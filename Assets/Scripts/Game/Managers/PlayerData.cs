using UnityEngine;

public class PlayerData:Singleton<PlayerData> {
	public Inventory inventory;

	public int maxHealth;
	private int health;

	public int speed;

	public int chargeMultiplier;

	public int attackDamage;
	public float attackDelay;
	public float attackRange;

	public float damageDelay;

	void Start() {
		health = maxHealth;
	}

	public int Health { 
		set { health = value; }
		get { return health; }
	}
}

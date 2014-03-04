using UnityEngine;
using System.Collections;
using System;

public class PlayerCombat:MonoBehaviour {
	[SerializeField] private GameObject weaponCollisionArea;

	private Player player;

	private Enemy targetEnemy;
	private Destructable targetDestructable;

	private bool canAttack;
	private bool attacking;
	private bool defending;
	
	void Start() {
		player = GetComponent<Player>();
	}

	void Update() {
		canAttack = false;

		if(targetEnemy != null) {
			if(targetEnemy.Dead) {
				player.PlayerAnimation.SetInteger("Attack", 0);
				targetEnemy = null;

				attacking = false;
				StopCoroutine("AttackDelay");
			} else {
				if(targetEnemy.Moved)
					if(Vector3.Distance(transform.position, targetEnemy.transform.position) >= player.PlayerData.attackRange / 2)
						player.TargetPosition = targetEnemy.transform.position;

				if(Vector3.Distance(transform.position, targetEnemy.transform.position) <= player.PlayerData.attackRange) {
					player.TargetPosition = transform.position;
					canAttack = !defending;
				}
			}
		}
	}
	
	public void Defend(bool state) {
		DeselectTarget();

		defending = state;

		player.PlayerAnimation.SetBool("IsBlocking", state);
	}

	public void Attack(GameObject target, string type) {
		DeselectTarget();

		switch(type) {
		case "Enemy":
			targetEnemy = target.GetComponent<Enemy>();
			break;
		case "Destructable":
			targetDestructable = target.GetComponent<Destructable>();
			break;
		}

		player.TargetPosition = target.transform.position;
		attacking = true;

		StartCoroutine("AttackDelay");

		Debug.Log ("Start attack");
	}

	private void DeselectTarget() {
		targetEnemy = null;
		targetDestructable = null;
	}

	private IEnumerator AttackDelay() {
		while(attacking) {
			if(canAttack) {
				Debug.Log("Can Attack");
				int randomAnimation = UnityEngine.Random.Range(1, 4);

				Collider[] hits = Physics.OverlapSphere(weaponCollisionArea.transform.position, 1);

				player.PlayerAnimation.SetInteger("Attack", randomAnimation);

				foreach(Collider collider in hits)
					if(collider.CompareTag("Enemy"))
						collider.GetComponent<Enemy>().Damage(player.PlayerData.attackDamage);

				yield return new WaitForSeconds(0.01f);

				player.PlayerAnimation.SetInteger("Attack", 0);

				yield return new WaitForSeconds(player.PlayerData.attackDelay);
			} else {
				Debug.Log("Cant Attack");
				yield return new WaitForSeconds(0.01f);
			}
		}
	}

	public Enemy TargetEnemy {
		set { targetEnemy = value; }
		get { return targetEnemy; }
	}

	public Destructable TargetDestructable {
		set { targetDestructable = value; }
		get { return targetDestructable; }
	}

	public bool Attacking {	get { return attacking; } }

	public bool Defending { get { return defending; } }
}

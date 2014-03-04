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
				DeselectTarget();
			} else {
				if(targetEnemy.Moved)
					if(Vector3.Distance(transform.position, targetEnemy.transform.position) >= player.PlayerData.attackRange / 2)
						player.TargetPosition = targetEnemy.transform.position;

				if(Vector3.Distance(transform.position, targetEnemy.transform.position) <= player.PlayerData.attackRange) {
					player.TargetPosition = transform.position;
					canAttack = !defending;
				}
			}
		} else if(targetDestructable != null) {
			if(targetDestructable.Destroyed) {
				DeselectTarget();
			} else if(Vector3.Distance(transform.position, targetDestructable.transform.position) <= player.PlayerData.attackRange) {
				player.TargetPosition = transform.position;
				canAttack = !defending;
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
			StartCoroutine("AttackEnemyDelay");
			break;
		case "Destructable":
			targetDestructable = target.GetComponent<Destructable>();
			StartCoroutine("AttackDestructableDelay");
			break;
		}

		player.TargetPosition = target.transform.position;
	}

	private void DeselectTarget() {
		player.PlayerAnimation.SetInteger("Attack", 0);

		targetEnemy = null;
		targetDestructable = null;

		attacking = false;

		StopCoroutine("AttackEnemyDelay");
		StopCoroutine("AttackDestructableDelay");
	}

	private IEnumerator AttackEnemyDelay() {
		attacking = true;

		while(attacking) {
			if(canAttack) {
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
				yield return new WaitForSeconds(0.01f);
			}
		}
	}

	private IEnumerator AttackDestructableDelay() {
		attacking = true;

		while(attacking) {
			if(canAttack) {
				int randomAnimation = UnityEngine.Random.Range(1, 4);
				
				Collider[] hits = Physics.OverlapSphere(weaponCollisionArea.transform.position, 1);
				
				player.PlayerAnimation.SetInteger("Attack", randomAnimation);
				
				foreach(Collider collider in hits)
					if(collider.CompareTag("Destructable"))
						collider.GetComponent<Destructable>().Damage(player.PlayerData.attackDamage);
				
				yield return new WaitForSeconds(0.01f);
				
				player.PlayerAnimation.SetInteger("Attack", 0);
				
				yield return new WaitForSeconds(player.PlayerData.attackDelay);
			} else {
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

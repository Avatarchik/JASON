using UnityEngine;
using System.Collections;
using System;
using SGUI;

public class PlayerCombat:MonoBehaviour {
	private Player player;
	private PlayerWeaponCollisionManager weaponCollisionManager;

	private GameObject weaponCollisionArea;
	private GameObject target;

	private bool canAttack;
	private bool attacking;
	private bool defending;

	void Start() {
		weaponCollisionArea = transform.FindChild("Weapon Collision Area").gameObject;

		player = GetComponent<Player>();
		weaponCollisionManager = weaponCollisionArea.GetComponent<PlayerWeaponCollisionManager>();
	}

	void Update() {
		canAttack = false;

		if(target != null) {
			Enemy enemy = target.GetComponent<Enemy>();

			if(enemy.IsDead) {
				DeselectTarget();
			} else {
				if(weaponCollisionManager.Colliders.Length > 0) {
					foreach(Collider collider in weaponCollisionManager.Colliders) {
						if(collider.gameObject.Equals(target)) {
							player.TargetPosition = transform.position;
							canAttack = !defending;
							break;
						}
					}
				}
			}
		}
	}
	
	public void Defend(bool state) {
		DeselectTarget();

		defending = state;

		player.PlayerAnimation.SetBool("IsBlocking", state);
		
		if(defending)
			player.TargetPosition = transform.position;
	}

	public void StartAttack(GameObject target) {
		DeselectTarget();

		this.target = target;
		player.TargetPosition = target.transform.position;

		StartCoroutine(Attack());
	}

	private void DeselectTarget() {
		player.PlayerAnimation.SetInteger("Attack", 0);

		target = null;

		attacking = false;

		StopCoroutine("Attack");
	}

	private IEnumerator Attack() {
		attacking = true;

		while(attacking) {
			if(canAttack) {
				int randomAnimation = UnityEngine.Random.Range(1, 4);

				player.PlayerAnimation.SetInteger("Attack", randomAnimation);

				target.GetComponent<Enemy>().Damage(PlayerData.Instance.AttackDamage);

				yield return new WaitForSeconds(0.01f);

				player.PlayerAnimation.SetInteger("Attack", 0);

				yield return new WaitForSeconds(PlayerData.Instance.AttackDelay);
			} else {
				yield return new WaitForSeconds(0.01f);
			}
		}
	}

	public GameObject Target {
		set { target = value; }
		get { return target; }
	}

	public bool Attacking {	get { return attacking; } }

	public bool Defending { get { return defending; } }

	public GameObject WeaponCollisionArea {
		get { return weaponCollisionArea; }
	}
}

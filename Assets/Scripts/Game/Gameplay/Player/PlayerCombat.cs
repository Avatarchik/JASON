﻿using UnityEngine;
using System.Collections;
using System;
using SGUI;

public class PlayerCombat:MonoBehaviour {
	[SerializeField] Collider shieldCollider;

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
	
	/** Defend */
	public void Defend(bool state) {
		DeselectTarget();

		defending = state;
		shieldCollider.enabled = state;

		player.PlayerAnimation.SetBool("IsBlocking", state);
		
		if(defending)
			player.TargetPosition = transform.position;
	}

	/** Start attacking */
	public void StartAttack(GameObject target) {
		DeselectTarget();

		this.target = target;
		player.TargetPosition = target.transform.position;

		weaponCollisionArea.collider.enabled = true;

		StartCoroutine(Attack());
	}

	/** Deselect the current target */
	private void DeselectTarget() {
		player.PlayerAnimation.SetInteger("Attack", 0);

		target = null;

		attacking = false;

		weaponCollisionArea.collider.enabled = false;

		StopCoroutine("Attack");
	}

	/** Attack */
	private IEnumerator Attack() {
		attacking = true;

		while(attacking) {
			if(canAttack) {
				int randomAnimation = UnityEngine.Random.Range(1, 4);
				Debug.Log(canAttack);
				player.PlayerAnimation.SetInteger("Attack", randomAnimation);
				AudioManager.Instance.SetAudio(AudioManager.AudioFiles.SwordSlash,true);
				target.GetComponent<Enemy>().Damage(PlayerData.Instance.AttackDamage);
				
				yield return new WaitForSeconds(0.01f);

				player.PlayerAnimation.SetInteger("Attack", 0);

				yield return new WaitForSeconds(PlayerData.Instance.AttackDelay);
			} else {
				yield return new WaitForSeconds(0.01f);
			}
		}
	}

	/** Set and/or get the target of the player */
	public GameObject Target {
		set { target = value; }
		get { return target; }
	}

	/** Get whether the player is currently attacking */
	public bool Attacking {	
		get { return attacking; } 
	}

	/** Get wheter the player is currently defending */
	public bool Defending {
		get { return defending; }
	}

	/** Get the weapon collision area */
	public GameObject WeaponCollisionArea {
		get { return weaponCollisionArea; }
	}

	/** Get wheter or not the player is currently in combat */
	public bool InCombat {
		get { return target != null; }
	}
}

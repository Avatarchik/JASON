using UnityEngine;
using System.Collections;

public class Warrior:Enemy {
	[SerializeField] private GameObject overlapPosition;

	private Animator enemyAnimation;

	private bool isAttackingPlayer;

	protected override void Start() {
		base.Start();

		enemyAnimation = GetComponentInChildren<Animator>();
	}
	
	protected override void FixedUpdate() {
		base.FixedUpdate();

		Animate();

		if(isDead) {
			if(enemyAnimation.GetCurrentAnimatorStateInfo(0).IsName("DeadIdle")) {
				enemyAnimation.enabled = false;

				gameObject.tag = "Corpse";

				Destroy(GetComponent<Rigidbody>());
				Destroy(GetComponent<CapsuleCollider>());
			}

			return;
		}

		if(isChasingPlayer)
			moveToPlayer();
	}

	/** Called when the enemy has been killed */
	protected override void OnKilled() {
		base.OnKilled();

		StopCoroutine("Attack");

		enemyAnimation.SetInteger("State", 991);
		enemyAnimation.SetBool("IsDead", true);
	}

	/** Determine which animation to play */
	private void Animate() {
		if(!isChasingPlayer) {
			enemyAnimation.SetInteger("State", 1);
		} else if(distanceToPlayer > 4) {
			enemyAnimation.SetInteger("State", 2);
		} else {
			if(!isAttackingPlayer)
				StartCoroutine("Attack");
		}
	}

	/** Attack the player */
	private IEnumerator Attack() {
		int random = Random.Range(3, 5);

		isAttackingPlayer = true;

		enemyAnimation.SetInteger("State", random);

		yield return new WaitForSeconds(enemyAnimation.GetCurrentAnimatorStateInfo(0).length);

		enemyAnimation.SetInteger("State",1);

		Collider[] hits = Physics.OverlapSphere(overlapPosition.transform.position, 1);

		for(int i = 0; i < hits.Length; i++) {
			if(hits[i].name == "Player")
				player.GetComponent<Player>().Damage(data.AttackDamage, data.StunTime, false);
		}

		yield return new WaitForSeconds(data.AttackDelay + Random.Range(0.3f, 1.0f));

		isAttackingPlayer = false;
	}

	/** Move towards the player */
	private void moveToPlayer() {
		if(Vector3.Distance(transform.position, player.transform.position) <= data.AttackDamage)
			return;

		hasMoved = true;

		Vector3 playerPos = player.transform.position;
		Vector3 lookPos = playerPos - transform.position;

		Quaternion rotation = Quaternion.LookRotation(lookPos);

		float step = data.RunSpeed * Time.deltaTime;

		rotation.x = 0;
		rotation.z = 0;

		if(enemyAnimation.GetCurrentAnimatorStateInfo(0).IsName("Run")) {
			transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
		}
	}
}

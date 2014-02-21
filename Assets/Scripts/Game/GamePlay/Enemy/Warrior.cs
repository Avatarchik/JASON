using UnityEngine;
using System.Collections;

public class Warrior:Enemy {
	[SerializeField] private Animator enemyAnimation;
	[SerializeField] private GameObject overlapPosition;

	private bool isAttacking;
	
	protected override void FixedUpdate() {
		if(isDead) {
			if(enemyAnimation.GetCurrentAnimatorStateInfo(0).IsName("DeadIdle")) {
				enemyAnimation.enabled = false;

				gameObject.tag = "Corpse";

				Destroy(GetComponent<Rigidbody>());
				Destroy(GetComponent<CapsuleCollider>());
			}

			return;
		}

		base.FixedUpdate();

		Animate();

		if(playerFound) {
			moveToPlayer();
		} else {
			// TODO: Do own behavior if player isn't found
		}
	}

	/** Kill the enemy */
	public override void Die() {
		isDead = true;

		StopCoroutine("Attack");

		enemyAnimation.SetInteger("State", 991);
		enemyAnimation.SetBool("IsDead", true);
	}

	/** Determine which animation to play */
	private void Animate() {
		if(distanceToPlayer > data.range) {
			enemyAnimation.SetInteger("State", 1);
		} else if (distanceToPlayer > 4) {
			enemyAnimation.SetInteger("State", 2);
		} else {
			if(!isAttacking)
				StartCoroutine("Attack");
		}
	}

	/** Attack */
	private IEnumerator Attack() {
		int random = Random.Range(3, 5);
		isAttacking = true;

		enemyAnimation.SetInteger("State", random);

		yield return new WaitForSeconds(enemyAnimation.GetCurrentAnimatorStateInfo(0).length);

		enemyAnimation.SetInteger("State",1);

		Collider[] hits = Physics.OverlapSphere(overlapPosition.transform.position, 1);

		for(int i = 0; i < hits.Length; i++) {
			if(hits[i].name == "Player")
				player.GetComponent<Player>().getDamage(data.attackDamage);
		}

		yield return new WaitForSeconds(data.attackDelay + Random.Range(0.3f, 1.0f));

		isAttacking = false;
	}

	/** Move towards the player */
	private void moveToPlayer() {
		if(player.transform.position == transform.position)
			return;

		Vector3 playerPos = player.transform.position;
		Vector3 lookPos = playerPos - transform.position;

		Quaternion rotation = Quaternion.LookRotation(lookPos);

		float step = data.speed * Time.deltaTime;

		rotation.x = 0;
		rotation.z = 0;

		if (enemyAnimation.GetCurrentAnimatorStateInfo(0).IsName("Run")) {
			transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
		}
	}
}

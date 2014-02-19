using UnityEngine;
using System.Collections;

public class EnemyWarrior:Enemy {
	[SerializeField] private Animator enemyAnimation;

	[SerializeField] private GameObject overlapPosition;

	private bool isAttacking;

	protected override void Start() {
		base.Start();
	}

	protected override void FixedUpdate() {
		if(dead) {
			if(enemyAnimation.GetCurrentAnimatorStateInfo(0).IsName("DeadIdle")){
				enemyAnimation.enabled = false;
				gameObject.tag = "Corpse";

				Destroy(GetComponent<Rigidbody>());
				Destroy(GetComponent<SphereCollider>());
			}

			return;
		}

		base.FixedUpdate();

		animatePlayer();

		if(foundPlayer){
			moveToPlayer();
		} else {
			// TODO: Do own behavior
		}
	}

	public override void Die() {
		dead = true;

		StopCoroutine("Attack");

		enemyAnimation.SetInteger("State", 991);
		enemyAnimation.SetBool("IsDead", true);

		StartCoroutine(DeathIdle(1));
	}

	private void animatePlayer() {
		if(distance > 10) {
			enemyAnimation.SetInteger("State", 1);
		} else if (distance > 4) {
			enemyAnimation.SetInteger("State", 2);
		} else {
			if(!isAttacking)
				StartCoroutine("Attack");
		}
	}

	private IEnumerator DeathIdle(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	}

	private IEnumerator Attack() {
		int random = Random.Range(3, 5);

		isAttacking = true;

		enemyAnimation.SetInteger("State", random);

		yield return new WaitForSeconds(enemyAnimation.GetCurrentAnimatorStateInfo(0).length);

		enemyAnimation.SetInteger("State", 1);

		Collider[] hits = Physics.OverlapSphere(overlapPosition.transform.position, 1);

		for(int i = 0; i < hits.Length; i++)
			if(hits[i].name == "Player")
				player.GetComponent<Player>().getDamage(data.attackDamage);

		yield return new WaitForSeconds(data.attackDelay + Random.Range(0.3f, 1.0f));

		isAttacking = false;
	}

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

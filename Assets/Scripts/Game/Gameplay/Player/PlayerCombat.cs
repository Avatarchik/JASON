using UnityEngine;
using System.Collections;

public class PlayerCombat:MonoBehaviour {
	[SerializeField] private GameObject weaponCollision;

	private Player player;

	private Transform target;

	private Enemy currentEnemy;

	private bool canAttack;

	private bool attacking;
	private bool defending;

	void Start() {
		player = GetComponent<Player>();
	}
	
	void Update() {
		if(currentEnemy != null){
			if(currentEnemy.IsDead){
				target = null;
				currentEnemy = null;
				StopCoroutine("Attack");
				player.playerAnimation.SetInteger("Attack", 0);
			}
		}

		canAttack = false;
	
		if(target != null) {
			if(currentEnemy == null)
				currentEnemy = target.GetComponent<Enemy>();

			if(Vector3.Distance(transform.position, target.position) <= 2) {
				player.TargetPosition = transform.position;
				
				canAttack = true;
			} else {
				player.TargetPosition = target.position;
			}
		}
		
		if(canAttack && !attacking && currentEnemy != null)
			StartCoroutine(Attack(0.01f,1));
	}
	
	internal void Defend(bool state) {
		target = null;
		player.playerAnimation.SetBool("IsBlocking",state);
	}
	
	internal void StartAttack(Transform target) {
		this.target = target;

		player.TargetPosition = target.position;
	}
	
	private IEnumerator Attack(float duration,float hitDelay) {
		int randomAnimation = Random.Range(1, 4);

		player.playerAnimation.SetInteger("Attack", randomAnimation);
		attacking = true;

		Collider[] hits = Physics.OverlapSphere(weaponCollision.transform.position, 1);

		for(int i = 0; i < hits.Length; i++) {
			if(hits[i].tag == "Enemy") {
				hits[i].GetComponent<Enemy>().Damage(player.Data.attackDamage);
			}
		}

		yield return new WaitForSeconds(duration);

		player.playerAnimation.SetInteger("Attack", 0);

		yield return new WaitForSeconds(hitDelay);

		attacking = false;
	}
	
	public Transform Target {
		get { return target; }
		set { target = value; }
	}
}

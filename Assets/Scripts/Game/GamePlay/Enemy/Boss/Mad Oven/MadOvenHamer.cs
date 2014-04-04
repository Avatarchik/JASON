using UnityEngine;
using System.Collections;

public class MadOvenHamer:Boss {
	private bool attacking;
	public Animator animatie;
	void OnCollisionStay(Collision collision) {
		
		if(!attacking)
			return;
		
		if(collider.gameObject.CompareTag("Player"))
			collider.GetComponent<Player>().Damage(2, 1, false);
	}

	public void StartAttack() {
		StartCoroutine(Attack());
	}

	public override void Damage(int amount) {

	}

	private IEnumerator Attack() {
		while(true) {
			yield return new WaitForSeconds(data.AttackDelay / 2);
			animatie.SetInteger("Attack",Random.Range(0,4));
			attacking = true;
			int random = Random.Range(0,10);
			if(random >= 4){
				animatie.SetInteger("Attack",Random.Range(0,4));
				yield return new WaitForSeconds(2);
				animatie.SetInteger("Attack",0);
			}
			yield return new WaitForSeconds(data.AttackDelay / 2);
			animatie.SetInteger("Attack",0);
			attacking = false;
		}
	}
}

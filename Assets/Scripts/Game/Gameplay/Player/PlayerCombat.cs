using UnityEngine;
using System.Collections;

public class PlayerCombat:MonoBehaviour {
	private Player player;

	private Transform target;
	
	private bool canAttack;
	private bool attacking;
	
	void Start() {
		player = GetComponent<Player>();
	}
	
	void Update() {
		canAttack = false;
	
		if(target != null) {
			if(Vector3.Distance(transform.position, target.position) <= 1) {
				player.TargetPosition = transform.position;
				
				canAttack = true;
			} else {
				player.TargetPosition = target.position;
			}
		}
		
		if(canAttack && !attacking)
			StartCoroutine("Attack");
	}
	
	internal void Defend() {
		target = null;
		
		Debug.Log("Defend");
	}
	
	internal void StartAttack(Transform target) {
		Debug.Log("StartAttack");
		this.target = target;
		player.TargetPosition = target.position;
	}
	
	private IEnumerator Attack() {
		attacking = true;
		
		yield return new WaitForSeconds(player.Data.attackDelay);
		
		if(target != null)
			target.GetComponent<Enemy>().TakeDamage(player.Data.attackDamage);
		
		attacking = false;
	}
	
	public Transform Target {
		get { return target; }
		set { target = value; }
	}
}

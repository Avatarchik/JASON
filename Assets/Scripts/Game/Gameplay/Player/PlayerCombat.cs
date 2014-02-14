using UnityEngine;
using System.Collections;

public class PlayerCombat:MonoBehaviour {
	private Player player;

	private Transform target;
	
	private bool canAttack;
	private bool attacking;
	private bool isAttacking;
	public GameObject weaponCollision;
	void Start() {
		player = GetComponent<Player>();
	}
	
	void Update() {
		canAttack = false;
	
		if(target != null) {
			if(Vector3.Distance(transform.position, target.position) <= 2) {
				player.TargetPosition = transform.position;
				
				canAttack = true;
			} else {
				player.TargetPosition = target.position;
			}
		}
		
		if(canAttack && !attacking)
			StartCoroutine(Attack(0.01f,1));
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
	
	private IEnumerator Attack(float duration,float hitDelay) {
		attacking = true;
		weaponCollision.renderer.enabled = true;
		weaponCollision.collider.enabled = true;
		yield return new WaitForSeconds(duration);
		weaponCollision.collider.enabled = false;
		weaponCollision.renderer.enabled = false;
		yield return new WaitForSeconds(hitDelay);
		attacking = false;
	}
	
	public Transform Target {
		get { return target; }
		set { target = value; }
	}
}

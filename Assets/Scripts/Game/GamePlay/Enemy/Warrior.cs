using UnityEngine;
using System.Collections;

public class Warrior : Enemy {
	public Animator enemyAnimation;
	public bool isAttacking;
	public GameObject overlapPosition;
	// Use this for initialization
	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	new void FixedUpdate () {
		if(!isDead){
			base.FixedUpdate();

		animatePlayer();
		if(foundPlayer){
			moveToPlayer();
		}else{
			//Do Own Behaviour
		}
		}
		else{
			if(enemyAnimation.GetCurrentAnimatorStateInfo(0).IsName("DeadIdle")){
				enemyAnimation.enabled = false;
				Destroy(GetComponent<Rigidbody>());
				gameObject.tag = "Corpse";
				Destroy(GetComponent<SphereCollider>());
			}
		}
	}
	override public void Die(){
		isDead = true;
		StopCoroutine("Attack");
		enemyAnimation.SetInteger("State",991);
		enemyAnimation.SetBool("IsDead",true);
		StartCoroutine(DeathIdle(1));
	}
	void animatePlayer(){
		if(distance > 10){
			enemyAnimation.SetInteger("State",1);
		}else if (distance > 4){
			enemyAnimation.SetInteger("State",2);
		}else{
			if(!isAttacking){
				StartCoroutine("Attack");
			}
		}
	}
	IEnumerator DeathIdle(float waitTime){
		yield return new WaitForSeconds(waitTime);
		//enemyAnimation.enabled = false;
	}
	IEnumerator Attack(){
		isAttacking = true;
		int random = Random.Range(3,5);
		Debug.Log(random);
		enemyAnimation.SetInteger("State",random);
		yield return new WaitForSeconds(enemyAnimation.GetCurrentAnimatorStateInfo(0).length);
		enemyAnimation.SetInteger("State",1);
		Collider[] hits = Physics.OverlapSphere(overlapPosition.transform.position, 1);
		for(int i = 0; i < hits.Length; i++){
			if(hits[i].name == "Player"){
				player.GetComponent<Player>().getDamage(enemyData.attackDamage);
			}
		}
		yield return new WaitForSeconds(enemyData.attackDelay + Random.Range(0.3f,1.0f));
		isAttacking = false;
	}
	void moveToPlayer(){
		if(player.transform.position != transform.position) {
			Vector3 playerPos = player.transform.position;
			float step = enemyData.speed * Time.deltaTime;
			Vector3 lookPos = playerPos - transform.position;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			rotation.x = 0;
			rotation.z = 0;
			if (enemyAnimation.GetCurrentAnimatorStateInfo(0).IsName("Run")){

				transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
			}
			if(transform.position != playerPos){
				//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 30);
			}
		}
	}
}

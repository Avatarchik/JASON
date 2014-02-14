using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public int damage;
	public Player player;
	// Use this for initialization

	public void setBox(bool state,int dmg){
		damage = dmg;
		collider.enabled = state;
	}

	void OnTriggerStay(Collider coll){
		Debug.Log(coll.gameObject.name);
		if(coll.gameObject.tag == "Player"){
			player.Data.health -= damage;
		}
		setBox(false,0);
	}
}

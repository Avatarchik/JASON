using UnityEngine;
using System.Collections;

public class FireCollision : MonoBehaviour {

	void OnCollisionStay(Collision collision) {
		if(collider.gameObject.name != "FireAttack"){
		Debug.Log(collider.gameObject.name);
		}
		if(collider.gameObject.CompareTag("Player"))
			collider.GetComponent<Player>().Damage(0.3f, 1, false);
	}
}

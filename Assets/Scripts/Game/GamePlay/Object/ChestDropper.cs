using UnityEngine;
using System.Collections;

public class ChestDropper:MonoBehaviour {
	[SerializeField] private GameObject art;

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Player")) {
			GameObject bossKey = Instantiate(art, transform.position, Quaternion.identity) as GameObject;
			ThrowableObject to = bossKey.AddComponent<ThrowableObject>();

			to.Type = ThrowableObject.ObjectType.BossKey;
		}
	}
}

using UnityEngine;
using System.Collections;

public class BullStarter:MonoBehaviour {
	[SerializeField] private GameObject boss;

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.CompareTag("Player")) {
			boss.GetComponent<Bull>().StartAttack();
			
			this.collider.isTrigger = false;
			
			Transform spawn = transform.Find("Player Spawn");
			
			collider.gameObject.transform.position = spawn.position;
			collider.gameObject.transform.rotation = spawn.rotation;

			Player player = collider.GetComponent<Player>();

			player.TargetPosition = spawn.position;
			player.PlayerCamera.TargetPosition = new Vector3(-10, 15, player.PlayerCamera.TargetPosition.z);
			player.InBossRoom = true;
		}
	}
}

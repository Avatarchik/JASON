using UnityEngine;
using System.Collections;

public class BullStarter:MonoBehaviour {
	[SerializeField] private GameObject bull;

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.CompareTag("Player")) {
			Transform playerSpawn = transform.FindChild("Player Spawn");

			Player player = collider.GetComponent<Player>();
			Bull boss = bull.GetComponent<Bull>();

			this.collider.isTrigger = false;

			player.transform.position = playerSpawn.position;
			player.transform.rotation = playerSpawn.rotation;

			player.PlayerCamera.TargetPosition = new Vector3(-10, 15, player.PlayerCamera.TargetPosition.z);
			player.TargetPosition = playerSpawn.position;
			player.CurrentBoss = boss;
			player.InBossRoom = true;
			
			StartCoroutine(StartDelay(boss));
		}
	}

	private IEnumerator StartDelay(Bull boss) {
		yield return new WaitForSeconds(2.0f);

		boss.StartAttack();
	}
}

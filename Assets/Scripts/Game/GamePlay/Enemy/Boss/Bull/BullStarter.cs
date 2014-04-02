using UnityEngine;
using System.Collections;

public class BullStarter:MonoBehaviour {
	[SerializeField] private GameObject bull;

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.CompareTag("Player")) {
			Player player = collider.GetComponent<Player>();
			Bull boss = bull.GetComponent<Bull>();

			this.collider.isTrigger = false;

			player.PlayerCamera.TargetPosition = new Vector3(-10, 15, player.PlayerCamera.TargetPosition.z);
			player.TargetPosition = player.transform.position;
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

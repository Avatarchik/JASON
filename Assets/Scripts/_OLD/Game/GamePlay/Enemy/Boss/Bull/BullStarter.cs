using UnityEngine;
using System.Collections;

public class BullStarter:MonoBehaviour {
	[SerializeField] private GameObject bull;
	[SerializeField] private GameObject otherDoorEnd;

	void OnTriggerEnter(Collider collider) {
		otherDoorEnd.collider.enabled = true;
	}

	void OnTriggerExit(Collider collider) {
		if(collider.gameObject.CompareTag("Player")) {
			Old_Player player = collider.GetComponent<Old_Player>();
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
		AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BossMusic, true);
		AudioManager.Instance.SetAudio(AudioManager.AudioFiles.NormalMusic, false);

		yield return new WaitForSeconds(2.0f);
		
		boss.StartAttack();
	}
}

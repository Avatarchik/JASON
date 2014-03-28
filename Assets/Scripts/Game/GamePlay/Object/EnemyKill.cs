using UnityEngine;
using System.Collections;

public class EnemyKill:MonoBehaviour {
	[SerializeField] private Enemy[] enemies;

	private PlayerCamera playerCamera;

	private Transform oldTarget;
	private Transform eventTarget;

	private int numDead;

	private bool allDead;

	void Start() {
		playerCamera = Camera.main.gameObject.GetComponent<PlayerCamera>();

		eventTarget = transform.FindChild("Camera Focus");
	}
	
	void Update () {
		numDead = 0;

		foreach(Enemy enemy in enemies)
			if(enemy.IsDead)
				numDead++;

		if(numDead >= enemies.Length) {
			allDead = true;
			StartCoroutine(CameraEvent(Door.DoorState.Open));
		}
	}

	private IEnumerator CameraEvent(Door.DoorState state) {
		oldTarget = playerCamera.Target;
		playerCamera.Target = eventTarget;

		yield return new WaitForSeconds(1.5f);

		if(state == Door.DoorState.Open) {
			GetComponent<Door>().Open();
		} else if(state == Door.DoorState.Closed) {
			GetComponent<Door>().Close();
		}

		yield return new WaitForSeconds(1.5f);

		playerCamera.Target = oldTarget;
	}
}

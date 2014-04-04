using UnityEngine;
using System.Collections;

public class EnemyHandler:MonoBehaviour {
	[SerializeField] private Transform cameraEventTarget;

	[SerializeField] private Enemy[] enemies;
	[SerializeField] private Door[] doors;

	[SerializeField] private int checknum;

	private bool puzzleDone = false;
	
	private PlayerCamera playerCamera;
	
	void Start(){
		playerCamera = Camera.main.gameObject.GetComponent<PlayerCamera>();
	}
	
	void FixedUpdate () {
		checknum = 0;

		for (int i = 0; i < enemies.Length; i++)
			if(enemies[i].IsDead)
				checknum++;

		if(checknum == enemies.Length) {
			if(!puzzleDone) {
				puzzleDone = true;
				StartCoroutine(playerCamera.CameraEvent(cameraEventTarget, doors));
			}
		}
	}
}

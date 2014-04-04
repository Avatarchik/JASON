using UnityEngine;
using System.Collections;

public class EnemyHandler:MonoBehaviour {
	[SerializeField] private Transform cameraEventTarget;

	[SerializeField] private Enemy[] enemies;
	[SerializeField] private Door[] doors;

	[SerializeField] private int checknum;

	private bool puzzleDone = false;
	
	private PlayerCamera playerCamera;
	private Transform oldTarget;
	
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
				StartCoroutine(CameraEvent(0));
			}
		}
	}

	/** Camera event */
	private IEnumerator CameraEvent(Door.DoorState state) {
		GameHUD hud = GameObject.Find("HUD").GetComponent<GameHUD>();
		SGUI.SGUITexture activeBar = null;

		hud.Outerbar.Activated = false;

		foreach(SGUI.SGUITexture bar in hud.Innerbars) {
			if(bar.Activated) {
				activeBar = bar;
				bar.Activated = false;
			}
		}

		oldTarget = playerCamera.Target;
		playerCamera.Target = cameraEventTarget;

		yield return new WaitForSeconds(1.5f);

		foreach(Door door in doors) {
			if(state == Door.DoorState.Open) {
				door.Open();
			} else if(state == Door.DoorState.Closed) {
				door.Close();
			}
		}

		yield return new WaitForSeconds(1.5f);

		playerCamera.Target = oldTarget;

		if(activeBar != null)
			activeBar.Activated = true;

		hud.Outerbar.Activated = true;
	}
}

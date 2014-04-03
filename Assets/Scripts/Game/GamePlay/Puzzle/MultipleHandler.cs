using UnityEngine;
using System.Collections;

public class MultipleHandler:MonoBehaviour {
	[SerializeField] private Trigger[] triggers;

	private PlayerCamera playerCamera;

	private Transform oldTarget;
	private Transform cameraEventTarget;
	
	private int numEnabled;

	private bool puzzleDone;
	
	void Start() {
		playerCamera = Camera.main.gameObject.GetComponent<PlayerCamera>();
		cameraEventTarget = transform.FindChild("Camera Focus");
	}

	void Update() {
		if(puzzleDone)
			return;

		numEnabled = 0;

		foreach(Trigger trigger in triggers)
			if(trigger.IsActive)
				numEnabled++;

		if(numEnabled >= triggers.Length) {
			puzzleDone = true;
			StartCoroutine(CameraEvent(Door.DoorState.Open));
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

		if(state == Door.DoorState.Open) {
			GetComponent<Door>().Open();
		} else if(state == Door.DoorState.Closed) {
			GetComponent<Door>().Close();
		}

		yield return new WaitForSeconds(1.5f);

		playerCamera.Target = oldTarget;

		if(activeBar != null)
			activeBar.Activated = true;

		hud.Outerbar.Activated = true;
	}
}

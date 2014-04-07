using UnityEngine;
using System.Collections;

public class TimedarrowTriggerMaster:MonoBehaviour {
	[SerializeField] private Transform cameraEventTarget;

	[SerializeField] private TimedArrowTriggerSlave[] slaves;

	[SerializeField] private DoorTrigger[] doors;

	void Update() {
		int slavesActive = 0;

		foreach(TimedArrowTriggerSlave slave in slaves)
			if(slave.Triggered)
				slavesActive++;

		if(slavesActive >= slaves.Length) {
			StartCoroutine(CameraManager.Instance.CameraEvent(cameraEventTarget, 3, delegate(string s) {
				foreach(DoorTrigger door in doors)
					door.Open();
			}));
		}
	}
}

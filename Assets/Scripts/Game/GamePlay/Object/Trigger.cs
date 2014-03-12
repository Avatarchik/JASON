using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	enum TriggerType{
		PlayerSwitch,
		BlockSwitch
	}

	[SerializeField] private bool toggle;
	[SerializeField] private TriggerType type;
	[SerializeField] private Door[] doors;
	// Update is called once per frame
	void OnCollisionEnter (Collision coll) {
		Debug.Log(coll.gameObject.name);
		if(coll.gameObject.tag == "Player"){
			if(type == TriggerType.PlayerSwitch){
				for (int i = 0; i < doors.Length; i++){
					doors[i].OpenDoor();
				}
			}
		}else if(coll.gameObject.tag == "PushableObject"){
			if(type == TriggerType.BlockSwitch){
				for (int i = 0; i < doors.Length; i++){
					doors[i].OpenDoor();
				}
			}
		}
	}
	void OnCollisionExit(Collision coll){
		if(toggle){

			if(coll.gameObject.tag == "Player"){
				if(type == TriggerType.PlayerSwitch){
					for (int i = 0; i < doors.Length; i++){
						doors[i].CloseDoor();
					}
				}

			}else if(coll.gameObject.tag == "PushableObject"){
				if(type == TriggerType.BlockSwitch){
					for (int i = 0; i < doors.Length; i++){
						doors[i].CloseDoor();
					}
				}
			}

		}
	}

}

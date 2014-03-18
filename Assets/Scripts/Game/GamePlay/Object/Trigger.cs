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

	private bool isTriggered = false;
	private bool onceActivated;
	private PlayerCamera cam;
	private Transform character;

	public Transform cameraEventTarget;
	// Update is called once per frame

	void Start(){
		cam = Camera.main.gameObject.GetComponent<PlayerCamera>();
	}

	void OnCollisionEnter (Collision coll) {
		if(!isTriggered){
		if(coll.gameObject.tag == "Player"){
			if(type == TriggerType.PlayerSwitch){
					StartCoroutine(CameraEvent(0));
			}
			}else if(coll.gameObject.tag == "PushableObject"){
				if(type == TriggerType.BlockSwitch){
					StartCoroutine(CameraEvent(0));
				}
			}
			if(coll.gameObject.tag == "Arrow"){
				StartCoroutine(CameraEvent(0));
			}
		}
	}
	void OnCollisionExit(Collision coll){
		if(toggle){

			if(coll.gameObject.tag == "Player"){
				if(type == TriggerType.PlayerSwitch){
					StartCoroutine(CameraEvent(1));
				}

			}else if(coll.gameObject.tag == "PushableObject"){
				if(type == TriggerType.BlockSwitch){
					StartCoroutine(CameraEvent(1));
				}
			}

		}
	}
	
	IEnumerator CameraEvent(int doorstate){
		character = cam.Target;
		cam.Target = cameraEventTarget;
		yield return new WaitForSeconds(1.5f);
		for (int i = 0; i < doors.Length; i++){
			if(doorstate == 0){
				doors[i].Open();
				isTriggered = true;
			}else{
			doors[i].Close();
			isTriggered = false;
			}
		}

		if(!onceActivated){
			yield return new WaitForSeconds(1.5f);
			onceActivated = true;
			cam.Target = character;
		}else{
			return false;
		}
		
	}
	
}

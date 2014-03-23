using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	enum TriggerType{
		PlayerSwitch,
		BlockSwitch,
		ArrowSwitch,
		ArrowSwitchShort,
		FireItemSwitch
	}

	[SerializeField] private bool toggle;
	[SerializeField] private TriggerType type;
	[SerializeField] private Door[] doors;

	private bool isTriggered = false;
	private bool onceActivated;
	private PlayerCamera cam;
	private Transform character;

	public Transform cameraEventTarget;

	public bool arrowEnabled;
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
			if(coll.gameObject.tag == "FireDungeonItem"){
				if(type == TriggerType.FireItemSwitch){
					StartCoroutine(CameraEvent(0));
				}
			}
			if(coll.gameObject.tag == "Arrow"){
				Debug.Log("ArrowHit");
				if(type == TriggerType.ArrowSwitch){
				StartCoroutine(CameraEvent(0));
				}else if(type == TriggerType.ArrowSwitchShort){
					StartCoroutine("Enabling");
				}
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
	IEnumerator Enabling(){
		arrowEnabled = true;
		yield return new WaitForSeconds(1);
		arrowEnabled = false;
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

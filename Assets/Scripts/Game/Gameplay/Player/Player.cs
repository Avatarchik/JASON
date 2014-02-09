using UnityEngine;
using System;
using System.Collections;

public class Player:MonoBehaviour {	
	[SerializeField]
	private PlayerData data;
	private PlayerCombat playerCombat;
	
	private Vector3 targetPosition;
	public GameObject playerModel;

	public bool ischarging;
	
	public int chargingMultiplier = 1;
	void Start() {
		playerCombat = GetComponent<PlayerCombat>();
	
		targetPosition = transform.position;
	}
	
	void Update() {	
		if(ischarging){
			StartCoroutine("ChargingTimer");
		}else{
			StopCoroutine("ChargingTimer");
			StartCoroutine("ChargingPeriod");
		}
		CheckForTouch();
		if(targetPosition != transform.position) {
			float step = data.speed * chargingMultiplier * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

			Vector3 lookPos = targetPosition - playerModel.transform.position;
			
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			rotation.x = 0;
			rotation.z = 0;

			if(transform.position != targetPosition){
			playerModel.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 30);
			}
		}
	}
	IEnumerator ChargingPeriod(){
		yield return new WaitForSeconds(2);
		chargingMultiplier = 1;
	}
	IEnumerator ChargingTimer(){
		yield return new WaitForSeconds(2);
		chargingMultiplier = 2;
	}
	private void CheckForTouch() {
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			CheckTouchPosition(ray);

		}
		
		if(Input.touchCount == 0)
			return;
		
		for(int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch(i);
			if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				CheckTouchPosition(Camera.main.ScreenPointToRay(touch.position));

				Debug.Log(touch.position);
			}
		}	
	}
	
	private void CheckTouchPosition(Ray screenRay) {
		RaycastHit hit;
		if (Physics.Raycast(screenRay,out hit, 100)){
		}
		if(hit.collider != null) {
			switch(hit.transform.tag) {
			case "Floor":
				ischarging = false;
				Move(hit.point);
				break;
			case "Player":
				ischarging = true;
				playerCombat.Defend();
				break;
			case "Enemy":
				playerCombat.StartAttack(hit.transform);
				break;
			default:
				ischarging = false;
				break;
			}
		} else {
			ischarging = true;
			Move(hit.point);
		}
	}
	
	private void Move(Vector3 position) {
		playerCombat.Target = null;
		targetPosition = new Vector3(position.x,1,position.z);
	}
	void OnGUI(){
		GUI.Label(new Rect(0,0,100,100),"" + ischarging);
	}
	public PlayerData Data { get { return data; } }
	
	public Vector3 TargetPosition {
		get { return targetPosition; }
		set { targetPosition = value; }
	}
	
	[Serializable]
	public class PlayerData {
		public int health;
		public int speed;
		public int attackDamage;
		
		public float attackDelay;
	}
}

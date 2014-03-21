using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour {
	public Enemy[] enemies;
	public Door[] doors;
	public int checknum;
	private bool puzzleDone = false;
	
	private PlayerCamera cam;
	private Transform character;
	public Transform cameraEventTarget;
	// Use this for initialization
	void Start(){
		cam = Camera.main.gameObject.GetComponent<PlayerCamera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		checknum = 0;
		for (int i = 0; i < enemies.Length; i++){
			if(enemies[i].IsDead){
				checknum++;
			}
		}
		if(checknum == enemies.Length){
			if(!puzzleDone){
				puzzleDone = true;
				StartCoroutine(CameraEvent(0));
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
			}else{
				doors[i].Close();
			}
		}
		
		yield return new WaitForSeconds(1.5f);
		cam.Target = character;
		
	}
}

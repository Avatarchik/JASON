using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	/*
	Order of Checking:

	1: Does player touch?
	2: Does it hit the character? yes? if yes Defend.
	3: if not. Does it hit enemy? yes? walk to Enemy.
	4: Walk to position.
	
	*/


	public int speed = 3;
	public Vector2 fingerPosition;
	public string hitname;
	public GameObject touchpoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = Vector3.zero;
		CheckTouch();
	}
	void CheckTouch(){
		Vector2 mousePosition;
		if(Input.touchCount == 0){
			hitname = "Idle";
		}
		for (int i = 0; i < Input.touchCount; i++){
			if (Input.GetTouch(i).phase == TouchPhase.Stationary ||Input.GetTouch(i).phase == TouchPhase.Moved){	
				mousePosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);	
				CheckTouchPosition(mousePosition);
			}
		}
	}
	void CheckTouchPosition(Vector2 position){
		RaycastHit2D hit = Physics2D.Raycast(position,position);
		if (hit == true) {
			//Check if it Hits Player or Enemy
			Transform rayhit = hit.transform;
			if(rayhit.tag == "Player"){
				Defence();
			}else if(rayhit.tag == "Enemy"){

			}else{
			MovePlayer(position);
			}
		} else{
			MovePlayer(position);
		}
	}
	void Defence(){
		hitname = "Defence";
	}
	void MovePlayer(Vector2 position){
		float step = 5 * Time.deltaTime;
		transform.position = Vector2.MoveTowards(transform.position,position,step);
		hitname = "Walking";
	}











	void OnGUI (){
		GUI.Label(new Rect(0,0,100,100), "" + transform.position.x + " : " + transform.position.y); 
		GUI.Label(new Rect(0,50,100,100), "" + hitname); 
	}
}

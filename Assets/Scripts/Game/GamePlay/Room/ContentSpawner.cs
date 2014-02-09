using UnityEngine;
using System.Collections;

public class ContentSpawner : MonoBehaviour {

	//Rotates the Content to the rooms Rotation
	void Start () {
		StartCoroutine("WaitForLevelGenerator");
	}
	IEnumerator WaitForLevelGenerator(){
		yield return new WaitForSeconds(1);
		GameObject content;
		switch(gameObject.name){
		case "Default":
			content = Instantiate(LevelGenerator.Instance.defaultRoomContent[Random.Range(0,LevelGenerator.Instance.defaultRoomContent.Length)],transform.position,transform.rotation) as GameObject;
			content.transform.parent = transform;
			content.transform.rotation = transform.rotation;
			break;
		case "Hall":
			content = Instantiate(LevelGenerator.Instance.hallRoomContent[Random.Range(0,LevelGenerator.Instance.hallRoomContent.Length)],transform.position,transform.rotation) as GameObject;
			content.transform.parent = transform;
			content.transform.rotation = transform.rotation;
			break;
		case "Corner":
			content = Instantiate(LevelGenerator.Instance.cornerRoomContent[Random.Range(0,LevelGenerator.Instance.cornerRoomContent.Length)],transform.position,transform.rotation) as GameObject;
			content.transform.parent = transform;
			content.transform.rotation = transform.rotation;
			break;
		case "TRoom":
			content = Instantiate(LevelGenerator.Instance.TroomRoomContent[Random.Range(0,LevelGenerator.Instance.TroomRoomContent.Length)],transform.position,transform.rotation) as GameObject;
			content.transform.parent = transform;
			content.transform.rotation = transform.rotation;
				break;
		case "Plus":
			content = Instantiate(LevelGenerator.Instance.plusRoomContent[Random.Range(0,LevelGenerator.Instance.plusRoomContent.Length)],transform.position,transform.rotation) as GameObject;
			content.transform.parent = transform;
			content.transform.rotation = transform.rotation;
			break;

		case "BigRoom":
			content = Instantiate(LevelGenerator.Instance.bigRoomContent[Random.Range(0,LevelGenerator.Instance.bigRoomContent.Length)],transform.position,transform.rotation) as GameObject;
			content.transform.parent = transform;
			content.transform.rotation = transform.rotation;
			break;
		}
	}
}

using UnityEngine;
using System.Collections;

public class ContentSpawner:MonoBehaviour {
	void Start() {
		StartCoroutine("WaitForLevelGenerator");
	}

	private IEnumerator WaitForLevelGenerator(){
		GameObject content = null;

		yield return new WaitForSeconds(1);

		switch(gameObject.name){
		case "Default":
			content = Instantiate(LevelGenerator.Instance.defaultRoomContent[Random.Range(0, LevelGenerator.Instance.defaultRoomContent.Length)], transform.position, transform.rotation) as GameObject;
			break;
		case "Hall":
			content = Instantiate(LevelGenerator.Instance.hallRoomContent[Random.Range(0, LevelGenerator.Instance.hallRoomContent.Length)], transform.position, transform.rotation) as GameObject;
			break;
		case "Corner":
			content = Instantiate(LevelGenerator.Instance.cornerRoomContent[Random.Range(0, LevelGenerator.Instance.cornerRoomContent.Length)], transform.position, transform.rotation) as GameObject;
			break;
		case "TRoom":
			content = Instantiate(LevelGenerator.Instance.TroomRoomContent[Random.Range(0, LevelGenerator.Instance.TroomRoomContent.Length)], transform.position, transform.rotation) as GameObject;
			break;
		case "Plus":
			content = Instantiate(LevelGenerator.Instance.plusRoomContent[Random.Range(0, LevelGenerator.Instance.plusRoomContent.Length)], transform.position, transform.rotation) as GameObject;
			break;
		case "BigRoom":
			content = Instantiate(LevelGenerator.Instance.bigRoomContent[Random.Range(0, LevelGenerator.Instance.bigRoomContent.Length)], transform.position, transform.rotation) as GameObject;
			break;
		}

		content.transform.parent = transform;
		content.transform.rotation = transform.rotation;
	}
}
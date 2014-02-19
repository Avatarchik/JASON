using UnityEngine;
using System.Collections;

public class ContentSpawner:MonoBehaviour {
	void Start() {
		StartCoroutine("WaitForLevelGenerator");
	}

	private IEnumerator WaitForLevelGenerator() {
		Object generator = null;

		yield return new WaitForSeconds(1);

		switch(gameObject.name){
		case "Default":
			generator = LevelGenerator.Instance.DefaultRoomContent[Random.Range(0, LevelGenerator.Instance.DefaultRoomContent.Length)];
			break;
		case "Hall":
			generator = LevelGenerator.Instance.HallRoomContent[Random.Range(0, LevelGenerator.Instance.HallRoomContent.Length)];
			break;
		case "Corner":
			generator = LevelGenerator.Instance.CornerRoomContent[Random.Range(0, LevelGenerator.Instance.CornerRoomContent.Length)];
			break;
		case "TRoom":
			generator = LevelGenerator.Instance.TRoomContent[Random.Range(0, LevelGenerator.Instance.TRoomContent.Length)];
			break;
		case "Plus":
			generator = LevelGenerator.Instance.PlusRoomContent[Random.Range(0, LevelGenerator.Instance.PlusRoomContent.Length)];
			break;
		case "BigRoom":
			generator = LevelGenerator.Instance.BigRoomContent[Random.Range(0, LevelGenerator.Instance.BigRoomContent.Length)];
			break;
		}

		GameObject content = Instantiate(generator, transform.position, transform.rotation) as GameObject;

		content.transform.parent = transform;
		content.transform.rotation = transform.rotation;
	}
}
using UnityEngine;
using System.Collections;

public class ChunkData:MonoBehaviour {
	public enum ChunkType {
		One,
		Two,
		Three,
		Four
	}

	public enum RotationDegree {
		None = 0,
		Degree90 = 90,
		Degree180 = 180,
		Degree270 = 270
	}

	public ChunkType type;
	public RotationDegree rotation;

	private GameObject chunk;
	private int setRotation;

	void Start() {
		StartCoroutine("WaitForLevelGenerator");
	}

	private IEnumerator WaitForLevelGenerator(){
		yield return new WaitForSeconds(0.1f);

		switch(type) {
		case ChunkType.One:
			chunk = Instantiate(LevelGenerator.Instance.oneSideChunk[Random.Range(0,LevelGenerator.Instance.oneSideChunk.Length)],transform.position,transform.rotation) as GameObject;
			break;
		case ChunkType.Two:
			chunk = Instantiate(LevelGenerator.Instance.twoSideChunk[Random.Range(0,LevelGenerator.Instance.twoSideChunk.Length)],transform.position,transform.rotation) as GameObject;
			break;
		case ChunkType.Three:
			chunk = Instantiate(LevelGenerator.Instance.threeSideChunk[Random.Range(0,LevelGenerator.Instance.threeSideChunk.Length)],transform.position,transform.rotation) as GameObject;
			break;
		case ChunkType.Four:
			chunk = Instantiate(LevelGenerator.Instance.fourSideChunk[Random.Range(0,LevelGenerator.Instance.fourSideChunk.Length)],transform.position,transform.rotation) as GameObject;
			break;
		}

		setRotation = (int)rotation;
		
		if(type == ChunkType.Two){
			int random = Random.Range(1, 3);
			setRotation += 180 * random;
		}

		chunk.transform.eulerAngles = new Vector3(		
			chunk.transform.eulerAngles.x,			
			chunk.transform.eulerAngles.y + setRotation,			
			chunk.transform.eulerAngles.z
		);

		chunk.transform.parent = transform.parent;
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(100, 20, 100));
	}
}
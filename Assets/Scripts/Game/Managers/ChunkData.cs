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
	public RotationDegree rotationDegree;

	private GameObject chunk;
	private int rotation;

	void Start () {
		StartCoroutine("WaitForLevelGenerator");
	}

	private IEnumerator WaitForLevelGenerator(){
		yield return new WaitForSeconds(0.1f);

		Object generator = null;

		switch(type) {
		case ChunkType.One:
			generator = LevelGenerator.Instance.OneSideChunk[Random.Range(0, LevelGenerator.Instance.OneSideChunk.Length)];
			break;
		case ChunkType.Two:
			generator = LevelGenerator.Instance.TwoSideChunk[Random.Range(0, LevelGenerator.Instance.TwoSideChunk.Length)];
			break;
		case ChunkType.Three:
			generator = LevelGenerator.Instance.ThreeSideChunk[Random.Range(0, LevelGenerator.Instance.ThreeSideChunk.Length)];
			break;
		case ChunkType.Four:
			generator = LevelGenerator.Instance.FourSideChunk[Random.Range(0, LevelGenerator.Instance.FourSideChunk.Length)];
			break;
		}

		chunk = Instantiate(generator, transform.position, transform.rotation) as GameObject;
		rotation = (int)rotationDegree;
		
		if(type == ChunkType.Two){
			int random = Random.Range(1, 3);
			rotation += 180 * random;
		}

		chunk.transform.eulerAngles = new Vector3(		
			chunk.transform.eulerAngles.x,			
			chunk.transform.eulerAngles.y + rotation,			
			chunk.transform.eulerAngles.z
		);

		chunk.transform.parent = transform.parent;
	}
}
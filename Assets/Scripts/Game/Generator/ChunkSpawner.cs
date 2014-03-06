using UnityEngine;
using System.Collections;

public class ChunkSpawner:MonoBehaviour {
	public enum ChunkSides {
		One,
		Two,
		Three,
		Four
	}

	[SerializeField] private ChunkSides sides;
	[SerializeField] private int rotation;

	void Start() {
		StartCoroutine("WaitForLevelGenerator");
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(100, 20, 100));
	}

	private IEnumerator WaitForLevelGenerator(){
		GameObject chunk = null;
		Object selected = null;
	
		yield return new WaitForSeconds(0.1f);

		switch(sides) {
		case ChunkSides.One:
			selected = LevelGenerator.Instance.RandomOneSidedChunk();
			break;
		case ChunkSides.Two:
			selected = LevelGenerator.Instance.RandomTwoSidedChunk();
			rotation += 180 * Random.Range(1, 3);
			break;
		case ChunkSides.Three:
			selected = LevelGenerator.Instance.RandomThreeSidedChunk();
			break;
		case ChunkSides.Four:
			selected = LevelGenerator.Instance.RandomFourSidedChunk();
			break;
		}
		
		chunk = Instantiate(selected, transform.position, transform.rotation) as GameObject;

		chunk.transform.eulerAngles = new Vector3(		
			chunk.transform.eulerAngles.x,			
			chunk.transform.eulerAngles.y + rotation,			
			chunk.transform.eulerAngles.z
		);

		chunk.transform.parent = transform.parent;
		chunk.name = sides + " Sided Chunk";
		
		Destroy(gameObject);
	}	
}
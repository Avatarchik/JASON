using UnityEngine;
using System.Collections;

public class ChunkData : MonoBehaviour {
	public enum ChunkType{
		Side1,
		Side2,
		Side3,
		Side4
	}

	public ChunkType type;
	public enum RotationDegree{
		None,
		Degree90,
		Degree180,
		Degree270
	}
	public RotationDegree rotation;
	private GameObject chunk;
	private int setrotation;
	// Use this for initialization
	void Start () {
		StartCoroutine("WaitForLevelGenerator");
	}
	IEnumerator WaitForLevelGenerator(){
		yield return new WaitForSeconds(0.1f);
		switch(type){
		case ChunkType.Side1:
			chunk = Instantiate(LevelGenerator.Instance.oneSideChunk[Random.Range(0,LevelGenerator.Instance.oneSideChunk.Length)],transform.position,transform.rotation) as GameObject;
			break;
		case ChunkType.Side2:
			chunk = Instantiate(LevelGenerator.Instance.twoSideChunk[Random.Range(0,LevelGenerator.Instance.twoSideChunk.Length)],transform.position,transform.rotation) as GameObject;
			break;
		case ChunkType.Side3:
			chunk = Instantiate(LevelGenerator.Instance.threeSideChunk[Random.Range(0,LevelGenerator.Instance.oneSideChunk.Length)],transform.position,transform.rotation) as GameObject;
			break;
		case ChunkType.Side4:
			chunk = Instantiate(LevelGenerator.Instance.fourSideChunk[Random.Range(0,LevelGenerator.Instance.oneSideChunk.Length)],transform.position,transform.rotation) as GameObject;
			break;
		}
		
		switch(rotation){
		case RotationDegree.Degree90:
			setrotation = 90;
			break;
		case RotationDegree.Degree180:
			setrotation = 180;
			break;
		case RotationDegree.Degree270:
			setrotation = 270;
			break;
		}
		
		if(type == ChunkType.Side2){
			int random = Random.Range (1,3);
			setrotation += 180 * random;
		}
		chunk.transform.eulerAngles = new Vector3(
			
			chunk.transform.eulerAngles.x,
			
			chunk.transform.eulerAngles.y + setrotation,
			
			chunk.transform.eulerAngles.z
			
			);
		chunk.transform.parent = transform.parent;
	}
	// Update is called once per frame
	void Update () {
	
	}
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position,new Vector3(100,20,100));
	}
}

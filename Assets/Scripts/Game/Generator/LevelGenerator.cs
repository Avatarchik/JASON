using UnityEngine;
using System.Collections;

public class LevelGenerator:Singleton<LevelGenerator> {
	public const string contentPath = "Editor/Content Creator/Saved Rooms/";
	public const string chunkPath = "Editor/Content Creator/Saved Chunks/";

	private Object[] contentDefault;
	private Object[] contentHall;
	private Object[] contentCorner;
	private Object[] contentT;
	private Object[] contentPlus;
	private Object[] contentBig;

	private Object[] chunkOneSide;
	private Object[] chunkTwoSide;
	private Object[] chunkThreeSide;
	private Object[] chunkFourSide;

	void Start() {
		contentDefault = Resources.LoadAll(contentPath + "Default");
		contentHall = Resources.LoadAll(contentPath + "Hall");
		contentCorner = Resources.LoadAll(contentPath + "Corner");
		contentT = Resources.LoadAll(contentPath + "T");
		contentPlus = Resources.LoadAll(contentPath + "Plus");
		contentBig = Resources.LoadAll(contentPath + "Big");

		chunkOneSide = Resources.LoadAll(chunkPath + "1 Sided");
		chunkTwoSide = Resources.LoadAll(chunkPath + "2 Sided");
		chunkThreeSide = Resources.LoadAll(chunkPath + "3 Sided");
		chunkFourSide = Resources.LoadAll(chunkPath + "4 Sided");
	}
	
	/** Return a random object from the default content array */
	public Object RandomDefault() {
		return contentDefault[Random.Range(0, contentDefault.Length)];
	}
	
	/** Return a random object from the hall content array */
	public Object RandomHall() {
		return contentHall[Random.Range(0, contentHall.Length)];
	}
	
	/** Return a random object from the corner content array */
	public Object RandomCorner() {
		return contentCorner[Random.Range(0, contentCorner.Length)];
	}
	
	/** Return a random object from the t content array */
	public Object RandomT() {
		return contentT[Random.Range(0, contentT.Length)];
	}
	
	/** Return a random object from the plus content array */
	public Object RandomPlus() {
		return contentPlus[Random.Range(0, contentPlus.Length)];
	}
	
	/** Return a random object from the big content array */
	public Object RandomBig() {
		return contentBig[Random.Range(0, contentBig.Length)];
	}
	
	/** Return a random object from the 1 sided chunks array */
	public Object RandomOneSidedChunk() {
		return chunkOneSide[Random.Range(0, chunkOneSide.Length)];
	}
	
	/** Return a random object from the 2 sided chunks array */
	public Object RandomTwoSidedChunk() {
		return chunkTwoSide[Random.Range(0, chunkTwoSide.Length)];
	}
	
	/** Return a random object from the 3 sided chunks array */
	public Object RandomThreeSidedChunk() {
		return chunkThreeSide[Random.Range(0, chunkThreeSide.Length)];
	}
	
	/** Return a random object from the 4 sided chunks array */
	public Object RandomFourSidedChunk() {
		return chunkFourSide[Random.Range(0, chunkFourSide.Length)];
	}
}
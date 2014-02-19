using UnityEngine;
using System.Collections;

public class LevelGenerator:Singleton<LevelGenerator> {
	private Object[] defaultRoomContent;
	private Object[] hallRoomContent;
	private Object[] cornerRoomContent;
	private Object[] tRoomContent;
	private Object[] plusRoomContent;
	private Object[] bigRoomContent;

	private Object[] oneSideChunk;
	private Object[] twoSideChunk;
	private Object[] threeSideChunk;
	private Object[] fourSideChunk;

	private Texture currentMap;

	void Start() {
		defaultRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Default");
		hallRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Hall");
		cornerRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Corner");
		tRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Troom");
		plusRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Plus");
		bigRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/BigRoom");

		oneSideChunk = Resources.LoadAll("Prefabs/Rooms/Chunks/1Side");
		twoSideChunk = Resources.LoadAll("Prefabs/Rooms/Chunks/2Side");
		threeSideChunk = Resources.LoadAll("Prefabs/Rooms/Chunks/3Side");
		fourSideChunk = Resources.LoadAll("Prefabs/Rooms/Chunks/4Side");
	}

	public Object[] DefaultRoomContent { get { return defaultRoomContent; } }
	public Object[] HallRoomContent { get { return hallRoomContent; } }
	public Object[] CornerRoomContent { get { return cornerRoomContent; } }
	public Object[] TRoomContent { get { return tRoomContent; } }
	public Object[] PlusRoomContent { get { return plusRoomContent; } }
	public Object[] BigRoomContent { get { return bigRoomContent; } }

	public Object[] OneSideChunk { get { return oneSideChunk; } }
	public Object[] TwoSideChunk { get { return twoSideChunk; } }
	public Object[] ThreeSideChunk { get { return threeSideChunk; } }
	public Object[] FourSideChunk { get { return fourSideChunk; } }

	public Texture CurrentMap {
		set { currentMap = value; }
		get { return currentMap; }
	}
}
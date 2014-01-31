using UnityEngine;
using System.Collections;

public class GameManager:Singleton<GameManager> {
	[SerializeField] private Vector2 nativeScreenSize;

	[SerializeField] private Font guiFont;
	
	[SerializeField] private bool isPaused;
	
	public Font GUIFont { get { return GameManager.Instance.guiFont; } }
	
	public float NativeScreenWidth { get { return nativeScreenSize.x; } }
	
	public float NativeScreenHeight { get { return nativeScreenSize.y; } }
	
	public bool Paused {
		set { isPaused = value; }
		get { return isPaused; }
	}
}

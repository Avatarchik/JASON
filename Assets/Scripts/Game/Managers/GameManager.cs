using UnityEngine;
using System.Collections;

public class GameManager:Singleton<GameManager> {
	[SerializeField] private Font guiFont;
	
	public Font GUIFont { get { return GameManager.Instance.guiFont; } }
}

using UnityEngine;
using System.Collections;

public class DebugSettings:Singleton<DebugSettings> {
	[SerializeField]
	private bool disableGUI;
	
	public bool DisableGUI { get { return disableGUI; } }
}

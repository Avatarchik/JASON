using UnityEngine;
using System.Collections;

public class DebugSettings:Singleton<DebugSettings> {
	public enum LogLevel {
		None = 0,
		Messages = 1,
		Warnings = 2,
		Errors = 3
	}

	[SerializeField]
	private LogLevel loggingLevel;

	[SerializeField]
	private bool disableGUI;
	
	public void Logger(LogLevel priority, object message) {
		if((int)priority >= (int)loggingLevel) {
			Debug.Log("[Logger] " + message);
		}
	}
	
	public bool DisableGUI { get { return disableGUI; } }
}

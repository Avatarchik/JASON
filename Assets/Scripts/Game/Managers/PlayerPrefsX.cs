using UnityEngine;
using System.Collections;

public class PlayerPrefsX {
	/** Set a bool with the default value */
	public static void SetBool(string name, bool booleanValue) {
		PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
	}
	
	/** Get a bool */
	public static bool GetBool(string name) {
		return PlayerPrefs.GetInt(name) == 1 ? true : false;
	}
	
	/** Get a bool or a default value */
	public static bool GetBool(string name, bool defaultValue) {
		if(PlayerPrefs.HasKey(name))
			return GetBool(name);
		
		return defaultValue;
	}
}
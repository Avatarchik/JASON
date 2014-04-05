using UnityEngine;

public class Singleton<T>:MonoBehaviour where T:MonoBehaviour {
	protected static T instance;
	
	/** Get the instance of the singleton */
	public static T Instance {
		get {
			if(instance == null) {
				instance = (T)FindObjectOfType(typeof(T));
			}
			
			if(instance == null)
				throw new System.NullReferenceException("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
			
			return instance;
		}
	}
}

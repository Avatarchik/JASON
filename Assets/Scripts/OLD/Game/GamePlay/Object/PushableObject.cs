using UnityEngine;
using System.Collections;

public class PushableObject:InteractableObject {
	[SerializeField] private bool isLocked;

	/** Set and/or get whether the object is locked */
	public bool Locked {
		set { isLocked = value; }
		get { return isLocked; }
	}
}

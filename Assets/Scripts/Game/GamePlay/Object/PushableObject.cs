using UnityEngine;
using System.Collections;

public class PushableObject:InteractableObject {
	[SerializeField] private bool isLocked;

	public bool Locked {
		set { isLocked = value; }
		get { return isLocked; }
	}
}

using UnityEngine;
using System;

[Serializable]
public class SUI {
	[SerializeField] protected bool activated = true;
	
	[SerializeField] protected Vector2 position;

	/** Activate or deactivate the SUI object */
	public bool Activated {
		set { activated = value; }
		get { return activated; }
	}

	/** Set or get the position of the SUI object */
	public Vector2 Position {
		set { position = value; }
		get { return position; }
	}
}

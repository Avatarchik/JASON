using UnityEngine;
using System.Collections;

public class PoolObject {
	protected bool available = true;
	protected bool forceStopped = false;

	public virtual void Update() { }

	public void ForceStop() {
		forceStopped = true;
	}

	/** Set or get wheter or not this object is available or not */
	public bool Available {
		set { available = value; }
		get { return available; }
	}
}

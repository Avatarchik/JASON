using UnityEngine;
using System.Collections;

public class PoolObject {
	protected bool available = true;
	protected bool forceStopped = false;

	public virtual void Update() { }

	public void ForceStop() {
		forceStopped = true;
	}

	public bool Available {
		set { available = value; }
		get { return available; }
	}
}

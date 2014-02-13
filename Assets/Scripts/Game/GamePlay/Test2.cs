using UnityEngine;
using System.Collections;

public class Test2:MonoBehaviour {
	public Test test;
	public Test test2;

	private ObjectPool<Test> pool;

	void Start() {
		pool = new ObjectPool<Test>(new Test[] { test, test2 }, true);
	}

	void Update() {
		pool.Update();

		if(Input.GetMouseButtonDown(0)) {
			try {
				pool.getFreeObject().Play();
			} catch(System.NullReferenceException) { }
		}
	}
}

using UnityEngine;
using System.Collections;

public class ObjectPool<T> where T:PoolObject {
	private T[] objects;

	private bool useFirst;

	public ObjectPool(T[] pool, bool useFirst) {
		objects = pool;

		this.useFirst = useFirst;
	}

	public void Update() {
		foreach(PoolObject obj in objects)
			obj.Update();
	}

	/** Return the first free object in the pool */
	public T getFreeObject() {
		foreach(T obj in objects) {
			if(obj.Available) {
				obj.Available = false;
				return obj;
			}
		}

		if(useFirst) {
			T[] temp = new T[objects.Length];

			for(int i = 1; i < objects.Length; i++)
				temp[i - 1] = objects[i];

			temp[objects.Length - 1] = objects[0];
			objects = temp;

			objects[objects.Length - 1].Available = false;
			objects[objects.Length - 1].ForceStop();
			return objects[objects.Length - 1];
		}

		return null;
	}
}

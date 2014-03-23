using UnityEngine;
using System.Collections;

public class StoreTransform {
	public Vector3 position;
	public Quaternion rotation;
	public Vector3 localScale;
}

public static class TransformSerializationExtension {
	/** Save a local transform */
	public static StoreTransform SaveLocal(this Transform transform) {
		return new StoreTransform() {
			position = transform.localPosition,
			rotation = transform.localRotation,
			localScale = transform.localScale
		};
	}
	
	/** Save a world transform */
	public static StoreTransform SaveWorld(this Transform transform) {
		return new StoreTransform() {
			position = transform.position,
			rotation= transform.rotation,
			localScale = transform.localScale
		};
	}
	
	/** Load a local transform */
	public static void LoadLocal(this Transform transform, StoreTransform savedTransform) {
		transform.localPosition = savedTransform.position;
		transform.localRotation = savedTransform.rotation;
		transform.localScale  = savedTransform.localScale;
	}
	
	/** Load a world transform */
	public static void LoadWorld(this Transform transform, StoreTransform savedTransform) {
		transform.position = savedTransform.position;
		transform.rotation = savedTransform.rotation;
		transform.localScale  = savedTransform.localScale;
	}
}
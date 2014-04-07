using UnityEngine;

public class RaycastUtils {
	public static RaycastHit PositionToRaycastHit(Vector2 position) {
		return PositionToRaycastHit(position, 0);
	}

	public static RaycastHit PositionToRaycastHit(Vector2 position, int layerMask) {
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hit;

		Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

		return hit;
	}
}

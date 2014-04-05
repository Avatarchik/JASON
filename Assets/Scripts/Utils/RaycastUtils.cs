using UnityEngine;

public class RaycastUtils {
	public static RaycastHit PositionToRaycastHit(Vector2 position) {
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hit;

		Physics.Raycast(ray, out hit, 100);

		return hit;
	}

	public static RaycastHit PositionToRaycastHit(Vector2 position, int layerMask) {
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hit;

		Physics.Raycast(ray, out hit, 100, layerMask);

		return hit;
	}
}

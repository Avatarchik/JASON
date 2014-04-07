using UnityEngine;
using System.Collections;

public class Utils {
	/** <summary>Calculate the angle between two vectors</summary>
	 * <param name="from">The starting vector</param>
	 * <param name="to">The end vector</param>
	 * <param name="up">The up direction of the starting vector</param> 
	 * <returns>The angle between the two vectors</returns> */
	public static float GetAngle(Vector3 from, Vector3 to, Vector3 up) {
		Vector3 referenceRight = Vector3.Cross(up, from);

		float angle = Vector3.Angle(to, from);
		float sign = (Vector3.Dot(to, referenceRight) > 0.0f) ? 1.0f : -1.0f;

		return sign * angle;
	}
}

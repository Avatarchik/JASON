using UnityEngine;

public class Utils {
	/** <summary>Rotate towards the specified position</summary>
	 * <param name="objectPosition">The position of the object</param>
	 * <param name="objectRotation">The rotation of the object</param>
	 * <param name="position">The position to rotate to</param> */
	public static Quaternion RotateTowards(Vector3 objectPosition, Quaternion objectRotation, Vector3 position) {
		Quaternion targetRotation = Quaternion.identity;

		targetRotation = Quaternion.LookRotation(position - objectPosition);

		targetRotation.x = 0;
		targetRotation.z = 0;

		return Quaternion.Slerp(objectRotation, targetRotation, 30);
	}
}

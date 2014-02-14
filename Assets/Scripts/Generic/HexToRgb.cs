using UnityEngine;
using System.Collections;

public class HexToRgb {
	public static Color HexToColor(int hex) {
		string hexCode = hex.ToString();
		Debug.Log(hexCode);
		byte r = byte.Parse(hexCode.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hexCode.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hexCode.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

		return new Color32(r, g, b, 255);
	}
}
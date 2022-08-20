
using UnityEngine;
using System.Runtime.InteropServices;

public class BatteryLevel {
	
	[DllImport ("__Internal")]
	static extern float _GetBatteryLevel ();
	
	public static float GetBatteryLevel () {
		#if UNITY_IPHONE
			return _GetBatteryLevel ();
		#else
			return 1.0f;
		#endif
	}
}
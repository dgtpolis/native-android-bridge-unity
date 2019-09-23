using System;
using UnityEngine;

namespace WarpGate.Native
{
	public static class ExitGame
	{

#if UNITY_ANDROID
        // Class name
        private const string UNITY_PLAYER_CLASS_NAME = "com.unity3d.player.UnityPlayer";
        private const string NATIVE_EXIT_CLASS_NAME = "dev.warpgate.nativeandroidbridge.ExitAndroidBridge";

        // Function name
        private const string EXIT_FUNCTION = "exitApplication";

        // Static name
        private const string CURRENT_ACTIVITY = "currentActivity";
#endif

		public static void Exit()
		{
#if UNITY_ANDROID
            WithActivity((activity) => {
                using (var nativeClass = new AndroidJavaClass(NATIVE_EXIT_CLASS_NAME)) {
                    nativeClass.CallStatic(EXIT_FUNCTION, activity);
                }
            });
#endif
		}

		private static void WithActivity(Action<AndroidJavaObject> androidFunc)
		{
#if UNITY_ANDROID
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass(UNITY_PLAYER_CLASS_NAME)) {
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY)) {
                    androidFunc(currentActivity);
                }
            }
#endif
		}
	}
}
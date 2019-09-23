using System;
using UnityEngine;

namespace WarpGate.Native {
    public static class Clipboard {
#if UNITY_ANDROID
        // Class name
        private const string UNITY_PLAYER_CLASS_NAME = "com.unity3d.player.UnityPlayer";
        private const string NATIVE_CLIPBOARD_CLASS_NAME = "dev.warpgate.nativeandroidbridge.ClipboardAndroidBridge";

        // Function name
        private const string GET_APPLICATION_CONTEXT_FUNCTION = "getApplicationContext";
        private const string COPY_FUNCTION = "copy";

        // Static name
        private const string CURRENT_ACTIVITY = "currentActivity";
#endif

        public static void Copy(string label, string text) {
#if UNITY_ANDROID
            WithApplicationContext((applicationContext) => {
                using (var nativeClass = new AndroidJavaClass(NATIVE_CLIPBOARD_CLASS_NAME)) {
                    nativeClass.CallStatic(COPY_FUNCTION, applicationContext, label, text);
                }
            });
#endif
        }

        private static void WithApplicationContext(Action<AndroidJavaObject> androidFunc) {
#if UNITY_ANDROID
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass(UNITY_PLAYER_CLASS_NAME)) {
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY)) {
                    using (AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>(GET_APPLICATION_CONTEXT_FUNCTION)) {
                        androidFunc(context);
                    }
                }
            }
#endif
        }
    }

}

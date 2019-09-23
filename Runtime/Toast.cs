using System;
using UnityEngine;


namespace WarpGate.Native {
    public enum ToastLength {
        Short,
        Long
    }

    public static class Toast {
#if UNITY_ANDROID
        // Class name
        private const string UNITY_PLAYER_CLASS_NAME = "com.unity3d.player.UnityPlayer";
        private const string NATIVE_TOAST_MESSAGE_CLASS_NAME = "dev.warpgate.nativeandroidbridge.ToastAndroidBridge";

        // Function name
        private const string GET_APPLICATION_CONTEXT_FUNCTION = "getApplicationContext";
        private const string TOAST_LENGTH_SHORT_FUNCION = "toastLengthShort";
        private const string TOAST_LENGTH_LONG_FUNCION = "toastLengthLong";

        // Static name
        private const string CURRENT_ACTIVITY = "currentActivity";
#endif

        public static void MakeText(string text, ToastLength toastLength) {
#if UNITY_ANDROID
            switch (toastLength) {
                case ToastLength.Short:
                    ToastShort(text);
                    break;
                case ToastLength.Long:
                    ToastLong(text);
                    break;
            }
#endif
        }

        private static void ToastShort(string text) {
#if UNITY_ANDROID
            WithApplicationContext((applicationContext) => {
                using (var nativeClass = new AndroidJavaClass(NATIVE_TOAST_MESSAGE_CLASS_NAME)) {
                    nativeClass.CallStatic(TOAST_LENGTH_SHORT_FUNCION, applicationContext, text);
                }
            });
#endif
        }

        private static void ToastLong(string text) {
#if UNITY_ANDROID
            WithApplicationContext((applicationContext) => {
                using (var nativeClass = new AndroidJavaClass(NATIVE_TOAST_MESSAGE_CLASS_NAME)) {
                    nativeClass.CallStatic(TOAST_LENGTH_LONG_FUNCION, applicationContext, text);
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

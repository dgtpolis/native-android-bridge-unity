using System;
using UnityEngine;

namespace WarpGate.Native {
    public static class Share {

#if UNITY_ANDROID
        // Class name
        private const string UNITY_PLAYER_CLASS_NAME = "com.unity3d.player.UnityPlayer";
        private const string NATIVE_SHARE_CLASS_NAME = "dev.warpgate.nativeandroidbridge.ShareAndroidBridge";

        // Function name
        private const string SHARE_TEXT_FUNCTION = "shareTextWithChooser";
        private const string SHARE_IMAGE_FUNCTION = "shareImageWithChooser";

        // Static name
        private const string CURRENT_ACTIVITY = "currentActivity";
#endif

        public static void TextWithChooser(string header, string text) {
#if UNITY_ANDROID
            WithActivity((activity) => {
                using (var nativeClass = new AndroidJavaClass(NATIVE_SHARE_CLASS_NAME)) {
                    nativeClass.CallStatic(SHARE_TEXT_FUNCTION, activity, header, text);
                }
            });
#endif
        }

        public static void ImageWithChooser(string header, string imagePath) {
#if UNITY_ANDROID
            WithActivity((activity) => {
                using (var nativeClass = new AndroidJavaClass(NATIVE_SHARE_CLASS_NAME)) {
                    nativeClass.CallStatic(SHARE_IMAGE_FUNCTION, activity, header, imagePath);
                }
            });
#endif
        }

        private static void WithActivity(Action<AndroidJavaObject> androidFunc) {
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
package dev.warpgate.nativeandroidbridge

import android.app.Activity
import android.content.Intent
import android.os.Bundle

class ExitAndroidBridge {
    companion object {
        @JvmStatic fun exitApplication(activity: Activity) {
            android.os.Process.killProcess(android.os.Process.myPid())
            if (android.os.Build.VERSION.SDK_INT >= 21) {
                activity.finishAndRemoveTask()
            } else {
                val intent = Intent(activity.applicationContext, ExitCurrentAppActivity::class.java).apply {
                    addFlags(
                        Intent.FLAG_ACTIVITY_NEW_TASK or
                                Intent.FLAG_ACTIVITY_CLEAR_TASK or
                                Intent.FLAG_ACTIVITY_NO_ANIMATION or
                                Intent.FLAG_ACTIVITY_EXCLUDE_FROM_RECENTS
                    )
                }
                activity.startActivity(intent)
            }
        }
    }
}

class ExitCurrentAppActivity : Activity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        finish()
    }
}
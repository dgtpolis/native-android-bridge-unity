package dev.warpgate.nativeandroidbridge

import android.content.Context
import android.widget.Toast

class ToastAndroidBridge {
    companion object {
        @JvmStatic fun toastLengthShort(context: Context, text: String) {
            Toast.makeText(context, text, Toast.LENGTH_SHORT).show()
        }

        @JvmStatic fun toastLengthLong(context: Context, text: String) {
            Toast.makeText(context, text, Toast.LENGTH_LONG).show()
        }
    }
}

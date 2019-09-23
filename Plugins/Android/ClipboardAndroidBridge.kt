package dev.warpgate.nativeandroidbridge

import android.content.ClipData
import android.content.ClipboardManager
import android.content.Context
import android.widget.Toast

class ClipboardAndroidBridge {
    companion object {
        @JvmStatic fun copy(context: Context, label: String, text: String) {
            val clipboard = context.getSystemService(Context.CLIPBOARD_SERVICE) as ClipboardManager
            val clip: ClipData = ClipData.newPlainText(label, text);

            clipboard.setPrimaryClip(clip)
        }
    }
}
package dev.warpgate.nativeandroidbridge

import android.app.Activity
import android.content.Intent
import androidx.core.content.FileProvider
import java.io.File

class ShareAndroidBridge {
    companion object {
        @JvmStatic fun shareTextWithChooser(activity: Activity, header: String, message: String) {
            val shareIntent = Intent().apply {
                action = Intent.ACTION_SEND
                putExtra(Intent.EXTRA_TEXT, message)
                type = "text/plain"
            }

            activity.startActivity(Intent.createChooser(shareIntent, header))
        }

        @JvmStatic fun shareImageWithChooser(activity: Activity, header: String, filePath: String, fileProvider: String) {
            val contentFile = File(filePath)
            val contentUri = FileProvider.getUriForFile(
                activity.applicationContext,
                fileProvider,
                contentFile
            )

            val shareIntent = Intent().apply {
                action = Intent.ACTION_SEND
                flags = Intent.FLAG_GRANT_READ_URI_PERMISSION
                flags = Intent.FLAG_GRANT_WRITE_URI_PERMISSION
                putExtra(Intent.EXTRA_STREAM, contentUri)
                type = "image/*"
            }

            if (shareIntent.resolveActivity(activity.packageManager) != null) {
                activity.startActivity(Intent.createChooser(shareIntent, header))
            }
        }
    }
}
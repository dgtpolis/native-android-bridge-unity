# native-android-bridge-unity
This is Native Android Bridge Unity that will help you use Android Native power in Unity

# Installation
You have to add `"dev.warpgate.nativeandroidbridge": "git://github.com/dgtpolis/native-android-bridge-unity"` in your manifest.json in [Project]/Packages folder.

## AndroidManifest.xml
You have to add provider tag in AndroidManifest.xml and replace {YOUR_FILE_PROVIDER_NAME} with your file provider name.
```
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
<application>
    <provider
      android:name="androidx.core.content.FileProvider"
      android:authorities="{YOUR_FILE_PROVIDER_NAME}"
      android:exported="false"
      android:grantUriPermissions="true">
        <meta-data
          android:name="android.support.FILE_PROVIDER_PATHS"
          android:resource="@xml/provider_paths" />
    </provider>
</application>
```

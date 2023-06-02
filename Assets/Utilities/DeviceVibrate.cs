using UnityEngine;

public static class DeviceVibrate
{


#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    public static AndroidJavaClass vibrationEffectClass;
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
    public static AndroidJavaClass vibrationEffectClass;
    public static int defaultAmplitude;
#endif

    public static bool isAndroid() {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
    return false;
#endif
    }
    private static int getSDKInt() {
        if(isAndroid()) {
            using (var version = new AndroidJavaClass("android.os.Build$VERSION")) {
                return version.GetStatic<int>("SDK_INT");
            }
        }
        else {
            return -1;
        }
    }

    public static void Vibrate(long milliseconds = 250) {
        if(isAndroid()) {
            vibrator.Call("vibrate", milliseconds);
        }
        else {
            Handheld.Vibrate();
        }
    }

    public static void VibrateWithAmp(long milliseconds, int amplitude) {
         if (isAndroid()) {
            //If Android 8.0 (API 26+) or never use the new vibrationeffects
            if (getSDKInt() >= 26) {
                vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
                CreateVibrationEffect("createOneShot", new object[] { milliseconds, amplitude });
            }
            else {
                Vibrate(milliseconds);
            }
        }
        //If not android do simple solution for now
        else {
            Handheld.Vibrate();
        }
    }
    //Handels all new vibration effects
    private static void CreateVibrationEffect(string function, params object[] args) {

        AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>(function, args);
        vibrator.Call("vibrate", vibrationEffect);
    }
}

using System.Collections;
using System.IO;
using UnityEngine;
using UnityNative.Sharing.Example;

public class ShareOnSocialMedia : MonoBehaviour
{
    public void HandleShare()
    {
        StartCoroutine(TakeScreenShotAndShare());
    }

    IEnumerator TakeScreenShotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();

        string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");
        File.WriteAllBytes(path, tx.EncodeToPNG());

        Destroy(tx); // to avoid memory leaks
        UnityNativeSharingHelper.ShareScreenshotAndText("share your score with your friends", path, false, "Select App To Share With");
        // new NativeShare()
        //     .AddFile(path)
        //     .SetSubject("This is my score")
        //     .SetText("share your score with your friends")
        //     .Share();

        // if you are using panel share, deactivate panel share down here
    }
}

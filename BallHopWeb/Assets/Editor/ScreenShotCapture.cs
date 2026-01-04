using UnityEditor;
using UnityEngine;

public class ScreenShotCapture : EditorWindow
{
    [MenuItem("Tools/Screenshot Taker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ScreenShotCapture));
    }

    void OnGUI()
    {
        GUILayout.Label("Press Ctrl + Alt + S to take a screenshot.");
    }

    void Update()
    {
        if (Event.current != null)
        {
            if (Event.current.type == EventType.KeyDown &&
                Event.current.control && Event.current.alt && Event.current.keyCode == KeyCode.S)
            {
                TakeScreenshot();
            }
        }
    }

    static void TakeScreenshot()
    {
        string folderPath = "Assets/Screenshots/";
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        string fileName = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        string fullPath = folderPath + fileName;

        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log("Screenshot saved to " + fullPath);

        // Refresh the AssetDatabase after the screenshot has been saved.
        AssetDatabase.Refresh();
    }
}

using UnityEngine;

public class ScreenCaptureTool : MonoBehaviour
{
    [SerializeField] string filename;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot($"{filename}.png");
        }
    }
}
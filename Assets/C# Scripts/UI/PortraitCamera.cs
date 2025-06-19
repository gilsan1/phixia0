using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PortraitCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private RawImage portaitImage;

    private int width = 256;
    private int height = 256;
    private RenderTexture rt;



    private void Awake()
    {
        cam.enabled = false;
    }
    private IEnumerator Start()
    {
        yield return null;
        yield return null;
        CapturePortrait();
    }

    public void CapturePortrait()
    {
        cam.enabled = true;
        Texture2D portrait = Capture(cam, width, height);
        portaitImage.texture = portrait;
        cam.enabled = false;
    }

    private Texture2D Capture(Camera cam, int width, int height)
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
        cam.targetTexture = rt;

        Texture2D result = new Texture2D(width, height, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = rt;
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();

        cam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        return result;
    }
}
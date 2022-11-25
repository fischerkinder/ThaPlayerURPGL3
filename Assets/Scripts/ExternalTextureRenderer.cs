using System;
using UnityEngine;
using UnityEngine.UI;

public class ExternalTextureRenderer : MonoBehaviour
{
    [SerializeField] private RawImage image;
    private Texture2D _imageTexture2D;
    private IntPtr _nativeTexturePointer;

    private AndroidJavaObject _androidApiInstance;

    private void Start()
    {
        _imageTexture2D = new Texture2D(1280, 800, TextureFormat.ARGB32, false)
        { filterMode = FilterMode.Point };
        _imageTexture2D.Apply();
        image.texture = _imageTexture2D;
        _nativeTexturePointer = _imageTexture2D.GetNativeTexturePtr();

    }

    private void Update()
    {
        if (_androidApiInstance == null)
        {
            // it is important to call this in update method. Single Threaded Rendering will run in UnityMain Thread
            InitializeAndroidSurface(1280, 800);
        }
        else
        {
            _androidApiInstance.Call("updateTexImage");
        }
    }

    public void InitializeAndroidSurface(int viewportWidth, int viewportHeight)
    {

        AndroidJavaClass androidWebViewApiClass =
            new AndroidJavaClass("com.pvr.player.MainActivity");

        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject currentActivityObject = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

        _androidApiInstance =
            androidWebViewApiClass.CallStatic<AndroidJavaObject>("InitSurface", currentActivityObject,
                viewportWidth, viewportHeight, _nativeTexturePointer.ToInt32());
    }
}
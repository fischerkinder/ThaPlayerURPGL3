using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class XrEventBroadcaster : MonoBehaviour
{
    private AndroidJavaObject _ajc;
    
    private void Awake()
    {
        

        InitializeAndroidPlugin();
        
    }


    
    void Start()
    {
        

        
        
    }

    void Update()
    {
       
    }

    private void InitializeAndroidPlugin()
    {
        _ajc = new AndroidJavaClass("com.xrchisense.xrevent.broadcaster.GeckoViewPlugin");
        _ajc.CallStatic<AndroidJavaObject>("CreateInstance", new object[] { 1920, 1080, UserAgent.mobile.ToString("G") });

        
    }

    public enum UserAgent
    {
        mobile,
        desktop,
        vr
    }
}

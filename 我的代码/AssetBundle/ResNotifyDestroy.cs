﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResNotifyDestroy : MonoBehaviour
{
    public int UseCount = 0;
    public int RecyltCount = 0;

    public bool activeNotify = true;
    private bool _quit;
    private void OnDestroy()
    {
        if (SceneLoadManager.Instance != null && SceneLoadManager.Instance.isInTestScene)
        {
            activeNotify = false;
        }

        if (activeNotify && !_quit && !GameController.ApplicationQuit)
            DebugUtil.LogError("The res object should be detroyed by Res.Recyle:  "+transform.gameObject.name);
    }
    private void OnApplicationQuit()
    {
        _quit = true;
    }
}

using System;
using UnityEngine;

namespace ScreenshotCompanionFramework
{
    [Serializable]
    public class CameraObject
    {
        public GameObject cam;
        [HideInInspector] public bool deleteQuestion;
        public KeyCode hotkey = KeyCode.None;
    }
}
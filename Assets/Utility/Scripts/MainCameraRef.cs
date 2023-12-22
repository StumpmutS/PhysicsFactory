using UnityEngine;

namespace Utility.Scripts
{
    public static class MainCameraRef
    {
        private static Camera _mainCamera;
        
        public static Camera Cam
        {
            get
            {
                if (_mainCamera == null) _mainCamera = Camera.main;
                return _mainCamera;
            }
        }
    }
}

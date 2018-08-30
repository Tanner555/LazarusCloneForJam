using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    [ExecuteInEditMode]
    public class OrthosizeEditorUtility : MonoBehaviour
    {
        #region FieldsAndProps
        public float desiredCameraHeight = 5f;
        private float repeatRate = 0.05f;

        Camera myCamera
        {
            get
            {
                if (_myCamera == null)
                    _myCamera = Camera.main;

                return _myCamera;
            }
        }
        Camera _myCamera = null;

        float desiredAspect
        {
            get { return 16f / 9f; }
        }

        float aspect
        {
            get { return myCamera.aspect; }
        }

        float ratio
        {
            get { return desiredAspect / aspect; }
        }
        #endregion

        private void Update()
        {
            UpdateOrthographicCameraHeight();
        }

        void UpdateOrthographicCameraHeight()
        {
            if (myCamera == null ||
                myCamera.orthographic == false)
            {
                if (myCamera == null)
                    Debug.Log("myCamera is null");

                if (myCamera.orthographic == false)
                    Debug.Log("myCamera isn't orthographic");

                CancelInvoke();
                return;
            }

            myCamera.orthographicSize = desiredCameraHeight * ratio;
        }
    }
}
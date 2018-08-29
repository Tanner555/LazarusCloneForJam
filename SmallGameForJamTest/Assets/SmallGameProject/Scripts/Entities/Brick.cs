using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class Brick : MonoBehaviour
    {
        [Range(0.0f, 5f)]
        public float downwardSpeed = 1f;
        float downwardRepeatRate = 0.5f;

        private void Start()
        {
            InvokeRepeating("SE_UpdateBrickPosition", 0.1f, downwardRepeatRate);
        }

        void SE_UpdateBrickPosition()
        {
            this.transform.position = this.transform.position +
                new Vector3(0.0f, -downwardSpeed, 0.0f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            
        }
    }
}

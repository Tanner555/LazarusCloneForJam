using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class Brick : MonoBehaviour
    {
        #region Fields
        [Range(0.0f, 5f)]
        public float downwardSpeed = 1f;
        float downwardRepeatRate = 0.5f;
        #endregion

        #region ComponentProperties
        GameMaster gamemaster
        {
            get { return GameMaster.thisInstance; }
        }

        GameManager gamemanager
        {
            get { return GameManager.thisInstance; }
        }
        #endregion

        #region UnityMessages
        private void Start()
        {
            InvokeRepeating("SE_UpdateBrickPosition", downwardRepeatRate, downwardRepeatRate);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == gamemanager.AreaBoundsTag)
            {
                gamemaster.CallOnBrickWasPlaced();
            }
        }
        #endregion

        #region Services
        void SE_UpdateBrickPosition()
        {
            this.transform.position = this.transform.position +
                new Vector3(0.0f, -downwardSpeed, 0.0f);
        }
        #endregion

    }
}

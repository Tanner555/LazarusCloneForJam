using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class PlayerCollisionChecker : MonoBehaviour
    {
        #region Fields
        public bool bIsTriggering = false;
        public bool bOutsidePlayArea = false;

        bool bHasDelayStart = false;
        #endregion

        #region Properties
        public bool bTriggeringOrOutsideBounds
        {
            get { return bIsTriggering || bOutsidePlayArea; }
        }

        public bool bTriggerAndNotOutsideBounds
        {
            get { return bIsTriggering && !bOutsidePlayArea; }
        }

        public bool bNotTriggerAndNotOutsideBounds
        {
            get { return !bIsTriggering && !bOutsidePlayArea; }
        }
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
            Invoke("DelayStart", 0.5f);
        }

        void DelayStart()
        {
            bHasDelayStart = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (bHasDelayStart == false) return;
            if(collision.tag == gamemanager.BrickTag)
            {
                bIsTriggering = true;
            }else if(collision.tag == gamemanager.AreaBoundsTag)
            {
                bOutsidePlayArea = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (bHasDelayStart == false) return;
            if (collision.tag == gamemanager.BrickTag)
            {
                bIsTriggering = false;
            }else if(collision.tag == gamemanager.AreaBoundsTag)
            {
                bOutsidePlayArea = true;
            }
        }
        #endregion
    }
}
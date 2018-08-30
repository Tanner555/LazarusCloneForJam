using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class PlayerCollisionChecker : MonoBehaviour
    {
        #region Fields
        [HideInInspector]
        public bool bIsTriggering = false;
        [HideInInspector]
        public bool bIsTriggeringWFallingBrick = false;
        [HideInInspector]
        public bool bOutsidePlayArea = false;

        bool bHasDelayStart = false;

        [Header("Brick Checking On Trigger")]
        [Tooltip("Should Brick Checking Be On Trigger, Default Is False And Updated On Linecast")]
        public bool bUpdateOnTrigger = false;
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

        #region Handlers
        void ClearBrickIsFalling()
        {
            if (bIsTriggeringWFallingBrick)
            {
                bIsTriggeringWFallingBrick = false;
                bIsTriggering = true;
            }
        }
        #endregion

        #region PublicPlayerMethods
        public void UpdateIsTriggeringByLinecasting(Vector3 _startPos, bool _debug = false)
        {
            if (_debug)
            {
                Debug.DrawLine(_startPos, transform.position, Color.green, 1f, false);
            }
            //All Collision Checkers should be on the IgnoreRaycast Layer
            RaycastHit2D myHit = Physics2D.Linecast(_startPos, transform.position, gamemanager.CheckForCollisionLayersIgnorePlayerAndBounds);
            if (myHit.transform != null)
            {
                if(myHit.transform.tag == gamemanager.BrickTag)
                {
                    bIsTriggering = true;
                    bIsTriggeringWFallingBrick = false;
                }else if(myHit.transform.tag == gamemanager.BrickFallingTag)
                {
                    bIsTriggering = false;
                    bIsTriggeringWFallingBrick = true;
                }
            }
            else
            {
                bIsTriggering = false;
                bIsTriggeringWFallingBrick = false;
            }
        }
        #endregion

        #region UnityMessages
        private void OnEnable()
        {
            bOutsidePlayArea = gamemanager.isOutsideBounds(this.transform.position);
            gamemaster.OnBrickWasPlaced += ClearBrickIsFalling;
        }

        private void OnDisable()
        {
            gamemaster.OnBrickWasPlaced -= ClearBrickIsFalling;
        }

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
            if (collision.tag == gamemanager.AreaBoundsTag)
            {
                bOutsidePlayArea = false;
            }

            if (bHasDelayStart == false) return;

            if (bUpdateOnTrigger)
            {
                if (collision.tag == gamemanager.BrickTag)
                {
                    bIsTriggering = true;
                    bIsTriggeringWFallingBrick = false;
                }
                else if (collision.tag == gamemanager.BrickFallingTag)
                {
                    bIsTriggeringWFallingBrick = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == gamemanager.AreaBoundsTag)
            {
                bOutsidePlayArea = true;
            }

            if (bHasDelayStart == false) return;

            if (bUpdateOnTrigger)
            {
                if (collision.tag == gamemanager.BrickTag)
                {
                    bIsTriggering = false;
                }
                else if (collision.tag == gamemanager.BrickFallingTag)
                {
                    bIsTriggeringWFallingBrick = false;
                }
            }
        }
        #endregion
    }
}
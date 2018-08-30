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

        private float distanceCheck = 5f;
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
            var _heading = transform.position - _startPos;
            var _distance = _heading.magnitude;
            var _direction = _heading / _distance;

            RaycastHit2D[] _myHits = Physics2D.RaycastAll(_startPos, _direction, distanceCheck, gamemanager.CheckForCollisionLayersIgnorePlayerAndBounds);
            if(_myHits.Length <= 0)
            {
                bIsTriggering = false;
                bIsTriggeringWFallingBrick = false;
            }
            else
            {
                //Used For Debugging
            }

            float _closestDistance = float.MaxValue;
            Transform _closestBrick = null;
            float _distanceFromStartPos = Vector3.Distance(_startPos, transform.position);

            foreach (var _myHit in _myHits)
            {
                string _hitTag = _myHit.transform.tag;
                if (_hitTag != gamemanager.BrickTag &&
                    _hitTag != gamemanager.BrickFallingTag)
                {
                    continue;
                }
                else
                {
                    float _checkDistance = Vector3.Distance(_myHit.transform.position, transform.position);
                    //Only Include Closest Brick If Distance Is Less Than From Start Position To Checker
                    if (_checkDistance > _distanceFromStartPos) continue;
                    else if (_checkDistance < _closestDistance)
                    {
                        _closestBrick = _myHit.transform;
                        _closestDistance = _checkDistance;
                    }
                }
            }

            if (_closestBrick != null)
            {
                if (_closestBrick.tag == gamemanager.BrickTag)
                {
                    bIsTriggering = true;
                    bIsTriggeringWFallingBrick = false;
                }
                else if (_closestBrick.tag == gamemanager.BrickFallingTag)
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
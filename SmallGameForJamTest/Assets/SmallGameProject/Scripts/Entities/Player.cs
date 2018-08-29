﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class Player : MonoBehaviour
    {
        #region Fields
        protected float MovementSpeed = 1;

        //CollisionCheckers
        [Header("CollisionCheckers")]
        [SerializeField]
        PlayerCollisionChecker LeftChecker = null;
        [SerializeField]
        PlayerCollisionChecker UpperLeftChecker = null;
        [SerializeField]
        PlayerCollisionChecker LowerLeftChecker = null;
        [SerializeField]
        PlayerCollisionChecker RightChecker = null;
        [SerializeField]
        PlayerCollisionChecker UpperRightChecker = null;
        [SerializeField]
        PlayerCollisionChecker LowerRightChecker = null;
        #endregion

        #region Properties
        BoxCollider2D myCollider
        {
            get
            {
                if (_myCollider == null)
                    _myCollider = GetComponent<BoxCollider2D>();

                return _myCollider;
            }
        }
        BoxCollider2D _myCollider = null;

        bool bHasAllCheckers
        {
            get
            {
                return LeftChecker && UpperLeftChecker && LowerLeftChecker
                    && RightChecker && LowerRightChecker && UpperRightChecker;
            }
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

        #region EasyProperties
        float LeftBounds
        {
            get { return gamemanager.LeftBounds; }
        }
        float RightBounds
        {
            get { return gamemanager.RightBounds; }
        }
        float UpwardBounds
        {
            get { return gamemanager.UpwardBounds; }
        }
        float DownwardBounds
        {
            get { return gamemanager.DownwardBounds; }
        }
        float LeftBoundsWPlayer
        {
            get { return LeftBounds + myCollider.size.x / 2; }
        }
        float RightBoundsWPlayer
        {
            get { return RightBounds - myCollider.size.x / 2; }
        }
        #endregion

        #region UnityMessages
        private void OnEnable()
        {
            SubToEvents();
        }

        private void OnDisable()
        {
            UnsubFromEvents();
        }
        #endregion

        #region Handlers
        void OnMoveLeft()
        {
            //Only Move If Hasn't Reached Bounds
            if (this.transform.position.x - MovementSpeed <= LeftBounds) return;

            //Only Move If Has All Checkers
            if (bHasAllCheckers == false)
            {
                Debug.LogError("Doesn't have all checkers");
                return;
            }

            if (LeftChecker.bTriggerAndNotOutsideBounds)
            {

            }
            else if (LeftChecker.bNotTriggerAndNotOutsideBounds)
            {
                this.transform.position = this.transform.position +
                    new Vector3(-MovementSpeed, 0, 0);
            }
        }

        void OnMoveRight()
        {
            //Only Move If Hasn't Reached Bounds
            if (this.transform.position.x + MovementSpeed >= RightBounds) return;

            //Only Move If Has All Checkers
            if (bHasAllCheckers == false)
            {
                Debug.LogError("Doesn't have all checkers");
                return;
            }

            if (RightChecker.bTriggerAndNotOutsideBounds)
            {

            }
            else if (RightChecker.bNotTriggerAndNotOutsideBounds)
            {
                this.transform.position = this.transform.position +
                new Vector3(MovementSpeed, 0, 0);
            }
        }
        #endregion

        #region Initialization
        void SubToEvents()
        {
            gamemaster.OnInputMoveLeft += OnMoveLeft;
            gamemaster.OnInputMoveRight += OnMoveRight;
        }

        void UnsubFromEvents()
        {
            gamemaster.OnInputMoveLeft -= OnMoveLeft;
            gamemaster.OnInputMoveRight -= OnMoveRight;
        }
        #endregion
    }
}
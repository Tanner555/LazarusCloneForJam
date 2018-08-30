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
        PlayerCollisionChecker VeryLowerLeftChecker = null;
        [SerializeField]
        PlayerCollisionChecker RightChecker = null;
        [SerializeField]
        PlayerCollisionChecker UpperRightChecker = null;
        [SerializeField]
        PlayerCollisionChecker LowerRightChecker = null;
        [SerializeField]
        PlayerCollisionChecker VeryLowerRightChecker = null;
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
                    && RightChecker && LowerRightChecker && UpperRightChecker
                    && VeryLowerLeftChecker && VeryLowerRightChecker;
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

            //Using Left Check Instead Of Player To Avoid Unnecessary Collisions
            VeryLowerLeftChecker.UpdateIsTriggeringByLinecasting(LeftChecker.transform.position);
            
            LowerLeftChecker.UpdateIsTriggeringByLinecasting(transform.position);
            LeftChecker.UpdateIsTriggeringByLinecasting(transform.position);
            UpperLeftChecker.UpdateIsTriggeringByLinecasting(transform.position);

            if (LowerLeftChecker.bNotTriggerAndNotOutsideBounds &&
                LowerLeftChecker.bIsTriggeringWFallingBrick == false &&
                LeftChecker.bIsTriggeringWFallingBrick == false &&
                VeryLowerLeftChecker.bTriggeringOrOutsideBounds)
            {
                this.transform.position = this.transform.position +
                new Vector3(-MovementSpeed, -MovementSpeed, 0);
            }
            else if (LeftChecker.bTriggerAndNotOutsideBounds &&
                LeftChecker.bIsTriggeringWFallingBrick == false)
            {
                if (UpperLeftChecker.bNotTriggerAndNotOutsideBounds &&
                    UpperLeftChecker.bIsTriggeringWFallingBrick == false)
                {
                    this.transform.position = this.transform.position +
                    new Vector3(-MovementSpeed, MovementSpeed, 0);
                }
            }
            else if (LeftChecker.bNotTriggerAndNotOutsideBounds &&
                LeftChecker.bIsTriggeringWFallingBrick == false &&
                LowerLeftChecker.bTriggeringOrOutsideBounds)
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

            //Using Right Check Instead Of Player To Avoid Unnecessary Collisions
            VeryLowerRightChecker.UpdateIsTriggeringByLinecasting(RightChecker.transform.position);

            LowerRightChecker.UpdateIsTriggeringByLinecasting(transform.position);
            RightChecker.UpdateIsTriggeringByLinecasting(transform.position);
            UpperRightChecker.UpdateIsTriggeringByLinecasting(transform.position);

            if (LowerRightChecker.bNotTriggerAndNotOutsideBounds &&
                LowerRightChecker.bIsTriggeringWFallingBrick == false &&
                RightChecker.bIsTriggeringWFallingBrick == false &&
                VeryLowerRightChecker.bTriggeringOrOutsideBounds)
            {
                this.transform.position = this.transform.position +
                new Vector3(MovementSpeed, -MovementSpeed, 0);
            }
            else if (RightChecker.bTriggerAndNotOutsideBounds &&
                RightChecker.bIsTriggeringWFallingBrick == false)
            {
                if (UpperRightChecker.bNotTriggerAndNotOutsideBounds &&
                    UpperRightChecker.bIsTriggeringWFallingBrick == false)
                {
                    this.transform.position = this.transform.position +
                    new Vector3(MovementSpeed, MovementSpeed, 0);
                }
            }
            else if (RightChecker.bNotTriggerAndNotOutsideBounds &&
                RightChecker.bIsTriggeringWFallingBrick == false &&
                LowerRightChecker.bTriggeringOrOutsideBounds)
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
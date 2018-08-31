using System.Collections;
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

        bool bIsMoving
        {
            get { return myEventHandler.bPlayerIsMoving; }
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

        PlayerEventHandler myEventHandler
        {
            get
            {
                if (_myEventHandler == null)
                    _myEventHandler = GetComponent<PlayerEventHandler>();

                return _myEventHandler;
            }
        }
        PlayerEventHandler _myEventHandler = null;
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
            //Only Move If Hasn't Reached Bounds Or Isn't Moving
            if (this.transform.position.x - MovementSpeed <= LeftBounds || bIsMoving) return;

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
                myEventHandler.CallOnPlayerMoveStart(EPlayerMovementPosition.LowerLeft);
            }
            else if (LeftChecker.bTriggerAndNotOutsideBounds &&
                LeftChecker.bIsTriggeringWFallingBrick == false)
            {
                if (UpperLeftChecker.bNotTriggerAndNotOutsideBounds &&
                    UpperLeftChecker.bIsTriggeringWFallingBrick == false)
                {
                    myEventHandler.CallOnPlayerMoveStart(EPlayerMovementPosition.UpperLeft);
                }
            }
            else if (LeftChecker.bNotTriggerAndNotOutsideBounds &&
                LeftChecker.bIsTriggeringWFallingBrick == false &&
                LowerLeftChecker.bTriggeringOrOutsideBounds)
            {
                myEventHandler.CallOnPlayerMoveStart(EPlayerMovementPosition.Left);
            }
        }

        void OnMoveRight()
        {
            //Only Move If Hasn't Reached Bounds Or Isn't Moving
            if (this.transform.position.x + MovementSpeed >= RightBounds || bIsMoving) return;

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
                myEventHandler.CallOnPlayerMoveStart(EPlayerMovementPosition.LowerRight);
            }
            else if (RightChecker.bTriggerAndNotOutsideBounds &&
                RightChecker.bIsTriggeringWFallingBrick == false)
            {
                if (UpperRightChecker.bNotTriggerAndNotOutsideBounds &&
                    UpperRightChecker.bIsTriggeringWFallingBrick == false)
                {
                    myEventHandler.CallOnPlayerMoveStart(EPlayerMovementPosition.UpperRight);
                }
            }
            else if (RightChecker.bNotTriggerAndNotOutsideBounds &&
                RightChecker.bIsTriggeringWFallingBrick == false &&
                LowerRightChecker.bTriggeringOrOutsideBounds)
            {
                myEventHandler.CallOnPlayerMoveStart(EPlayerMovementPosition.Right);
            }
        }

        void OnPlayerEndMove(EPlayerMovementPosition _movePos)
        {
            switch (_movePos)
            {
                case EPlayerMovementPosition.LowerLeft:
                    this.transform.position = this.transform.position +
                    new Vector3(-MovementSpeed, -MovementSpeed, 0);
                    break;
                case EPlayerMovementPosition.Left:
                    this.transform.position = this.transform.position +
                    new Vector3(-MovementSpeed, 0, 0);
                    break;
                case EPlayerMovementPosition.UpperLeft:
                    this.transform.position = this.transform.position +
                    new Vector3(-MovementSpeed, MovementSpeed, 0);
                    break;
                case EPlayerMovementPosition.LowerRight:
                    this.transform.position = this.transform.position +
                    new Vector3(MovementSpeed, -MovementSpeed, 0);
                    break;
                case EPlayerMovementPosition.Right:
                    this.transform.position = this.transform.position +
                    new Vector3(MovementSpeed, 0, 0);
                    break;
                case EPlayerMovementPosition.UpperRight:
                    this.transform.position = this.transform.position +
                    new Vector3(MovementSpeed, MovementSpeed, 0);
                    break;
                default:
                    break;
            }
        }

        void KillPlayer()
        {
            Destroy(this.gameObject, 0.0f);
        }
        #endregion

        #region Initialization
        void SubToEvents()
        {
            gamemaster.OnInputMoveLeft += OnMoveLeft;
            gamemaster.OnInputMoveRight += OnMoveRight;
            gamemaster.OnPlayerWasKilled += KillPlayer;
            myEventHandler.OnPlayerMoveEnd += OnPlayerEndMove;
        }

        void UnsubFromEvents()
        {
            gamemaster.OnInputMoveLeft -= OnMoveLeft;
            gamemaster.OnInputMoveRight -= OnMoveRight;
            gamemaster.OnPlayerWasKilled -= KillPlayer;
            myEventHandler.OnPlayerMoveEnd -= OnPlayerEndMove;
        }
        #endregion
    }
}
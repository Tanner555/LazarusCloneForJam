using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    #region Enums
    public enum EPlayerMovementPosition
    {
        LowerLeft = 0, Left = 1, UpperLeft = 2,
        LowerRight = 3, Right = 4, UpperRight = 5
    }
    #endregion

    public class PlayerEventHandler : MonoBehaviour
    {
        #region Fields
        private bool _bPlayerIsMoving = false;
        #endregion

        #region Properties
        public bool bPlayerIsMoving
        {
            get { return _bPlayerIsMoving; }
        }

        #endregion

        #region DelegatesAndEvents
        public delegate void PlayerMovementPosHandler(EPlayerMovementPosition _movePos);
        public event PlayerMovementPosHandler OnPlayerMoveStart;
        public event PlayerMovementPosHandler OnPlayerMoveEnd;
        #endregion

        #region EventCalls
        public void CallOnPlayerMoveStart(EPlayerMovementPosition _movePos)
        {
            _bPlayerIsMoving = true;
            if (OnPlayerMoveStart != null) OnPlayerMoveStart(_movePos);
        }

        public void CallOnPlayerMoveEnd(EPlayerMovementPosition _movePos)
        {
            _bPlayerIsMoving = false;
            if (OnPlayerMoveEnd != null) OnPlayerMoveEnd(_movePos);
        }
        #endregion

        #region UnityMessages
        //// Use this for initialization
        //void Start()
        //{

        //}

        //// Update is called once per frame
        //void Update()
        //{

        //}
        #endregion
    }
}
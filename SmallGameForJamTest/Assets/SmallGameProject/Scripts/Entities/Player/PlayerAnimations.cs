using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class PlayerAnimations : MonoBehaviour
    {
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

        Player myPlayer
        {
            get
            {
                if (_myPlayer == null)
                    _myPlayer = GetComponent<Player>();

                return _myPlayer;
            }
        }
        Player _myPlayer = null;
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
        void StartPlayingAnimation(EPlayerMovementPosition _movePos)
        {
            Debug.Log($"Moving To {_movePos}");
            myEventHandler.CallOnPlayerMoveEnd(_movePos);
        }
        #endregion

        #region Initialization
        void SubToEvents()
        {
            myEventHandler.OnPlayerMoveStart += StartPlayingAnimation;
        }

        void UnsubFromEvents()
        {
            myEventHandler.OnPlayerMoveStart -= StartPlayingAnimation;
        }
        #endregion
    }
}
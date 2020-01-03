using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class SoundManager : MonoBehaviour
    {
        #region Fields
        public AudioClip JumpSound;
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

        PlayerEventHandler playerEventHandler
        {
            get
            {
                if (_playerEventHandler == null)
                    _playerEventHandler = gamemanager.playerTransform.GetComponent<PlayerEventHandler>();

                return _playerEventHandler;
            }
        }
        PlayerEventHandler _playerEventHandler = null;

        AudioSource myAudioSource
        {
            get
            {
                if (_myAudioSource == null)
                    _myAudioSource = GetComponent<AudioSource>();

                return _myAudioSource;
            }
        }
        AudioSource _myAudioSource = null;
        #endregion

        #region UnityMessages
        // Use this for initialization
        void Start()
        {
            SubToEvents();
        }

        private void OnDisable()
        {
            if (gamemanager != null && 
                gamemanager.playerInstance != null &&
                gamemanager.playerTransform != null)
            {
                UnsubFromEvents();
            }
        }
        #endregion

        #region Handlers
        void OnPlayerMove(EPlayerMovementPosition _movePos)
        {
            myAudioSource.clip = JumpSound;
            myAudioSource.Play();
        }
        #endregion

        #region Initialization
        void SubToEvents()
        {
            playerEventHandler.OnPlayerMoveStart += OnPlayerMove;
        }

        void UnsubFromEvents()
        {
            playerEventHandler.OnPlayerMoveStart -= OnPlayerMove;
        }
        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class GameManager : BaseSingleton<GameManager>
    {
        #region Fields
        [SerializeField]
        protected BoxCollider2D AreaBounds;
        #endregion

        #region BoundsProperties
        /// <summary>
        /// Got Bound Formulas From Unity Answers At
        /// https://answers.unity.com/questions/860212/world-coordinates-of-boxcollider2d.html
        /// </summary>
        public float LeftBounds
        {
            get
            {
                if (_LeftBounds == float.MaxValue)
                {
                    _LeftBounds = AreaBounds == null ? 0.0f :
                    AreaBounds.offset.x - (AreaBounds.size.x / 2f);
                }
                return _LeftBounds;
            }
        }
        private float _LeftBounds = float.MaxValue;

        public float RightBounds
        {
            get
            {
                if (_RightBounds == float.MaxValue)
                {
                    _RightBounds = AreaBounds == null ? 0.0f :
                        AreaBounds.offset.x + (AreaBounds.size.x / 2f);
                }
                return _RightBounds;
            }
        }
        private float _RightBounds = float.MaxValue;

        public float UpwardBounds
        {
            get
            {
                if (_UpwardBounds == float.MaxValue)
                {
                    _UpwardBounds = AreaBounds == null ? 0.0f :
                    AreaBounds.offset.y + (AreaBounds.size.y / 2f);
                }
                return _UpwardBounds;
            }
        }
        private float _UpwardBounds = float.MaxValue;

        public float DownwardBounds
        {
            get
            {
                if (_DownwardBounds == float.MaxValue)
                {
                    _DownwardBounds = AreaBounds == null ? 0.0f :
                    AreaBounds.offset.y - (AreaBounds.size.y / 2f);
                }
                return _DownwardBounds;
            }
        }
        private float _DownwardBounds = float.MaxValue;
        #endregion

        #region PlayerProperties
        public Player playerInstance
        {
            get
            {
                if (_playerInstance == null)
                    _playerInstance = GameObject.FindObjectOfType<Player>();

                return _playerInstance;
            }
        }
        private Player _playerInstance = null;

        public Transform playerTransform
        {
            get
            {
                return playerInstance.transform;
            }
        }
        #endregion

        #region ComponenProperties
        GameMaster gamemaster
        {
            get { return GameMaster.thisInstance; }
        }
        #endregion

        #region UnityMessages
        private void Start()
        {
            if(AreaBounds == null)
            {
                Debug.LogError("No Area Bounds On GameManager");
            }
            else
            {
                //Start Game!
                
            }
        }
        #endregion
    }
}
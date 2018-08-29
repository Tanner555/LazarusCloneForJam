using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class Player : MonoBehaviour
    {
        #region Fields
        protected float MovementSpeed = 1;
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
            if (this.transform.position.x - MovementSpeed <= LeftBounds) return;
            //Only Move If Hasn't Reached Bounds
            this.transform.position = this.transform.position +
                new Vector3(-MovementSpeed, 0, 0);
        }

        void OnMoveRight()
        {
            if (this.transform.position.x + MovementSpeed >= RightBounds) return;
            //Only Move If Hasn't Reached Bounds
            this.transform.position = this.transform.position +
            new Vector3(MovementSpeed, 0, 0);
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
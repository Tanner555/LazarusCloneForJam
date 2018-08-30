using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class InputManager : BaseSingleton<InputManager>
    {
        #region Fields
        [Header("Inputs Used For Game")]
        public KeyCode moveLeft;
        public KeyCode moveRight;
        public KeyCode gameMenu;
        #endregion

        #region Properties
        GameMaster gamemaster
        {
            get { return GameMaster.thisInstance; }
        }

        GameManager gamemanager
        {
            get { return GameManager.thisInstance; }
        }

        protected UiMaster uiMaster
        {
            get { return UiMaster.thisInstance; }
        }
        #endregion

        #region UnityMessages
        private void Start()
        {
            if (gamemaster == null || gamemanager == null ||
                uiMaster == null)
            {
                Debug.LogError("Missing Components From Input Manager");
                Destroy(this, 0.1f);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (gamemaster.bIsGamePaused == false)
            {
                if (Input.GetKeyDown(moveLeft))
                {
                    gamemaster.CallOnInputMoveLeft();
                }
                if (Input.GetKeyDown(moveRight))
                {
                    gamemaster.CallOnInputMoveRight();
                }
            }
            if (Input.GetKeyDown(gameMenu))
            {
                uiMaster.CallEventMenuToggle();
            }
        }
        #endregion

    }
}
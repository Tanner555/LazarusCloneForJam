using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class InputManager : BaseSingleton<InputManager>
    {
        [Header("Inputs Used For Game")]
        public KeyCode moveLeft;
        public KeyCode moveRight;

        #region Properties
        GameMaster gamemaster
        {
            get { return GameMaster.thisInstance; }
        }

        GameManager gamemanager
        {
            get { return GameManager.thisInstance; }
        }
        #endregion

        // Update is called once per frame
        void Update()
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


    }
}
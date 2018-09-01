using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class WinPlacement : MonoBehaviour
    {
        #region Properties
        GameMaster gamemaster
        {
            get { return GameMaster.thisInstance; }
        }

        GameManager gamemanager
        {
            get { return GameManager.thisInstance; }
        }

        GameInstance gameinstance
        {
            get { return GameInstance.thisInstance; }
        }

        protected UiMaster uiMaster
        {
            get { return UiMaster.thisInstance; }
        }
        #endregion

        #region UnityMessages
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == gamemanager.PlayerTag)
            {
                Debug.Log("Player Won!");
                gamemaster.CallOnPlayerWon();
            }
        }
        #endregion
    }
}
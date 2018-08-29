using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class GameMaster : BaseSingleton<GameMaster>
    {
        #region DelegatesAndEvents
        //Input Events
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler OnInputMoveLeft;
        public event GeneralEventHandler OnInputMoveRight;
        //Game Events
        public event GeneralEventHandler OnBrickWasPlaced;
        public event GeneralEventHandler OnPlayerWasKilled;
        public event GeneralEventHandler OnPlayerWon;
        #endregion

        #region EventCalls
        //Input Event Calls
        public void CallOnInputMoveLeft()
        {
            if (OnInputMoveLeft != null) OnInputMoveLeft();
        }

        public void CallOnInputMoveRight()
        {
            if (OnInputMoveRight != null) OnInputMoveRight();
        }

        //Game Events
        public void CallOnBrickWasPlaced()
        {
            if (OnBrickWasPlaced != null) OnBrickWasPlaced();
        }

        public void CallOnPlayerWasKilled()
        {
            if (OnPlayerWasKilled != null) OnPlayerWasKilled();
        }

        public void CallOnPlayerWon()
        {
            if (OnPlayerWon != null) OnPlayerWon();
        }
        #endregion
    }
}
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
        #endregion
    }
}
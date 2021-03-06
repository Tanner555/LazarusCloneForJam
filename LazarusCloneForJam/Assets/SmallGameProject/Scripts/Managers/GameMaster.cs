﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class GameMaster : BaseSingleton<GameMaster>
    {
        #region Properties
        //Components
        public GameInstance gameInstance
        {
            get { return GameInstance.thisInstance; }
        }

        public GameManager gamemode
        {
            get { return GameManager.thisInstance; }
        }

        protected UiMaster uiMaster
        {
            get { return UiMaster.thisInstance; }
        }

        //Access Properties
        public virtual bool bIsGamePaused
        {
            get { return _bIsGamePaused; }
            protected set { _bIsGamePaused = value; }
        }
        private bool _bIsGamePaused = false;

        public virtual bool isGameOver
        {
            get; protected set;
        }
        public virtual bool isMenuOn
        {
            get; protected set;
        }
        #endregion

        #region Fields
        //May Not Need These
        protected float loadLevelDelay = 0.2f;
        protected float timePauseDelay = 0.05f;
        #endregion

        #region DelegatesAndEvents
        //Input Events
        public delegate void GeneralEventHandler();
        public delegate void GeneralVector3ListHandler(List<Vector3> _locations);
        public event GeneralEventHandler OnInputMoveLeft;
        public event GeneralEventHandler OnInputMoveRight;
        //Game Events
        public event GeneralEventHandler OnBrickWasPlaced;
        public event GeneralVector3ListHandler OnBricksWereDestroyed;
        public event GeneralEventHandler OnPlayerWasKilled;
        public event GeneralEventHandler OnPlayerWon;
        //Debugging
        public event GeneralEventHandler OnPressedDebugKey;
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

        public void CallOnBricksWereDestroyed(List<Vector3> _locations)
        {
            if (OnBricksWereDestroyed != null) OnBricksWereDestroyed(_locations);
        }

        public void CallOnPlayerWasKilled()
        {
            uiMaster.CallEventAnyUIToggle(true);
            if (OnPlayerWasKilled != null) OnPlayerWasKilled();
        }

        public void CallOnPlayerWon()
        {
            uiMaster.CallEventAnyUIToggle(true);
            if (OnPlayerWon != null) OnPlayerWon();
        }

        public void CallOnToggleIsGamePaused()
        {
            CallOnToggleIsGamePaused(!bIsGamePaused);
        }

        public void CallOnToggleIsGamePaused(bool _enable)
        {
            bIsGamePaused = _enable;
            Time.timeScale = _enable ? 0f : 1f;
        }

        //Debug
        public void CallOnPressedDebugKey()
        {
            if (OnPressedDebugKey != null) OnPressedDebugKey();
        }
        #endregion

        #region Handlers
        void HandleAnyUIToggle(bool _enable)
        {
            CallOnToggleIsGamePaused(_enable);
        }
        #endregion

        #region UnityMessages
        protected virtual void OnEnable()
        {
            SubToEvents();
        }

        protected virtual void OnDisable()
        {
            UnsubFromEvents();
        }
        #endregion

        #region Initialization
        protected virtual void SubToEvents()
        {
            uiMaster.EventAnyUIToggle += HandleAnyUIToggle;
        }

        protected virtual void UnsubFromEvents()
        {
            uiMaster.EventAnyUIToggle -= HandleAnyUIToggle;
        }
        #endregion
    }
}
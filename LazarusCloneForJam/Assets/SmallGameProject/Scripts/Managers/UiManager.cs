using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LazarusClone
{
    public class UiManager : BaseSingleton<UiManager>
    {
        #region Properties
        public GameMaster gamemaster { get { return GameMaster.thisInstance; } }
        public GameManager gamemode { get { return GameManager.thisInstance; } }
        public GameInstance gameInstance { get { return GameInstance.thisInstance; } }

        public virtual bool AllUiCompsAreValid
        {
            get
            {
                return MenuUiPanel && WinnerUiPanel &&
                  NextLevelButton && GameOverUiPanel;
            }
        }

        public UiMaster uiMaster
        {
            get
            {
                return UiMaster.thisInstance;
            }
        }
        #endregion

        #region Fields
        bool hasStarted = false;
        #endregion

        #region UIGameObjects
        [Header("Main Ui GameObjects")]
        public GameObject MenuUiPanel;

        [Header("Winner/GameOver UI")]
        public GameObject WinnerUiPanel;
        public GameObject NextLevelButton;
        public GameObject GameOverUiPanel;

        #endregion

        #region UnityMessages
        protected virtual void OnEnable()
        {
            SubToEvents();
        }

        protected virtual void Start()
        {
            if (hasStarted == false)
            {
                hasStarted = true;
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void OnDisable()
        {
            UnsubEvents();
        }
        #endregion

        #region ButtonCalls-MainMenu
        public virtual void CallGoToMainMenu()
        {
            if (gamemaster.bIsGamePaused)
            {
                gamemaster.CallOnToggleIsGamePaused(false);
            }
            gameInstance.GoToMainMenu();
        }

        public virtual void CallCheckNextLevel()
        {
            bool _scenario = false;
            bool _level = false;
            if (gameInstance.IsLoadingNextPermitted(out _scenario, out _level))
            {
                if (gamemaster.bIsGamePaused)
                {
                    gamemaster.CallOnToggleIsGamePaused(false);
                }
                if (_scenario) gameInstance.GoToNextScenario();
                else if (_level) gameInstance.GoToNextLevel();
            }
        }

        public virtual void CallRestartLevel()
        {
            if (gamemaster.bIsGamePaused)
            {
                gamemaster.CallOnToggleIsGamePaused(false);
            }
            gameInstance.RestartCurrentLevel();
        }
        #endregion

        #region Handlers-General/Toggles
        //Toggles Ui GameObjects
        protected virtual void TogglePauseMenuUi(bool enable)
        {
            if (MenuUiPanel != null)
                MenuUiPanel.SetActive(enable);
        }

        //Winner / GameOver Activations
        protected virtual void ActivateWinnerUi()
        {
            if (AllUiCompsAreValid == false) return;
            WinnerUiPanel.SetActive(true);
            bool _nextScenario = false;
            bool _nextLevel = false;
            if (gameInstance.IsLoadingNextPermitted(out _nextScenario, out _nextLevel))
            {
                NextLevelButton.SetActive(true);
                Text _btnText = NextLevelButton.GetComponentInChildren<Text>();
                if (_btnText && _nextScenario)
                {
                    _btnText.text = "Go To Next Scenario";
                }
                else if (_btnText && _nextLevel)
                {
                    _btnText.text = "Go To Next Level";
                }
            }
            else
            {
                NextLevelButton.SetActive(false);
            }
        }

        protected virtual void ActivateGameOverUi()
        {
            if (AllUiCompsAreValid == false) return;
            GameOverUiPanel.SetActive(true);
        }
        #endregion

        #region Initialization
        protected virtual void SubToEvents()
        {
            //Toggles
            uiMaster.EventMenuToggle += TogglePauseMenuUi;
            gamemaster.OnPlayerWon += ActivateWinnerUi;
            gamemaster.OnPlayerWasKilled += ActivateGameOverUi;
        }

        protected virtual void UnsubEvents()
        {
            //Toggles
            uiMaster.EventMenuToggle -= TogglePauseMenuUi;
            gamemaster.OnPlayerWon -= ActivateWinnerUi;
            gamemaster.OnPlayerWasKilled -= ActivateGameOverUi;
        }
        #endregion
    }
}
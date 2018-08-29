using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class BrickPlacer : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        protected GameObject BrickPrefab = null;
        //protected float BrickPlacementRate = 1f;
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

        #region UnityMessages
        private void OnEnable()
        {
            gamemaster.OnBrickWasPlaced += PlaceBrick;
        }

        private void OnDisable()
        {
            gamemaster.OnBrickWasPlaced -= PlaceBrick;
        }

        private void Start()
        {
            PlaceBrick();
        }
        #endregion

        #region Handlers
        void PlaceBrick()
        {
            if (BrickPrefab == null)
            {
                Debug.LogError("No Brick Prefab On Placer");
                return;
            }

            Debug.Log("Placing Brick");
        }
        #endregion

        #region Initialization

        #endregion
    }
}
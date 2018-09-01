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
        [SerializeField]
        protected GameObject HeavyBrickPrefab = null;
        [SerializeField]
        protected Transform BrickHolder = null;
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

        #region EasyProps
        Transform PlayerTransform
        {
            get { return gamemanager.playerTransform; }
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
            if (CanPlaceBrick() == false) return;

            var _brickPlacement = FindClosestBrickContainer();
            GameObject.Instantiate(BrickPrefab, _brickPlacement.position, _brickPlacement.rotation, BrickHolder);
        }


        #endregion

        #region Getters / Finders
        Transform FindClosestBrickContainer()
        {
            Transform _closestBrick = null;
            float _closestDistance = float.MaxValue;
            foreach (Transform _child in this.transform)
            {
                float _checkDistance = Vector3.Distance(_child.position, PlayerTransform.position);
                if (_checkDistance < _closestDistance)
                {
                    _closestBrick = _child;
                    _closestDistance = _checkDistance;
                }
            }
            return _closestBrick;
        }

        bool CanPlaceBrick()
        {
            if (BrickPrefab == null)
            {
                Debug.LogError("No Brick Prefab On Placer");
                return false;
            }
            if (BrickHolder == null)
            {
                Debug.LogError("No BrickHolder On Placer");
                return false;
            }
            return true;
        }
        #endregion

        #region Initialization

        #endregion
    }
}
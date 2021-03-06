﻿using System.Collections;
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
        protected float heavyBrickDropChance = 0.5f;
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

        GameInstance gameinstance
        {
            get { return GameInstance.thisInstance; }
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
            gamemaster.OnPressedDebugKey += PlaceHeavyOnDebug;
            var _settings = gameinstance.GetLazarusDifficultySettings();
            heavyBrickDropChance = _settings.heavyBrickDropChancePercentage;
        }

        private void OnDisable()
        {
            gamemaster.OnBrickWasPlaced -= PlaceBrick;
            gamemaster.OnPressedDebugKey -= PlaceHeavyOnDebug;
        }

        private void Start()
        {
            PlaceBrick();
        }
        #endregion

        #region Handlers
        void PlaceHeavyOnDebug()
        {
            var _brickPlacement = FindClosestBrickContainer();
            var _gObject = GameObject.Instantiate(HeavyBrickPrefab, _brickPlacement.position, _brickPlacement.rotation, BrickHolder);
            _gObject.GetComponent<Brick>().SetCallPlaceBrickOnLanding(false);
        }

        void PlaceBrick()
        {
            if (CanPlaceBrick() == false) return;

            var _brickPlacement = FindClosestBrickContainer();
            var _brickPrefab = Random.value <= heavyBrickDropChance ?
                HeavyBrickPrefab : BrickPrefab;
            GameObject.Instantiate(_brickPrefab, _brickPlacement.position, _brickPlacement.rotation, BrickHolder);
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
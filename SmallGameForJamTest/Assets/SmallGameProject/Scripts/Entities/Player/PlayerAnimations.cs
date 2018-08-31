using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class PlayerAnimations : MonoBehaviour
    {
        #region Fields
        public AnimationClip LowerLeftClip;
        public AnimationClip LeftClip;
        public AnimationClip UpperLeftClip;
        public AnimationClip LowerRightClip;
        public AnimationClip RightClip;
        public AnimationClip UpperRightClip;

        public string LowerLeftTrigger;
        public string LeftTrigger;
        public string UpperLeftTrigger;
        public string LowerRightTrigger;
        public string RightTrigger;
        public string UpperRightTrigger;
        #endregion

        #region Properties
        bool bHasAnimClips
        {
            get
            {
                return LowerLeftClip && LeftClip && UpperLeftClip &&
                    LowerRightClip && RightClip && UpperRightClip;
            }
        }
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

        PlayerEventHandler myEventHandler
        {
            get
            {
                if (_myEventHandler == null)
                    _myEventHandler = GetComponent<PlayerEventHandler>();

                return _myEventHandler;
            }
        }
        PlayerEventHandler _myEventHandler = null;

        Player myPlayer
        {
            get
            {
                if (_myPlayer == null)
                    _myPlayer = GetComponent<Player>();

                return _myPlayer;
            }
        }
        Player _myPlayer = null;

        Animator myAnimator
        {
            get
            {
                if (_myAnimator == null)
                    _myAnimator = GetComponentInChildren<Animator>();

                return _myAnimator;
            }
        }
        Animator _myAnimator = null;
        #endregion

        #region UnityMessages
        private void OnEnable()
        {
            SubToEvents();
        }

        private void OnDisable()
        {
            UnsubFromEvents();
        }
        #endregion

        #region Handlers
        void StartPlayingAnimation(EPlayerMovementPosition _movePos)
        {
            if(bHasAnimClips == false)
            {
                Debug.LogError("No Animation Clips On Player Animations");
                return;
            }

            StartCoroutine(PlayAnimationCoroutine(_movePos));        
        }
        #endregion

        #region Animations
        IEnumerator PlayAnimationCoroutine(EPlayerMovementPosition _movePos)
        {
            var _clip = GetAnimClipFromMovePos(_movePos);
            var _trigger = GetAnimTriggerFromMovePos(_movePos);
            if (_clip != null)
            {
                yield return new WaitForSeconds(_clip.length);
                myAnimator.SetTrigger(_trigger);
            }
            myEventHandler.CallOnPlayerMoveEnd(_movePos);
        }
        #endregion

        #region Getters
        AnimationClip GetAnimClipFromMovePos(EPlayerMovementPosition _movePos)
        {
            switch (_movePos)
            {
                case EPlayerMovementPosition.LowerLeft:
                    return LowerLeftClip;
                case EPlayerMovementPosition.Left:
                    return LeftClip;
                case EPlayerMovementPosition.UpperLeft:
                    return UpperLeftClip;
                case EPlayerMovementPosition.LowerRight:
                    return LowerRightClip;
                case EPlayerMovementPosition.Right:
                    return RightClip;
                case EPlayerMovementPosition.UpperRight:
                    return UpperRightClip;
                default:
                    return null;
            }
        }

        string GetAnimTriggerFromMovePos(EPlayerMovementPosition _movePos)
        {
            switch (_movePos)
            {
                case EPlayerMovementPosition.LowerLeft:
                    return LowerLeftTrigger;
                case EPlayerMovementPosition.Left:
                    return LeftTrigger;
                case EPlayerMovementPosition.UpperLeft:
                    return UpperLeftTrigger;
                case EPlayerMovementPosition.LowerRight:
                    return LowerRightTrigger;
                case EPlayerMovementPosition.Right:
                    return RightTrigger;
                case EPlayerMovementPosition.UpperRight:
                    return UpperRightTrigger;
                default:
                    return null;
            }
        }
        #endregion

        #region Initialization
        void SubToEvents()
        {
            myEventHandler.OnPlayerMoveStart += StartPlayingAnimation;
        }

        void UnsubFromEvents()
        {
            myEventHandler.OnPlayerMoveStart -= StartPlayingAnimation;
        }
        #endregion
    }
}
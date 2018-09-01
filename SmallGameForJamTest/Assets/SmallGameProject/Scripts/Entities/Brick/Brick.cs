using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class Brick : MonoBehaviour
    {
        #region Fields   
        protected float downwardSpeed = 1f;
        //[Range(0.05f, 5f)]
        protected float downwardRepeatRate = 0.5f;

        protected int brickHealth = 2;

        protected bool bCallPlaceBrickOnLanding = true;

        /// <summary>
        /// Used Primarily For Debugging
        /// </summary>
        public bool bHasResumedMovementAfterLanding { get; protected set; } = false;

        [SerializeField]
        protected Sprite BrickCrumbledSprite = null;

        //Raycasting
        protected float rayCastForBricksLength = 25f;
        #endregion

        #region ComponentProperties
        protected GameMaster gamemaster
        {
            get { return GameMaster.thisInstance; }
        }

        protected GameManager gamemanager
        {
            get { return GameManager.thisInstance; }
        }

        protected GameInstance gameinstance
        {
            get { return GameInstance.thisInstance; }
        }

        protected SpriteRenderer spriteRenderer
        {
            get
            {
                if (_spriteRenderer == null)
                    _spriteRenderer = GetComponent<SpriteRenderer>();

                return _spriteRenderer;
            }
        }
        private SpriteRenderer _spriteRenderer = null;
        #endregion

        #region EasyProps
        protected BoxCollider2D myCollider
        {
            get
            {
                if (_myCollider == null)
                    _myCollider = GetComponent<BoxCollider2D>();

                return _myCollider;
            }
        }
        BoxCollider2D _myCollider = null;
        #endregion

        #region UnityMessages
        protected virtual void OnEnable()
        {
            gamemaster.OnBricksWereDestroyed += OnBricksWereDestroyed;
        }

        protected virtual void OnDisable()
        {
            gamemaster.OnBricksWereDestroyed -= OnBricksWereDestroyed;
        }

        protected virtual void Start()
        {
            var _settings = gameinstance.GetLazarusDifficultySettings();
            downwardRepeatRate = _settings.brickDownwardRepeatRate;
            InvokeRepeating("SE_UpdateBrickPosition", 0.5f, downwardRepeatRate);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == gamemanager.PlayerTag)
            {
                gamemaster.CallOnPlayerWasKilled();
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            //if (collision.tag == gamemanager.AreaBoundsTag)
            //{
            //    gamemaster.CallOnBrickWasPlaced();
            //}
        }
        #endregion

        #region Services
        protected virtual void SE_UpdateBrickPosition()
        {
            this.transform.position = this.transform.position +
                new Vector3(0.0f, -downwardSpeed, 0.0f);
        }
        #endregion

        #region Setters
        /// <summary>
        /// Prevents Multiple Bricks From Being Placed At Same Time
        /// </summary>
        /// <param name="_enable"></param>
        public void SetCallPlaceBrickOnLanding(bool _enable)
        {
            bCallPlaceBrickOnLanding = _enable;
        }
        #endregion

        #region Getters
        public virtual int GetDamageRate()
        {
            return 0;
        }

        public virtual float GetDamageRange()
        {
            return 0;
        }
        #endregion

        #region Handlers
        void OnBricksWereDestroyed(List<Vector3> _locations)
        {
            bool _destroyedWasBelow = false;
            foreach (var _brickPos in _locations)
            {
                //Destroyed Brick Is Somewhere 
                //Underneath This Brick
                if(Mathf.Abs(transform.position.x - _brickPos.x) <= 0.25f)
                {
                    _destroyedWasBelow = true;
                    break;
                }
            }
            if (_destroyedWasBelow && CheckBrickBelowByRaycasting())
            {
                ResumeBrickMovement();
            }
        }
        #endregion

        #region PublicCollisionMethods
        public virtual bool TakeDamageDestroysBrick(int _amount)
        {
            brickHealth = Mathf.Max(brickHealth - _amount, 0);
            if(brickHealth <= 0)
            {
                DestroyBrick();
                return true;
            }
            else
            {
                SetBrickSpriteToDamaged();
                return false;
            }
        }

        public virtual void EscapedHittingPlayer()
        {
            
        }

        public virtual void CloseToHittingPlayer()
        {
            //CancelInvoke();
        }

        public virtual void ResumeBrickMovement()
        {
            bHasResumedMovementAfterLanding = true;
            if (this.tag == gamemanager.BrickTag)
            {
                this.tag = gamemanager.BrickFallingTag;
            }

            if (IsInvoking("SE_UpdateBrickPosition") == false)
            {
                InvokeRepeating("SE_UpdateBrickPosition", 0.1f, downwardRepeatRate);
            }

            SetCallPlaceBrickOnLanding(false);
        }

        public virtual void PauseBrickMovement()
        {
            if (this.tag == gamemanager.BrickFallingTag)
            {
                this.tag = gamemanager.BrickTag;
            }

            if (IsInvoking("SE_UpdateBrickPosition"))
            {
                CancelInvoke("SE_UpdateBrickPosition");
            }
        }

        public virtual bool BrickDestroyedOnHit(Brick _brick)
        {
            CancelInvoke();
            if (bCallPlaceBrickOnLanding)
            {
                gamemaster.CallOnBrickWasPlaced();
            }
            this.tag = gamemanager.BrickTag;
            return false;
        }

        public virtual void HitAreaBounds()
        {
            CancelInvoke();
            if (bCallPlaceBrickOnLanding)
            {
                gamemaster.CallOnBrickWasPlaced();
            }
            this.tag = gamemanager.BrickTag;
        }
        #endregion

        #region OtherBrickMethods
        protected virtual void DestroyBrick()
        {
            Destroy(this.gameObject, 0.1f);
        }

        protected virtual void SetBrickSpriteToDamaged()
        {
            spriteRenderer.sprite = BrickCrumbledSprite;
        }
        #endregion

        #region Helpers
        protected virtual bool CheckBrickBelowByRaycasting()
        {
            var _belowHit = Physics2D.Raycast(transform.position, Vector3.down,
                downwardSpeed, gamemanager.CheckForCollisionLayersIgnorePlayerAndBounds);
            if(_belowHit.transform != null &&
                _belowHit.transform.tag == gamemanager.BrickTag)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected virtual List<Brick> GetAllBricksBelowTransform(bool _debug = false)
        {
            List<Brick> _bricksFound = new List<Brick>();
            if (_debug)
            {
                Debug.DrawLine(transform.position, Vector3.down, Color.green, rayCastForBricksLength, false);
            }

            RaycastHit2D[] _myHits = Physics2D.RaycastAll(
                //StartPos, Direction, Distance
                transform.position, Vector3.down, rayCastForBricksLength,
                gamemanager.CheckForCollisionLayersIgnorePlayerAndBounds);
            foreach (var _myHit in _myHits)
            {
                if(_myHit.transform != null &&
                    _myHit.transform.tag == gamemanager.BrickTag)
                {
                    _bricksFound.Add(_myHit.transform.GetComponent<Brick>());
                }
            }

            return _bricksFound;
        }
        #endregion

        #region CommentedCode
        //private BoxCollider2D childBoxCollider
        //{
        //    get
        //    {
        //        if (_childBoxCollider == null)
        //        {
        //            _childBoxCollider = transform.GetComponentInChildren<BoxCollider2D>();
        //        }
        //        return _childBoxCollider;
        //    }
        //}
        //private BoxCollider2D _childBoxCollider = null;
        //bool CanUpdateBrickPos()
        //{
        //    if(childBoxCollider == null)
        //    {
        //        Debug.LogError("childBoxCollider doesn't exist on brick");
        //        return false;
        //    }
        //    return true;
        //}

        //void ProtoRaycast()
        //{
        //    Collision Checking
        //    Ray myRay;
        //    RaycastHit myHit;

        //    myRay.origin = transform.position + new Vector3(0, -(myCollider.size.y / 2), 0);
        //    myRay.direction = Vector3.down;
        //    //Debug.DrawRay(transform.position + new Vector3(0, -(myCollider.size.y / 2), 0), Vector3.down, Color.green, 500);
        //    if (Physics.Raycast(myRay, out myHit, downwardSpeed, gamemanager.CheckForCollisionLayers))
        //    {
        //        Debug.Log("I hit: " + myHit.transform.name);
        //    }
        //}
        #endregion
    }
}

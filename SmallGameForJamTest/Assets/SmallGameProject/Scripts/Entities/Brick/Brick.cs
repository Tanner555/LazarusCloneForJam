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

        [SerializeField]
        protected Sprite BrickCrumbledSprite = null;
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

        #region Getters
        public virtual int GetDamageRate()
        {
            return 0;
        }
        #endregion

        #region PublicCollisionMethods
        public virtual bool TakeDamageDestroysBrick(int _amount)
        {
            brickHealth = Mathf.Min(brickHealth - _amount, 0);
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

        public virtual bool BrickDestroyedOnHit(Brick _brick)
        {
            CancelInvoke();
            gamemaster.CallOnBrickWasPlaced();
            this.tag = gamemanager.BrickTag;
            return false;
        }

        public virtual void HitAreaBounds()
        {
            CancelInvoke();
            gamemaster.CallOnBrickWasPlaced();
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

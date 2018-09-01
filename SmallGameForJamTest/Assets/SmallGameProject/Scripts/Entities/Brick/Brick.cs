using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class Brick : MonoBehaviour
    {
        #region Fields   
        private float downwardSpeed = 1f;
        [Range(0.05f, 5f)]
        public float downwardRepeatRate = 0.5f;
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
        BoxCollider2D myCollider
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
        private void Start()
        {
            InvokeRepeating("SE_UpdateBrickPosition", downwardRepeatRate, downwardRepeatRate);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == gamemanager.PlayerTag)
            {
                gamemaster.CallOnPlayerWasKilled();
            }
        }

        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if(collision.tag == gamemanager.AreaBoundsTag)
        //    {
        //        gamemaster.CallOnBrickWasPlaced();
        //    }
        //}
        #endregion

        #region Services
        void SE_UpdateBrickPosition()
        {
            this.transform.position = this.transform.position +
                new Vector3(0.0f, -downwardSpeed, 0.0f);
        }
        #endregion

        #region PublicCollisionMethods
        public void EscapedHittingPlayer()
        {
            
        }

        public void CloseToHittingPlayer()
        {
            //CancelInvoke();
        }

        public void HitBrick()
        {
            CancelInvoke();
            gamemaster.CallOnBrickWasPlaced();
            this.tag = gamemanager.BrickTag;
        }

        public void HitAreaBounds()
        {
            CancelInvoke();
            gamemaster.CallOnBrickWasPlaced();
            this.tag = gamemanager.BrickTag;
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

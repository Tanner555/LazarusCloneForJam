using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class Brick : MonoBehaviour
    {
        #region Fields
        [Range(0.0f, 5f)]
        public float downwardSpeed = 1f;
        float downwardRepeatRate = 0.5f;

        private BoxCollider2D childBoxCollider
        {
            get
            {
                if(_childBoxCollider == null)
                {
                    _childBoxCollider = transform.GetComponentInChildren<BoxCollider2D>();
                }
                return _childBoxCollider;
            }
        }
        private BoxCollider2D _childBoxCollider = null;
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

        //private void OnTriggerEnter2D(Collider2D collision)
        //{

        //}

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
        public void HitPlayer()
        {
            Debug.Log("Hit Player");
            CancelInvoke();
        }

        public void HitBrick()
        {
            Debug.Log("Hit Brick");
            CancelInvoke();
        }

        public void HitAreaBounds()
        {
            Debug.Log("Hit Area Bounds");
            CancelInvoke();
        }
        #endregion

        #region CommentedCode
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class BrickButtomCollisionChecker : MonoBehaviour
    {
        #region Fields
        bool bHasDelayStart = false;
        #endregion

        #region Properties
        private Brick myBrick
        {
            get
            {
                if (_myBrick == null)
                    _myBrick = transform.GetComponentInParent<Brick>();

                return _myBrick;
            }
        }
        private Brick _myBrick = null;
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
        private void Start()
        {
            Invoke("DelayStart", 0.5f);
        }

        void DelayStart()
        {
            bHasDelayStart = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (bHasDelayStart == false) return;
            if (collision.tag == gamemanager.PlayerTag)
            {
                myBrick.CloseToHittingPlayer();
            }
            else if(collision.tag == gamemanager.BrickTag &&
                collision.transform != transform)
            {
                var _brickComp = collision.transform.GetComponent<Brick>();
                //Hitting Other Brick Doesn't Destroy It
                if (_brickComp != null && 
                    myBrick.BrickDestroyedOnHit(_brickComp) == false)
                {
                    myBrick.PauseBrickMovement();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (bHasDelayStart == false) return;
            if (collision.tag == gamemanager.AreaBoundsTag)
            {
                myBrick.HitAreaBounds();
                myBrick.PauseBrickMovement();
            }
            else if(collision.tag == gamemanager.BrickTag)
            {
                myBrick.ResumeBrickMovement();
            }
            else if(collision.tag == gamemanager.PlayerTag)
            {
                myBrick.EscapedHittingPlayer();
            }
        }
        #endregion
    }
}
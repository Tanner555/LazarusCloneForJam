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
                myBrick.HitPlayer();
                Destroy(this, 0.1f);
            }
            else if(collision.tag == gamemanager.BrickTag &&
                collision.transform != transform)
            {
                myBrick.HitBrick();
                Destroy(this, 0.1f);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (bHasDelayStart == false) return;
            if (collision.tag == gamemanager.AreaBoundsTag)
            {
                myBrick.HitAreaBounds();
                Destroy(this, 0.1f);
            }
        }
        #endregion
    }
}
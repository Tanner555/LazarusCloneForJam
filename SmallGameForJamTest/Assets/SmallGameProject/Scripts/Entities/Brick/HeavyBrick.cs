using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class HeavyBrick : Brick
    {
        public override bool BrickDestroyedOnHit(Brick _brick)
        {
            bool _destroyedBrick = false;
            foreach (var _brickBelow in GetAllBricksBelowTransform())
            {
                if(_brickBelow != null &&
                    (_brickBelow is HeavyBrick) == false &&
                    Vector3.Distance(transform.position, _brickBelow.transform.position) <= GetDamageRange())
                {
                    //TODO:Lazarus Make It So Bricks Fall Again
                    //When Brick Below Them Gets Destroyed
                    if (_brickBelow.TakeDamageDestroysBrick(GetDamageRate()) &&
                        _brick == _brickBelow)
                    {
                        //Damage Destroys Brick
                        _destroyedBrick = true;
                    }
                }
            }

            if (_destroyedBrick == false)
            {
                //Brick Still Remains
                //Use Base Method Functionality
                CancelInvoke();
                gamemaster.CallOnBrickWasPlaced();
                this.tag = gamemanager.BrickTag;
            }

            return _destroyedBrick;
        }

        public override int GetDamageRate()
        {
            return 1;
        }

        public override float GetDamageRange()
        {
            return 3f;
        }
    }
}
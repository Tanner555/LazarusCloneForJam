using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class HeavyBrick : Brick
    {
        public override bool BrickDestroyedOnHit(Brick _brick)
        {
            if(_brick != null &&
                (_brick is HeavyBrick) == false)
            {
                if (_brick.TakeDamageDestroysBrick(GetDamageRate()))
                {
                    //Damage Destroys Brick
                    return true;
                }
                else
                {
                    //Brick Still Remains
                    //Use Base Method Functionality
                    CancelInvoke();
                    gamemaster.CallOnBrickWasPlaced();
                    this.tag = gamemanager.BrickTag;
                }
            }
            return false;
        }

        public override int GetDamageRate()
        {
            return 1;
        }
    }
}
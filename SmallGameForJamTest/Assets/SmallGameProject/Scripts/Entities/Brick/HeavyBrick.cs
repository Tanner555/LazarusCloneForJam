using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazarusClone
{
    public class HeavyBrick : Brick
    {
        public override bool BrickDestroyedOnHit(Brick _brick)
        {
            bool _destroyedBrickHitBelow = false;
            bool _destroyedAnyBrickBelow = false;
            List<Vector3> _locations = new List<Vector3>();
            foreach (var _brickBelow in GetAllBricksBelowTransform())
            {
                if(_brickBelow != null &&
                    (_brickBelow is HeavyBrick) == false &&
                    Vector3.Distance(transform.position, _brickBelow.transform.position) <= GetDamageRange())
                {
                    //TODO:Lazarus Make It So Bricks Fall Again
                    //When Brick Below Them Gets Destroyed
                    Vector3 _brickPos = _brickBelow.transform.position;
                    if (_brickBelow.TakeDamageDestroysBrick(GetDamageRate()))
                    {
                        _destroyedAnyBrickBelow = true;
                        if (_brick == _brickBelow)
                        {
                            //Damage Destroys Brick Immediately Below
                            _destroyedBrickHitBelow = true;
                        }
                        _locations.Add(_brickPos);
                    }
                }
            }

            //Destroyed Brick Immediately Below This One
            if (_destroyedBrickHitBelow == false)
            {
                //Brick Still Remains
                //Use Base Method Functionality
                CancelInvoke();
                gamemaster.CallOnBrickWasPlaced();
                this.tag = gamemanager.BrickTag;
            }

            //Destroyed Any Brick Below
            if (_destroyedAnyBrickBelow)
            {
                gamemaster.CallOnBricksWereDestroyed(_locations);
            }

            return _destroyedBrickHitBelow;
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
using System;
using System.Collections;
using BehaviourTree.Core;
using BehaviourTree.UnityCore;

namespace Entities.Enemy
{
    public class TragedyController : BaseController
    {
        protected override void Awake()
        {
            base.Awake();
        }

        void FixedUpdate()
        {
            base.FixedUpdate();
            IsSeen = _playerController.ActiveMask == PlayerController.Masks.BLUE && _fov != null && _fov.visibleTargets.Count > 0 && _fov.visibleTargets.Find((target) => target == transform) && !IsSeen;

        }
    }
}
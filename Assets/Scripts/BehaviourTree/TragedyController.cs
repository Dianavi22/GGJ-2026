using System;
using System.Collections;
using BehaviourTree.Core;
using BehaviourTree.UnityCore;

namespace Entities.Enemy
{
  public class TragedyController : BaseController
  {
        protected override IEnumerator AttackCoroutine(Action onComplete)
            {
               yield return null ;
            }
  }
}

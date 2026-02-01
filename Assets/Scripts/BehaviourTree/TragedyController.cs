using System;
using System.Collections;
using BehaviourTree.Core;
using BehaviourTree.Leaves;
using BehaviourTree.UnityCore;
using UnityEngine;

namespace Entities.Enemy
{
  public class TragedyController : BaseController
  {
    protected override IEnumerator AttackCoroutine(Action onComplete)
    {
      // TODO
      _canMove = true;
      yield return new WaitForSeconds(4f); 
      onComplete?.Invoke();
      print("GAME OVER");
      _canMove = false;
    }
  }
}

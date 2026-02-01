using System;
using System.Collections;
using BehaviourTree.Core;
using BehaviourTree.Leaves;
using BehaviourTree.UnityCore;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
      SceneManager.LoadScene("GameOverSceneBlue");
      _canMove = false;
    }
  }
}

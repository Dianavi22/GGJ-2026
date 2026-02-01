using System;
using System.Collections;
using BehaviourTree.Core;
using BehaviourTree.UnityCore;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Entities.Enemy
{
  public class ComedyController : BaseController
  {
        protected override IEnumerator AttackCoroutine(Action onComplete)
        {
            _canMove = true;
            yield return new WaitForSeconds(4f);
            onComplete?.Invoke();
            SceneManager.LoadScene("GameOverSceneRed");
            _canMove = false;
        }
    }
}

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
            yield return null;
            onComplete?.Invoke();
        }


        private void t() {
            SceneManager.LoadScene("GameOverSceneRed");
        }
    }
}

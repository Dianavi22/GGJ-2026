
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Comedy : CommonBehavior
{
    private const float DANCE_ITERATION = 3;
    private const float DANCE_DELAY = 0.1f;
    private const float DELAY_BEWEEN_DANCES = 4f;

    private static WaitForSeconds _waitForMilliSeconds1 = new(DANCE_DELAY);
    private static WaitForSeconds _waitBetweenDanceTime = new(DELAY_BEWEEN_DANCES + (DANCE_DELAY * DANCE_ITERATION));

    private int _indexDance = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // SetDestination(_behaviorPoints, _behaviorIndex);
        StartCoroutine(StartDanceBehavior());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.Update();
        // if (IsDestinationComplete())
        // {

        //     SetDestination(_behaviorPoints, _behaviorIndex);
        // }
    }

    IEnumerator StartDanceBehavior()
    {
        StartCoroutine(Danse());
        yield return _waitBetweenDanceTime;
    }

    IEnumerator Danse()
    {
        _behaviorIndex += 1;
        if (_behaviorIndex > _behaviorPoints.Length)
        {
            _behaviorIndex = 0;
        }
        yield return _waitForMilliSeconds1;
    }
}

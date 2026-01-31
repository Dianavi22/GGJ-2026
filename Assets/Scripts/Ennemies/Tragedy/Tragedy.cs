
using UnityEngine;
using UnityEngine.AI;

public class Tragedy : CommonBehavior
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // SetDestination(_behaviorPoints, _behaviorIndex);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // if (IsDestinationComplete())
        // {
        //     _behaviorIndex += 1;
        //     if (_behaviorIndex >= _behaviorPoints.Length)
        //     {
        //         _behaviorIndex = 0;
        //     }
        //     SetDestination(_behaviorPoints, _behaviorIndex);
        // }
    }
}

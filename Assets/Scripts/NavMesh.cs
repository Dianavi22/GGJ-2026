using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void BakeAtRuntime()
    {

    }
}

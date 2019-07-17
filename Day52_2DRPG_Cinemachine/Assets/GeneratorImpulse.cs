using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GeneratorImpulse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
    }
}

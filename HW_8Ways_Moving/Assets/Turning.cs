using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour
{
    Moving _Moving;

    // Start is called before the first frame update
    void Start()
    {
        _Moving = transform.parent.GetComponent<Moving>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnPlayer();

    }

    public void TurnPlayer()
    {
        if (_Moving.moveDir_X == 0 && _Moving.moveDir_Z == 0)
            return;
        Quaternion newRotation = Quaternion.LookRotation(_Moving.totalMove);

        transform.rotation = newRotation;

    }
}

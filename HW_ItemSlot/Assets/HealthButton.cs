using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthButton : MonoBehaviour
{
    Slot slot;

    // Start is called before the first frame update
    void Start()
    {
        slot = transform.parent.GetComponent<Slot>();
        GetComponent<Button>().onClick.AddListener(() => slot.DrinkPotion());
    }

}

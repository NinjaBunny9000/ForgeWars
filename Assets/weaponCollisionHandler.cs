using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCollisionHandler : MonoBehaviour
{

    public GameObject weaponForger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // todo only respond to hammer colllisions
        weaponForger.gameObject.SendMessage("registerHammerHit");
    }


    void OnCollisionExit(Collision collision)
    {
        weaponForger.gameObject.SendMessage("endHammerHit");
    }
}

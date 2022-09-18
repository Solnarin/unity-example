using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class breakableSoulController : MonoBehaviour
{


    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "platform")
        {
            Destroy(gameObject);
        }

    }

}

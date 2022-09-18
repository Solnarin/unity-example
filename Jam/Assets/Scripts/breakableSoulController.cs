using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableSoulController : MonoBehaviour
{

    public LayerMask breakableWallLayer;


    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

    }

}

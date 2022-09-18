using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float followSpeed;
    public Transform target;
    public float xDistance;
    public float yDistance;
    public new Camera camera;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }


    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            camera.orthographicSize = camera.orthographicSize - 100 * Time.deltaTime;
            if (camera.orthographicSize <= 5)
            {
                camera.orthographicSize = 5; // Min size 
            }

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {

            camera.orthographicSize = camera.orthographicSize + 100 * Time.deltaTime;



            if (camera.orthographicSize >= 8f)
            {
                camera.orthographicSize = 8f; // Max size
            }
        }



        Vector3 newPos = new Vector3(target.position.x + xDistance, target.position.y + yDistance, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);

    }

}

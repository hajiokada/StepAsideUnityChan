using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{

    GameObject mainCamera;

    void Start()
    {
        mainCamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.z < this.mainCamera.transform.position.z || this.transform.position.z >= 360)
        {
            Destroy(gameObject);
        }
    }
}

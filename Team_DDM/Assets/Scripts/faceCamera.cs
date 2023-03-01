using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCamera : MonoBehaviour
{

    public Transform mLookat;
    Transform localT;

    // Start is called before the first frame update
    void Start()
    {
        localT = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mLookat)
        {
            localT.LookAt(2 * localT.position - mLookat.position);
            localT.rotation = new Quaternion(0, localT.rotation.y, 0, 1);
        }
    }
}

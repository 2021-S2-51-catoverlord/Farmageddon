using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField]
    protected Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Make the camera follow the target.
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}

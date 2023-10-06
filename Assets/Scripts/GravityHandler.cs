using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHandler : MonoBehaviour
{
    public float gravityScale = 9.81f;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -gravityScale, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

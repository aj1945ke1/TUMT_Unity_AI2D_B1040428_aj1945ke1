using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gogo : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.PingPong(Time.time *2 , 6.3f)-7.9f, transform.position.y);
    }
}

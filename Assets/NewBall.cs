using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBall : MonoBehaviour
{
    public Object ball;
    // Start is called before the first frame update
    void Start()
    {
    
       // ClientScene.RegisterPrefab(ball);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {

            Object clone;
            Vector3 location = new Vector3(0, 5, 0);
            clone = Instantiate(ball, location, transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CheckWalls", 5);
    }

    public void CheckWalls()
    {
        if (Physics.Raycast(transform.position, transform.right * -1, 6))
            Destroy(transform.GetChild(3).gameObject);
        if (Physics.Raycast(transform.position, transform.right, 6))
            Destroy(transform.GetChild(2).gameObject);
        if (Physics.Raycast(transform.position, transform.forward * -1, 6))
            Destroy(transform.GetChild(1).gameObject);
        if (Physics.Raycast(transform.position, transform.forward, 6))
            Destroy(transform.GetChild(0).gameObject);
    }
}

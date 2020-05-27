using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject chunk;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(chunk, this.transform.position + new Vector3(16 * x, 0, 16 * j), this.transform.rotation);
            }
        }
    }


}

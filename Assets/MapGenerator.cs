using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject tile;
    public int mapSize;
    public int randomAmount;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = -mapSize; x <= mapSize; ++x)
        {
            for (int y = -mapSize; y <= mapSize; ++y)
            {
                if (x == -mapSize || x == mapSize ||
                    y == -mapSize || y == mapSize)
                {
                    Instantiate(tile, new Vector3(x, y, 0), new Quaternion());
                }
                else if (randomAmount > 0)
                {
                    switch (Random.Range(0, randomAmount * 2))
                    {
                        case 0:
                            --randomAmount;
                            Instantiate(tile, new Vector3(x, y, 0), new Quaternion());
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}

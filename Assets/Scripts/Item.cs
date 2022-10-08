using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float timer;
    public bool isUsingLifeCycle;
    [SerializeField] private float lifeCycle;

    private void Start()
    {
        timer = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isUsingLifeCycle)
        {
            if (timer >= lifeCycle)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().bulletNumber += 1;
            Destroy(gameObject);
        }
    }
}

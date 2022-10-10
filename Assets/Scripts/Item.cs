using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float timer;
    public bool isUsingLifeCycle;
    [SerializeField] private float lifeCycle;
    ExpManager expManager;

    private void Start()
    {
        timer = 0.0f;
        expManager = FindObjectOfType<ExpManager>().gameObject.GetComponent<ExpManager>();
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
            expManager.updatedExp += 1;
            Destroy(gameObject);
        }
    }
}

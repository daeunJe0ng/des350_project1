using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer;
    [SerializeField] private float lifeCycle;

    private void Start()
    {
        timer = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeCycle)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().healthPoint -= 1;
        }
        else if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}

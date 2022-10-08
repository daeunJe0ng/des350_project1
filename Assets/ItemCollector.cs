using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private Transform target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactive")
        {
            collision.gameObject.transform.position = Vector2.MoveTowards(collision.gameObject.transform.position, target.position, speed * Time.deltaTime);
        }
    }
}

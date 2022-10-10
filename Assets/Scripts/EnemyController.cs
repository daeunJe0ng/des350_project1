using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int healthPoint;
    public float speed;
    public int damage = 1;
    public bool isUsingLifeCycle = false;
    public float lifeCycle = 1.0f;
    private float timer = 0.0f;
    [SerializeField] private GameObject itemPrefab;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(isUsingLifeCycle)
        {
            if(timer > lifeCycle)
            {
                Destroy(gameObject);
            }
        }

        if (transform.rotation != Quaternion.Euler(0.0f, 0.0f, 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        if (healthPoint <= 0)
        {
            Instantiate(itemPrefab, gameObject.transform.localPosition, gameObject.transform.localRotation);
            Destroy(gameObject);
        }

        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().healthPoint -= damage;
        }
    }
}

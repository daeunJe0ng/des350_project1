using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBehavior : MonoBehaviour
{
    public int waveCount;
    public int basicEnemyCount;
    public int bigEnemyCount;
    public int fastEnemyCount;

    public GameObject basicEnemy;
    public GameObject bigEnemy;
    public GameObject fastEnemy;

    private float timer = 0;
    public float coolDown;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float width = transform.position.x;
        float height = transform.position.y;

        timer += Time.deltaTime;

        if (timer > coolDown)
        {
            for (int i = 0; i < waveCount / 2; i++)
            {
                Instantiate(basicEnemy, new Vector3(transform.position.x + Random.Range(-width, width), 3, transform.position.z + height + Random.Range(10, 30)), Quaternion.identity);
            }

            for (int i = 0; i < waveCount / 4; i++)
            {
                Instantiate(bigEnemy, new Vector3(transform.position.x + Random.Range(-width, width), 3, transform.position.z + height + Random.Range(10, 30)), Quaternion.identity);
            }

            for (int i = 0; i < waveCount / 8; i++)
            {
                Instantiate(fastEnemy, new Vector3(transform.position.x + Random.Range(-width, width), 3, transform.position.z + height + Random.Range(10, 30)), Quaternion.identity);
            }

            waveCount++;
            timer = 0.0f;
        }
    }
}

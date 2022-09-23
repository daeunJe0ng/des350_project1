using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public LevelManager levelManager;

    public int waveCount;
    public int basicEnemyCount;
    public int bigEnemyCount;
    public int fastEnemyCount;
    public int bossEnemyCount;

    public GameObject basicEnemy;
    public GameObject bigEnemy;
    public GameObject fastEnemy;
    public GameObject bossEnemy;
    public Camera camera;

    private float timer = 0;
    public float coolDown;

    // Start is called before the first frame update
    void Start()
    {
        float width = camera.orthographicSize * camera.aspect + 1;
        float height = camera.orthographicSize + 1;

        Instantiate(basicEnemy, new Vector3(camera.transform.position.x + Random.Range(-width, width), 3, camera.transform.position.z + height + Random.Range(10, 30)), Quaternion.identity);
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float width = camera.orthographicSize * camera.aspect + 1;
        float height = camera.orthographicSize + 1;

        timer += Time.deltaTime;

        if (levelManager.timer < 150.0f)
        {
            if (timer > coolDown)
            {
                for (int i = 0; i < waveCount / 2; i++)
                {
                    Instantiate(basicEnemy, new Vector3(camera.transform.position.x + Random.Range(-width, width), 3, camera.transform.position.z + height + Random.Range(10, 30)), Quaternion.identity);
                }

                for (int i = 0; i < waveCount / 10; i++)
                {
                    Instantiate(bigEnemy, new Vector3(camera.transform.position.x + Random.Range(-width, width), 3, camera.transform.position.z + height + Random.Range(10, 30)), Quaternion.identity);
                }

                for (int i = 0; i < waveCount / 20; i++)
                {
                    Instantiate(fastEnemy, new Vector3(camera.transform.position.x + Random.Range(-width, width), 3, camera.transform.position.z + height + Random.Range(10, 30)), Quaternion.identity);
                }

                waveCount++;
                timer = 0.0f;
            }
        }
        else if (bossEnemyCount != 1)
        {
            Instantiate(bossEnemy, new Vector3(camera.transform.position.x + Random.Range(-width, width), 3, camera.transform.position.z + height + Random.Range(10, 30)), Quaternion.identity);
            ++bossEnemyCount;
        }
    }
}
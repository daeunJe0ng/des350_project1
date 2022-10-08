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
    public Camera mainCamera;

    private float timer = 0;
    public float coolDown;
    private float width;
    private float height;
    private float widthOffset;
    private float heightOffset;

    // Start is called before the first frame update
    void Start()
    {
        width = mainCamera.orthographicSize * mainCamera.aspect;
        height = mainCamera.orthographicSize;

        widthOffset = width / 10;
        heightOffset = height / 10;

        InstantiateEnemy(basicEnemy);
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (levelManager.timer < 150.0f)
        {
            if (timer > coolDown)
            {
                for (int i = 0; i < waveCount / 2; i++)
                {
                    InstantiateEnemy(basicEnemy);
                }

                for (int i = 0; i < waveCount / 10; i++)
                {
                    InstantiateEnemy(bigEnemy);
                }

                for (int i = 0; i < waveCount / 20; i++)
                {
                    InstantiateEnemy(fastEnemy);
                }

                waveCount++;
                timer = 0.0f;
            }
        }
        else if (bossEnemyCount != 1)
        {
            InstantiateEnemy(bossEnemy);
            ++bossEnemyCount;
        }
    }

    void InstantiateEnemy(GameObject gameObject)
    {
        float randomX = 0.0f, randomY = 0.0f;

        switch (Random.Range(0, 2))
        {
            case 0:
                randomX = mainCamera.transform.position.x + Random.Range(-width - widthOffset, -width);
                Debug.Log("x0");
                break;
            case 1:
                randomX = mainCamera.transform.position.x + Random.Range(width, width + widthOffset);
                Debug.Log("x1");
                break;
            default:
                break;
        }

        switch (Random.Range(0, 2))
        {
            case 0:
                randomY = mainCamera.transform.position.y + Random.Range(-height - heightOffset, -height);
                break;
            case 1:
                randomY = mainCamera.transform.position.y + Random.Range(height, height + heightOffset);
                break;
            default:
                break;
        }

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0.0f);
        Instantiate(gameObject, spawnPosition, Quaternion.identity);

        Debug.Log("Enemy: " + spawnPosition.ToString());
        Debug.Log("Width: " + width.ToString());
        Debug.Log("Height: " + height.ToString());
    }
}
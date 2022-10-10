using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBehavior : MonoBehaviour
{
    public MapGenerator mapGenerator;

    public int waveCount;
    public int basicEnemyCount;
    public int bigEnemyCount;
    public int fastEnemyCount;

    public GameObject miniBossEnemy;
    public GameObject explosionVFX;

    private float timer = 0;
    private float lifeTimer = 0;
    public float coolDown;
    public float lifeCoolTime;

    Animator animator;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        animator = GetComponent<Animator>();

        mainCamera = FindObjectOfType<Camera>().gameObject.GetComponent<Camera>();
        mapGenerator = FindObjectOfType<MapGenerator>().gameObject.GetComponent<MapGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        float width = 0.0f, height = 0.0f;

        if (mainCamera != null)
        {
            width = mainCamera.orthographicSize * mainCamera.aspect;
            height = mainCamera.orthographicSize;
        }

        timer += Time.deltaTime;
        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifeCoolTime)
        {
            Explosion();
        }

        if (lifeTimer > lifeCoolTime / 2)
        {
            animator.speed = 2.0f;
        }

        if (timer > coolDown)
        {
            for (int i = 0; i < waveCount / 2; i++)
            {
                float randomX = 0.0f, randomY = 0.0f;

                switch (Random.Range(0, 2))
                {
                    case 0:
                        randomX = transform.position.x + Random.Range(-width / 2, -width + 1);
                        break;
                    case 1:
                        randomX = transform.position.x + Random.Range(width - 1, width / 2);
                        break;
                    default:
                        break;
                }

                switch (Random.Range(0, 2))
                {
                    case 0:
                        randomY = transform.position.y + Random.Range(-height / 2, -height + 1);
                        break;
                    case 1:
                        randomY = transform.position.y + Random.Range(height - 1, height / 2);
                        break;
                    default:
                        break;
                }

                if (randomX < 0 && randomX <= -mapGenerator.mapSize)
                {
                    randomX = transform.position.x + Random.Range(width - 1, width / 2);
                }
                else if (randomX > 0 && randomX >= mapGenerator.mapSize)
                {
                    randomX = transform.position.x + Random.Range(-width / 2, -width + 1);
                }

                if (randomY < 0 && randomY <= -mapGenerator.mapSize)
                {
                    randomY = transform.position.y + Random.Range(height - 1, height / 2);
                }
                else if (randomY > 0 && randomY >= mapGenerator.mapSize)
                {
                    randomY = transform.position.y + Random.Range(-height / 2, -height + 1);
                }

                Instantiate(miniBossEnemy, new Vector3(randomX, randomY, 0), Quaternion.identity);
            }

            waveCount++;
            timer = 0.0f;
        }
    }


    void Explosion()
    {

        if (gameObject.name == "BossEnemy(Clone)")
        {
            explosionVFX.transform.localScale = new Vector3(50, 50, 0);
            explosionVFX.GetComponent<EnemyController>().damage = 5;
        }

        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        //StartCoroutine(CameraShake(2.0f, 2.0f));
        Destroy(gameObject);
    }

    //IEnumerator CameraShake(float duration, float magnitude)
    //{
    //    Vector3 originalPosition = mainCamera.transform.position;

    //    float elapsedTime = 0.0f;

    //    while (elapsedTime < duration)
    //    {
    //        float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
    //        float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

    //        transform.localPosition = new Vector3(xOffset, yOffset, originalPosition.z);

    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    mainCamera.transform.localPosition = originalPosition;
    //}
}

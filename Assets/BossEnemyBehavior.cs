using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBehavior : MonoBehaviour
{
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
    GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        animator = GetComponent<Animator>();

        mainCamera = FindObjectOfType<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float width = transform.position.x;
        float height = transform.position.y;

        timer += Time.deltaTime;
        lifeTimer += Time.deltaTime;

        if (lifeTimer > lifeCoolTime)
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
                Instantiate(miniBossEnemy, new Vector3(transform.position.x + Random.Range(-width, width), 3, transform.position.z + height + Random.Range(10, 30)), Quaternion.identity);
            }

            waveCount++;
            timer = 0.0f;
        }
    }

    void Explosion()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
        StartCoroutine("CameraShake");
    }

    IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPosition = mainCamera.transform.position;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(xOffset, yOffset, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.localPosition = originalPosition;
    }
}

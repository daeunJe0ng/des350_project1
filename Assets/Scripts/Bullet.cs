using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer;
    Camera mainCamera;
    float cameraWidth, cameraHeight;

    private void Start()
    {
        timer = 0.0f;
        mainCamera = FindObjectOfType<Camera>();
        cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        cameraHeight = mainCamera.orthographicSize;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if ((transform.position.x > 0 && transform.position.x >= cameraWidth) ||
            (transform.position.x < 0 && transform.position.x <= -cameraWidth) ||
            (transform.position.y > 0 && transform.position.y >= cameraHeight) ||
            (transform.position.y < 0 && transform.position.y <= -cameraHeight))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().healthPoint -= 1;
        }
        
        if (collision.gameObject.tag != "Player")
        {
            if (collision.gameObject.tag == "Interactive")
            {
                return;
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public AudioClip enemyDyingClip;
    public AudioClip playerHurtClip;
    public AudioClip explosionClip;

    private AudioSource audioSource;
    private Renderer renderer;
    private Collider2D collider;
    private bool isDeathTriggered = false;

    public bool isAttacked = false;
    private Color originalColor;

    Renderer[] renderers;
    Color[] colors;

    public GameObject damageText;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        originalColor = renderer.material.color;

        if (collider == null)
        {
            collider = GetComponent<CircleCollider2D>();
        }

        renderers = GetComponentsInChildren<SpriteRenderer>();
        
        if(renderers != null)
        {
            colors = new Color[renderers.Length];
        }

        for (int i = 0; i < renderers.Length; ++i)
        {
            colors[i] = renderers[i].material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacked)
        {
            for (int i = 0; i < renderers.Length; ++i)
            {
                if (renderers[i].material.color.a >= 0.25f)
                {
                    renderers[i].material.color = new Vector4(originalColor.r, originalColor.g, originalColor.b, renderers[i].material.color.a - Time.deltaTime * 2.0f);
                }
                else
                {
                    renderers[i].material.color = colors[i];

                    if (i == renderers.Length - 1)
                    {
                        isAttacked = false;
                    }
                }
            }
        }

        timer += Time.deltaTime;

        if (isUsingLifeCycle)
        {
            if (timer > lifeCycle)
            {
                if (!isDeathTriggered)
                {
                    isDeathTriggered = true;

                    if (enemyDyingClip == null)
                    {
                        audioSource.clip = explosionClip;
                    }
                    else
                    {
                        audioSource.clip = enemyDyingClip;
                    }

                    audioSource.Play();
                    
                    renderer.enabled = false;
                    collider.enabled = false;

                    Destroy(gameObject, audioSource.clip.length);
                }
            }
        }

        if (transform.rotation != Quaternion.Euler(0.0f, 0.0f, 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        if (healthPoint <= 0)
        {
            if (!isDeathTriggered)
            {
                isDeathTriggered = true;

                Instantiate(itemPrefab, gameObject.transform.localPosition, gameObject.transform.localRotation);

                if (enemyDyingClip == null)
                {
                    audioSource.clip = explosionClip;
                }
                else
                {
                    audioSource.clip = enemyDyingClip;
                }

                audioSource.Play();

                if (renderer != null)
                {
                    renderer.enabled = false;
                }

                if (collider != null)
                {
                    collider.enabled = false;
                }

                Destroy(gameObject, audioSource.clip.length);
            }
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

            collision.gameObject.GetComponent<PlayerController>().isAttacked = true;

            audioSource.clip = playerHurtClip;
            audioSource.Play();
        }
    }

    public void PopUpDamageText(float damage)
    {
        string text = "-";
        text += damage.ToString();
        damageText.GetComponent<TMPro.TextMeshPro>().SetText(text);

        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 1;

        Instantiate(damageText, spawnPosition, Quaternion.identity);
    }
}

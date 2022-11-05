using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int healthPoint;

    [SerializeField] private float playerSpeed;
    private Rigidbody2D rigidBody;
    private Vector2 playerDirection;

    private float timer;
    [SerializeField] private float coolDown;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> firePointOffset;
    [SerializeField] private float fireForce;
    public int upgradeNumber = 0;

    [SerializeField] private LevelManager levelManager;

    private AudioSource audioSource;
    public AudioClip bulletClip;
    public AudioClip playerDeathClip;

    private bool isDeathTriggered = false;

    private Renderer renderer;
    private Color originalColor;
    public bool isAttacked = false;

    void Start()
    {
        timer = 0.0f;
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        originalColor = renderer.material.color;
    }

    private void Fire()
    {
        for (int i = 0; i < firePointOffset.Count; ++i)
        {
            if (i >= upgradeNumber)
            {
                break;
            }

            Vector3 updatedPosition = gameObject.transform.localPosition + firePointOffset[i].localPosition;
            GameObject bullet = Instantiate(bulletPrefab, updatedPosition, firePointOffset[i].localRotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * fireForce, ForceMode2D.Force);

            if (upgradeNumber > firePointOffset.Count)
            {
                bullet.GetComponent<Bullet>().damage++;
            }
        }
    }

    void Update()
    {
        if (isAttacked)
        {
            if (renderer.material.color.a >= 0.25f)
            {
                renderer.material.color = new Vector4(originalColor.r, originalColor.g, originalColor.b, renderer.material.color.a - Time.deltaTime * 2.0f);
            }
            else
            {
                renderer.material.color = originalColor;
                isAttacked = false;
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
                levelManager.Lose();

                audioSource.clip = playerDeathClip;
                audioSource.Play();

                Destroy(gameObject, audioSource.clip.length);

                isDeathTriggered = true;
            }
        }

        timer += Time.deltaTime;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (timer > coolDown)
        {
            audioSource.clip = bulletClip;
            audioSource.Play();

            Fire();

            timer = 0.0f;
        }

        playerDirection = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + playerDirection * playerSpeed * Time.fixedDeltaTime);
    }
}

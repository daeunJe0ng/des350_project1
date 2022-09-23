using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
    public int bulletNumber;
    readonly int upgradeNumber = 10;

    [SerializeField] private LevelManager levelManager;

    void Start()
    {
        timer = 0.0f;
        bulletNumber = upgradeNumber;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Fire()
    {
        for (int i = 0; i < bulletNumber / upgradeNumber; ++i)
        {
            if (i < 9)
            {
                Vector3 updatedPosition = gameObject.transform.localPosition + firePointOffset[i].localPosition;
                GameObject bullet = Instantiate(bulletPrefab, updatedPosition, firePointOffset[i].localRotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * fireForce, ForceMode2D.Force);
            }
            else
            {
                //Vector3 updatedPosition = gameObject.transform.localPosition + firePointOffset[i / 9].localPosition;
                //GameObject bullet = Instantiate(bulletPrefab, updatedPosition, firePointOffset[i / 9].localRotation);
                //bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * fireForce, ForceMode2D.Force);
            }
        }
    }

    void Update()
    {
        if (healthPoint <= 0)
        {
            levelManager.Lose();
            Destroy(gameObject);
            //Time.timeScale = 0;
        }

        timer += Time.deltaTime;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (timer > coolDown)
        {
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

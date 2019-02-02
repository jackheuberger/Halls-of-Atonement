using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float speed;
    public float damage;
    private PlayerData pd;

    private Rigidbody2D rb;

    private void Start()
    {
        pd = PlayerData.Instance;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
        speed = pd.arrowSpeed;
        damage = pd.arrowDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

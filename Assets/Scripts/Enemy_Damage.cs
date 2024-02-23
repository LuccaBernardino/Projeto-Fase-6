using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Enemy_Damage : NetworkBehaviour
{
    [SyncVar]
    public float hp;
    public float maxHP;
    public float attack;
    public Image enemyHPBar;
    

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    [Command(requiresAuthority = false)]
    void TakeDamage(float value)
    {
        hp -= value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player && collision.collider == player.weaponCollider)
        {
            TakeDamage(player.attack);

            Vector2 direction = (transform.position - player.transform.position).normalized;
            rb.velocity = direction * 10;

            Invoke("StopMoving" , 0.2f);
        }
    }

    void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }
    
    private void Update()
    {
        enemyHPBar.fillAmount = hp / maxHP;
        
        if(hp <= 0)
        {
            GetComponent<DropItems>().Drop();
            Destroy(gameObject);
        }
            
            
    }
}

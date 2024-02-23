using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using System;
using Cinemachine;


public class Player : NetworkBehaviour
{

    Rigidbody2D rb;
    float inputX;
    float inputY;
    bool inputAttack;
    public float speed;

    //eventos que ser�o disparados quando o jogador mover o Player e quiser atacar
    public InputEvent OnDirectionChanged;
    public BoolEvent OnAttack;
    public GameObject myCamera;
    public bool isWeaponEquipped;
    public float attack;
    public Collider2D weaponCollider;
    [SyncVar]
    public float hp;
    public float maxHP;

    public FloatEvent OnHPChanged;

    bool isTakingDamage;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        PolygonCollider2D collider = GameObject.FindGameObjectWithTag("CameraLimit").GetComponent<PolygonCollider2D>();
        myCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = collider;

        if (isLocalPlayer == false )
        {
            myCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
        }

        else
        {
            OnHPChanged.RemoveAllListeners();
            OnHPChanged.AddListener(GameObject.FindGameObjectWithTag("HUD"). GetComponent<StatsHUD>().UpdateHPBar);
        }
        GameObject.FindGameObjectWithTag("HUD").GetComponent<InventoryHUD>().OnChangeEquipment.AddListener(EquipWeapon);
    }

    void Update()
    {

        if (isLocalPlayer)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
            OnDirectionChanged?.Invoke(inputX, inputY);

            //por enquanto o ataque est� desabilitado, vamos programar ele em aula!
            if(isWeaponEquipped)
            {
                inputAttack = Input.GetKeyDown(KeyCode.Space);
                OnAttack?.Invoke(inputAttack);
            }

            if(isTakingDamage == false)
            {
             
                rb.velocity = new Vector2(inputX, inputY) * speed;

            }
        }
    
        
    }

    void EquipWeapon(SO_Weapons weapon)
    {
        isWeaponEquipped = true;
        attack = weapon.attackBonus;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Enemy_Damage enemy = collision.gameObject.GetComponent<Enemy_Damage>();

        if(enemy && weaponCollider.gameObject.activeSelf == false && isTakingDamage == false)
        {
            TakeDamage(enemy.attack);

            Vector2 direction = (transform.position - enemy.transform.position).normalized;

            rb.velocity = direction * 10;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player && player != this && collision.collider == player.weaponCollider)
        {
            if(player.GetComponent<Player_Group>().friendPlayer ==  null)
            {
                TakeDamage(player.attack);
            
                Vector2 direction = (transform.position - player.transform.position).normalized;
                rb.velocity = direction * 10;
            }
        }
    }

    void TakeDamage(float value)
    {
        hp -= value;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
        OnHPChanged.Invoke(hp / maxHP);

        isTakingDamage = true;
        Invoke("StopTakingDamage" , 0.3f);
    }

    void StopTakingDamage()
    {
        isTakingDamage = false;
    }

}

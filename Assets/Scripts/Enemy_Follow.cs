using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class Enemy_Follow : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    Animator anim;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        anim = GetComponentInChildren<Animator>();
    }



    void Update()
    {
        if (target)
        {
            agent.SetDestination(target.position);
            ChangeAnimation();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && target == null)
        {
            target = collision.gameObject.transform;
            agent.isStopped = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            target = null;
            agent.isStopped = true;
            anim.Play("Enemy_Idle");
        }
    }

    void ChangeAnimation()
    {
        if(transform.position.x > target.position.x)
        {
            anim.Play("Enemy_WalkHorizontal");
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Enemy_Damage>().enemyHPBar.fillOrigin = 0;
        }
        else if (transform.position.x < target.position.x)
        {
            anim.Play("Enemy_WalkHorizontal");
            transform.localScale = new Vector3(-1, 1, 1);
            GetComponent<Enemy_Damage>().enemyHPBar.fillOrigin = 1;
        }
    }
}

﻿using UnityEngine;
using System.Collections;
namespace Neutral
{
    public class MajorNavMeshController : MonoBehaviour
    {
        private Animator anim;
        NavMeshAgent navMeshAgent;
        AnimationUtilities AnimHelper;
        int isAttack = Animator.StringToHash("isAttack");
        int isSpawn = Animator.StringToHash("isSpawn");
        int speed = Animator.StringToHash("speed");

        void Start()
        {

            anim = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            AnimHelper = new AnimationUtilities();
        }


        void Update()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    navMeshAgent.SetDestination(hit.point);
                }
            }


            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            anim.SetFloat(speed, Mathf.Abs(navMeshAgent.velocity.magnitude));
            if (Mathf.Abs(navMeshAgent.velocity.magnitude) > 0.5)
            {
                anim.Play("Run", 0);
            }
            else if (stateInfo.IsName("Run"))
            {
                anim.Play("Idle", 0);
            }

            if (Input.GetKey("m"))
            {
                anim.SetBool(isSpawn, true);
                anim.Play("Spawn");
            }
            if (stateInfo.IsName("Spawn"))
            {
				//currently hard coding a value that lowers the time that the animation plays
                if (AnimHelper.AnimationFinished(stateInfo, stateInfo.length-0.9f))
                {
                    anim.SetBool(isSpawn, false);
                }
            }


            /*
                    if (Input.GetKey("z"))
                    {
                        anim.SetBool(isDead, true);
                        anim.Play("Death");
                    }
                    else if (Input.GetKey("c"))
                    {
                        anim.SetBool(isMerge, true);
                        anim.Play("Merge");
                    }
                    else if (Input.GetKey("x"))
                    {
                        anim.SetBool(isRespawn, true);
                        anim.Play("Respawn");
                    }


                    if (Input.GetKey("r"))
                    {
                        anim.SetBool(isDead, false);
                        anim.SetBool(isMerge, false);
                        anim.SetBool(isRespawn, false);
                    }
                    */

        }
    }
}
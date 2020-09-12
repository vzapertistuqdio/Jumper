using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotEnemy : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Death()
    {
        anim.SetTrigger("isDead");
        StartCoroutine(DeathAfterTime(3));
    }

    private IEnumerator DeathAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}

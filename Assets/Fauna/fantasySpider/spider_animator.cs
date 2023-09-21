using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider_animator : MonoBehaviour
{
    private Transform pos;
    private Vector3 posBuf;
    private Animation animations;

    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        animations = GetComponent<Animation>();
        posBuf = pos.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckAnimation());
    }

    IEnumerator CheckAnimation()
    {
        if (pos.position.x <= posBuf.x || pos.position.y <= posBuf.y || pos.position.z <= posBuf.z ||
            pos.position.x >= posBuf.x || pos.position.y >= posBuf.y || pos.position.z >= posBuf.z)
        {
            animations.Play("run");
            yield return new WaitForSeconds(1);
            posBuf = pos.position;
        }
        else
        {
            animations.Play("idle");
            yield return new WaitForSeconds(1);
        }
    }
}

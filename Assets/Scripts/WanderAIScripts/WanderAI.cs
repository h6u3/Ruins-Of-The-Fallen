using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WanderAI : MonoBehaviour {

    private Animation animation;
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    private void Start() {
        animation = GetComponent<Animation>();
    }

    private void Update() {

        if (isWandering == false) {
            StartCoroutine(Wander());
        }

        if (isRotatingRight == true) {

            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true) {

            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true) {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wander() {

        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);

        isWalking = true;
        animation.CrossFade("run");
        yield return new WaitForSeconds(walkTime);

        isWalking = false;
        animation.Play("idle");
        yield return new WaitForSeconds(rotateWait);

        if (rotateLorR == 1) {

            isRotatingRight = true;
            animation.CrossFade("run");
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
            animation.Play("idle");
        }

        if (rotateLorR == 2) {

            isRotatingLeft = true;
            animation.CrossFade("run");
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
            animation.Play("idle");
        }
        isWandering = false;
    }
}
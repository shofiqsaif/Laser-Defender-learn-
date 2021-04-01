using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config parameter
    [SerializeField]private float moveSpeed = 10f;
    [SerializeField] float deltaMinMaxXY = 0.5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.5f;

    Coroutine firingCoroutine;
    float minX,maxX,minY,maxY;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestCouroutine());
        SetUpMoveBoundaries();

    }

    

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.mousePosition.x / Screen.width);
        move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
        

    }

    private void move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        
        var newXPos = transform.position.x + deltaX;
        newXPos = Mathf.Clamp(newXPos, minX, maxX);
        var newYPos = transform.position.y + deltaY;
        newYPos = Mathf.Clamp(newYPos, minY, maxY);
        transform.position = new Vector2(newXPos,newYPos);

    }


    private void SetUpMoveBoundaries()      // SetUp minX,minY,maxX,maxY
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + deltaMinMaxXY;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - deltaMinMaxXY;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + deltaMinMaxXY;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - deltaMinMaxXY;


        Debug.Log(minX + "    " + maxX);
        Debug.Log(minY + "    " + maxY);
    }

    IEnumerator TestCouroutine()
    {
        Debug.Log("Before yield");
        //yield return new WaitForSeconds(3);
        yield return new WaitForSeconds(3);

        Debug.Log("After yield");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCannon : MonoBehaviour {

    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private GameObject bubblePrefab;

    [SerializeField]
    private GameObject aim;

    [SerializeField]
    private GameObject shotPlaceHolder;

    private GameObject currentBullet;
	// Use this for initialization
	void Start () {
        InitiateShot();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && currentBullet != null)
        {
            //determina a direção da bala
            Vector2 dir = aim.transform.position - shotPlaceHolder.transform.position;
            Rigidbody2D rigidbody2D = currentBullet.GetComponent<Rigidbody2D>();
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            rigidbody2D.AddForce(dir.normalized * bulletSpeed);
            currentBullet.GetComponent<Bubble>().stopped += InitiateShot;

            currentBullet = null;

        }
	}

    private void InitiateShot()
    {
        currentBullet = Instantiate(bubblePrefab, shotPlaceHolder.transform.position, Quaternion.identity);
    }
}

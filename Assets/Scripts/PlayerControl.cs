using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{

    public float speed;

    public float upRotation;
    public float downRotation;

    CharacterController characterControl;
    public Transform playerCam;

    Vector3 vel;

    public float lookSensitivity;

    float xRotation = 0;

    public GameObject bulletPrefab;

    List<GameObject> bulletPool = new List<GameObject>();
    public int bulletNum;

    int bulletIndex = 0;

    public bool hasKey = false;
    //public TMP_Text itemText;

    public Camera mainCam;

    public float castDist;

    public GameObject hitMarker;
    Vector3 pointHit;
    bool hitSometing;

    GameObject hitObj;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterControl = GetComponent<CharacterController>();
        //itemText.text = "nothing!";
        CreateBulletPool();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * lookSensitivity, 0);
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -upRotation, downRotation);
        playerCam.localRotation = Quaternion.Euler(xRotation, 0, 0);

        vel.z = Input.GetAxis("Vertical") * speed;
        vel.x = Input.GetAxis("Horizontal") * speed;

        vel = transform.TransformDirection(vel);
        characterControl.Move(vel * Time.deltaTime);

        if (Input.GetMouseButtonDown(1))
        {
            GameObject currentBullet = bulletPool[bulletIndex];
            currentBullet.SetActive(true);
            currentBullet.transform.position = transform.position;
            currentBullet.GetComponent<Rigidbody>().velocity = 2 * transform.forward;
            bulletIndex++;
            if(bulletIndex >= bulletPool.Count)
            {
                bulletIndex = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && hitObj != null)
        {
            hasKey = true;
            Destroy(hitObj);
            //Instantiate(hitMarker, pointHit, Quaternion.identity);
        }

    }

    void CreateBulletPool()
    {
        for(int i = 0; i < bulletNum; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.SetActive(false);
            bulletPool.Add(newBullet);
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 rayStart = mainCam.ViewportToWorldPoint(Input.mousePosition);
        //if (Physics.Raycast(rayStart, playerCam.forward, out hit, castDist))
        //{
        //    Debug.Log(hit.transform.name);
        //    hitSometing = true;
        //    pointHit = hit.point;
        //    if (hit.transform.name == "Key") {
        //        hitObj = hit.transform.gameObject;
        //    }
        //}
        //else
        //{
        //    hitSometing = false;
        //    hitObj = null;
        //}

        if (Physics.SphereCast(rayStart, 1, playerCam.forward, out hit, castDist))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.name == "Key")
            {
                hitSometing = true;
                hitObj = hit.transform.gameObject;
            }
        }
        else
        {
            hitSometing = false;
            hitObj = null;
        }

        Debug.DrawRay(rayStart, playerCam.forward * castDist, Color.red);
    }

}

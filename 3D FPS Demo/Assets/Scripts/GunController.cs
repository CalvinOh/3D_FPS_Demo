using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float gunRange;
    public int gunDamage;
    public float fireRate;
    public float hitForce;

    public Transform gunEnd;


    [SerializeField]
    private Camera fpsCamera;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);
    private LineRenderer laserLine;
    private float nextFire;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        fpsCamera = GetComponentInParent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire += Time.time + fireRate;

            Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            laserLine.enabled = true;

            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                //laserLine.SetPosition(1, rayOrigin + (fpsCamera.transform.forward * gunRange));
                laserLine.enabled = false;
            }

           

        }
        else
        {
            laserLine.enabled = false;
        }
    }

}

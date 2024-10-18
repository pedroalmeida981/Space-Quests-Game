using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    private Camera fpsCam;
    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    private RaycastHit hit;

    // Look for the camera tag 
    void Start()
    {
        if (fpsCam == null)
            fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
    }
    
    // Shoot
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            //Target get damaged
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }
}

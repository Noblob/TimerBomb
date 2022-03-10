using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{

    public float damage = 100f;
    public float range = 25;
    public GameObject ExplosionArea;
    public GameObject Bomb;
    public GameObject Player;
    public Transform PlayerTransform;
    public Transform BombTransform;
    public Camera Cam;

    // Start is called before the first frame update
    void Start()
    {
        Bomb.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(Explosion());
        }

        TestExplosion();
    }

    void TestExplosion()
    {
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                Destroy(Player);
            }
        }
    }

    IEnumerator Explosion()
    {
        Bomb.SetActive(true);
        PlayerTransform.DetachChildren();
        Bomb.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(10f);
        BombTransform.DetachChildren();
        ExplosionArea.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(Bomb);
        Destroy(ExplosionArea);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Arrow : MonoBehaviour {
    public RoleType roleType;
    public int speed = 5;

    public GameObject explosionEffect;

    public bool isLocal = false;

    private Rigidbody rgd;

	// Use this for initialization
	void Start () {
        rgd = transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rgd.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ShootPerson);
            if(isLocal)
            {
                bool playerIsLocal = other.GetComponent<PlayerInfo>().isLocal;
                if (isLocal != playerIsLocal)
                {
                    GameFacade.Instance.SendAttack(Random.Range(10, 20));
                }
            }
        }
        else
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Miss);
        }
        GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        GameObject.Destroy(this.gameObject);
    }
}

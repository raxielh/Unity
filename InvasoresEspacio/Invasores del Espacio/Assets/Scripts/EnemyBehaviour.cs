using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private Transform player;
    private float speed = 0.02f;
    private int healt = 2;
    public Transform explosion;

    // Use this for initialization
    void Start () {

        player = GameObject.Find("playerShip").transform;

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.position - this.transform.position;
        direction.Normalize();
        this.transform.position += direction * speed;

    }

    void OnTriggerEnter2D(Collider2D theCollider)
    {

        if (theCollider.gameObject.name.Contains("Laser"))
        {

            LaserBehaviour laser = theCollider.gameObject.GetComponent("LaserBehaviour") as LaserBehaviour;
            healt -= laser.damage;
            Destroy(theCollider.gameObject);

        }

        if (healt <= 0)
        {

            Destroy(this.gameObject);
            GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            controller.killEnemigo();

            if (explosion)
            {

                GameObject exploder = ((Transform)Instantiate(explosion, 
                                                              this.transform.position,
                                                              this.transform.rotation)
                                                              ).gameObject;
                Destroy(exploder,2.0f);

            }

        }

        if (theCollider.gameObject.name.Contains("playerShip"))
        {
        }

    }

}

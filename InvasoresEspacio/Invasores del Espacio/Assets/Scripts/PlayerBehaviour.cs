using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public float playerSpeed = 4.0f;
	private float currentSpeed = 0.0f;
	private Vector3 lastMovement = new Vector3();
    public Transform laser;
    private float laserDistanciaInicial = -.8f;
    public float tiempoEntreDisparos = .3f;
    private float tiempoSiguienteDisparo = 0.0f;
    public List<KeyCode> shootButton;

	void Update () {
		Rotate();
		Move();
        Fire();
	}

	void Rotate (){
		
		Vector3 worldPos = Input.mousePosition;

		worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        Vector3 spaceShipPos = this.transform.position;

		float dx = spaceShipPos.x - worldPos.x;
		float dy = spaceShipPos.y - worldPos.y;

		float angle = Mathf.Atan2(dy,dx) * Mathf.Rad2Deg;

		Quaternion rot = Quaternion.Euler( new Vector3( 0,0,angle+90 ) );

        this.transform.rotation = rot;

	}

	void Move (){

        Vector3 movement = new Vector3();

        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");

        movement.Normalize();

        if (movement.magnitude > 0)
        {
            currentSpeed = playerSpeed;
            //Espacio S=V*T , V=vector de movimiento * velocidad actual, t=tiempo
            this.transform.Translate(movement * Time.deltaTime * currentSpeed , Space.World);
            lastMovement = movement;
        }
        else
        {
            //Inercia ultimo movimiento
            this.transform.Translate(movement * Time.deltaTime * currentSpeed , Space.World);
            currentSpeed *= 0.9f;
        }

    }

    void Fire (){

        foreach (KeyCode key in shootButton)
        {
            if (Input.GetKey(key) && tiempoSiguienteDisparo < 0)
            {
                tiempoSiguienteDisparo = tiempoEntreDisparos;
                ShootLaser();
                break;
            }
        }

        tiempoSiguienteDisparo -= Time.deltaTime;

    }

    void ShootLaser() {

        Vector3 LaserPos = this.transform.position;

        float rotationAngle = this.transform.localEulerAngles.z - 90;

        LaserPos.x += Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * laserDistanciaInicial;
        LaserPos.y += Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * laserDistanciaInicial;

        Instantiate(laser,LaserPos,this.transform.rotation);

    }

}

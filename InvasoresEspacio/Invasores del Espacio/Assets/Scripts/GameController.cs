using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Transform enemy;

    [Header("Oleadas de Enemigos")]

    public float tiempoAntesOleada = 1.2f;
    public float tiempoEntreEnemigos = 0.25f;
    public float tiempoEntreOleadas = 2.0f;

    public int enemigosPorOleada = 10;
    private int cuantosEnemigos = 0;

    void Start() {
        StartCoroutine(SpawnEnemigos());
    }

    IEnumerator SpawnEnemigos() {

        yield return new WaitForSeconds(tiempoAntesOleada);

        while (true)
        {
            if (cuantosEnemigos <= 0)
            {
                for (int i = 0; i < enemigosPorOleada; i++)
                {

                    float randDistance = Random.Range(10, 25);
                    Vector2 randDirection = Random.insideUnitCircle;
                    Vector3 enemigoPos = this.transform.position;
                    enemigoPos.x += randDirection.x * randDistance;
                    enemigoPos.y += randDirection.y * randDistance;

                    Instantiate(enemy, enemigoPos, this.transform.rotation);
                    cuantosEnemigos++;

                    yield return new WaitForSeconds(tiempoEntreEnemigos);

                }
            }

            yield return new WaitForSeconds(tiempoEntreOleadas);
        }


    }

    public void killEnemigo() {

        cuantosEnemigos--;

    }
	
}

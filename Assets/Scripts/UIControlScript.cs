using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControlScript : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject prefabMovimiento;
    public GameObject prefabEstatico;

    // Start is called before the first frame update
    void Start()
    {
        prefabEstatico.SetActive(false);
        prefabMovimiento.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posicionObjeto1 = prefabMovimiento.transform.position; //captamos la posicion del prefab en movimiento
        prefabEstatico.transform.position = posicionObjeto1; //con base en la posicion anterior posicionamos nuestro prefab estatico 

        if (gameManager.isGameOver == true) //el personaje murio
        {
            prefabEstatico.SetActive(true);
            prefabMovimiento.SetActive(false);
        }
        else if(gameManager.isGameOver == false)
        {
            prefabEstatico.SetActive(false);
            prefabMovimiento.SetActive(true);
        }

        if (gameManager.isPaused == true)
        {
            prefabEstatico.SetActive(true);
            prefabMovimiento.SetActive(false);
        }
        else if (gameManager.isPaused == false)
        {
            prefabEstatico.SetActive(false);
            prefabMovimiento.SetActive(true);
        }

    }
}

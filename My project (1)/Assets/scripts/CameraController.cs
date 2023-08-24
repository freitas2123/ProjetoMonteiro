using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Capsula;
    public float sensibilidade = 2;
    public float suavizacao = 1.5f;

    private Vector2 velocidadeFrame, velocidadeRotacao;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseInputs = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawVelocidadeFrame = Vector2.Scale(mouseInputs, Vector2.one * sensibilidade);
        velocidadeFrame = Vector2.Lerp(velocidadeFrame, rawVelocidadeFrame, 1 / suavizacao);
        velocidadeRotacao += velocidadeFrame;
        velocidadeRotacao.y = Mathf.Clamp(velocidadeRotacao.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-velocidadeRotacao.y, Vector3.right);
        Capsula.localRotation = Quaternion.AngleAxis(velocidadeRotacao.x, Vector3.up);
    }

}

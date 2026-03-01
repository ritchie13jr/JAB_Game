using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public Transform cameraTransform;
    public Transform menuPoint;
    public Transform gamePoint;
    public GameObject menuCanvas;
    public GameObject creditsCanvas;

    public float speed = 2f;

    void Awake()
    {
        Instance = this;

        cameraTransform.position = menuPoint.position;
        cameraTransform.rotation = menuPoint.rotation;

        menuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    public void PlayGame()
    {
        menuCanvas.SetActive(false);
        StartCoroutine(MoveCamera(gamePoint));
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit(); esta es para cuando hagamos la build con esta linea mejor
    }
    public void OpenCredits()
    {
        menuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void ReturnToMenu()
    {
        StopAllCoroutines();

        StartCoroutine(MoveCamera(menuPoint));

        menuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    IEnumerator MoveCamera(Transform target)
    {
        while (Vector3.Distance(cameraTransform.position, target.position) > 0.01f)
        {
            cameraTransform.position = Vector3.Lerp(
                cameraTransform.position,
                target.position,
                Time.deltaTime * speed);

            cameraTransform.rotation = Quaternion.Lerp(
                cameraTransform.rotation,
                target.rotation,
                Time.deltaTime * speed);

            yield return null;
        }

        cameraTransform.position = target.position;
        cameraTransform.rotation = target.rotation;
    }
}
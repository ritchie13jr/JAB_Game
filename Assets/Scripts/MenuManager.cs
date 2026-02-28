using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform menuPoint;
    public Transform gamePoint;
    public GameObject menuCanvas;

    public float speed = 2f;

    void Awake()
    {
        // Forzar inicio SIEMPRE antes que otros scripts
        cameraTransform.position = menuPoint.position;
        cameraTransform.rotation = menuPoint.rotation;

        menuCanvas.SetActive(true);
    }

    public void PlayGame()
    {
        //Debug.Log("PLAY PRESSED");
        menuCanvas.SetActive(false);
        StartCoroutine(MoveCamera(gamePoint));
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
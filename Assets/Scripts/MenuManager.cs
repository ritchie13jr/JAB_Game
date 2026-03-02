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

    private bool isInTransition;
    float lerpTimer;

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
        isInTransition = true;
        lerpTimer = 0.0f;
        float l_pct = 0.0f;
        Vector3 l_currentPos = cameraTransform.position;
        Quaternion l_currentRot = cameraTransform.rotation;

        while (l_pct < 1.0f)
        {
            l_pct = Mathf.Min(lerpTimer / speed, 1.0f);

            cameraTransform.position = Vector3.Lerp(
            l_currentPos,
            target.position,
            l_pct);

            cameraTransform.rotation = Quaternion.Lerp(
                l_currentRot,
                target.rotation,
                l_pct);

            //Debug.Log(l_pct);

            yield return null;
        }        

        cameraTransform.position = target.position;
        cameraTransform.rotation = target.rotation;

        isInTransition = false;
    }

    private void Update()
    {
        if (isInTransition)
        {
            lerpTimer += Time.deltaTime;
        }
    }
}
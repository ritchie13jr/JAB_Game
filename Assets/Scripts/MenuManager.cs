using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("CameraPooints")]
    public Transform cameraTransform;
    public Transform menuPoint;
    public Transform gamePoint;
    public Transform playPoint;

    [Header("Canvas")]
    public GameObject menuCanvas;
    public GameObject creditsCanvas;
    public GameObject howToPlayCanvas;
    public List<GameObject> pages;
    
    [Header("TransitionTime")]
    public float playTransitionTime = 4f;
    public float transitionTime = 2f;

    Transform[] cameraPointsToPlay; 

    private bool isInTransition;
    private float lerpTimer;
    private int currentPage;

    void Awake()
    {
        Instance = this;

        cameraTransform.position = menuPoint.position;
        cameraTransform.rotation = menuPoint.rotation;
        cameraPointsToPlay = new Transform[] {gamePoint, playPoint};

        currentPage = 0;
        menuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
        howToPlayCanvas.SetActive(false);
    }

    public void PlayGame()
    {
        menuCanvas.SetActive(false);
        StartCoroutine(MoveCameraThroughPoints(cameraPointsToPlay));
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

    public void OpenHowToPlay() 
    {
        menuCanvas.SetActive(false);
        howToPlayCanvas.SetActive(true);
        pages[currentPage].SetActive(true);
    }

    public void CloseHowToPlay() 
    {
        if (currentPage >= pages.Count)
            currentPage = 0;

        menuCanvas.SetActive(true);
        howToPlayCanvas.SetActive(false);
        pages[currentPage].SetActive(false);
        currentPage = 0;
    }

    public void NextPage() 
    {
        pages[currentPage].SetActive(false);
        currentPage++;
        if (currentPage >= pages.Count) 
        {
            CloseHowToPlay();
            currentPage = 0;
        }

        pages[currentPage].SetActive(true);
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

    IEnumerator MoveCameraThroughPoints(Transform[] points)
    {
        isInTransition = true;
        
        float transDuration = playTransitionTime / points.Length;

        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;

        foreach (Transform t in points) 
        {
            lerpTimer = 0.0f;
            while (lerpTimer < transDuration) 
            {
                float pct = lerpTimer / transDuration;
                cameraTransform.position = Vector3.Lerp(startPos, t.position, pct);
                cameraTransform.rotation = Quaternion.Lerp(startRot, t.rotation, pct);

                lerpTimer += Time.deltaTime;
                yield return null;
            }
            cameraTransform.position = t.position;
            cameraTransform.rotation = t.rotation;

            startPos = t.position;
            startRot = t.rotation;
        }

        isInTransition = false;
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
            l_pct = Mathf.Min(lerpTimer / transitionTime, 1.0f);

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
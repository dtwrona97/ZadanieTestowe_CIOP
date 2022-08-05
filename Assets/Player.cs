using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform mainCameraTransform;
    [SerializeField] private ClosetItem[] closetItems;
    private LineRenderer lineRenderer;
    public string currentHit;
    public bool isMaskOn = false;
    public int wornItemsCounter = 0;

    void Start()
    {
        if (GetComponent<LineRenderer>()) lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawLineRendereOnRaycastHit();
    }

    private void DrawLineRendereOnRaycastHit()
    {
        lineRenderer.SetPosition(0, transform.position);

        RaycastHit hit;

        if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out hit))
        {
            lineRenderer.SetPosition(1, hit.point);
            currentHit = hit.collider.name;

            if (hit.collider.tag.Equals("Closet"))
            {
                if (hit.collider.gameObject.GetComponent<Closet>())
                {
                    hit.collider.gameObject.GetComponent<Closet>().ShowItems();
                }
            }

            if (!hit.collider.tag.Equals("Closet") && !hit.collider.tag.Equals("ClosetItem"))
            {
                if (Singleton.Instance)
                {
                    Singleton.Instance.closetClothes.HideItems();
                    Singleton.Instance.closetMasks.HideItems();
                    Singleton.Instance.closetGuns.HideItems();
                }
            }

            if (hit.collider.tag.Equals("ClosetItem"))
            {
                hit.collider.GetComponent<ClosetItem>().SetHighlight(true);
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.GetComponent<ClosetItem>())
                    {

                        if (!hit.collider.GetComponent<ClosetItem>().isWorn)
                        {
                            StartCoroutine(hit.collider.GetComponent<ClosetItem>().GoToSlotCoroutine());
                            wornItemsCounter++;
                        }
                        else
                        {
                            StartCoroutine(hit.collider.GetComponent<ClosetItem>().GoToClosetCoroutine());
                            wornItemsCounter--;
                        }
                        if (wornItemsCounter == 8)
                        {
                            if (Singleton.Instance) Singleton.Instance.scenarioManager.CompleteScenarioStep();
                        }
                    }
                }
            }
            else
            {
                foreach (ClosetItem item in closetItems) item.SetHighlight(false);
            }
        }
    }



}

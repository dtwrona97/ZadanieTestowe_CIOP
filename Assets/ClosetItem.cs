using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetItem : MonoBehaviour
{
    [SerializeField] private Transform mySlotTransform;
    [SerializeField] private bool isMask;
    [SerializeField] private Material standardMat;
    [SerializeField] private Material highlightMat;
    private bool isShown = false;
    public bool isWorn = false;


    public void Show()
    {
        if (isShown) return;
        if (GetComponent<Animator>()) GetComponent<Animator>().Play("ClosetItemShow");
        isShown = true;
    }
    public void Hide()
    {
        if (!isShown) return;
        if (GetComponent<Animator>()) GetComponent<Animator>().Play("ClosetItemHide");
        isShown = false;
    }

    public IEnumerator GoToSlotCoroutine()
    {
        if (isWorn || mySlotTransform == null) yield break;
        if (isMask)
        {
            if (Singleton.Instance)
            {
                if (!Singleton.Instance.player.isMaskOn)
                    Singleton.Instance.player.isMaskOn = true;
                else
                    yield break;
            }

        }
        isWorn = true;
        GetComponent<Animator>().enabled = false;
        while (Vector3.Distance(transform.localPosition, Vector3.zero) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, mySlotTransform.position, Time.deltaTime * 5);
            yield return null;
        }
    }

    public IEnumerator GoToClosetCoroutine()
    {
        if (!isWorn) yield break;
        isWorn = false;
        if (isMask)
        {
            if (Singleton.Instance)
            {
                Singleton.Instance.player.isMaskOn = false;
            }
        }
        while (Vector3.Distance(transform.position, mySlotTransform.position) > 0.05f)
        {
            transform.localPosition = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime * 5);
            yield return null;
        }
        GetComponent<Animator>().enabled = true;
    }

    public void SetHighlight(bool isHighlight)
    {
        GetComponent<Renderer>().material = isHighlight ? highlightMat : standardMat;

        //if (isHighlight)
        //{
        //    GetComponent<Renderer>().material = highlightMat;
        //}
        //else
        //{
            
        //    GetComponent<Renderer>().material = highlightMat;
        //}
    }
}

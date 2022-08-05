using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    [SerializeField] private ClosetItem[] closetItems;

    public void ShowItems()
    {
        foreach (ClosetItem cI in closetItems) cI.Show();
    }
    public void HideItems()
    {
        foreach (ClosetItem cI in closetItems) cI.Hide();

    }

}

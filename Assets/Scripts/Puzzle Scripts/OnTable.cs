using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTable : MonoBehaviour
{
    public List<GameObject> correctItems = new List<GameObject>();

    List<GameObject> placedItems = new List<GameObject>();

    public bool correct = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Movable")
        {
            return;
        }
        if (!placedItems.Contains(other.gameObject))
        {
            placedItems.Add(other.gameObject);
        }
        correct = true;
        foreach (GameObject go in correctItems)
        {
            if (!placedItems.Contains(go))
            {
                correct = false;
            }
        }

        if (correct)
        {
            transform.parent.GetComponent<TablePuzzle>().CheckSolution();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Movable")
        {
            return;
        }
        if (placedItems.Contains(other.gameObject))
        {
            placedItems.Remove(other.gameObject);
        }
    }
}

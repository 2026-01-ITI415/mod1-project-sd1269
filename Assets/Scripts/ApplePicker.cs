using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Basket Settings")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;

    [Header("Castle Settings")]
    public GameObject castlePrefab;
    public Vector3 castleSpawnPosition = new Vector3(0f, -12f, 0f);

    private GameObject currentCastle;

    void Start()
    {
        // Spawn baskets
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }

        // Spawn castle
        if (castlePrefab != null)
        {
            currentCastle = Instantiate(castlePrefab, castleSpawnPosition, Quaternion.identity);
        }
    }

    public void AppleDestroyed()
    {
        // Destroy all of the falling apples
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray)
        {
            Destroy(tGO);
        }

        // Destroy one of the baskets
        int basketIndex = basketList.Count - 1;
        GameObject tBasketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        // If there are no Baskets left, restart the game
        if (basketList.Count == 0)
        {
            SceneManager.LoadScene("ApplePicker");
        }
    }
}
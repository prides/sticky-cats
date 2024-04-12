using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoGameManager : MonoBehaviour
{
    private static ProtoGameManager instance;
    public static ProtoGameManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private GameObject catPrefab;
    [SerializeField]
    private GameObject catSpawnAnchor;
    [SerializeField]
    private DragRigidbody dragger;
    [SerializeField]
    private Text score;

    [SerializeField, ReadOnly]
    private ProtoCatController activeCat;

    private int catNum = 0;
    public int CatNum
    {
        get { return catNum; }
        private set
        {
            catNum = value;
            score.text = catNum.ToString();
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        instance = this;
        SpawnCat();
    }

    public void CatSelected(GameObject gameObject)
    {
        ProtoCatController catController = gameObject.GetComponent<ProtoCatController>();
        if (catController == null)
        {
            return;
        }
        catController.EnableCat();
    }

    public void OnCatOut(ProtoCatController catController)
    {
        Debug.Log("cat out" + catController.name);
        Destroy(catController.gameObject);
        if (catController == activeCat)
        {
            activeCat = null;
        }
        else
        {
            CatNum--;
        }
        SpawnCat();
    }

    public void OnCatStick(ProtoCatController catController)
    {
        if (catController == activeCat)
        {
            activeCat = null;
        }
        dragger.Release();
        SpawnCat();
        CatNum++;
    }

    private void SpawnCat()
    {
        if (activeCat != null)
        {
            Debug.LogWarning("active cat already exists");
            return;
        }
        GameObject cat = Instantiate(catPrefab, catSpawnAnchor.transform.position, Quaternion.identity);

        activeCat = cat.GetComponent<ProtoCatController>();
    }
}

using UnityEngine;

public class Switch : MonoBehaviour
{
    public ParticleSystem flames;
    private Color otherPlanColor;
    private SpriteRenderer spriteRenderer;
    private int firstPlanLayer, secondPlanLayer, playerLayer; 
    private GameObject[] firstPlanObjects, secondPlanObjects;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        firstPlanLayer = LayerMask.NameToLayer("1stPlan");
        secondPlanLayer = LayerMask.NameToLayer("2ndPlan");
        playerLayer = LayerMask.NameToLayer("Player");
        firstPlanObjects = GameObject.FindGameObjectsWithTag("1stPlan");
        secondPlanObjects = GameObject.FindGameObjectsWithTag("2ndPlan");
        otherPlanColor = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        ChangeMaterial(secondPlanObjects, otherPlanColor);
    }

    void Update()
    {
        if (spriteRenderer && flames && Input.GetButtonDown("Switch"))
        {
            string layerName = spriteRenderer.sortingLayerName;
            if (layerName.Equals("1stPlan"))
            {
                Physics2D.IgnoreLayerCollision(firstPlanLayer, playerLayer, true);
                Physics2D.IgnoreLayerCollision(secondPlanLayer, playerLayer, false);
                ChangePlan("2ndPlan");
                ChangeMaterial(firstPlanObjects, otherPlanColor);
                ChangeMaterial(secondPlanObjects, Color.white);
            }
            else if(layerName.Equals("2ndPlan"))
            {
                Physics2D.IgnoreLayerCollision(firstPlanLayer, playerLayer, false);
                Physics2D.IgnoreLayerCollision(secondPlanLayer, playerLayer, true);
                spriteRenderer.sortingOrder = 1;
                flames.GetComponent<Renderer>().sortingOrder = 1;
                ChangePlan("1stPlan");
                ChangeMaterial(firstPlanObjects, Color.white);
                ChangeMaterial(secondPlanObjects, otherPlanColor);
            }
        }
    }

    void ChangePlan(string planName)
    {
        spriteRenderer.sortingLayerName = planName;
        flames.GetComponent<Renderer>().sortingLayerName = planName;
    }

    void ChangeMaterial(GameObject[] planObjects, Color color)
    {
        foreach (GameObject planObject in planObjects)
            planObject.GetComponent<SpriteRenderer>().material.color = color;
    }
}
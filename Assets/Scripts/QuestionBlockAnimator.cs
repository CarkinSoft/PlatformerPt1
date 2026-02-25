using UnityEngine;

public class QuestionBlockAnimator : MonoBehaviour
{
    public Material questionMaterial; 
    public float cycleTime = 0.5f; 
    public int shadeCount = 5; 

    private int currentIndex = 0;
    private float timer = 0f;

    void Start()
    {
        SetShade(0);
    }

    void Update()
    {
        if (questionMaterial == null || shadeCount < 2) return;
        timer += Time.deltaTime;
        if (timer >= cycleTime)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % shadeCount;
            SetShade(currentIndex);
        }
    }

    void SetShade(int index)
    {
    
        float sliceHeight = 1f / shadeCount;
        Vector2 offset = new Vector2(1f, 1f - sliceHeight * (index + 1));
        Vector2 scale = new Vector2(-1, -sliceHeight);
        questionMaterial.mainTextureOffset = offset;
        questionMaterial.mainTextureScale = scale;
    }
}

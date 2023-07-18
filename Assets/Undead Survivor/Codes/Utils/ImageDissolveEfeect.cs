using UnityEngine;
using UnityEngine.UI;

public class ImageDissolveEfeect : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float speed = 1.0f;
    private Image image;
    private Material material;
    private float dissolveAmount = 0.0f;

    void Awake()
    {
        image = GetComponent<Image>();
        material = new Material(Shader.Find("Custom/ImageDissolveShader"));
        image.material = material;
    }

    void Update()
    {
        dissolveAmount += speed * Time.deltaTime;
        dissolveAmount = Mathf.Clamp01(dissolveAmount);
        material.SetFloat("_Amount", dissolveAmount);
    }
}

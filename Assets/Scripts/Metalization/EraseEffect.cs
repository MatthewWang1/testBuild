using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EraseEffect : MonoBehaviour
{
    public RawImage maskImage; // Reference to the UI Mask Image
    private Texture2D maskTexture; // Editable mask texture
    public float brushSize; // Size of the eraser brush
    public Camera uiCamera; // Reference to the UI camera

    [SerializeField] private TextMeshProUGUI liftOffLeftText;
    private bool liftoff_enable;
    private bool complete;

    private void Start()
    {
        liftoff_enable = false;
        complete = false;
        // Create a new modifiable Texture2D with RGBA32 format
        Texture2D originalTexture = maskImage.texture as Texture2D;
        maskTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);

        // Copy pixels from original texture to make an editable version
        Color[] pixels = originalTexture.GetPixels();
        maskTexture.SetPixels(pixels);
        maskTexture.Apply();

        // Assign the modifiable texture back to the UI
        maskImage.texture = maskTexture;
    }

    public void LiftoffEnable()
    {
        liftoff_enable = true;
    }

    public bool IsComplete()
    {
        return complete;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && liftoff_enable) // Check for left mouse button click
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                maskImage.rectTransform, Input.mousePosition, uiCamera, out localPoint))
            {
                // Convert local UI position to texture coordinates

                // int x = Mathf.RoundToInt((localPoint.x + maskImage.rectTransform.rect.width / 2) / maskImage.rectTransform.rect.width);
                // int y = Mathf.RoundToInt((localPoint.y + maskImage.rectTransform.rect.height / 2) / maskImage.rectTransform.rect.height);
                // Debug.Log($"Erasing at ({localPoint.x}, {localPoint.y})");
                // EraseAt(x,y);

                Vector2 texturePosition;
                if (GetMouseTextureCoordinates(out texturePosition))
                {
                    EraseAt((int)texturePosition.x, (int)texturePosition.y);
                }
            }
        }

        float left = GetOpacityPercentage(maskTexture);

        liftOffLeftText.text = ((int)(left*100f/77.5f)).ToString();

        if(GetOpacityPercentage(maskTexture)<1) complete = true;



        float GetOpacityPercentage(Texture2D texture)
        {
            // Get all the pixels in the texture
            Color[] pixels = texture.GetPixels();

            // Count the number of opaque pixels (alpha = 1)
            int opaqueCount = 0;

            foreach (Color pixel in pixels)
            {
                if (pixel.a == 1f) // Fully opaque pixel
                {
                    opaqueCount++;
                }
            }

            // Calculate the percentage of opaque pixels
            float opacityPercentage = (float)opaqueCount / pixels.Length * 100f;
            return opacityPercentage;
        }

        bool GetMouseTextureCoordinates(out Vector2 textureCoord)
        {
            textureCoord = Vector2.zero;
            
            // Get the world position of the mouse
            Vector2 screenMousePos = Input.mousePosition;

            // Get the world position of the UI element
            RectTransform rt = maskImage.rectTransform;
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);

            // Convert to texture space
            float minX = corners[0].x;
            float maxX = corners[2].x;
            float minY = corners[0].y;
            float maxY = corners[2].y;

            if (screenMousePos.x < minX || screenMousePos.x > maxX || screenMousePos.y < minY || screenMousePos.y > maxY)
                return false; // Mouse is outside of the image

            float normalizedX = (screenMousePos.x - minX) / (maxX - minX);
            float normalizedY = (screenMousePos.y - minY) / (maxY - minY);

            // Convert to texture pixel coordinates
            textureCoord.x = normalizedX * maskTexture.width;
            textureCoord.y = normalizedY * maskTexture.height;

            return true;
        }
    }

    void EraseAt(int x, int y)
    {
        int radius = Mathf.RoundToInt(brushSize / 2);

        for (int i = -radius; i < radius; i++)
        {
            for (int j = -radius; j < radius; j++)
            {
                int px = Mathf.Clamp(x + i, 0, maskTexture.width - 1);
                int py = Mathf.Clamp(y + j, 0, maskTexture.height - 1);

                // Make pixel fully transparent
                maskTexture.SetPixel(px, py, new Color(0, 0, 0, 0));
            }
        }
        maskTexture.Apply(); // Apply changes to the texture
    }
}

using UnityEngine;

public class Chunk : MonoBehaviour
{
  public int chunkX { get; private set; }
  public int chunkY { get; private set; }
  public const int size = 32;
  public Pixel[,] grid;
  public bool changesMade { get; private set; } = false;

  public void Init(int x, int y)
  {
    chunkX = x;
    chunkY = y;
    gameObject.AddComponent<SpriteRenderer>();
    gameObject.AddComponent<CustomCollider2D>();
    
    GenerateContents();
  }
  
  private void GenerateContents()
  // generate contents using procedural generation
  {
    grid = new Pixel[size, size];
    for (int x = 0; x < size; x++)
    {
      for (int y = 0; y < size; y++)
      {
        grid[x, y] = new Pixel((uint)Random.Range(0,3));
      }
    }
    
    GenerateColliderAndSprite();
  }

  private void LoadContents()
  // get contents from save file
  {
    GenerateColliderAndSprite();
  }
  
  private void GenerateColliderAndSprite()
  {
    CustomCollider2D customCollider2D = gameObject.GetComponent<CustomCollider2D>();
    SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    
    PhysicsShapeGroup2D shape = new PhysicsShapeGroup2D();
    Texture2D texture2D = new Texture2D(size, size);

    for (int x = 0; x < size; x++)
    {
      for (int y = 0; y < size; y++)
      {
        if (grid[x, y] != null || x == 0 || x == size - 1 || y == 0 || y == size - 1)
        {
          shape.AddBox(new Vector2(chunkX * size + x, chunkY * size + y), new Vector2(Pixel.size, Pixel.size));
          texture2D.SetPixel(x, y, grid[x, y].color);
        }
        if (x == 0 || x == size - 1 || y == 0 || y == size - 1)
        {
          shape.AddBox(new Vector2(chunkX * size + x, chunkY * size + y), new Vector2(Pixel.size, Pixel.size));
        }
      }
    }
    
    customCollider2D.ClearCustomShapes();
    customCollider2D.SetCustomShape(shape, 0 , 0);
    customCollider2D.offset = gameObject.transform.position;
    
    texture2D.Apply();
    spriteRenderer.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
  }
}
using System;
using UnityEngine;

public class Pixel
{
  public uint id { get; private set; }
  public Color32 color;
  public const float size = 0.5f;
  private static readonly Color32[] colors = new[] { new Color32(255, 0, 0, 255), new Color32(0, 255, 0, 255), new Color32(0, 0, 255, 255) };
  private static readonly Color32 error = new Color32(255, 16, 192, 255);

  public Pixel(uint id)
  {
    this.id = id;
    if (id >= colors.Length)
    {
      color = error;
    }
    color = colors[id];
  }
}
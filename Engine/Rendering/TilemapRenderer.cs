using System;
using System.Linq;
using Hel.Engine.Rendering.Models.Enums;
using Hel.Engine.Rendering.Models.Payloads;
using Hel.Tiled.Models.Enums.Layer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hel.Engine.Rendering
{
    /// <summary>
    /// TODO: Rewrite so it uses vertex buffer instead of spritebatch
    /// </summary>
    public class TilemapRenderer : IRenderer<TilemapRendererPayload>
    {
        public RendererApi RendererApi { get; } = RendererApi.OpenGL;
        
        static readonly uint FLIPPED_HORIZONTALLY_FLAG = 0x80000000;
        static readonly uint FLIPPED_VERTICALLY_FLAG   = 0x40000000;
        static readonly uint FLIPPED_DIAGONALLY_FLAG   = 0x20000000;
        
        public void Draw(TilemapRendererPayload payload, SpriteBatch spriteBatch)
        {
            SpriteEffects horizontalFlipEffect; 
            SpriteEffects verticalFlipEffect;
            for(int layerIndex = 0; layerIndex < payload.Tilemap.Layers.Count; layerIndex++ )
            {
                var layer = payload.Tilemap.Layers[layerIndex];
                if (layer.Type != LayerTypeEnum.TileLayer) continue;
                
                for (var i = 0; i < layer.Data.Length; i++)
                {
                    var gid = layer.Data[i];
    
                    if (gid == 0) continue;
                    
                    horizontalFlipEffect = (gid & FLIPPED_HORIZONTALLY_FLAG) != 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    verticalFlipEffect = (gid & FLIPPED_VERTICALLY_FLAG) != 0 ? SpriteEffects.FlipVertically : SpriteEffects.None;
                    //var flipped_diagonally = (gid & FLIPPED_DIAGONALLY_FLAG) != 0;

                    var spriteEffect = (horizontalFlipEffect | verticalFlipEffect);
                    
                    gid &= (int) ~(FLIPPED_HORIZONTALLY_FLAG |
                                   FLIPPED_VERTICALLY_FLAG |
                                   FLIPPED_DIAGONALLY_FLAG);


                    var tilemapX = (i % payload.Tilemap.Width) * payload.Tilemap.TileWidth;
                    var tilemapY = (float)Math.Floor(i / (double)payload.Tilemap.Width) * payload.Tilemap.TileHeight;
    
                    for (var setCount = payload.Tilemap.Tilesets.Count - 1; setCount >= 0; setCount--)
                    {
                        if (payload.Tilemap.Tilesets[setCount].FirstGid > gid) continue;
    
                        var tileset = payload.Tilemap.Tilesets[setCount].Tileset;
                        var texture = (Texture2D) tileset.Texture;
    
                        var tileRect = tileset.TileRectangles[gid - payload.Tilemap.Tilesets[setCount].FirstGid];

                        spriteBatch.Draw(texture,
                            new Rectangle(tilemapX, (int)tilemapY, tileset.TileWidth, tileset.TileHeight),
                            new Rectangle(tileRect.X, tileRect.Y, tileset.TileWidth, tileset.TileHeight ),
                            Color.White, 0, Vector2.Zero, spriteEffect , (float) layerIndex * 0.01f);
                            
                        break;
                    }
                }
            }
        }
    }
}
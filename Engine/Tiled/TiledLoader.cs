using System.Linq;
using Hel.Tiled;
using Hel.Tiled.Models.Tilemap;
using Microsoft.Xna.Framework.Graphics;

namespace Hel.Engine.Tiled
{
    /// <summary>
    /// Load Tiled data from <see cref="Hel.Tiled"/>
    /// </summary>
    public static class TiledLoader
    {
        /// <summary>
        /// Load the tilemap at the path provided and populate it with data. Also returns all tilesets associated with the tilemap
        /// and loads and assigns the textures for the tilesets.
        /// </summary>
        /// <param name="path">The full path of the tilemap minus 'Content/'</param>
        /// <returns>Fully loaded and structured <see cref="Tilemap"/></returns>
        public static Tilemap TilemapLoader(string path)
        {
            path = !path.StartsWith("Content") ? $@"Content/{path}" : path;
            var tilemap = Loader.LoadTilemap(path);

            foreach (var tileset in tilemap.Tilesets.Select(set => set.Tileset))
            {
                tileset.Texture = Engine.Game.Content.Load<Texture2D>(tileset.Image.Split('.')[0]);
            }

            return tilemap;
        }
    }
}
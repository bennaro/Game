using Raylib_cs;
using TiledCS;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize the Raylib window
            int screenWidth = 800;
            int screenHeight = 600;
            Raylib.InitWindow(screenWidth, screenHeight, "Raylib C# Example");

            // Load player texture and map
            string playerTexturePath = "C:/Users/Faisa/Documents/Game/Assets/link-sheet.png";
            string mapPath = "C:/Users/Faisa/Documents/Game/Assets/world.tmx";
            string assetsPath = "C:/Users/Faisa/Documents/Game/Assets/";

            PlayerComponent playerComponent = new PlayerComponent(playerTexturePath, 32, 32);
            TiledMap map = new TiledMap(mapPath);
            var tilesets = map.GetTiledTilesets(assetsPath); // Ensure trailing slash

            // Load tileset textures
            Dictionary<TiledTileset, Texture2D> tilesetTextures = new Dictionary<TiledTileset, Texture2D>();
            foreach (var tileset in tilesets.Values)
            {
                tilesetTextures[tileset] = Raylib.LoadTexture(assetsPath + tileset.Image.source);
            }

            var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);

            Entity entity = new Entity();
            entity.Attach("player", playerComponent);

            // Set the target FPS (optional)
            Raylib.SetTargetFPS(60);

            // Main game loop
            while (!Raylib.WindowShouldClose())
            {
                // Start drawing
                Raylib.BeginDrawing();

                // Clear the background to white
                Raylib.ClearBackground(Color.White);

                // Render tile layers
                foreach (var layer in tileLayers)
                {
                    for (var y = 0; y < layer.height; y++)
                    {
                        for (var x = 0; x < layer.width; x++)
                        {
                            var index = (y * layer.width) + x; // Assuming the default render order is used which is from right to bottom
                            var gid = layer.data[index]; // The tileset tile index
                            var tileX = (x * map.TileWidth);
                            var tileY = (y * map.TileHeight);

                            // Gid 0 is used to tell there is no tile set
                            if (gid == 0)
                            {
                                continue;
                            }

                            // Helper method to fetch the right TiledMapTileset instance. 
                            // This is a connection object Tiled uses for linking the correct tileset to the gid value using the firstgid property.
                            var mapTileset = map.GetTiledMapTileset(gid);

                            // Retrieve the actual tileset based on the firstgid property of the connection object we retrieved just now
                            var tileset = tilesets[mapTileset.firstgid];

                            // Use the connection object as well as the tileset to figure out the source rectangle.
                            var rect = map.GetSourceRect(mapTileset, tileset, gid);

                            // Render sprite at position tileX, tileY using the rect
                            Raylib.DrawTextureRec(tilesetTextures[tileset], new Rectangle(rect.x, rect.y, rect.width, rect.height), new Vector2(tileX, tileY), Color.White);
                        }
                    }
                }

                // Draw player
                entity.Draw();

                // End drawing
                Raylib.EndDrawing();
            }

            // Unload textures
            foreach (var texture in tilesetTextures.Values)
            {
                Raylib.UnloadTexture(texture);
            }

            // Close the Raylib window
            Raylib.CloseWindow();
        }
    }
    }
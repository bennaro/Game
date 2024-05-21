﻿using Raylib_cs;

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

            PlayerComponent playerComponent = new PlayerComponent("./Assets/link-sheet.png",32,32);

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

                // Add any drawing logic here
                Raylib.DrawText("Hello, Raylib!", 200, 200, 20, Color.Black);



                entity.Draw();


                // End drawing
                Raylib.EndDrawing();
            }

            // Close the Raylib window
            Raylib.CloseWindow();
        }
    }
}
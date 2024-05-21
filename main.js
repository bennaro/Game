const raylib  = require('raylib');



raylib.InitWindow(800,500,"Game");


while(!raylib.WindowShouldClose) {



    raylib.BeginDrawing();
    raylib.ClearBackground("#3333");


    raylib.EndDrawing();

}


raylib.CloseWindow();
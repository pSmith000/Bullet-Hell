using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose;
        public static int CurrentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();

        Scene scene = new Scene();


        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call start for the entire application
            Start();

            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;

            Player player = new Player('@', 10, 10, 100, 3, Color.DARKPURPLE, "Player");

            //Loop until the application is told to close
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Get how much time has passed since the application started
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the difference in time from the last time recorded to the current time
                deltaTime = currentTime - lastTime;

                //Update the application
                Update(deltaTime, player);
                //Draw all items
                Draw();

                //Set the last time recorded to be the current time
                lastTime = currentTime;
            }

            //Call end for the entire application
            End();

        }

        /// <summary>
        /// Calledwhen the application starts
        /// </summary>
        private void Start()
        {
            _stopwatch.Start();
            //Create a window using raylib
            Raylib.InitWindow(800, 450, "Math for Games");
            Raylib.SetTargetFPS(60);

            Player player = new Player('@', 0, 0, 200, 3, Color.DARKPURPLE, "Player");
            Actor actor = new Actor('A', 5, 5, Color.BLACK, "Actor");
            Enemy enemy = new Enemy('E', 500, 200, 25, 200, 180, player, Color.GREEN, "Enemy");
            Enemy enemy1 = new Enemy('E', 500, 200, 50, 200, 180, player, Color.BLUE, "Enemy");
            Enemy enemy2 = new Enemy('E', 500, 200, 75, 200, 180, player, Color.YELLOW, "Enemy");
            Enemy enemy3 = new Enemy('E', 500, 200, 100, 200, 180, player, Color.ORANGE, "Enemy");
            Enemy enemy4 = new Enemy('E', 500, 200, 125, 200, 180, player, Color.RED, "Enemy");
            LivesCounter livesCounter = new LivesCounter(0, 0, "lives counter", Color.BLACK, player);
            

            scene.AddActor(player);
            scene.AddUIElement(livesCounter);
            scene.AddActor(enemy);
            scene.AddActor(enemy1);
            scene.AddActor(enemy2);
            scene.AddActor(enemy3);
            scene.AddActor(enemy4);
            //scene.AddUIElement(text);
            CurrentSceneIndex = AddScene(scene);
            _scenes[CurrentSceneIndex].Start();
        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update(float deltaTime, Player player)
        {
            _scenes[CurrentSceneIndex].Update(deltaTime, _scenes[CurrentSceneIndex]);
            _scenes[CurrentSceneIndex].UpdateUI(deltaTime, _scenes[CurrentSceneIndex]);

            while (Console.KeyAvailable)
                Console.ReadKey(true);

        }

        /// <summary>
        /// Called everytime the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.PINK);

            //Adds all actor icons to buffer
            _scenes[CurrentSceneIndex].Draw();
            _scenes[CurrentSceneIndex].DrawUI();

            Raylib.EndDrawing();
        }

        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End()
        {
            _scenes[CurrentSceneIndex].End();
            Raylib.CloseWindow();
        }

        /// <summary>
        /// Adds a scene to the engine's scene array
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index where the new scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Create a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all values from old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Set the last indec to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //Return the last index
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Gets the next key in the input stream
        /// </summary>
        /// <returns>The key that was pressed</returns>
        public static ConsoleKey GetNextKey()
        {
            //If there is no key being pressed...
            if (!Console.KeyAvailable)
                //...return
                return 0;

            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Ends the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}

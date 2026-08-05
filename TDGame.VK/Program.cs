using Dreambit;
using TDGame.Core;

using var game = new Core(title: "TDGame.VK", width: 1280, height: 720);

Core.Level = LogLevel.Trace;
Core.SetTargetFps(360);
var scene = new MainMenuScene();

Scene.SetNextScene(scene);

game.Run();

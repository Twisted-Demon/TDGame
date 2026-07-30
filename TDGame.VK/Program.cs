using Dreambit;
using TDGame.Core;

using var game = new Core(title: "TDGame.VK", width: 1280, height: 720);

Core.Level = LogLevel.Warn;

var scene = new TDGameScene();

Scene.SetNextScene(scene);

Window.SetVsync(false);
Window.SetFixedTimeStep(false);

game.Run();

using Dreambit;
using TDGame.Core.Scenes;

using var game = new Core(title: "TDGame.VK");

Core.Level = LogLevel.Trace;

var scene = new GameScene();

Scene.SetNextScene(scene);

game.Run();
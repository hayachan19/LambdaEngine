# LambdaEngine
Basic game engine experiment

Experiment as in the fact it uses C# and is mostly based on assumptions how the game engine works.

General flow of the program relies on the Core - sort of a communication layer between the game module and runtime app. Runtime itself also serves as a layer between the logical (game) and technical (render/audio) parts of the system. (TODO: Add a diagram?)

At this stage the project is more of a mishmash made of reflection and OpenGL experiments, with actual game engine elements being rough drafts.

## Compilation note
Visual Studio doesn't use relative paths for compiled assemblies, so you have to build everything and replace broken references. Could use a diagram for that...
OpenGL renderer relies on AssimpNet that was repackaged for .Net Standard, available here https://github.com/mellinoe/assimp-net.

## Things to do that could be considered milestones
* Basic OpenGL 3.3 plugin
* Core functionality that can load maps with dynamic objects
* Working input/camera system
* Assimp implementation (or custom model loader)
* Basic OpenAL plugin
* Everything else

## Things to do after everything seems to be done
* Performance tuning
* Basic functionality without game module loaded (VRML/X3D 6DOF browser sounds like an interesting side project)
* Additional renderers including software one because why not
* C++ rewrite

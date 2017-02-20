# orleans.demo
Orleans demo base on orleans 1.0.9


@kims: Left a brief note atfter trying this project.

## What is it?

Similar as the *Presence* sample contained Orleans on the github repository but more specific on game.

Unlike the *Presense* sample, it includes the following features:

* Surppot room based game play mamagement
* Support quick match
* Used gRPC for the communication protocol

## How to build

Without building Orleans from scratch, it can be quickly done with installation of the following packages via Nuget. Be careful the version of the packages, even with the latest stable version, it couldn't build without error.

<center>Package          | <center>Version
----------------------------|-------------------------
 OrleansRuntime           |  1.0.9
 OrleansProviders         |  1.0.9
 Grpc                           |  0.6.0
 Grpc.Core                   |  0.6.0
 Grpc.Auth                   |  0.6.0
 grpc.native.csharp_ext  |  0.10.0

 > NOTE: `OrleansProviders` can not be automatically installed with `OrleansRuntime`, in that case it should be manually installed.

## Run  

After succesfully building the solution, you will get  four execulatble files.

1. Run `OrleansSilo.exe`
2. Run `ProcessManager.exe`
3. Open cmdline then run `LoadGenerator.exe` with the appropriate arguments.
```
>LoadGenerator.exe 0 8
```
> NOTE: The first argument, 0 is start index for quick match and the second one is number of players.
4. `LoadGenerator.exe` will automatically run `SessionLauncher.exe`

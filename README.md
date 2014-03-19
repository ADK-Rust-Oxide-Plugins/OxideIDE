This project aims to become a useful tool for developing plugins and providing simple emulation capabilities to test some parts of your plugin without taking down your server for each simple change gone wrong.
Load your plugin files in the editor to get syntax highlighting and autocompletion
Execute your plugin and any hooks with user defined data to test how certain conditions are handled by the plugin.
Extend the editor: hooks and the rust api can be fully modified without touching the editor sourcecode itself.
Required dependencies: .NET 4.5 and Microsoft Visual c++ Runtime (x86), install those if the editor crashes upon loading a plugin or doesn't start.

Introduction
This page explains how to setup a development environment to participate in Ox(ide)2's development.
Prerequisites
This Project is developed using Visual Studio 2013 which needs to be installed to begin development
The project is hosted in a mercurial project on bitbucket. Get a mercurial client. Either a commandline one or a visual one e.g. TortoiseHg
This project uses nuget to manage dependencies. You need at least the commandline client, the nuget package manager extension for visual studio would make it easier for you.
Development
Pull the source code from this repository.
If you are using the commandline nuget client get all dependencies for the project.
Open RustOxide.sln in Visual Studio.
Compile / Run
Extend it with cool new features.
Getting changes into the project
Fork the project on bitbucket
Push your own changes into it
Send me (billykater) a pull request.
I'll review and comment on the request as necessary. (Please keep up with changes from the main repository so I don't have to find a way to merge your changes from an older version of Ox(ide)2
Help
Feel free to contact me(billykater) if you need help with something.
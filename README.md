<div align="center">
  <p align="center">
    <img src="data/logos/Unitube-vertical.png">
    <br>
    An Open Source client for YouTube.
  </p>
</div>
<div align="center">
  <p align="center">
    <a href="https://gitlab.com/nucleux-software/unitube/commits/master">
      <img alt="pipeline status" src="https://gitlab.com/nucleux-software/unitube/badges/master/pipeline.svg"/>
    </a>
    <a href="LICENSE">
      <img alt="license" src="https://img.shields.io/github/license/nucleuxsoft/unitube.svg">
    </a>
  </p>
</div>

## About
UniTube is a new client for YouTube built in with .NET technologies to provide a
new YouTube experience on all possible platforms.

Originally, this project was designed only for UWP, but since I'm working on
different platforms, I decided to start over and target all platforms where .NET
can run... And where I can do properly debugging 😅️

## Status
I'm working on the unified core for the project. It will be able to send and
receive HTTP requests from the YouTube Data API, and manage local data from a
local database on SQLite. Why I do not use the one provided by Google? 'Cause I
don't know if it works on UWP correctly. So, to avoid future problems, I prefer
to use something that I'm sure that works.

## Build
For all those who want to build this project, they simply need the following:

- .NET Core 2.2 of higher
- Any properly IDE or code editor to work with .NET Core

And the obvious requirements for any platform that they want to program for. By
example, for UWP you need the last version of Windows 10 and the last version
of the SDK for UWP, for GTK you need GTK installed on your system, and so on.

Clone this project, with any tool that you commonly use, or with this command

```sh
git clone https://gitlab.com/nucleux-software/unitube.git
cd unitube
```

Then you need to use the tools that provide your IDE or code editor to build the
project. Note that actually I only provide tools for Visual Studio Code, that is
the code editor that I use. But if you don't want to use any tool like that, or
if your code editor doesn't include any tool for building .NET Core projects,
you can use this command to build the project:

```sh
dotnet build
```

If you want to run the project, just select the desired project and launch it
with:

```sh
dotnet run --project /path/to/the/project
```

> But right now there's no project to run 😅️ my bad.

## Contribute
I really appreciate any kind of help, from logos (just like the actual one,
thanks to [@davidmind](https://github.com/davidmind)) to code contributions.

If you want to contribute to the code, just document all your changes, and try
to follow the... default code style for C#? But trying to not use expressions
bodies for methods or properties, except if it's redundant, always
use braces, even for `if` blocks with only one line of code, and use a soft 80
character line limit.

## License
This project is licensed under the [GNU General Public License v3](LICENSE).

But if you're too lazy to read (just like me), basically you may copy,
distribute and modify the software as long as you track changes/dates in source
files. Any modifications to or software including (via compiler) GPL-licensed
code must also be available under the GPL along with build & install
instructions.

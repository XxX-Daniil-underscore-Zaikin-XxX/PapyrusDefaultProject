# Default Papyrus project

This is a handy template for a Skyrim mod with Papyrus scripts. To get started, install all the tools and dependencies, fork this repository, and clone it locally. For a more detailed guide, keep reading!

The `HelloWorldMod` (from the wonderful [tutorial](https://www.youtube.com/watch?v=i2yLYdVOaLk) by [SkyrimScripting](https://www.youtube.com/@SkyrimScripting)) included in this project is nothing more than a placeholder. I would recommend deleting the `ESP Patch` folder and `HelloModQuestScript.psc` before you get started on your actual project.

## Tools and Dependencies

Before you can properly make use of this template, you must first configure your computer. I'll assume that some people may not be familiar with coding, so I tailored this guide as such.

If you need help with the instructions in this guide, Google is your best friend. It can get you surprisingly far.

### VSCode

The first thing you'll need is a text editor for your project. VSCode is what I prefer for Papyrus, and many features of this project will only work in VSCode. If you'd like to learn more, check [this](https://code.visualstudio.com/docs) out.

#### Installation

You can download an installer from the [official website](https://code.visualstudio.com/download).

#### Extensions

To make the most of VSCode, I'd recommend adding the following extensions:
 - GitHub Actions
 - GitLens
 - IntelliCode
 - Papyrus

Don't be put off by GitLens's warnings about premium features - if your `git` repository is public, you can use everything for free.

### git

The second thing you need is `git`.

#### Why git?

`git` is a version control system - in other words, it tracks the changes you and others make to a project. It greatly speeds up development by providing a safety net (by letting developers quickly roll back to a working version of their project) and allowing for structured and consistent collaboration (through branches and the Pull Request system). You can read more about it [here](https://www.atlassian.com/git/tutorials/what-is-git) and [here](https://git-scm.com/book/en/v2/Getting-Started-What-is-Git%3F).

#### Installation

Follow the guide [here](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git) to install it.

### Spriggit

Spriggit is a tool for converting Bethesda plugins (e.g. `.esp` files) into human-readable `.yaml`s or `.json`s. Since I have chosen `.yaml` for this project, I'll refer to the Spriggit-generated files as such going forward.

#### Why Spriggit?

In the Spriggit lingo, you *serialize* a plugin when you convert it from `.esp` to `.yaml`. You *deserialize* it when you convert it from `.yaml` to `.esp`.

The primary purpose of serializing a plugin isn't to make it human-readable; instead, what we're after is `git`. Because Bethesda plugins are compressed, `git` cannot properly track their changes. This makes it an absolute pain for multiple people to work on one project, and it becomes exceptionally difficult to keep an account of changes in the mod. When that plugin is serialized, `git` can track its changes and be used properly.

#### Installation

From the [latest releases](https://github.com/Mutagen-Modding/Spriggit/releases), download `SpriggitCLI.zip` and extract it to any folder not in `Program Files` or `Program Files (x86)`. Make sure to remember this location for later.

### Caprica

Caprica is a blazing-fast open-source Papyrus compiler. You can read more about it [here](https://github.com/Orvid/Caprica).

Do note that the Language Extensions are disabled for this project until support for them is added in the VSCode Papyrus extension.

#### Installation

The best place to install Caprica from is [here](https://github.com/Orvid/Caprica/actions/runs/9566782007/artifacts/1613167444). Extract it to any folder not in `Program Files` and remember that location for later.

### Pyro

Pyro is an incremental build tool for Papyrus. In layman's terms, it lets the compiler save time by only compiling the files with recent changes. You can start [here](https://github.com/fireundubh/pyro) to learn more about Pyro.

#### Installation

This will be more involved than the other installations. Now, the Papyrus extension for VSCode already has a bundled Pyro installation, but it's an older version, and it doesn't quite work with Caprica. It must be replaced.

First, download [this](https://github.com/XxX-Daniil-underscore-Zaikin-XxX/pyro/actions/runs/10465455857/artifacts/1830685822) version of Pyro.

Next, open your VSCode extension storage. You'll usually find it under Users\\YourUsername\\.vscode\\extensions (e.g. C:\\Users\\YourUsername\.vscode\extensions\\)

Finally, open `joelday.papyrus-lang-vscode-3.2.0` and replace the contents of the `pyro` folder with the contents of what you just downloaded.

## Setting up your project

Now that you've downloaded the dependencies, you can finally get started with this template.

### Initializing your repository

I would recommend [forking](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/fork-a-repo) the [template repository](https://github.com/XxX-Daniil-underscore-Zaikin-XxX/PapyrusDefaultProject) and [cloning](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository) it into a local folder. You can also use the GitLens extension after forking the template to initialize a new repository, then - under Remotes - add your forked repository.

When all is said and done, you should have a fork of my template repository under your GitHub account and a copy on your local computer. 

Open it in VSCode. Check the Source Control tab to ensure you've initialized the repository (you can also run the command `git branch` on a terminal in the same directory), then go into the Papyrus tab. Ensure it's functional by descending the Skyrim SE/AE dropdowns until you find `HelloModQuestScript`.

### Deleting sample project

Delete the `ESP Patch` folder and `Source\\Scripts\\HelloModQuestScript.psc`. Make sure `Source\\Scripts\\skyrimse.ppj` remains!

### Changing constants

Before you begin, you must tell this project where to find all those tools you downloaded, as well as what it should call itself.

Change the constants in `.vscode\\settings.json` as per the comments. The `NAME`s can be anything, but the `PATH`s must point to where you installed the corresponding tools.

Your next stop is `Source\\Scripts\\skyrimse.ppj`. Change the `Variables` as per the comments. If you use a mod manager other than Mod Organizer 2, set `modspath` to point to that mod manager's modlist instead. If (i.e. when) your scripts pull from other script sources, add them to the `.ppj` in the same format as the comment between `<Import>.</Import>` and `<Import>@gamepath\Data\Scripts\Source</Import>`; keep in mind that scripts lower on the list get overwritten by those higher.

You can find more constants in `.github\\workflows\\build-release.yml`. Under `env`, change `ESP_NAME` to mirror `settings.json` and `PROJECT_NAME` to something similar to `settings.json` but without spaces.

The `PROJECT_NAME` from `settings.json` should also go into `moduleName` in `FOMOD Files\fomod\ModuleConfig.xml` and `Name` in `FOMOD Files\fomod\info.xml`. As for the rest of `info.xml`, you can fill it in with whatever strikes your fancy.

### Editing .gitignore

I suggest editing the .gitignore file to keep as much sensitive information (which unfortunately includes your installation paths) away from prying eyes as possible.

To the existing contents, add `*.ppj` on one line and `settings.json` on another.

And that's it! You're ready to start working on your mod.

## Extra Features

### VSCode Build Tasks

Since this project is intended for VSCode, we may as well use it to its full potential.

Included in this repository is a `.vscode` folder. It contains three files: `launch.json`, `tasks.json`, and `settings.json`. The first sets up the Papyrus debugger - a complex affair I won't get into here. The second one, `tasks.json`, provides a few automated tasks to simplify compiling your project. The third, `settings.json`, is typically used to configure your VSCode Workspace settings, but in this case, we will use it to pass variables to `tasks.json`.

To get `tasks.json` working, we must first fill in `settings.json`. Change the `NAME`s to the ones you'd prefer, and change the `PATH`s to where you previously installed your tools.

Currently, this project includes five tasks. You can run any of them through Quick Open (Ctrl+P), typing in 'task' (or clicking on Run Task), and choosing from the drop-down. Let's go over each one.

#### pyro: Compile Project

The task itself is quite simple. It passes the given values through to Pyro, which then uses Caprica as it sees fit. 

#### Serialize project

This command converts the `.esp` into `.yaml` files under the `ESP Patch` folder. It also runs the `Lint ESP` command, which sorts some inconsistently serialised values.

You should run this command before you commit your code.

#### Deserialize

This converts the `.yaml` files into an `.esp`.

You should run this command before you make any changes to the `.esp` and before you launch the game with the mod enabled. 

As tempting as it may be, I recommend against editing the `.yaml` files directly. It's far more consistent and usually faster to use `xEdit` to directly change the `.esp`.

#### Lint ESP

All this does is sort Script Properties in the `.esp`. Without this, making changes in there results in the Script Properties being shuffled around, and this makes it harder for `git` to find what changed.

This is run automatically when `Serialize project` is invoked.

#### Build All

This runs `pyro` and `Serialize project`.

### GitHub Actions

A nifty little script can be found in `.github\\workflows\\build-release.yml`. Its purpose is to automate building and publishing releases of this mod - every single time you commit a change, it outputs a completed, publishable package.

Every night it also publishes such a package to your repository's [releases](https://github.com/XxX-Daniil-underscore-Zaikin-XxX/PapyrusDefaultProject/releases). If you have some early-stage features you'd like people to test, it's super easy to share these nightly releases.

You can read more about them [here](https://docs.github.com/en/actions)

### VSCode Papyrus Extension

This is a wonderful tool that drags Papyrus kicking and screaming into the age of modern IDEs. It's got autocomplete, error highlighting, and much more. You can even Ctrl-Click on symbols to view their definitions.

It does come with a caveat: Papyrus, as you may know, requires all parent scripts to be present for compilation. If you use Creation Kit, this is a non-issue since your Mod Organizer will load all scripts from all your mods at once. In VSCode, however, you must tell the Papyrus extension exactly where to find every last script. On your local computer, this is as simple as adding lines to `Source\\Scripts\\skyrimse.ppj` to point to the folders which contain scripts you want to inherit from.

But for GitHub Actions, it's not that simple. Here's the rough procedure:

1. Find where you can download the parent scripts without an account. A `git` repository works best. *Note that these parent scripts do not have to be functional for your code to compile - they just need to have definitions for everything that's used in your code. You can check the included build script for an example.*
2. For the scripts you can't find, use the [Papyrus Headliner](https://github.com/IHateMyKite/PapyrusSourceHeadliner) to remove their code, then put them into a separate repository. An example of such a repository can be found [here](https://github.com/IHateMyKite/PAPYRUS).
3. Copy the `Checkout Papyrus Sources` build step for every new download in steps 1 and 2. `build-release.yml` should include a rough template to follow.
4. Add all new downloads to the `Cache Script Sources` step.
5. Add all the required Papyrus-containing directories in each of the downloads to the `--import` option in the `Run Caprica` step.

This may seem complicated, but I implore you to put in the time and effort. A project with functional CI (e.g. GitHub Actions) is magnitudes easier to develop for than one without.
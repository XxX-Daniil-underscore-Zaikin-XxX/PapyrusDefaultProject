# Default Papyrus project

placeholder for now

## VSCode Build Tasks

Since this project is intented for VSCode, we may as well use it to its full potential.

Included in this repository is a `.vscode` folder. It contains three files: `launch.json`, `tasks.json`, and `settings.json`. The first sets up the Papyrus debugger - a complex affair I won't get into here. The second one, `tasks.json`, provides a few automated tasks to make compiling your project much easier. The third, `settings.json`, is normally used to configure your VSCode Workspace settings, but in this case we will use it to pass variables to `tasks.json`.

To get `tasks.json` working, we must first fill in `settings.json`. Change the `NAME`s to the ones you'd prefer, and change the `PATH`s to where you previously installed your tools.

Currently, this project includes five tasks. You can run any of them through Quick Open (Ctrl+P), typing in 'task' (or clicking on Run Task), and choosing from the drop-down. Let's go over each one.

### Pyro

Pyro is an incremental build tool for Papyrus. In layman's terms, it lets the compiler save time by only compiling the files with recent changes. If you would like to learn more about Pyro, you can start [here](https://github.com/fireundubh/pyro).

The task itself is quite simple. It passes the given values through to Pyro and lets it do its thing. 

### Spriggit

Spriggit is a tool for converting Bethesda plugins (e.g. `.esp` files) into human-readable `.yaml`s or `.json`s. Since I have chosen `.yaml` for this project, I'll refer to the Spriggit-generated files as such going forward.

In the Spriggit lingo, you *serialize* a plugin when you convert it from `.esp` to `.yaml`. You *deserialize* it when you convert it from `.yaml` to `.esp`.

The primary purpose of serializing a plugin isn't to make it human-readable; instead, what we're after is `git`. `git` is a version control system - in other words, it tracks the changes you and other people make to a project. It greatly speeds up development by providing a safety net (by letting developers quickly roll back to a working version of their project) and allowing for structured and consistent collaboration (through branches and the Pull Request system). You can read more about it [here](https://www.atlassian.com/git/tutorials/what-is-git) and [here](https://git-scm.com/book/en/v2/Getting-Started-What-is-Git%3F).

Because Bethesda plugins are compressed, `git` cannot properly track changes in them. This makes it an absolute pain for multiple people to work on one project, and it becomes exceptionally difficult to keep an account of changes in the mod. When that plugin is serialized, `git` is then able to track its changes and be used properly.

#### Serialize project

This command converts the `.esp` into `.yaml` files under the `ESP Patch` folder. It also runs the `Lint ESP` command, which sorts some values that are inconsistently serialized.

You should run this command before you commit your code.

#### Deserialize

This converts the `.yaml` files into an `.esp`.

You should run this command before you make any changes to the `.esp` and before you launch the game with the mod enabled. 

As tempting as it may be, I would recommend against editing the `.yaml` files directly. It's far more consistent, and usually faster, to use `xEdit` to change the `.esp` directly.

#### Lint ESP

All this does is sort Script Properties in the `.esp`. Without this, making changes in there results in the Script Properties being shuffled around, and this makes it harder for `git` to find what actually changed.

This is run automatically when `Serialize project` is invoked.

### Build All

This just runs `pyro` and `Serialize project`.
# Leaky's Blueprinter

This is a Windows desktop app for Space Engineers, made for the purpose of analyzing local blueprint files and executing various operations on them, for example:
- Repairing block damages and completing blocks
- Deleting deformations
- Changing block colors
- Analyzing flight and cargo capabilities
- Analyzing the total resources needed for building the blueprint
- Setting up gravity generators and projectors automatically according to grid dimensions
- Setting up wheel suspensions according to grid dimensions and mass
- And many more

It's intended to be useful for preparing blueprints before uploading them to the Steam Workshop.

# Project status

Early development, no release build, and master is not 'officially' stable yet. Initial commit have been just pushed out, after the volatility of the foundational aspects of the design seemed to have finally settled. :) So at the moment this won't be so interesting for common users.

So far I've been focusing on creating a robust, loosely coupled, extensible architecture that will allow me to come back to it at any arbitrary time and easily add new features, and/or update the existing ones to reflect the changes of the game.

## Main features already implemented:
- **Ability to load/deserialize all relevant definition files of the game** – This provides the app with *always-up-to-date information* about blocks, block recipes, components, component recipes, ingots, ingot recipes and ores.
- **Ability to load/deserialize blueprint files** – The basic architecture for opening and handling these files is already in place.
- **Ability to display multiple blueprints as individual workspaces** – The app supports opening multiple blueprints through the concept of 'workspaces', similarly to how we open multiple tabs in a web browser.

## Architectural aspects already in place:
- **Robust command/query implementation that encapsulates operations into easy-to-manage units** – This serves as the foundation of all the operations the app will support, and will facilitate the implementation of an undo/redo stack.
- **Fully self-contained and presentation-independent app core** – Core functionalities don't rely on the current GUI implementation; in fact I'm planning to add a console interface too.
- **Standard Model-View-ViewModel architecture** – With property change notifications, commanding, no codebehind, ViewModels fully decoupled from Views, and communication among ViewModels handled by a message bus.
- **Unit Testing with NUnit and Moq** – Most core features will be unit tested to detect regressions and ensure the integrity of the written blueprint files. 
- **Fluent nested test data builder to facilitate the unit testing of data-dependent classes** – One of the emerging foundational elements of unit testing in this project; allows the test data to be rapidly assembled with simple method call chains instead of having to rely on dozens of variations of static template data.

# Requirements for running the app

- As essentially all features of this app are dependent on external XML definition files found in the folder of the relevant game, it requires the game to be present, and upon first launch it asks for the location of the required resources (while also providing auto-detection). Later I'll consider including a static version of these files, if the developer consents.

# Roadmap
In the upcoming period I'll start to focus on pushing out a first release version with a basic set of features that can be actually useful for the community.

- **Blueprint saving with backup copies:** Well, this hardly needs any explanation, and it won't take long to implement. :)
- **Designing a GUI presentation that can expose complex, variable groups of information and commands:** This is the main dilemma at the moment. I want a system that can work pretty much automatically and won't require a lot of manual UI design. Especially because blueprints are highly complex and variable, so it needs to support blueprints with all sorts of contents (ship, base, wheeled vehicle; single grid, multi grid; cargo contents, etc.).
- **Additional blueprint queries:** Flight statistics of flight-capable grids, overall resources required for welding...
- **Integration testing:** Some basic testing of the subsystems of the app to ensure that they output what they're supposed to. It's crucial to avoid writing out blueprints with invalid structure.
- **First release build**
- **Undo/redo functionality for commands:** I'm planning to implement this after the initial release, because most likely people won't do deep command queues anyway, so I don't expect this to be a crucial feature here.
- **Adding additional features that query and modify blueprints:** Lots of possibilities here; I'll try to gather feedbacks from the community.
- **Creating custom groups of commands for quick blueprint-preparation:** To let users create their own custom command sets that they can execute on blueprints with a single click.
- **'Codex' screen for manual lookups:** In other words, looking up block, component, etc. information in the definitions database, for example if you want to see how much certain things weight or cost. Current plans include being able to place and pin the looked up items on a canvas, and saving/loading the created layouts.
- **Longer term goal: Advanced blueprint editing, cutting/pasting parts of blueprints:** I'd like to implement some more comprehensive editing features, along with some ways to display the actual blueprint content inside the app (without loading 3D models).
- **Also considered as a longer term goal: Programmable Block execution simulation on grids:** Exactly what it sounds like. Would be useful to be able to run the scripts inside programmable blocks, and see how does that affect the various blocks on the grid, including the contents of displays. Again, the primary difficulty here is coming up with a graphical presentation and integrating that into the existing interface, especially since I'm not a UI/UX designer.

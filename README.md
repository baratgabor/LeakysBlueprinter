# Leaky's Blueprinter

This is Windows desktop app (.Net/C#/WPF) for Space Engineers, made for the purpose of analyzing local blueprint files and executing various actions on them, for example:
- Correcting damages
- Deleting deformations
- Changing block colors
- Analyzing flight and cargo capabilities
- Analyzing total resources needed for building the bluprint
- Setting up gravity generators and projectors automatically according to grid dimensions
- And many more

Intended to be useful before uploading blueprints to the Steam Workshop.

# Project status:

Early development, no release build, master is not stable yet. Initial commit have been just pushed out, after the volatility of the foundational aspects of the design seemed to have finally settled. :) So at the moment this won't be so interesting for common users.

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
- **Fluent nested test data builder to facilitate the unit testing of data-dependent classes** – One of the emerging foundational elements of unit testing in this project; allows the rapid assembly of test data, instead of having to rely on dozens of variations of static template data.

# Requirements for running the app

- As essentially all features of this app are dependent on external XML definition files found in the folder of the relevant game, it requires the game to be present, and upon first launch it asks for the location of the required resources (while also providing auto-detection). Later I'll consider including a static version of these files, if the developer consents.


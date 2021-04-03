# TR2 Randomizer Tracker
TR2RandoTracker keeps track of the levels that Lara has completed while playing the [TR2 Randomizer](https://github.com/DanzaG/TR2-Rando). The tracker will also show icons if a level is unarmed or ammoless. There are no spoilers - the list updates in real-time during game play, so you will not find out which level comes next, or its armed/unarmed/ammoless state until that level starts. See the screenshots at the end of this page for some examples.

# Thanks
* FluxMonkii, Supergamer57, MidgeOnTwitch - for the relevant memory addresses for each TR2 exe, and the way to detect which exe is running. This is based on the [TR II Autosplitter for Livesplit](https://raw.githubusercontent.com/FluxMonkii/Autosplitters/master/TombRaiderII.asl). 

## Installation

This application uses a MIT license as described in the LICENSE file. Follow the steps below to download and use the application.

_Prerequisites_
* Windows 7 SP1, Windows 8.1, Windows 10
* .NET Framework 4.7.2

_Install Steps_
* Download the latest release from https://github.com/lahm86/TR2RandoTracker/releases/latest
* Extract the zip file to any location on your PC.
* Run TR2RandoTracker.exe

## Usage and Options

The main window can be dragged to your preferred location on-screen. Use the resize grip to make changes to the window size. Right-clicking anywhere on the main window will open a context menu as shown below.

![Context Menu](https://github.com/lahm86/TR2RandoTracker/blob/main/Resources/ContextMenu.png)

* Colour Scheme - change the background colour (including transparency), foreground colour and level list separator colour.
* Always On Top - if set, the main window will remain on top of all others.
* Show Resize Grip - untick this option to remove the resize grip from the bottom right corner of the main window.
* Reset Level List - manually clears the list of displayed levels.
* Check for Updates - checks GitHub for the latest release of TR2RandoTracker.
* About - shows information about TR2RandoTracker.
* Exit - close the TR2RandoTracker application.

# Playing the Randomizer
You can leave the main tracker window open while you stop and start TR2 - it will detect the executable starting and stopping. You can also randomize freely without closing and reopening the tracker.

To exit the tracker, right-click anywhere in the main window and choose Exit.

## Screenshots
The main title screen - who knows what's in store?

![Title Screen](https://github.com/lahm86/TR2RandoTracker/blob/main/Resources/TitleScreen.png)


Level 4 - no unarmed or ammoless levels yet

![Basic Level List](https://github.com/lahm86/TR2RandoTracker/blob/main/Resources/LevelList.png)


Level 5 was unarmed, level 6 was ammoless and level 12 is now also unarmed.

![Unarmed and Ammoless](https://github.com/lahm86/TR2RandoTracker/blob/main/Resources/UnarmedAmmoless.png)

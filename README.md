# TR Randomizer Tracker
TRRandoTracker keeps track of the levels that Lara has completed while playing the [TR Randomizer](https://github.com/LostArtefacts/TR-Rando). The tracker will also show icons if a level is unarmed or ammoless. There are no spoilers - the list updates in real-time during game play, so you will not find out which level comes next, or its armed/unarmed/ammoless state until that level starts. See the screenshots at the end of this page for some examples. The tracker currently supports playing [TR1X](https://github.com/LostArtefacts/TR1X), classic TR2 (any version), plus TR1R and TR2R.

# Thanks
* FluxMonkii, Supergamer57, MidgeOnTwitch - for the relevant memory addresses for each TR2 exe, and the way to detect which exe is running. This is based on the [TR II Autosplitter for Livesplit](https://raw.githubusercontent.com/FluxMonkii/Autosplitters/master/TombRaiderII.asl). 

## Installation

This application uses a MIT license as described in the LICENSE file. Follow the steps below to download and use the application.

_Prerequisites_
* Windows 7 SP1, Windows 8.1, Windows 10
* .NET Core 6.0

_Install Steps_
* Download the latest release from https://github.com/LostArtefacts/TRRandoTracker/releases/latest
* Extract the zip file to any location on your PC.
* Run TRRandoTracker.exe

## Usage and Options

The main window can be dragged to your preferred location on-screen. Use the resize grip to make changes to the window size. Right-clicking anywhere on the main window will open a context menu as shown below.

![Context Menu](https://github.com/LostArtefacts/TRRandoTracker/blob/main/Resources/ContextMenu094.png)

* Settings - open the main settings window to configure the following options.
  * Main window background colour.
  * Main window background image.
  * Change whether or not the main window supports transparency (for OBS/StreamLabs, this should be switched off).
  * Always On Top - if set, the main window will remain on top of all others.
  * Show Resize Grip - untick this option to remove the resize grip from the bottom right corner of the main window.
  * Choose to show/hide the main window title.
  * Set the content, colour, font size and style of the window title.
  * Set the colour, font size, index column size, font style and placeholder text for the level list.
  * Choose to show/hide the timer.
  * Set the colour, font size and style of the timer.
* Reset Level List - manually clears the list of displayed levels.
* Reset Timer - resets the timer to 00:00:00.
* Always On Top - see Settings.
* Show Resize Grip - see Settings.
* Check for Updates - checks GitHub for the latest release of TRRandoTracker.
* About - shows information about TRRandoTracker.
* Exit - close the TRRandoTracker application.

# Playing the Randomizer
You can leave the main tracker window open while you stop and start Tomb Raider - it will detect the executable starting and stopping. You can also randomize freely without closing and reopening the tracker.

To exit the tracker, right-click anywhere in the main window and choose Exit.

## Screenshots
The main title screen - who knows what's in store?

![Title Screen](https://github.com/LostArtefacts/TRRandoTracker/blob/main/Resources/TitleScreen094.png)


Level 4 - no unarmed or ammoless levels yet

![Basic Level List](https://github.com/LostArtefacts/TRRandoTracker/blob/main/Resources/LevelList094.png)


Level 5 was ammoless, level 6 was unarmed and level 11 is now also unarmed.

![Unarmed and Ammoless](https://github.com/LostArtefacts/TRRandoTracker/blob/main/Resources/UnarmedAmmoless094.png)

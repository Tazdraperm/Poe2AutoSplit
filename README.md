# Poe2AutoSplit
This is a LiveSplit component that enables auto splitting for Path of Exile 2. Inspired by [this code](https://github.com/brandondong/POE-LiveSplit-Component/).

## Setting up
1. Download the latest [release](https://github.com/Tazdraperm/Poe2AutoSplit/releases/).
2. Copy "**LiveSplit.Poe2AutoSplit.dll**" file into the "**LiveSplit/Components**" folder.
3. In LiveSplit, right-click and select "**Edit Layout**" then click on the "**+**" button and select "**Other -> Path of Exile 2 Auto Splitter**.
4. Double click on the newly added component to open its settings.
5. Ensure that the location of the **Client.txt** file is correct. For the Steam version of Path of Exile 2 the log file is most likely located at **C:\Program Files (x86)\Steam\steamapps\common\Path of Exile 2\logs\Client.txt**.
6. Change the config file location to point at **Poe2AutoSplitConfig.txt**.

## Custom Config file
It is possible to edit the "**Poe2AutoSplitConfig.txt**" file or create a new one from scratch. The file has a very simple format:
* Each line represents an area name or a boss name
* Entering that area or killing that boss will result in an automatic split

For the correct area\boss names please check the "**Poe2AutoSplitConfig.txt**" file.<br/>
When you are done editing the config file, click the "**Reload Config**" button to apply your changes. You can also click the "**Generate Splits**" button to automatically generate splits from the config file content.

## Splitting on boss kills
It is possible to add auto split upon killing specific bosses. Currently supported bosses are:
* Lachlann
* King in the Mists
* Count Geonor
* Jamanra
* Doryani

Please note that split occurs upon encountering specific voice line related to the boss death. Usuaully it happens right away, but in case of Count Geonor there's ~5 seconds delay between boss death and voice line.

## Limitations
Cruel difficulty is not supported.

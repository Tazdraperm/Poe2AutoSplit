# Poe2AutoSplit
This is a LiveSplit component that enables auto splitting for Path of Exile 2. Inspired by [this code](https://github.com/brandondong/POE-LiveSplit-Component/).

## Setting up
1. Download the latest [release](https://github.com/Tazdraperm/Poe2AutoSplit/releases/).
2. Copy the "**LiveSplit.Poe2AutoSplit.dll**" file into the "**LiveSplit/Components**" folder.
3. In LiveSplit, right-click and select "**Edit Layout**", then click on the "**+**" button and select "**Other -> Path of Exile 2 Auto Splitter**.
4. Double-click on the newly added component to open its settings.
5. Ensure that the location of the **Client.txt** file is correct. For the Steam version of Path of Exile 2 the log file is most likely located at **C:\Program Files (x86)\Steam\steamapps\common\Path of Exile 2\logs\Client.txt**.
6. Select areas and bosses you want to split on.
7. (Optional) Click "**Generate Splits**" to automatically create splits based on your selection.<br>
Generated splits can then be renamed/reorded using LiveSplit's built-in split editor.



## Limitations
Boss kill splits occur upon encountering a specific voice line related to the boss death. This usually happens immediately, but in the case of Count Geonor there's ~5 seconds delay between the boss death and the voice line.<br>

Cruel difficulty is not supported.

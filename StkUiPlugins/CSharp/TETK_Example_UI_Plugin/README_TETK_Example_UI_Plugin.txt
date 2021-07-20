PLUGIN OVERVIEW:

This sample plugin is to demonstrate how custom TETK worfklows can be created through custom UIs using TETK Connect commands. This particular example consists of an F35 ownship
with a radar system under test. The forward-facing radar of the F35 captures tracking data for ten other aircraft flying in the vicinity. TSPI data is loaded for the F35 ownship
as well as the other ten aircraft. The tracking data for the radar is also loaded and a track is created in the TETK Tracks workflow. A filtered track based on Track ID can also
be created. A desired track can then be promoted and a Track Comparison can be run to compare how well the radar tracked a particular aircraft relative to its TSPI. Several quick
buttons exist to create comparison graphs and a data display.


TO INSTALL:

Simply run the "STK12.UIPlugin.Copier.TetkExample.exe" in the Exe_Installer folder. You can place the plugin in any of the following locations:
1) "C:\Program Files\AGI\STK 12\Plugins\"  <-- STK looks here first, but needs admin privs
2) "C:\ProgramData\AGI\STK 12\Plugins"  <-- no admin privs, but gets wiped out w/ version changes (DEFAULT)
3) <STKuser>\Config\Plugins\  <-- no admin privs, but only visible to that user, and Reset Curr User makes it disappear


BEFORE RUNNING:

You will need to copy the entire "TETK_Notional_Test_Datasets" folder inside the Data_Files folder and paste it to your C drive here:
C:\AGI
* Note - You may need to create the AGI folder. This folder path is necessary since the plugin uses hardcoded file paths for certain buttons.


RUNNING THE PLUGIN:

- Right-click the toolbar area at the top of STK and make sure "TETK Example Plugin" is enabled.
- The plugin toolbar has two white target buttons, a "Workflow 1" button, and a "Test Connect" button.
- The actions within this buttons need to be run in order from left to right.

1st Target Icon Button- Load TETK Data
- Imports needed data mappings and loads ownship, associated state, and additional track data.

2nd Target Icon Button - Create TETK Tracks
- Button to add raw tracking data for all 10 associated aircraft.
- Can create individual, filtered tracks based on track IDs for each aircraft. Note that an aircraft may have several track IDs (sensor saw it for some time, lost it, regained it).
- Can promote a track to a heavy aircraft object (for use in Workflow 1 button).

Workflow 1 Button - Compute Track Comparison
- Create and compute a track comparison given a track, truth obj, and measured obj (promoted track from prev button).
- Several quick buttons to create track comparison products.

Test Connect Command Button
- Enter any Connect command as desired and click the "Run Connect Command".
- The Export button captures ALL Connect commands run in the plugin and exports them to a .txt file in the scenario folder.



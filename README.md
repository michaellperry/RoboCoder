# RoboCoder

Record and play back code editing. Great for demos, conference talks, and online courses!

## Motivation

I created RoboCoder in preparation for an extremely code-heavy talk. I wanted to do the entire talk from within Visual Studio so that I could show the impact of code changes. You can't compile PowerPoint.

I also record Pluralsight courses. Editing the coding demonstrations is extremely time-consuming. But without editing, the typing mistakes are very distracting.

RoboCoder records your keystrokes. You can use it with any editor. Then it will play back your keystrokes so you can give a live coding demo, or record a video. 

## Instructions

Pull and build the WPF app. When you run it, you will see a window with a *File* and *Record* button. Click *File* to create a script file. Files are saved automatically while you work because -- you know -- bugs.

Press Ctrl+Shift+F7 to start recording. This is a global hot key, so you can do it from Visual Studio or Sublime. Just don't use the mouse. Your keystrokes will appear in RoboCoder while you type. Good keylogger!

Press Ctrl+Shift+F8 to stop recording. Now you are free to use your keyboard without messing up your script.

You can Alt+Tab to RoboCoder to fix any mistakes that you just made. You can also insert blank lines to tell RoboCoder where to pause. When you are done, just return to your editor and start recording again.

Press Ctrl+Shift+F9 to play back your keystrokes. RoboCoder will play them up to the first blank line. Press Ctrl+Shift+F9 again to play the next section. If you edit the script, RoboCoder will start over.

## Tips

Try recording Ctrl+Home as the first keystroke of the script. Then use the arrow keys to move to the correct starting point. That will ensure that no matter where you place the cursor, the script will put the code in the right place.

While recording, pause, Alt+Tab to RoboCoder, append a couple of carriage returns, and then go back to the editor. This will record a pause.

If you want to edit some code off-screen, first use the arrow keys or code navigation such as Ctrl+{ to get to the right place. Then use Ctrl+Arrow to scroll the code fully into view. Finally, use Shift+Down to select the code you want to talk about and insert a pause. This will keep the audience from getting confused by large context switches.

If you make a small mistake, don't rerecord. Just pause the recording, fix your script, and fix the code in the editor. Remember where your cursor was! You might even want to drop a waypoint in CodeRush to be sure you get back to it.

Test your scripts! Make sure that they do exactly what you want them to before you start your demo or screen recording.

Create a Git branch at the starting point of your demo. You will need to reset to it over and over again while you record and test.

Check your scripts into Git. They're just text! But keep them in a separate repository than the code so that you can reset one without loosing the other.

## Roadmap

If you hit Backspace and the last keystroke was a character, then just delete that character from the script; don't insert a Backspace.

Give some indication of where you are in the script during playback.

Slow the characters down a bit during playback. Some editors will cache a bunch of keystrokes and make those edits non-visually, ruining the RoboCoding experience.
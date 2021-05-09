This is my datasacks module for easy UI event processing in Unity3D.

In this Unity project, see the DatasackUsageExamples.txt file for examples.

You can check out my games here:

iPhone:  https://itunes.apple.com/us/developer/kurt-dekker/id680019078

Android: https://play.google.com/store/apps/developer?id=Kurt+Dekker

Also: https://kurtdekker.itch.io/

For updating your project, see UpdateNotes.txt file.

--------------------------------- DATASACKS --------------------------

Fire up the example game scene for a silly clicker game that has some UI
elements that use datasacks for input and output.

Here is a sorta graphical overview of how things work:

![Datasacks Overview 2](https://raw.githubusercontent.com/kurtdekker/datasacks/master/20180724_datasacks_overview.png)

More examples coming soon. Here are some notes:

What are Datasacks?

Datasacks are Unity3D ScriptableObjects that contain data.

Internally the data is stored as a string but you can access it in
code as a string, an int, a float, a bool, and other ways.

The Datasack ScriptableObjects have a custom Unity3D inspector.
Via a button on that inspector you can "CODEGEN," which will produce
header code in (DSMCodegen.cs) containing all the names of the
Datasacks you have created in your project. This is purely for
code convenience and type-checking because you can always get
at a Datasack by its name (DSM.I.Get( string)), and of course
they are always available as draggable objects into Monobehaviors.

INPUT:

The general flow pattern is that you put the DSUserIntentButton.cs
script on any UI button, and when pressed that will put the name
of that button's GameObject into the DSM.UserIntent variable, unless
you specify another Datasack.

Over in code you have a switch statement that operates on the
name of the button received, and does different things. See below.

Datasacks can be subscribed to for changes, either on a permanent
listener basis or on a one-shot basis.

OUTPUT:

For data presentation, you can put the DSTextGetString.cs onto
your UnityEngine.UI.Text components and then the contents of
any particular Datasack you want will be live-updated to that
Text field.

See the little clicker game supplied for some sample uses. There
are other ways. The main point of this is to expose as much
functionality in the Unity editor/inspector as possible, and to
let non-engineers make meaningful changes to the game without
having to involve an engineer.

See notes.txt for what (might be) coming in the future.

Other things you can do with a Datasack can be seen looking at
its inspector: change values at runtime, display values, etc.

You can also check the Save boolean to cause it to persist to
the PlayerPrefs system between launches of the application.

Obviously you can use JSON and store whatever objects you want
into the string portion of a Datasack.

	// Here is an example data subscription pattern for your Monobehavior:
	// It falls back to DSM.UserIntent because that is the default sack.

	public Datasack dataSack;	// populate this in the Unity editor

	void	OnUserIntent( Datasack ds)
	{
		// Here is where you service the notification
		// that the contents of this Datasack changed
		// You can inspect the name of the Datasack if
		// you expect more than one Datasack to call
		// this function.
		switch( ds.Value)
		{
		case "ButtonAchievements":
			// start achievements scene
			break;
		case "ButtonPlay":
			// start playing the game
			break;
		}
	}

	void	OnEnable()
	{
		if (!dataSack) dataSack = DSM.UserIntent;
		dataSack.OnChanged += OnUserIntent;
	}
	void	OnDisable()
	{
		dataSack.OnChanged -= OnUserIntent;
	}

TextMeshPRO support added!

If you are using TextMeshPRO you may enable Datasacks to interoperate
with the TextMeshPRO objects. To do so you can either make a Scripting
define called USING_TEXTMESHPRO, or else add it into the DSTextAbstraction.cs
file directly.

Obviously the former approach is better.

Script pre-defines can be added in the Unity Player Settings. Be sure to
press ENTER after you add the text string or Unity will not save it.

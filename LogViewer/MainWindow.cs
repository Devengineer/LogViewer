using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	protected void OnOpen (object sender, EventArgs e)
	{
		// Reset the LogTreeView and change the window back to original size
		int width, height;
		this.GetDefaultSize (out width, out height);
		this.Resize (width, height);

		logTextView.Buffer.Text = "";

		//Create and display a fileChooserDialog
		FileChooserDialog chooser = new FileChooserDialog (
			"Please select a logfile to view ...",
			this,
			FileChooserAction.Open,
			"Cansel", ResponseType.Cancel,
			"Open", ResponseType.Accept);

		if (chooser.Run() == (int) ResponseType.Accept)
		{
			// Open the file for reading
			System.IO.StreamReader file = System.IO.File.OpenText (chooser.Filename);

			// Copy the contents into the LogTextView
			logTextView.Buffer.Text = file.ReadToEnd ();

			// Set the MainWindow Title to the filename
			this.Title = "Log Viewer -- " + chooser.Filename.ToString ();

			// Make the MainWindow bigger to accomodate the text in the LogTextView
			this.Resize (640, 480);

			//Close the file so as to not leave a mess
			file.Close();
		}
		chooser.Destroy ();
	}

	protected void OnClose (object sender, EventArgs e)
	{
		// Reset the LogTreeView and change the window back to original size
		int width, height;
		this.GetDefaultSize (out width, out height);
		this.Resize (width, height);

		logTextView.Buffer.Text = "";

		// Change the MainWindow Title back to the default
		this.Title = "Log Viewer";
	}

	protected void OnExit (object sender, EventArgs e)
	{
		Application.Quit ();
	}

	protected void OnAbout (object sender, EventArgs e)
	{
		// Create a new About dialog
		AboutDialog about = new AboutDialog ();

		// Change the Dialog's properties to the appropriate values
		about.ProgramName = "Log Viewer";
		about.Version = "1.0.0";

		// Show the Dialog and pass it control
		about.Run ();

		// Destroy the dialog
		about.Destroy ();
	}
}

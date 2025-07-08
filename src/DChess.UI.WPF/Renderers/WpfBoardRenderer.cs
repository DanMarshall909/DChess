using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using DChess.Core.Game;
using DChess.Core.Renderers;
using DChess.UI.WPF.Controls;

namespace DChess.UI.WPF.Renderers;

/// <summary>
///     A renderer that displays a chess board in a WPF window.
/// </summary>
public class WpfBoardRenderer : IBoardRenderer
{
    private readonly string _title;
    private readonly bool _waitForClose;
    private ChessBoardControl? _boardControl;
    private Window? _window;

    /// <summary>
    ///     Initializes a new instance of the <see cref="WpfBoardRenderer" /> class.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    public WpfBoardRenderer(string title = "Chess Board Visualization", bool waitForClose = false)
    {
        _title = title;
        _waitForClose = waitForClose;
    }

    /// <summary>
    ///     Renders the specified board in a WPF window.
    /// </summary>
    /// <param name="board">The board to render.</param>
    public void Render(Board board)
    {
        // Ensure we're on the UI thread
        if (Application.Current != null && !Application.Current.Dispatcher.CheckAccess())
        {
            Application.Current.Dispatcher.Invoke(() => Render(board));
            return;
        }

        // Create the application if it doesn't exist
        if (Application.Current == null)
        {
            var thread = new Thread(() =>
            {
                try
                {
                    // Create the application
                    var application = new Application();
                    application.Startup += (s, e) => CreateAndShowWindow(board);
                    application.Run();
                }
                catch (InvalidOperationException ex) when (ex.Message.Contains(
                                                               "Cannot create more than one System.Windows.Application"))
                {
                    // If we can't create a new Application, try to use the existing one
                    if (Application.Current != null)
                        Application.Current.Dispatcher.Invoke(() => CreateAndShowWindow(board));
                    else
                        Console.WriteLine("Failed to create or access a WPF Application instance: " + ex.Message);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            if (_waitForClose) thread.Join();
        }
        else
        {
            // Application exists, create or update the window
            Application.Current.Dispatcher.Invoke(() => CreateAndShowWindow(board));
        }
    }

    private void CreateAndShowWindow(Board board)
    {
        if (_window == null)
        {
            // Create the window and board control
            _window = new Window
            {
                Title = _title,
                Width = 500,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.CanResize,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            _boardControl = new ChessBoardControl();
            _window.Content = _boardControl;

            if (_waitForClose)
                _window.ShowDialog();
            else
                _window.Show();
        }

        // Update the board
        _boardControl?.UpdateBoard(board);
    }
}

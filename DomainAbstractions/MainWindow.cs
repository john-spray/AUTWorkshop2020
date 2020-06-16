﻿using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Libraries;
using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// <summary>
    /// This is the main window of the application. The output IUI in it takes the responsibility of getting the WPF UIElements 
    /// in a hierarchical calling order as it goes deeper in the abstractions wired to it. The output IEvent starts to execute once 
    /// the app starts running, which informs the abstraction who implements it to do the things it wants.
    /// ------------------------------------------------------------------------------------------------------------------
    /// Ports:
    /// 1. IEvent shutdown: input for close the window (exits the application)
    /// 2. IDataFlow<bool> visible: to enable(true) or disable(false, grey out) the UI
    /// 3. IUI iuiStructure: all the IUI contained within the MainWindow
    /// 4. IEvent appStart: IEvent that is pushed out once window has been loaded
    /// <summary>

    public class MainWindow : IEvent, IDataFlow<bool>
    {
        // Properties -----------------------------------------------------------------
        public string InstanceName = "Default";

        // Private fields -----------------------------------------------------------------
        private Window window;

        // Ports -----------------------------------------------------------------
        private IUI iuiStructure;
        private IEvent appStart;

        /// <summary>
        /// Generates the main UI window of the application and emits a signal that the Application starts running.
        /// </summary>
        /// <param name="title">title of the window</param>
        public MainWindow(string title = null)
        {
            window = new Window()
            {
                Title = title,
                Height = SystemParameters.PrimaryScreenHeight * 0.65,
                Width = SystemParameters.PrimaryScreenWidth * 0.6,
                MinHeight = 500,
                MinWidth = 750,
                Background = Brushes.White,
                
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            window.Loaded += (object sender, RoutedEventArgs e) =>
            {
                appStart?.Execute();
            };

            window.Closed += (object sender, EventArgs e) => ((IEvent)this).Execute();

            window.SizeToContent = SizeToContent.Height; // Resizes the popup window to match the height of its contained contents
        }

        public void Run()
        {
            window.Content = iuiStructure?.GetWPFElement();
            System.Windows.Application app = new System.Windows.Application();
            app.Run(window);
        }

        // IEvent implementation -------------------------------------------------------
        void IEvent.Execute() => System.Windows.Application.Current.Shutdown();

        // IDataFlow<bool> implementation ----------------------------------------------
        bool IDataFlow<bool>.Data
        {
            get => window.IsEnabled;
            set => window.IsEnabled = value;
        }
    }
}

﻿using System.Windows;
using Libraries;
using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// <summary>
    /// This abstraction is a general button which can be customized by it's Title, Width, Height, FontSize and Margin. 
    /// It is an interactive UI element which generates an IEvent when clicked.
    /// ------------------------------------------------------------------------------------------------------------------
    /// Ports:
    /// IUI "NEEDNAME": input IUI to get the WPF element
    /// IEvent "eventButtonClicked": output event when button is clicked
    /// </summary>
    public class Button : IUI
    {
        // properties ------------------------------------------------------------
        public string InstanceName = "Default";
        // the properties can extend for any UI customizing requirements
        public double Width { set => button.Width = value; }
        public double Height { set => button.Height = value; }
        public double FontSize { set => button.FontSize = value; }
        public Thickness Margin { set => button.Margin = value; }

        // ports ---------------------------------------------------------------
        private IEvent eventButtonClicked;

        // private fields --------------------------------------------------------
        private System.Windows.Controls.Button button;

        /// <summary>
        /// An interactive UI element which omits an IEvent output when clicked.
        /// It can be customized by setting the Title, Width, Height FontSize and Margin.
        /// </summary>
        /// <param name="title">The text displayed on the button</param>
        public Button(string title)
        {
            button = new System.Windows.Controls.Button();
            button.Content = title;
            button.FontSize = 14;
            button.Click += (object sender, RoutedEventArgs e) =>
            {
                eventButtonClicked?.Execute();
            };
        }

        // IUI implementation ------------------------------------------------------
        UIElement IUI.GetWPFElement()
        {
            return button;
        }
    }
}

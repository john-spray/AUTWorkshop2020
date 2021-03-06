﻿using ProgrammingParadigms;
using System.Windows;

namespace DomainAbstractions
{
    /// <summary>
    /// <para>Contains a WPF TextBox and both implements and provides ports for setting/getting the text inside.</para>
    /// <para>Ports:</para>
    /// <para>1. IUI wpfElement: returns the contained TextBox</para>
    /// <para>2. IDataFlow&lt;string&gt; content: The string contained in the TextBox</para>
    /// <para>3. IDataFlowB&lt;string&gt; returnContent: returns the string contained in the TextBox</para>
    /// <para>4. IEvent clear: clears the text content inside the TextBox</para>
    /// <para>5. IDataFlow&lt;string&gt; textOutput: outputs the string contained in the TextBox</para>
    /// </summary>
    public class TextBox : IUI, IEvent // IDataFlow<string>, IDataFlowB<string>
    {
        // properties
        public string InstanceName { get; set; } = "Default";
        public HorizontalAlignment horizontalAlignment { set => textBox.HorizontalAlignment = value; }
        public double Margin { set => textBox.Margin = new Thickness(value,value,value,value); }
        public Thickness Margins { set => textBox.Margin = value; }
        // public double Height { set => textBox.Height = value; }
        // public double MinWidth { set => textBox.MinWidth = value; }
        public double FontSize { set => textBox.FontSize = value; }
        public string Text { set { textBox.Text = value; } }

        public bool Multiline { set
            {
                textBox.TextWrapping = TextWrapping.Wrap;
                textBox.AcceptsReturn = true;
            }
        }

        // ports
        private IDataFlow<string> textOutput;





        // Fields
        // TextBox overlaps with Systems.Windows.Controls.TextBox if we have "using System.Windows.Controls;"
        private System.Windows.Controls.TextBox textBox = new System.Windows.Controls.TextBox();




        /// <summary>
        /// <para>Contains a WPF TextBox and both implements and provides ports for setting/getting the text inside.</para>
        /// </summary>
        public TextBox(bool readOnly = false)
        {
            textBox.TextChanged += (object sender, System.Windows.Controls.TextChangedEventArgs e) => TextBox_TextChanged();
            //DataChanged = TextBox_TextChanged;
            textBox.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            textBox.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            textBox.IsReadOnly = readOnly;
            Margin = 5;
        }

        private void TextBox_TextChanged()
        {
            if (textOutput != null) textOutput.Data = textBox.Text;
            // DataChanged?.Invoke();
        }

        // IUI implementation
        System.Windows.UIElement IUI.GetWPFElement()
        {
            return textBox;
        }

        /*
        // IDataFlow<string> implementation
        string IDataFlow<string>.Data
        {
            get => textBox.Text;
            set
            {
                textBox.Dispatcher.Invoke(() =>
                {
                    textBox.Text = value;
                });
            }
        }

        // IDataFlowB<string> implementation
        public event DataChangedDelegate DataChanged;

        string IDataFlowB<string>.Data
        {
            get => textBox.Text;
        }
        */

        // IEvent implementation
        void IEvent.Execute()
        {
            textBox.Clear();
        }

    }
}

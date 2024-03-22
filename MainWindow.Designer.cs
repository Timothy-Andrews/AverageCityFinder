namespace AverageCityFinder
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            countryListBox = new ListBox();
            countrySearchBox = new TextBox();
            cityListBox = new ListBox();
            citySearchBox = new TextBox();
            SuspendLayout();
            // 
            // countryListBox
            // 
            countryListBox.FormattingEnabled = true;
            countryListBox.ItemHeight = 15;
            countryListBox.Location = new Point(12, 81);
            countryListBox.Name = "countryListBox";
            countryListBox.Size = new Size(198, 154);
            countryListBox.TabIndex = 0;
            countryListBox.SelectedIndexChanged += countryListBox_SelectedIndexChanged;
            // 
            // countrySearchBox
            // 
            countrySearchBox.Location = new Point(12, 52);
            countrySearchBox.Name = "countrySearchBox";
            countrySearchBox.Size = new Size(198, 23);
            countrySearchBox.TabIndex = 1;
            countrySearchBox.TextChanged += countrySearchBox_TextChanged;
            // 
            // cityListBox
            // 
            cityListBox.FormattingEnabled = true;
            cityListBox.ItemHeight = 15;
            cityListBox.Location = new Point(216, 81);
            cityListBox.Name = "cityListBox";
            cityListBox.Size = new Size(198, 154);
            cityListBox.TabIndex = 2;
            cityListBox.SelectedIndexChanged += cityListBox_SelectedIndexChanged;
            // 
            // citySearchBox
            // 
            citySearchBox.Location = new Point(216, 52);
            citySearchBox.Name = "citySearchBox";
            citySearchBox.Size = new Size(198, 23);
            citySearchBox.TabIndex = 3;
            citySearchBox.TextChanged += citySearchBox_TextChanged;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(citySearchBox);
            Controls.Add(cityListBox);
            Controls.Add(countrySearchBox);
            Controls.Add(countryListBox);
            Name = "MainWindow";
            Text = "Average City Calculator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox countryListBox;
        private TextBox countrySearchBox;
        private ListBox cityListBox;
        private TextBox citySearchBox;
    }
}

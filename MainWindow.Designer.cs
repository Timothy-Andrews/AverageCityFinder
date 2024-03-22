﻿namespace AverageCityFinder
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
            citiesLivedGridView = new DataGridView();
            addCityBtn = new Button();
            calculateAverageLocationBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)citiesLivedGridView).BeginInit();
            SuspendLayout();
            // 
            // countryListBox
            // 
            countryListBox.FormattingEnabled = true;
            countryListBox.ItemHeight = 15;
            countryListBox.Location = new Point(12, 53);
            countryListBox.Name = "countryListBox";
            countryListBox.Size = new Size(198, 154);
            countryListBox.TabIndex = 0;
            countryListBox.SelectedIndexChanged += countryListBox_SelectedIndexChanged;
            // 
            // countrySearchBox
            // 
            countrySearchBox.Location = new Point(12, 24);
            countrySearchBox.Name = "countrySearchBox";
            countrySearchBox.Size = new Size(198, 23);
            countrySearchBox.TabIndex = 1;
            countrySearchBox.TextChanged += countrySearchBox_TextChanged;
            // 
            // cityListBox
            // 
            cityListBox.FormattingEnabled = true;
            cityListBox.ItemHeight = 15;
            cityListBox.Location = new Point(216, 53);
            cityListBox.Name = "cityListBox";
            cityListBox.Size = new Size(198, 124);
            cityListBox.TabIndex = 2;
            // 
            // citySearchBox
            // 
            citySearchBox.Location = new Point(216, 24);
            citySearchBox.Name = "citySearchBox";
            citySearchBox.Size = new Size(198, 23);
            citySearchBox.TabIndex = 3;
            citySearchBox.TextChanged += citySearchBox_TextChanged;
            // 
            // citiesLivedGridView
            // 
            citiesLivedGridView.BackgroundColor = SystemColors.ButtonHighlight;
            citiesLivedGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            citiesLivedGridView.Location = new Point(12, 213);
            citiesLivedGridView.Name = "citiesLivedGridView";
            citiesLivedGridView.Size = new Size(402, 197);
            citiesLivedGridView.TabIndex = 4;
            // 
            // addCityBtn
            // 
            addCityBtn.Location = new Point(216, 183);
            addCityBtn.Name = "addCityBtn";
            addCityBtn.Size = new Size(198, 23);
            addCityBtn.TabIndex = 5;
            addCityBtn.Text = "Add City";
            addCityBtn.UseVisualStyleBackColor = true;
            addCityBtn.Click += addCityBtn_Click;
            // 
            // calculateAverageLocationBtn
            // 
            calculateAverageLocationBtn.Location = new Point(12, 416);
            calculateAverageLocationBtn.Name = "calculateAverageLocationBtn";
            calculateAverageLocationBtn.Size = new Size(143, 49);
            calculateAverageLocationBtn.TabIndex = 6;
            calculateAverageLocationBtn.Text = "Calculate Average";
            calculateAverageLocationBtn.UseVisualStyleBackColor = true;
            calculateAverageLocationBtn.Click += calculateAverageLocationBtn_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 477);
            Controls.Add(calculateAverageLocationBtn);
            Controls.Add(addCityBtn);
            Controls.Add(citiesLivedGridView);
            Controls.Add(citySearchBox);
            Controls.Add(cityListBox);
            Controls.Add(countrySearchBox);
            Controls.Add(countryListBox);
            Name = "MainWindow";
            Text = "Average City Calculator";
            ((System.ComponentModel.ISupportInitialize)citiesLivedGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox countryListBox;
        private TextBox countrySearchBox;
        private ListBox cityListBox;
        private TextBox citySearchBox;
        private DataGridView citiesLivedGridView;
        private Button addCityBtn;
        private Button calculateAverageLocationBtn;
    }
}

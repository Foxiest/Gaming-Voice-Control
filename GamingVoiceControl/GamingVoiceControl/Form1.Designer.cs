namespace GamingVoiceControl
{
    partial class GVC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ControlGrid = new System.Windows.Forms.DataGridView();
            this.Input = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phrase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.RemoveInputButton = new System.Windows.Forms.Button();
            this.inpLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UpdatePhrasesButton = new System.Windows.Forms.Button();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ControlGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ControlGrid
            // 
            this.ControlGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ControlGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Input,
            this.Phrase,
            this.Duration});
            this.ControlGrid.Location = new System.Drawing.Point(12, 46);
            this.ControlGrid.Name = "ControlGrid";
            this.ControlGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ControlGrid.Size = new System.Drawing.Size(333, 289);
            this.ControlGrid.TabIndex = 0;
            this.ControlGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ControlGrid_CellContentClick);
            // 
            // Input
            // 
            this.Input.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Input.HeaderText = "Input";
            this.Input.Name = "Input";
            // 
            // Phrase
            // 
            this.Phrase.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Phrase.HeaderText = "phrase";
            this.Phrase.Name = "Phrase";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 355);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(93, 355);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(370, 98);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 3;
            this.AddButton.Text = "Add Input";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // RemoveInputButton
            // 
            this.RemoveInputButton.Location = new System.Drawing.Point(370, 127);
            this.RemoveInputButton.Name = "RemoveInputButton";
            this.RemoveInputButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveInputButton.TabIndex = 4;
            this.RemoveInputButton.Text = "Remove Input";
            this.RemoveInputButton.UseVisualStyleBackColor = true;
            this.RemoveInputButton.Click += new System.EventHandler(this.RemoveInputButton_Click);
            // 
            // inpLabel
            // 
            this.inpLabel.AutoSize = true;
            this.inpLabel.Location = new System.Drawing.Point(58, 30);
            this.inpLabel.Name = "inpLabel";
            this.inpLabel.Size = new System.Drawing.Size(78, 13);
            this.inpLabel.TabIndex = 5;
            this.inpLabel.Text = "Key to be input";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Phrase to trigger";
            // 
            // UpdatePhrasesButton
            // 
            this.UpdatePhrasesButton.Enabled = false;
            this.UpdatePhrasesButton.Location = new System.Drawing.Point(351, 156);
            this.UpdatePhrasesButton.Name = "UpdatePhrasesButton";
            this.UpdatePhrasesButton.Size = new System.Drawing.Size(109, 23);
            this.UpdatePhrasesButton.TabIndex = 7;
            this.UpdatePhrasesButton.Text = "Update Phrases";
            this.UpdatePhrasesButton.UseVisualStyleBackColor = true;
            this.UpdatePhrasesButton.Click += new System.EventHandler(this.UpdatePhrasesButton_Click);
            // 
            // Duration
            // 
            this.Duration.HeaderText = "Duration (ms)";
            this.Duration.Name = "Duration";
            // 
            // GVC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 390);
            this.Controls.Add(this.UpdatePhrasesButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inpLabel);
            this.Controls.Add(this.RemoveInputButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ControlGrid);
            this.Name = "GVC";
            this.Text = "Gaming Voice Control";
            ((System.ComponentModel.ISupportInitialize)(this.ControlGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ControlGrid;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button RemoveInputButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Input;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phrase;
        private System.Windows.Forms.Label inpLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UpdatePhrasesButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
    }
}


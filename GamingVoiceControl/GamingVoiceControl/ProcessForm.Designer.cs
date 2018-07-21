namespace GamingVoiceControl
{
    partial class ProcessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessForm));
            this.ProcessList = new System.Windows.Forms.ListBox();
            this.SelectButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.UpdateListButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProcessList
            // 
            this.ProcessList.FormattingEnabled = true;
            this.ProcessList.HorizontalScrollbar = true;
            this.ProcessList.Location = new System.Drawing.Point(65, 25);
            this.ProcessList.MultiColumn = true;
            this.ProcessList.Name = "ProcessList";
            this.ProcessList.Size = new System.Drawing.Size(239, 303);
            this.ProcessList.Sorted = true;
            this.ProcessList.TabIndex = 0;
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(65, 346);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(93, 23);
            this.SelectButton.TabIndex = 1;
            this.SelectButton.Text = "Select Process";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(164, 346);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // UpdateListButton
            // 
            this.UpdateListButton.Location = new System.Drawing.Point(310, 99);
            this.UpdateListButton.Name = "UpdateListButton";
            this.UpdateListButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateListButton.TabIndex = 3;
            this.UpdateListButton.Text = "Update list";
            this.UpdateListButton.UseVisualStyleBackColor = true;
            this.UpdateListButton.Click += new System.EventHandler(this.UpdateListButton_Click);
            // 
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 387);
            this.Controls.Add(this.UpdateListButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.ProcessList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessForm";
            this.Text = "ProcessForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ProcessList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button UpdateListButton;
        public System.Windows.Forms.Button SelectButton;
    }
}
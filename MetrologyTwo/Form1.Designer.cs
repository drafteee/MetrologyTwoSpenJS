namespace MetrologyTwo
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.TextBoxOfMainText = new System.Windows.Forms.TextBox();
            this.TextBoxOfResultText = new System.Windows.Forms.TextBox();
            this.ButtonOfProccedTask = new System.Windows.Forms.Button();
            this.MainTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(674, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TextBoxOfMainText
            // 
            this.TextBoxOfMainText.Location = new System.Drawing.Point(1, 361);
            this.TextBoxOfMainText.Multiline = true;
            this.TextBoxOfMainText.Name = "TextBoxOfMainText";
            this.TextBoxOfMainText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxOfMainText.Size = new System.Drawing.Size(408, 159);
            this.TextBoxOfMainText.TabIndex = 1;
            // 
            // TextBoxOfResultText
            // 
            this.TextBoxOfResultText.Enabled = false;
            this.TextBoxOfResultText.Location = new System.Drawing.Point(414, 27);
            this.TextBoxOfResultText.Multiline = true;
            this.TextBoxOfResultText.Name = "TextBoxOfResultText";
            this.TextBoxOfResultText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxOfResultText.Size = new System.Drawing.Size(257, 438);
            this.TextBoxOfResultText.TabIndex = 2;
            // 
            // ButtonOfProccedTask
            // 
            this.ButtonOfProccedTask.Enabled = false;
            this.ButtonOfProccedTask.Location = new System.Drawing.Point(416, 471);
            this.ButtonOfProccedTask.Name = "ButtonOfProccedTask";
            this.ButtonOfProccedTask.Size = new System.Drawing.Size(255, 38);
            this.ButtonOfProccedTask.TabIndex = 3;
            this.ButtonOfProccedTask.Text = "Procced Task";
            this.ButtonOfProccedTask.UseVisualStyleBackColor = true;
            this.ButtonOfProccedTask.Click += new System.EventHandler(this.ButtonOfProccedTask_Click);
            // 
            // MainTextBox
            // 
            this.MainTextBox.Location = new System.Drawing.Point(1, 27);
            this.MainTextBox.Multiline = true;
            this.MainTextBox.Name = "MainTextBox";
            this.MainTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MainTextBox.Size = new System.Drawing.Size(407, 328);
            this.MainTextBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 521);
            this.Controls.Add(this.MainTextBox);
            this.Controls.Add(this.ButtonOfProccedTask);
            this.Controls.Add(this.TextBoxOfResultText);
            this.Controls.Add(this.TextBoxOfMainText);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Kozlovski Sergei Metrology Spen";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox TextBoxOfMainText;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.TextBox TextBoxOfResultText;
        private System.Windows.Forms.Button ButtonOfProccedTask;
        private System.Windows.Forms.TextBox MainTextBox;
    }
}


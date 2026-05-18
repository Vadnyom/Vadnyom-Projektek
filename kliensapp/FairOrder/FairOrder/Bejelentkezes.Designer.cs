namespace FairOrder
{
    partial class Bejelentkezes
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
            LogInEmail = new TextBox();
            LogInButton = new Button();
            SuspendLayout();
            // 
            // LogInEmail
            // 
            LogInEmail.Location = new Point(36, 39);
            LogInEmail.Name = "LogInEmail";
            LogInEmail.Size = new Size(323, 27);
            LogInEmail.TabIndex = 0;
            // 
            // LogInButton
            // 
            LogInButton.Location = new Point(265, 96);
            LogInButton.Name = "LogInButton";
            LogInButton.Size = new Size(94, 29);
            LogInButton.TabIndex = 1;
            LogInButton.Text = "OK";
            LogInButton.UseVisualStyleBackColor = true;
            LogInButton.Click += LogInButton_Click;
            // 
            // Bejelentkezes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 149);
            Controls.Add(LogInButton);
            Controls.Add(LogInEmail);
            Name = "Bejelentkezes";
            Text = "Bejelentkezes";
            Load += Bejelentkezes_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox LogInEmail;
        private Button LogInButton;
    }
}
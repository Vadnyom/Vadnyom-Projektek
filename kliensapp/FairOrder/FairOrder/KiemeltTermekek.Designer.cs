namespace FairOrder
{
    partial class KiemeltTermekek
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
            KiemeltListBox = new ListBox();
            KiemeltFilter = new TextBox();
            OKButton = new Button();
            SelectedItems = new ListBox();
            button1 = new Button();
            button2 = new Button();
            KiemeltNev = new TextBox();
            SuspendLayout();
            // 
            // KiemeltListBox
            // 
            KiemeltListBox.FormattingEnabled = true;
            KiemeltListBox.Location = new Point(80, 79);
            KiemeltListBox.Name = "KiemeltListBox";
            KiemeltListBox.Size = new Size(310, 304);
            KiemeltListBox.TabIndex = 0;
            KiemeltListBox.SelectedIndexChanged += KiemeltListBox_SelectedIndexChanged;
            // 
            // KiemeltFilter
            // 
            KiemeltFilter.Location = new Point(80, 27);
            KiemeltFilter.Name = "KiemeltFilter";
            KiemeltFilter.Size = new Size(310, 27);
            KiemeltFilter.TabIndex = 1;
            // 
            // OKButton
            // 
            OKButton.Location = new Point(639, 400);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(94, 29);
            OKButton.TabIndex = 2;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // SelectedItems
            // 
            SelectedItems.FormattingEnabled = true;
            SelectedItems.Location = new Point(490, 83);
            SelectedItems.Name = "SelectedItems";
            SelectedItems.Size = new Size(226, 144);
            SelectedItems.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(412, 103);
            button1.Name = "button1";
            button1.Size = new Size(45, 29);
            button1.TabIndex = 4;
            button1.Text = ">>";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(412, 156);
            button2.Name = "button2";
            button2.Size = new Size(45, 29);
            button2.TabIndex = 5;
            button2.Text = "<<";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // KiemeltNev
            // 
            KiemeltNev.Location = new Point(79, 398);
            KiemeltNev.Name = "KiemeltNev";
            KiemeltNev.Size = new Size(311, 27);
            KiemeltNev.TabIndex = 6;
            // 
            // KiemeltTermekek
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(783, 450);
            Controls.Add(KiemeltNev);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(SelectedItems);
            Controls.Add(OKButton);
            Controls.Add(KiemeltFilter);
            Controls.Add(KiemeltListBox);
            Name = "KiemeltTermekek";
            Text = "KiemeltTermekek";
            Load += KiemeltTermekek_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox KiemeltListBox;
        private TextBox KiemeltFilter;
        private Button OKButton;
        private ListBox SelectedItems;
        private Button button1;
        private Button button2;
        private TextBox KiemeltNev;
    }
}
namespace FairOrder
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            imageList1 = new ImageList(components);
            SkuSearch = new TextBox();
            User = new Button();
            OrderList = new ListBox();
            OrderButton = new Button();
            QuantityText = new TextBox();
            Add = new Button();
            Delete = new Button();
            AddProduct = new Button();
            Cash = new RadioButton();
            Card = new RadioButton();
            PriceLabel = new Label();
            VATLabel = new Label();
            FullPriceLabel = new Label();
            PriceText = new TextBox();
            VATText = new TextBox();
            FullPriceText = new TextBox();
            panel1 = new Panel();
            KiemeltBeallButton = new Button();
            DelCart = new Button();
            ProductImagesPanel = new FlowLayoutPanel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // SkuSearch
            // 
            SkuSearch.Location = new Point(27, 105);
            SkuSearch.Name = "SkuSearch";
            SkuSearch.PlaceholderText = "Keresés cikkszám alapján";
            SkuSearch.Size = new Size(389, 27);
            SkuSearch.TabIndex = 0;
            SkuSearch.TextChanged += SkuSearch_TextChanged;
            // 
            // User
            // 
            User.Location = new Point(843, 28);
            User.Name = "User";
            User.Size = new Size(94, 29);
            User.TabIndex = 1;
            User.Text = "Felhasználó";
            User.UseVisualStyleBackColor = true;
            User.Visible = false;
            // 
            // OrderList
            // 
            OrderList.FormattingEnabled = true;
            OrderList.Location = new Point(631, 121);
            OrderList.Name = "OrderList";
            OrderList.Size = new Size(305, 164);
            OrderList.TabIndex = 2;
            // 
            // OrderButton
            // 
            OrderButton.BackColor = Color.FromArgb(69, 91, 60);
            OrderButton.FlatStyle = FlatStyle.Flat;
            OrderButton.ForeColor = Color.White;
            OrderButton.Location = new Point(819, 424);
            OrderButton.Name = "OrderButton";
            OrderButton.Size = new Size(117, 29);
            OrderButton.TabIndex = 3;
            OrderButton.Text = "Megrendelés";
            OrderButton.UseVisualStyleBackColor = false;
            OrderButton.Click += Order_Click;
            // 
            // QuantityText
            // 
            QuantityText.Location = new Point(445, 166);
            QuantityText.Name = "QuantityText";
            QuantityText.Size = new Size(125, 27);
            QuantityText.TabIndex = 5;
            // 
            // Add
            // 
            Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Add.BackColor = Color.FromArgb(69, 91, 60);
            Add.FlatAppearance.BorderColor = Color.White;
            Add.FlatAppearance.BorderSize = 0;
            Add.FlatStyle = FlatStyle.Flat;
            Add.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            Add.ForeColor = Color.FromArgb(228, 231, 222);
            Add.Location = new Point(445, 214);
            Add.Margin = new Padding(0);
            Add.Name = "Add";
            Add.Size = new Size(50, 42);
            Add.TabIndex = 6;
            Add.Text = "+";
            Add.UseVisualStyleBackColor = false;
            Add.Click += Add_Click;
            // 
            // Delete
            // 
            Delete.BackColor = Color.FromArgb(69, 91, 60);
            Delete.BackgroundImageLayout = ImageLayout.None;
            Delete.FlatAppearance.BorderSize = 0;
            Delete.FlatStyle = FlatStyle.Flat;
            Delete.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            Delete.ForeColor = Color.FromArgb(228, 231, 222);
            Delete.Location = new Point(520, 214);
            Delete.Name = "Delete";
            Delete.Size = new Size(50, 42);
            Delete.TabIndex = 7;
            Delete.Text = "-";
            Delete.UseVisualStyleBackColor = false;
            Delete.Click += Delete_Click;
            // 
            // AddProduct
            // 
            AddProduct.FlatAppearance.BorderColor = Color.FromArgb(31, 54, 4);
            AddProduct.FlatStyle = FlatStyle.Flat;
            AddProduct.Location = new Point(445, 282);
            AddProduct.Name = "AddProduct";
            AddProduct.Size = new Size(125, 29);
            AddProduct.TabIndex = 8;
            AddProduct.Text = "Hozzáadás";
            AddProduct.UseVisualStyleBackColor = true;
            AddProduct.Click += AddProduct_Click;
            // 
            // Cash
            // 
            Cash.AutoSize = true;
            Cash.Location = new Point(631, 415);
            Cash.Name = "Cash";
            Cash.Size = new Size(92, 24);
            Cash.TabIndex = 9;
            Cash.TabStop = true;
            Cash.Text = "Készpénz";
            Cash.UseVisualStyleBackColor = true;
            // 
            // Card
            // 
            Card.AutoSize = true;
            Card.Location = new Point(631, 445);
            Card.Name = "Card";
            Card.Size = new Size(102, 24);
            Card.TabIndex = 10;
            Card.TabStop = true;
            Card.Text = "Bankkártya";
            Card.UseVisualStyleBackColor = true;
            // 
            // PriceLabel
            // 
            PriceLabel.AutoSize = true;
            PriceLabel.Location = new Point(637, 291);
            PriceLabel.Name = "PriceLabel";
            PriceLabel.Size = new Size(84, 20);
            PriceLabel.TabIndex = 11;
            PriceLabel.Text = "Részösszeg";
            // 
            // VATLabel
            // 
            VATLabel.AutoSize = true;
            VATLabel.Location = new Point(637, 334);
            VATLabel.Name = "VATLabel";
            VATLabel.Size = new Size(35, 20);
            VATLabel.TabIndex = 12;
            VATLabel.Text = "ÁFA";
            // 
            // FullPriceLabel
            // 
            FullPriceLabel.AutoSize = true;
            FullPriceLabel.Location = new Point(637, 375);
            FullPriceLabel.Name = "FullPriceLabel";
            FullPriceLabel.Size = new Size(79, 20);
            FullPriceLabel.TabIndex = 13;
            FullPriceLabel.Text = "Végösszeg";
            // 
            // PriceText
            // 
            PriceText.Location = new Point(811, 291);
            PriceText.Name = "PriceText";
            PriceText.Size = new Size(125, 27);
            PriceText.TabIndex = 14;
            // 
            // VATText
            // 
            VATText.Location = new Point(811, 334);
            VATText.Name = "VATText";
            VATText.Size = new Size(125, 27);
            VATText.TabIndex = 15;
            // 
            // FullPriceText
            // 
            FullPriceText.Location = new Point(811, 375);
            FullPriceText.Name = "FullPriceText";
            FullPriceText.Size = new Size(125, 27);
            FullPriceText.TabIndex = 16;
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(KiemeltBeallButton);
            panel1.Controls.Add(User);
            panel1.Location = new Point(-1, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(993, 87);
            panel1.TabIndex = 17;
            panel1.Paint += panel1_Paint;
            // 
            // KiemeltBeallButton
            // 
            KiemeltBeallButton.Location = new Point(662, 28);
            KiemeltBeallButton.Name = "KiemeltBeallButton";
            KiemeltBeallButton.Size = new Size(158, 29);
            KiemeltBeallButton.TabIndex = 2;
            KiemeltBeallButton.Text = "Termékek beállítása";
            KiemeltBeallButton.UseVisualStyleBackColor = true;
            KiemeltBeallButton.Click += KiemeltBeallButton_Click;
            // 
            // DelCart
            // 
            DelCart.Location = new Point(942, 121);
            DelCart.Name = "DelCart";
            DelCart.Size = new Size(38, 29);
            DelCart.TabIndex = 18;
            DelCart.Text = "-";
            DelCart.UseVisualStyleBackColor = true;
            DelCart.Click += DelCart_Click;
            // 
            // ProductImagesPanel
            // 
            ProductImagesPanel.AutoScroll = true;
            ProductImagesPanel.BorderStyle = BorderStyle.FixedSingle;
            ProductImagesPanel.Location = new Point(12, 152);
            ProductImagesPanel.Name = "ProductImagesPanel";
            ProductImagesPanel.Size = new Size(417, 301);
            ProductImagesPanel.TabIndex = 19;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(992, 481);
            Controls.Add(ProductImagesPanel);
            Controls.Add(DelCart);
            Controls.Add(panel1);
            Controls.Add(FullPriceText);
            Controls.Add(VATText);
            Controls.Add(PriceText);
            Controls.Add(FullPriceLabel);
            Controls.Add(VATLabel);
            Controls.Add(PriceLabel);
            Controls.Add(Card);
            Controls.Add(Cash);
            Controls.Add(AddProduct);
            Controls.Add(Delete);
            Controls.Add(Add);
            Controls.Add(QuantityText);
            Controls.Add(OrderButton);
            Controls.Add(OrderList);
            Controls.Add(SkuSearch);
            ForeColor = Color.FromArgb(31, 54, 4);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ImageList imageList1;
        private TextBox SkuSearch;
        private Button User;
        private ListBox OrderList;
        private Button OrderButton;
        private TextBox QuantityText;
        private Button Add;
        private Button Delete;
        private Button AddProduct;
        private RadioButton Cash;
        private RadioButton Card;
        private Label PriceLabel;
        private Label VATLabel;
        private Label FullPriceLabel;
        private TextBox PriceText;
        private TextBox VATText;
        private TextBox FullPriceText;
        private Panel panel1;
        private Button DelCart;
        private FlowLayoutPanel ProductImagesPanel;
        private Button KiemeltBeallButton;
    }
}

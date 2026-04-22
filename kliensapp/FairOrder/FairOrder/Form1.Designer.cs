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
            imageList1 = new ImageList(components);
            SkuSearch = new TextBox();
            User = new Button();
            OrderList = new ListBox();
            Order = new Button();
            FilteredSku = new ListBox();
            textBox1 = new TextBox();
            Add = new Button();
            Delete = new Button();
            AddProduct = new Button();
            Cash = new RadioButton();
            Card = new RadioButton();
            Price = new Label();
            VAT = new Label();
            FullPrice = new Label();
            PriceText = new TextBox();
            VATText = new TextBox();
            FullPriceText = new TextBox();
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
            SkuSearch.Location = new Point(27, 61);
            SkuSearch.Name = "SkuSearch";
            SkuSearch.Size = new Size(389, 27);
            SkuSearch.TabIndex = 0;
            SkuSearch.TextChanged += SkuSearch_TextChanged;
            // 
            // User
            // 
            User.Location = new Point(842, 22);
            User.Name = "User";
            User.Size = new Size(94, 29);
            User.TabIndex = 1;
            User.Text = "Felhasználó";
            User.UseVisualStyleBackColor = true;
            // 
            // OrderList
            // 
            OrderList.FormattingEnabled = true;
            OrderList.Location = new Point(631, 61);
            OrderList.Name = "OrderList";
            OrderList.Size = new Size(305, 204);
            OrderList.TabIndex = 2;
            // 
            // Order
            // 
            Order.Location = new Point(819, 385);
            Order.Name = "Order";
            Order.Size = new Size(117, 29);
            Order.TabIndex = 3;
            Order.Text = "Megrendelés";
            Order.UseVisualStyleBackColor = true;
            // 
            // FilteredSku
            // 
            FilteredSku.FormattingEnabled = true;
            FilteredSku.Location = new Point(27, 117);
            FilteredSku.Name = "FilteredSku";
            FilteredSku.Size = new Size(389, 244);
            FilteredSku.TabIndex = 4;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(445, 117);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 5;
            // 
            // Add
            // 
            Add.Location = new Point(445, 162);
            Add.Name = "Add";
            Add.Size = new Size(50, 29);
            Add.TabIndex = 6;
            Add.Text = "+";
            Add.UseVisualStyleBackColor = true;
            // 
            // Delete
            // 
            Delete.Location = new Point(520, 162);
            Delete.Name = "Delete";
            Delete.Size = new Size(50, 29);
            Delete.TabIndex = 7;
            Delete.Text = "-";
            Delete.UseVisualStyleBackColor = true;
            // 
            // AddProduct
            // 
            AddProduct.Location = new Point(445, 207);
            AddProduct.Name = "AddProduct";
            AddProduct.Size = new Size(125, 29);
            AddProduct.TabIndex = 8;
            AddProduct.Text = "Hozzáadás";
            AddProduct.UseVisualStyleBackColor = true;
            // 
            // Cash
            // 
            Cash.AutoSize = true;
            Cash.Location = new Point(631, 385);
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
            Card.Location = new Point(631, 415);
            Card.Name = "Card";
            Card.Size = new Size(102, 24);
            Card.TabIndex = 10;
            Card.TabStop = true;
            Card.Text = "Bankkártya";
            Card.UseVisualStyleBackColor = true;
            // 
            // Price
            // 
            Price.AutoSize = true;
            Price.Location = new Point(637, 277);
            Price.Name = "Price";
            Price.Size = new Size(84, 20);
            Price.TabIndex = 11;
            Price.Text = "Részösszeg";
            // 
            // VAT
            // 
            VAT.AutoSize = true;
            VAT.Location = new Point(637, 309);
            VAT.Name = "VAT";
            VAT.Size = new Size(35, 20);
            VAT.TabIndex = 12;
            VAT.Text = "ÁFA";
            // 
            // FullPrice
            // 
            FullPrice.AutoSize = true;
            FullPrice.Location = new Point(637, 341);
            FullPrice.Name = "FullPrice";
            FullPrice.Size = new Size(79, 20);
            FullPrice.TabIndex = 13;
            FullPrice.Text = "Végösszeg";
            // 
            // PriceText
            // 
            PriceText.Location = new Point(782, 277);
            PriceText.Name = "PriceText";
            PriceText.Size = new Size(125, 27);
            PriceText.TabIndex = 14;
            // 
            // VATText
            // 
            VATText.Location = new Point(782, 309);
            VATText.Name = "VATText";
            VATText.Size = new Size(125, 27);
            VATText.TabIndex = 15;
            // 
            // FullPriceText
            // 
            FullPriceText.Location = new Point(782, 341);
            FullPriceText.Name = "FullPriceText";
            FullPriceText.Size = new Size(125, 27);
            FullPriceText.TabIndex = 16;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(981, 453);
            Controls.Add(FullPriceText);
            Controls.Add(VATText);
            Controls.Add(PriceText);
            Controls.Add(FullPrice);
            Controls.Add(VAT);
            Controls.Add(Price);
            Controls.Add(Card);
            Controls.Add(Cash);
            Controls.Add(AddProduct);
            Controls.Add(Delete);
            Controls.Add(Add);
            Controls.Add(textBox1);
            Controls.Add(FilteredSku);
            Controls.Add(Order);
            Controls.Add(OrderList);
            Controls.Add(User);
            Controls.Add(SkuSearch);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ImageList imageList1;
        private TextBox SkuSearch;
        private Button User;
        private ListBox OrderList;
        private Button Order;
        private ListBox FilteredSku;
        private TextBox textBox1;
        private Button Add;
        private Button Delete;
        private Button AddProduct;
        private RadioButton Cash;
        private RadioButton Card;
        private Label Price;
        private Label VAT;
        private Label FullPrice;
        private TextBox PriceText;
        private TextBox VATText;
        private TextBox FullPriceText;
    }
}

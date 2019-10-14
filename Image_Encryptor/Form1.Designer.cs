namespace Image_Encryptor
{
    partial class Image_Encryptor
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnChooseImg = new System.Windows.Forms.Button();
            this.BtnReadImage = new System.Windows.Forms.Button();
            this.BtnReadTextFile = new System.Windows.Forms.Button();
            this.BtnChooseText = new System.Windows.Forms.Button();
            this.TxtBoxPicLoc = new System.Windows.Forms.TextBox();
            this.TxtBoxTxtLoc = new System.Windows.Forms.TextBox();
            this.TxtKey = new System.Windows.Forms.TextBox();
            this.LblKey = new System.Windows.Forms.Label();
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.ImageFile = new System.Windows.Forms.OpenFileDialog();
            this.TxtPicture = new System.Windows.Forms.RichTextBox();
            this.TextFile = new System.Windows.Forms.OpenFileDialog();
            this.ReadText = new System.Windows.Forms.Button();
            this.ChkBoxComp = new System.Windows.Forms.CheckBox();
            this.CompWidth = new System.Windows.Forms.NumericUpDown();
            this.LblNwImgWidth = new System.Windows.Forms.Label();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.BtnSaveTxt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(391, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 268);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BtnChooseImg
            // 
            this.BtnChooseImg.Location = new System.Drawing.Point(12, 12);
            this.BtnChooseImg.Name = "BtnChooseImg";
            this.BtnChooseImg.Size = new System.Drawing.Size(90, 40);
            this.BtnChooseImg.TabIndex = 1;
            this.BtnChooseImg.Text = "Image";
            this.BtnChooseImg.UseVisualStyleBackColor = true;
            this.BtnChooseImg.Click += new System.EventHandler(this.BtnChooseImg_Click);
            // 
            // BtnReadImage
            // 
            this.BtnReadImage.Location = new System.Drawing.Point(12, 58);
            this.BtnReadImage.Name = "BtnReadImage";
            this.BtnReadImage.Size = new System.Drawing.Size(90, 40);
            this.BtnReadImage.TabIndex = 2;
            this.BtnReadImage.Text = "Read";
            this.BtnReadImage.UseVisualStyleBackColor = true;
            this.BtnReadImage.Click += new System.EventHandler(this.BtnReadImage_Click);
            // 
            // BtnReadTextFile
            // 
            this.BtnReadTextFile.Location = new System.Drawing.Point(634, 58);
            this.BtnReadTextFile.Name = "BtnReadTextFile";
            this.BtnReadTextFile.Size = new System.Drawing.Size(154, 40);
            this.BtnReadTextFile.TabIndex = 4;
            this.BtnReadTextFile.Text = "Read Text File";
            this.BtnReadTextFile.UseVisualStyleBackColor = true;
            this.BtnReadTextFile.Click += new System.EventHandler(this.BtnReadTextFile_Click);
            // 
            // BtnChooseText
            // 
            this.BtnChooseText.Location = new System.Drawing.Point(698, 12);
            this.BtnChooseText.Name = "BtnChooseText";
            this.BtnChooseText.Size = new System.Drawing.Size(90, 40);
            this.BtnChooseText.TabIndex = 3;
            this.BtnChooseText.Text = "Text";
            this.BtnChooseText.UseVisualStyleBackColor = true;
            this.BtnChooseText.Click += new System.EventHandler(this.BtnChooseText_Click);
            // 
            // TxtBoxPicLoc
            // 
            this.TxtBoxPicLoc.Location = new System.Drawing.Point(109, 19);
            this.TxtBoxPicLoc.Name = "TxtBoxPicLoc";
            this.TxtBoxPicLoc.Size = new System.Drawing.Size(254, 26);
            this.TxtBoxPicLoc.TabIndex = 5;
            // 
            // TxtBoxTxtLoc
            // 
            this.TxtBoxTxtLoc.Location = new System.Drawing.Point(438, 19);
            this.TxtBoxTxtLoc.Name = "TxtBoxTxtLoc";
            this.TxtBoxTxtLoc.Size = new System.Drawing.Size(254, 26);
            this.TxtBoxTxtLoc.TabIndex = 6;
            // 
            // TxtKey
            // 
            this.TxtKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtKey.Location = new System.Drawing.Point(334, 317);
            this.TxtKey.Name = "TxtKey";
            this.TxtKey.Size = new System.Drawing.Size(128, 26);
            this.TxtKey.TabIndex = 7;
            // 
            // LblKey
            // 
            this.LblKey.AutoSize = true;
            this.LblKey.Font = new System.Drawing.Font("Leelawadee", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblKey.Location = new System.Drawing.Point(369, 285);
            this.LblKey.Name = "LblKey";
            this.LblKey.Size = new System.Drawing.Size(57, 29);
            this.LblKey.TabIndex = 8;
            this.LblKey.Text = "Key:";
            // 
            // PicBox
            // 
            this.PicBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PicBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PicBox.Location = new System.Drawing.Point(47, 212);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(250, 250);
            this.PicBox.TabIndex = 10;
            this.PicBox.TabStop = false;
            // 
            // ImageFile
            // 
            this.ImageFile.Filter = "Image Files(*.BMP)|*.BMP|All files (*.*)|*.*";
            this.ImageFile.FileOk += new System.ComponentModel.CancelEventHandler(this.ImageFile_FileOk);
            // 
            // TxtPicture
            // 
            this.TxtPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPicture.Location = new System.Drawing.Point(502, 212);
            this.TxtPicture.Name = "TxtPicture";
            this.TxtPicture.Size = new System.Drawing.Size(250, 250);
            this.TxtPicture.TabIndex = 11;
            this.TxtPicture.Text = "";
            // 
            // TextFile
            // 
            this.TextFile.Filter = "Text Files(*.TXT)|*.TXT|All files (*.*)|*.*";
            this.TextFile.FileOk += new System.ComponentModel.CancelEventHandler(this.TextFile_FileOk);
            // 
            // ReadText
            // 
            this.ReadText.Location = new System.Drawing.Point(634, 166);
            this.ReadText.Name = "ReadText";
            this.ReadText.Size = new System.Drawing.Size(118, 40);
            this.ReadText.TabIndex = 12;
            this.ReadText.Text = "Read Text";
            this.ReadText.UseVisualStyleBackColor = true;
            this.ReadText.Click += new System.EventHandler(this.ReadText_Click);
            // 
            // ChkBoxComp
            // 
            this.ChkBoxComp.AutoSize = true;
            this.ChkBoxComp.Location = new System.Drawing.Point(109, 127);
            this.ChkBoxComp.Name = "ChkBoxComp";
            this.ChkBoxComp.Size = new System.Drawing.Size(116, 24);
            this.ChkBoxComp.TabIndex = 13;
            this.ChkBoxComp.Text = "Compress?";
            this.ChkBoxComp.UseVisualStyleBackColor = true;
            this.ChkBoxComp.CheckedChanged += new System.EventHandler(this.ChkBoxComp_CheckedChanged);
            // 
            // CompWidth
            // 
            this.CompWidth.Enabled = false;
            this.CompWidth.Location = new System.Drawing.Point(187, 157);
            this.CompWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CompWidth.Name = "CompWidth";
            this.CompWidth.Size = new System.Drawing.Size(120, 26);
            this.CompWidth.TabIndex = 14;
            this.CompWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // LblNwImgWidth
            // 
            this.LblNwImgWidth.AutoSize = true;
            this.LblNwImgWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNwImgWidth.Location = new System.Drawing.Point(43, 159);
            this.LblNwImgWidth.Name = "LblNwImgWidth";
            this.LblNwImgWidth.Size = new System.Drawing.Size(138, 20);
            this.LblNwImgWidth.TabIndex = 15;
            this.LblNwImgWidth.Text = "New Image Width:";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "\"txt files (*.txt)|*.txt|All files (*.*)|*.*\"";
            // 
            // BtnSaveTxt
            // 
            this.BtnSaveTxt.Location = new System.Drawing.Point(502, 166);
            this.BtnSaveTxt.Name = "BtnSaveTxt";
            this.BtnSaveTxt.Size = new System.Drawing.Size(118, 40);
            this.BtnSaveTxt.TabIndex = 16;
            this.BtnSaveTxt.Text = "Save Text";
            this.BtnSaveTxt.UseVisualStyleBackColor = true;
            this.BtnSaveTxt.Click += new System.EventHandler(this.BtnSaveTxt_Click);
            // 
            // Image_Encryptor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 495);
            this.Controls.Add(this.BtnSaveTxt);
            this.Controls.Add(this.LblNwImgWidth);
            this.Controls.Add(this.CompWidth);
            this.Controls.Add(this.ChkBoxComp);
            this.Controls.Add(this.ReadText);
            this.Controls.Add(this.TxtPicture);
            this.Controls.Add(this.PicBox);
            this.Controls.Add(this.LblKey);
            this.Controls.Add(this.TxtKey);
            this.Controls.Add(this.TxtBoxTxtLoc);
            this.Controls.Add(this.TxtBoxPicLoc);
            this.Controls.Add(this.BtnReadTextFile);
            this.Controls.Add(this.BtnChooseText);
            this.Controls.Add(this.BtnReadImage);
            this.Controls.Add(this.BtnChooseImg);
            this.Controls.Add(this.pictureBox1);
            this.MaximumSize = new System.Drawing.Size(827, 551);
            this.MinimumSize = new System.Drawing.Size(827, 551);
            this.Name = "Image_Encryptor";
            this.Text = "Encryptor/Decryptor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnChooseImg;
        private System.Windows.Forms.Button BtnReadImage;
        private System.Windows.Forms.Button BtnReadTextFile;
        private System.Windows.Forms.Button BtnChooseText;
        private System.Windows.Forms.TextBox TxtBoxPicLoc;
        private System.Windows.Forms.TextBox TxtBoxTxtLoc;
        private System.Windows.Forms.TextBox TxtKey;
        private System.Windows.Forms.Label LblKey;
        private System.Windows.Forms.PictureBox PicBox;
        private System.Windows.Forms.OpenFileDialog ImageFile;
        public System.Windows.Forms.RichTextBox TxtPicture;
        private System.Windows.Forms.OpenFileDialog TextFile;
        private System.Windows.Forms.Button ReadText;
        private System.Windows.Forms.CheckBox ChkBoxComp;
        private System.Windows.Forms.NumericUpDown CompWidth;
        private System.Windows.Forms.Label LblNwImgWidth;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Button BtnSaveTxt;
    }
}


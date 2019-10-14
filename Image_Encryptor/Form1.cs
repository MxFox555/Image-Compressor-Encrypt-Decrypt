/*
 * WRITTEN BY: Millen Boekel
 * USING: Pascal Case naming convention.
 * PURPOSE: To convert an image to text and encrypt / decrypt it and also to rebuild image from text
 * 
 * Copyright (c) 2019 Millen Boekel
 * Released under the MIT licence: http://opensource.org/licenses/mit-license
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Image_Encryptor
{
    /*
     * Stores almost everything it needs to know about the image, including the compressed image
     * 
     * @field   FullImage       Stores full image in byte array
     * @field   DimX            The Width of the image
     * @field   DimY            The Height of the image
     * @field   RedIndex        Indexes for the red pixels which translate to FullImage, is two dimensional
     * @field   GedIndex        Indexes for the green pixels which translate to FullImage, is two dimensional
     * @field   BlueIndex       Indexes for the Blue pixels which translate to FullImage, is two dimensional
     * @field   CompDimX        Width for the compressed image
     * @field   CompDimY        Height for the compressed image
     * @field   RedCompVals     The red value for the compressed image, stored last row first and lef tto right (as is bmp convention)
     * @field   GreenCompVals   The green value for the compressed image, stored last row first and lef tto right (as is bmp convention)
     * @field   BlueCompVals    The blue value for the compressed image, stored last row first and lef tto right (as is bmp convention)
     */
    public struct Image
    {
        public byte[] FullImage;
        public int DimX;
        public int DimY;
        public int[,] RedIndex;
        public int[,] GreenIndex;
        public int[,] BlueIndex;
        public int CompDimX;
        public int CompDimY;
        public decimal[,] RedCompVals;
        public decimal[,] GreenCompVals;
        public decimal[,] BlueCompVals;
    };

    /*
     * Stores the Base64Image for a whole image, whether that be encrypted or not
     * 
     * @field Base64Image       the whole image stored in one string
     */
    public struct TextPicture
    {
        public string Base64Image;
    };

    /*
     * Intended to be cycled through so the program knows what color the index of the pixel is meants to be
     */
    enum RGB
    {
        BLUE,
        RED,
        GREEN
    };

    public partial class Image_Encryptor : Form
    {

        public Image_Encryptor()
        {
            InitializeComponent();
        }

        /* 
         * Gets all the needed information from the image
         * 
         * @field   Img     The bitmap image
         * @field   ImgLocation     The location of the image
         */
        Image ReadImage(ref Bitmap Img, string ImgLocation)
        {
            Image Pic = new Image();
            // Getting picture dimensions
            Pic.DimX = Img.Width;
            Pic.DimY = Img.Height;
            // Getting the raw bytes
            Pic.FullImage = File.ReadAllBytes(ImgLocation);
            return Pic;
        }

        /* 
         * Getting the size of the bmp padding, padding is needed because each line has to be a factor of 4 to be able to be read
         * 
         * @field   Img     The Image struct to be passed
         */
        int GetPaddingSize(ref Image Img)
        {
            if (Img.CompDimX == 0) // If the picture is not being compressed 
            {
                // Gets the remainder when divided by four
                int Num1 = (Img.DimX * 3) % 4;
                // Gets what is needed to make it divisible by four
                int Num2 = 4 - Num1;
                if (Num2 == 4)
                    // if none is needed send 0
                    return 0;
                else
                    // Else send what is needed
                    return Num2;
            }
            else  // If the picture is being compressed
            {
                // Do the same process as before just witht he compressed width
                int Num1 = (Img.CompDimX * 3) % 4;
                int Num2 = 4 - Num1;
                if (Num2 == 4)
                    return 0;
                else
                    return Num2;
            }

        }

        /* 
         * Simply converting bytes to text using Base64 encoding
         * 
         * @field   Bytes   The bytes to be converted
         */
        string ConvertToText(ref byte[] Bytes)
        {
            return Convert.ToBase64String(Bytes);
        }

        /* 
         * Writing string to text box
         * 
         * @field   Text    The text to be written
         */
        void WriteToTextBox(string Text)
        {
            // Just in case I want to run it from a new thread for some reason
            TxtPicture.Invoke(new Action(() => TxtPicture.Text = Text));
        }


        /* 
         * Note: Stores bottom row, left to right first (of bitmap)
         * Gets the colour indexes of the FullImage array
         * 
         * @field   Img     The Image struct to be passed
         */
        void ReadColourIndexes(ref Image Img)
        {
            // Initialising the arrays
            Img.BlueIndex = new int[Img.DimX, Img.DimY];
            Img.GreenIndex = new int[Img.DimX, Img.DimY];
            Img.RedIndex = new int[Img.DimX, Img.DimY];
            // Used to cycle through and keep track of what colour is being indexed
            RGB Cycle = RGB.BLUE; //0 - Blue , 1 - Green , 2 - Red
            // Stores the padding needed for the end of each line
            int Padding = GetPaddingSize(ref Img);
            // Keeping track of which x value is being read
            int ColumnIndex = 0;
            // Keeps track fo which line reader is on
            int Line = 0;
            // Going through the byte array starting from end of header (where header ends is stored at byte 11, hence FullImage[10])
            for (int i = Img.FullImage[10]; i < (Img.DimX * Img.DimY * 3) + (Padding * Img.DimY) + Img.FullImage[10]; i++)
            {
                // Switch case to determine which colour needs to be indexed
                switch (Cycle)
                {
                    case RGB.BLUE:
                        // Storing blue index
                        Img.BlueIndex[ColumnIndex, Line] = i;
                        Cycle = RGB.GREEN;
                        break;
                    case RGB.GREEN:
                        // Storing green index
                        Img.GreenIndex[ColumnIndex, Line] = i;
                        Cycle = RGB.RED;
                        break;
                    case RGB.RED:
                        // Storing red index
                        Img.RedIndex[ColumnIndex, Line] = i;
                        // shifting accross an x value
                        ColumnIndex++;
                        // Restarting cycle
                        Cycle = RGB.BLUE;
                        break;
                }
                if (ColumnIndex == Img.DimX) // If at last x value
                {
                    if (Padding > 0) // If there is padding jump over it
                    {
                        i += Padding;
                    }
                    // Resetting x posiiton
                    ColumnIndex = 0;
                    // Going to next line
                    Line++;
                }
            }
        }



        /*
         * Create a block of data containing the image data without the header in byte form
         * 
         * @field   Img     The Image struct to be passed
         */
        byte[] GetCompDataBlock(ref Image Img)
        {
            // Once again getting padding size
            int PaddingSize = (GetPaddingSize(ref Img));
            // Initilising new data block
            byte[] DataBlock = new byte[(Img.CompDimX * Img.CompDimY * 3) + (PaddingSize * Img.CompDimY)];
            // Kind of like the previous procedure except it has been modified to fit its needs
            RGB Cycle = RGB.BLUE; //0 - Blue , 1 - Green , 2 - Red
            int Padding = GetPaddingSize(ref Img);
            int LineIndex = 0;
            int Line = 0;
            for (int i = 0; i < ((Img.CompDimX * Img.CompDimY * 3) + (Padding * Img.CompDimY)); i++)
            {
                switch (Cycle)
                {
                    case RGB.BLUE:
                        // If on blue cycle save blue to this byte
                        DataBlock[i] = Decimal.ToByte(Img.BlueCompVals[LineIndex, Line]);
                        Cycle = RGB.GREEN;
                        break;
                    case RGB.GREEN:
                        // If on green cycle save green to this byte
                        DataBlock[i] = Decimal.ToByte(Img.GreenCompVals[LineIndex, Line]);
                        Cycle = RGB.RED;
                        break;
                    case RGB.RED:
                        // If on red cycle save red to this byte
                        DataBlock[i] = Decimal.ToByte(Img.RedCompVals[LineIndex, Line]);
                        // Increase the x index
                        LineIndex++;
                        Cycle = RGB.BLUE;
                        break;
                }
                if (LineIndex == Img.CompDimX) // Again.. jump over padding
                {
                    if (Padding > 0)
                    {
                        i += Padding;
                    }
                    LineIndex = 0;
                    Line++;
                }
            }
            return DataBlock;
        }

        /*
         * Building the header and putting it all together
         * 
         * @field   Img     The Image struct to be passed
         */
        Bitmap BuildCompressed(ref Image Img)
        {
            // Specify new header as a list because bmp headers can vary
            List<byte> NewHeader = new List<byte>();
            // Get data block from previous function
            byte[] DataBlock = GetCompDataBlock(ref Img);
            // Get header from old one as a template
            for (int i = 0; i < Img.FullImage[10]; i++)
            {
                NewHeader.Add(Img.FullImage[i]);
            }
            // Because of the delicacy of the header it needs to be virtually hardcoded in
            // Grabbing the size of the whole file in bytes and putting it in its byte slots
            byte[] FileSize = BitConverter.GetBytes(DataBlock.Length + NewHeader.Count);
            NewHeader[2] = FileSize[0];
            NewHeader[3] = FileSize[1];
            NewHeader[4] = FileSize[2];
            NewHeader[5] = FileSize[3];
            // Getting width in bytes and putting in byte slots
            byte[] WidthByte = BitConverter.GetBytes(Img.CompDimX);
            NewHeader[18] = WidthByte[0];
            NewHeader[19] = WidthByte[1];
            NewHeader[20] = WidthByte[2];
            NewHeader[21] = WidthByte[3];
            // Getting height in bytes and putting in byte slots
            byte[] HeightByte = BitConverter.GetBytes(Img.CompDimY);
            NewHeader[22] = HeightByte[0];
            NewHeader[23] = HeightByte[1];
            NewHeader[24] = HeightByte[2];
            NewHeader[25] = HeightByte[3];
            // Getting the size of the image without the header and putting into header byte slots
            byte[] ImgSize = BitConverter.GetBytes(DataBlock.Length);
            NewHeader[34] = ImgSize[0];
            NewHeader[35] = ImgSize[1];
            NewHeader[36] = ImgSize[2];
            NewHeader[37] = ImgSize[3];

            // Combining both the header and the image data block 
            byte[] Combined = new byte[NewHeader.Count + DataBlock.Length];
            // Writing in all the data from the new header
            for (int i = 0; i < NewHeader.Count; i++)
            {
                Combined[i] = NewHeader[i];
            }
            // Writing in all the data from the image data block
            for (int i = 0; i < DataBlock.Length; i++)
            {
                Combined[NewHeader.Count + i] = DataBlock[i];
            }
            // Re assigning full image to be the compressed image
            Img.FullImage = Combined;
            // Rebuilding a bitmap from the byte array that was just made
            Bitmap NewBitmap;
            var Ms = new MemoryStream(Combined);
            Ms.Seek(0, SeekOrigin.Begin);
            NewBitmap = new Bitmap(Ms);
            // Returning the bitmap
            return NewBitmap;
        }

        // Opening the choose image dialog when the button is pressed
        private void BtnChooseImg_Click(object sender, EventArgs e)
        {
            ImageFile.ShowDialog();
        }

        // Executes when file is legit
        private void ImageFile_FileOk(object sender, CancelEventArgs e)
        {
            // Showing the location of the file in the location text box
            TxtBoxPicLoc.Text = ImageFile.FileName;
            // Getting the image and passing it as a bitmap
            Bitmap Image = new Bitmap(ImageFile.FileName);
            // If the image is to be compressed the pixel width obviously cannot go past the images pizel width
            CompWidth.Maximum = Image.Width;
        }

        // When the read image button is clicked
        private void BtnReadImage_Click(object sender, EventArgs e)
        {
            // Storing the image location
            string ImgLocation = ImageFile.FileName;
            // Checks if there actually is an image location
            if (ImgLocation != "")
            {
                // Storing the bitmap from the location
                Bitmap BitmapImage = new Bitmap(ImgLocation);
                // Initialising the structs
                Image Img = new Image();
                TextPicture TextPic = new TextPicture();
                TextPicture EncryptedTextPic = new TextPicture();

                // Getting information about the image
                Img = ReadImage(ref BitmapImage, ImgLocation);
                // Reading the colour indexes about the image
                ReadColourIndexes(ref Img);
                if (ChkBoxComp.Checked == true) // If the compression checkbox has been checked
                {
                    // This is compressing the image
                    Compression.Compress(ref Img, Convert.ToInt32(CompWidth.Value));
                    Bitmap CompImg;
                    // building the now compressed image
                    CompImg = BuildCompressed(ref Img);
                    // Encoding it into Base64
                    TextPic.Base64Image = ConvertToText(ref Img.FullImage);
                    // Showing the compressed image
                    PicBox.BackgroundImage = CompImg;
                }
                else // If the compression checkbox hasnt been checked
                {
                    // Converting image to Base64
                    TextPic.Base64Image = ConvertToText(ref Img.FullImage);
                    // Showing the image
                    PicBox.BackgroundImage = BitmapImage;
                }

                if (TxtKey.Text == "")
                {
                    // Not Encrypted

                    // Write the Base64 characters to the text box
                    WriteToTextBox(TextPic.Base64Image);
                }
                else
                {
                    //Encrypted

                    // Converting the image bytes to Base64 encoding
                    TextPic.Base64Image = ConvertToText(ref Img.FullImage);
                    // Encrypting the text
                    EncryptedTextPic.Base64Image = Encryption.Encrypt(TextPic.Base64Image, TxtKey.Text);
                    // Writing the tex tto the text box
                    WriteToTextBox(EncryptedTextPic.Base64Image);
                }
            }
            else
            {
                // Warning that no path has been entered
                MessageBox.Show("A path needs to be entered");
            }
        }

        // When the check box has been changed
        private void ChkBoxComp_CheckedChanged(object sender, EventArgs e)
        {
            // Switching when the compression checkbox is checked/not checked
            if (ChkBoxComp.Checked == true)
                CompWidth.Enabled = true;
            else
                CompWidth.Enabled = false;
        }

        // When the choose text button is clicked show file menu
        private void BtnChooseText_Click(object sender, EventArgs e)
        {
            TextFile.ShowDialog();
        }

        // When read text file button is clicked
        private void BtnReadTextFile_Click(object sender, EventArgs e)
        {
            // Storing text location
            string TxtLocation = TxtBoxTxtLoc.Text;
            // If there is text location
            if (TxtLocation != "")
            {
                // Storing text file information
                string PicText = File.ReadAllText(TxtLocation);
                // Write text file info to text box
                WriteToTextBox(PicText);
                // Convert text to image
                ConvertToImage(PicText);
            }
            // If there is not text location
            else
            {
                // Warning message
                MessageBox.Show("A path needs to be entered");
            }
        }

        // When text is manually entered and read button is clicked
        private void ReadText_Click(object sender, EventArgs e)
        {
            // If there is text entered
            if (TxtPicture.Text != "")
            {
                // Store text
                string PicText = TxtPicture.Text;
                // Then convert it
                ConvertToImage(PicText);
            }
            else
            {
                // Warning message
                MessageBox.Show("Information needs to be entered");
            }
        }

        /*
         * Decode the text and made an image out of it
         * 
         * @field   PicText     The text of the picture to be converted to an image
         */
        void ConvertToImage(string PicText)
        {
            //Initalising variables 
            byte[] PicBytes;
            string Unencrypted = "";
            // If there is a key entered
            if (TxtKey.Text != "")
            {
                // Unencrypt it first
                Unencrypted = Encryption.Decrypt(PicText, TxtKey.Text);
                // Now convert from string to bytes using Base64
                PicBytes = Convert.FromBase64String(Unencrypted);
            }
            else
            {
                // convert from string to bytes using Base64
                PicBytes = Convert.FromBase64String(PicText);
            }
            Bitmap ConvertedBitmap;
            // Attempt to convert the bytes to a legible image
            try
            {
                var ms = new MemoryStream(PicBytes);
                ms.Seek(0, SeekOrigin.Begin);
                ConvertedBitmap = new Bitmap(ms);
                PicBox.BackgroundImage = ConvertedBitmap;
            }
            catch
            {
                // If an image can't be conceived the key must be wrong or the text must be gibberish
                MessageBox.Show("Invalid text or incorrect Key");
            }
        }

        private void TextFile_FileOk(object sender, CancelEventArgs e)
        {
            // Showing the location of the file in the location text box
            TxtBoxTxtLoc.Text = TextFile.FileName;
        }

        // Saves the text directly to a file
        private void BtnSaveTxt_Click(object sender, EventArgs e)
        {
            // Makes sure the file name and its properties is ok before proceeding
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Writes the text to the specified path
                File.WriteAllText(SaveFileDialog.FileName, TxtPicture.Text);
            }
        }
    }

    // the class where all the encrpytion and decryption happens
    public class Encryption
    {
        // Taken from:
        // https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp

        public static string Encrypt(string clearText, string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText, string EncryptionKey)
        {
            try
            {
                cipherText = cipherText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch
            {
                // If the text cannot be decrypted
                MessageBox.Show("Cannot decipher text");
                return ("");
            }
        }
    }

    // The class which deals with image compression
    public class Compression
    {
        /*
         * To compress the image
         * 
         * @field   Picture     The Image struct of the picture
         * @field   PicWidth    The new width of the picture
         */
        public static void Compress(ref Image Picture, int PicWidth)
        {
            // The ratio of width to height of the image to the get the new height for compression
            double ratio = (double)Picture.DimY / Picture.DimX;
            // Getting the corresponding PicHeight for PicWidth
            int PicHeight = Convert.ToInt32(PicWidth * ratio);
            // How many x pixels will be in one grouping
            int GroupingX = Picture.DimX / PicWidth;
            // How many pixels are left over
            int GroupingXRemainder = Picture.DimX % PicWidth;
            // How many y pixels are in one grouping
            int GroupingY = Picture.DimY / PicHeight;
            // How many pixels are left over
            int GroupingYRemainder = Picture.DimY % PicHeight;
            // The overlap if the pixel width or height is not a factor of the main image width then this will try to mitiage pixel loss
            int OverlapX = GroupingXRemainder / 2;
            int OverlapY = GroupingYRemainder / 2;
            // Initialising the arrays
            Picture.RedCompVals = new decimal[PicWidth, PicHeight];
            Picture.GreenCompVals = new decimal[PicWidth, PicHeight];
            Picture.BlueCompVals = new decimal[PicWidth, PicHeight];
            // Setting the new pixel height and width
            Picture.CompDimX = PicWidth;
            Picture.CompDimY = PicHeight;

            // For the first values that are gathered at the start of each section
            bool first;
            /*
             * So, pretty much how this works is it sets its width (x) and height (y) constraints
             * then within that and its grouping it gathers up all the other colours with x2 and y2
             * and gets the average of those for that one grouping, rince and repeat
             */
            for (int x = 0; x < PicWidth; x++)
            {
                for (int y = 0; y < PicHeight; y++)
                {
                    // Set first as true since there are no other values to get average of initially
                    first = true;
                    for (int x2 = (x * GroupingX); x2 < (x * GroupingX) + GroupingX + OverlapX; x2++)
                    {
                        for (int y2 = (y * GroupingY); y2 < (y * GroupingY) + GroupingY + OverlapY; y2++)
                        {
                            if (first == true)
                            {
                                // Get initial values for each group
                                Picture.RedCompVals[x, y] = Picture.FullImage[Picture.RedIndex[x2, y2]];
                                Picture.GreenCompVals[x, y] = Picture.FullImage[Picture.GreenIndex[x2, y2]];
                                Picture.BlueCompVals[x, y] = Picture.FullImage[Picture.BlueIndex[x2, y2]];
                                // The upcoming values are no longer the first group, hence set first to false
                                first = false;
                            }
                            else
                            {
                                // Get average values from now on
                                Picture.RedCompVals[x, y] = (Picture.RedCompVals[x, y] + Picture.FullImage[Picture.RedIndex[x2, y2]]) / 2;
                                Picture.GreenCompVals[x, y] = (Picture.GreenCompVals[x, y] + Picture.FullImage[Picture.GreenIndex[x2, y2]]) / 2;
                                Picture.BlueCompVals[x, y] = (Picture.BlueCompVals[x, y] + Picture.FullImage[Picture.BlueIndex[x2, y2]]) / 2;
                            }
                        }
                    }

                }
            }
            // Rounding each decimal to a whole number
            for (int x = 0; x<PicWidth; x++)
            {
                for (int y = 0; y<PicHeight; y++)
                {
                    Picture.RedCompVals[x, y] = Decimal.Round(Picture.RedCompVals[x, y]);
                    Picture.GreenCompVals[x, y] = Decimal.Round(Picture.GreenCompVals[x, y]);
                    Picture.BlueCompVals[x, y] = Decimal.Round(Picture.BlueCompVals[x, y]);
                }
}
        }
    }

}

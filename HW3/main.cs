using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW3
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void load_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog imagefileopen = new OpenFileDialog();
            imagefileopen.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png) |*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (imagefileopen.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = new Bitmap(imagefileopen.FileName);
                pictureBox.Size = pictureBox.Image.Size;
            }
        }

        private Bitmap getGrayScale(Bitmap image)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);
            for (int j = 0; j < result.Height; j++)
            {
                for (int i = 0; i < result.Width; i++)
                {
                    Color color = image.GetPixel(i, j);
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    Color nColor = Color.FromArgb(gray, gray, gray);
                    result.SetPixel(i, j, nColor);
                }
            }
            return result;
        }

        private Bitmap getSmoothed(Bitmap image)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);
            int[,] filter = new int[,] {
                {1,  4,  7,  4, 1},
                {4, 16, 26, 16, 4},
                {7, 26, 41, 26, 7},
                {4, 16, 26, 16, 4},
                {1,  4,  7,  4, 1},
            };

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int nPixel = 0;
                    // apply filter to pixel
                    for (int j = -2; j <= 2; j++)
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            int u = x + i;
                            int v = y + j;
                            if (u < 0 || u >= image.Width) u = x - i;
                            if (v < 0 || v >= image.Height) v = y - j;

                            Color value = image.GetPixel(u, v);
                            nPixel += value.R * filter[2 + j, 2 + i];
                        }
                    }
                    nPixel += 136;
                    nPixel /= 273;
                    Color color = Color.FromArgb(nPixel, nPixel, nPixel);
                    result.SetPixel(x, y, color);
                }
            }

            return result;
        }

        private struct Gradient
        {
            public double dx;
            public double dy;
            public double mag;
            public int quadrant;
        }

        private Gradient[,] getGradient(Bitmap image)
        {
            Gradient[,] gradients = new Gradient[image.Width, image.Height];

            for (int y = 1; y < image.Height-1; y++)
            {
                for (int x = 1; x < image.Width-1; x++)
                {
                    // calculate gradient vector
                    int c1 = image.GetPixel(x+1, y).R;
                    int c2 = image.GetPixel(x-1, y).R;
                    double dx = (c1 - c2) / 2f;

                    c1 = image.GetPixel(x, y + 1).R;
                    c2 = image.GetPixel(x, y - 1).R;
                    double dy = (c1 - c2) / -2f;

                    // store dx and dy in array
                    gradients[x, y].dx = dx;
                    gradients[x, y].dy = dy;

                    // store magnitude in array
                    gradients[x, y].mag = Math.Abs(dx) + Math.Abs(dy);

                    // calculate direction of gradient
                    int q = 1;
                    if (dx < 0 && dy > 0) q = 2;
                    else if (dx < 0 && dy < 0) q = 3;
                    else if (dx > 0 && dy < 0) q = 4;
                    gradients[x, y].quadrant = q;
                }
            }

            return gradients;
        }

        private Bitmap convertFromGradient(Bitmap image, Gradient[,] gradients)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);

            for (int y = 1; y < image.Height-1; y++)
            {
                for (int x = 1; x < image.Width-1; x++)
                {
                    int c = (int)gradients[x, y].mag;
                    Color color = Color.FromArgb(c, c, c);
                    result.SetPixel(x, y, color);
                }
            }

            return result;
        }

        private Bitmap nonMaxSuppression(Bitmap image, Gradient[,] gradients)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);
            int u = 0, v = 0;

            for (int y = 1; y < image.Height - 1; y++)
            {
                for (int x = 1; x < image.Width - 1; x++)
                {
                    int value = image.GetPixel(x, y).R;
                    Gradient pixel = gradients[x, y];
                    if (pixel.quadrant == 1 || pixel.quadrant == 3) {
                        if (Math.Abs(pixel.dx) > Math.Abs(pixel.dy))
                        {
                            u = (image.GetPixel(x + 1, y - 1).R + image.GetPixel(x + 1, y).R + 1) / 2;
                            v = (image.GetPixel(x - 1, y + 1).R + image.GetPixel(x - 1, y).R + 1) / 2;
                        }
                        if (Math.Abs(pixel.dx) < Math.Abs(pixel.dy))
                        {
                            u = (image.GetPixel(x + 1, y - 1).R + image.GetPixel(x, y - 1).R + 1) / 2;
                            v = (image.GetPixel(x - 1, y + 1).R + image.GetPixel(x, y + 1).R + 1) / 2;
                        }
                        if (Math.Abs(pixel.dx) == Math.Abs(pixel.dy))
                        {
                            u = image.GetPixel(x + 1, y - 1).R;
                            v = image.GetPixel(x - 1, y + 1).R;
                        }
                    } else { 
                        if (Math.Abs(pixel.dx) > Math.Abs(pixel.dy))
                        {
                            u = (image.GetPixel(x - 1, y - 1).R + image.GetPixel(x - 1, y).R + 1) / 2;
                            v = (image.GetPixel(x + 1, y + 1).R + image.GetPixel(x + 1, y).R + 1) / 2;
                        }
                        if (Math.Abs(pixel.dx) < Math.Abs(pixel.dy))
                        {
                            u = (image.GetPixel(x - 1, y - 1).R + image.GetPixel(x, y - 1).R + 1) / 2;
                            v = (image.GetPixel(x + 1, y + 1).R + image.GetPixel(x, y + 1).R + 1) / 2;
                        }
                        if (Math.Abs(pixel.dx) == Math.Abs(pixel.dy))
                        {
                            u = image.GetPixel(x - 1, y - 1).R;
                            v = image.GetPixel(x + 1, y + 1).R;
                        }
                    }
                    if (value > u && value > v)
                    {
                        result.SetPixel(x, y, image.GetPixel(x, y));
                    } else
                    {
                        result.SetPixel(x, y, Color.Black);
                    }
                }
            }

            return result;
        }

        private Bitmap hysteresis(Bitmap image, Gradient[,] gradients)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);

            // get 5% threshold
            int[] gList = new int[gradients.Length];
            int write = 0;
            for (int i = 1; i <= gradients.GetUpperBound(0) - 1; i++)
            {
                for (int j = 1; j <= gradients.GetUpperBound(1) - 1; j++)
                {
                    gList[write++] = (int)gradients[i, j].mag;
                }
            }

            Array.Sort(gList);
            int upperThreshold = gList[(int)(gList.Length * 0.95)];

            // go through image and apply the threshold
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (gradients[i, j].mag > upperThreshold)
                    {
                        result.SetPixel(i, j, Color.White);
                    } else
                    {
                        result.SetPixel(i, j, Color.Black);
                    }
                }
            }

            return result;
        }

        private void smoothed_Click(object sender, EventArgs e)
        {
            Form wn = new image();
            wn.Text = "Smoothed Image";
            Bitmap imageGrayScale = getGrayScale((Bitmap)pictureBox.Image);
            Bitmap imageSmoothed = getSmoothed(imageGrayScale);

            PictureBox picture = new PictureBox();
            picture.Size = imageSmoothed.Size;
            picture.Image = imageSmoothed;
            wn.Controls.Add(picture);
            wn.Show();
        }

        private void gradient_Click(object sender, EventArgs e)
        {
            Form wn = new image();
            wn.Text = "Gradient Phase";
            Bitmap imageGrayScale = getGrayScale((Bitmap)pictureBox.Image);
            Bitmap imageSmoothed = getSmoothed(imageGrayScale);
            Gradient[,] gradientArray = getGradient(imageSmoothed);
            Bitmap imageGradient = convertFromGradient(imageSmoothed, gradientArray);

            PictureBox picture = new PictureBox();
            picture.Size = imageGradient.Size;
            picture.Image = imageGradient;
            wn.Controls.Add(picture);
            wn.Show();
        }

        private void edge_detection_Click(object sender, EventArgs e)
        {
            Form wn = new image();
            wn.Text = "Canny Edge Detection";
            Bitmap imageGrayScale = getGrayScale((Bitmap)pictureBox.Image);
            Bitmap imageSmoothed = getSmoothed(imageGrayScale);
            Gradient[,] gradientArray = getGradient(imageSmoothed);
            Bitmap imageGradient = convertFromGradient(imageSmoothed, gradientArray);
            Bitmap imageEdgeDetect = nonMaxSuppression(imageGradient, gradientArray);
            Bitmap finalImage = hysteresis(imageEdgeDetect, gradientArray);

            PictureBox picture = new PictureBox();
            picture.Size = finalImage.Size;
            picture.Image = finalImage;
            wn.Controls.Add(picture);
            wn.Show();
        }
    }
}

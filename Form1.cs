using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBSCAN
{
    public partial class Form1 : Form
    {
        List<MyPoint> points = new List<MyPoint>();

        Bitmap bitmap;

        int xMultiple, yMultiple;

        public Form1()
        {
            InitializeComponent();


            openFileButton.Click += new System.EventHandler(openFileButton_Click);
            runButton.Click += new System.EventHandler(runButton_Click);

            this.Load += new System.EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(canvas.Width, canvas.Height);
        }

        /// <summary>
        /// 清理畫布
        /// </summary>
        /// <param name="graphics"></param>
        private void clearGraphics(Graphics graphics)
        {
            graphics.Clear(Color.White);

            canvas.Image = bitmap;
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text Files (*.txt)|*.txt";

            openFileDialog.Title = "請選擇資料集";

            if (openFileDialog.ShowDialog(this) == DialogResult.Cancel)
                return;

            points.Clear();

            clearGraphics(Graphics.FromImage(bitmap));

            FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open);

            StreamReader streamReader = new StreamReader(fileStream);

            //挑出資料集最大的X、Y軸 方便計算畫布與點的倍率
            float maxX = 0;
            float maxY = 0;

            while (streamReader.Peek() != -1)
            {
                String[] str = streamReader.ReadLine().Split(' ');

                //取得X軸
                float x = float.Parse(str[0]);
                //取得Y軸
                float y = float.Parse(str[1]);

                maxX = Math.Max(maxX, x);
                maxY = Math.Max(maxY, y);

                MyPoint myPoint = new MyPoint(x, y);

                points.Add(myPoint);
            }

            //關閉檔案串流
            fileStream.Close();

            xMultiple = (int)(canvas.Width / maxX);
            yMultiple = (int)(canvas.Height / maxY);

            //圓的大小
            int drawSize = 10;

            //畫圓需要使用筆刷
            SolidBrush solidBrush = new SolidBrush(Color.Blue);

            //畫出資料點
            for (int i = 0; i < points.Count; i++)
            {
                Graphics.FromImage(bitmap).FillEllipse(solidBrush, points[i].x * xMultiple - drawSize / 2, points[i].y * yMultiple - drawSize / 2, drawSize, drawSize);

                canvas.Image = bitmap;
            }

            runButton.Enabled = true;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            clearGraphics(Graphics.FromImage(bitmap));

            //計算所有集合點 是否為 核心點、邊界點、雜訊點
            for (int i = 0; i < points.Count; i++)
            {
                //因為要包含本身的點 所以預設為1
                int number = 1;

                /*
                 * 用來記錄以圓為中心有包含住的點(不包括本身的點)
                 * 假設這個點是核心點 那就放到MyPoint類別中的subPoints
                 * 這樣做是方便尋找(密度直達)與(密度相連)的集合有哪些
                 */
                List<MyPoint> pointList = new List<MyPoint>();

                //必須與其他不是本身的點做距離計算
                for (int j = 0; j < points.Count; j++)
                {
                    if (i != j)
                    {
                        //歐基里德距離公式  開根號((x1 - x2)平方 + (y1 - y2)平方)
                        double distance = Math.Sqrt(Math.Pow(points[i].x - points[j].x, 2) + Math.Pow(points[i].y - points[j].y, 2));

                        //如果發現在圓內或圓邊 則把number + 1; 並把點加入至pointList
                        if (Double.Parse(epsTextBox.Text) >= distance)
                        {
                            number = number + 1;
                            pointList.Add(points[j]);
                        }
                    }
                }

                //如果number >= minPtsTextBox 所設定的數量 代表此點為核心點
                if (number >= Int16.Parse(minPtsTextBox.Text))
                {
                    //設定此點為核心點
                    points[i].type = MyPoint.MAIN_POINT;

                    for (int j = 0; j < pointList.Count; j++)
                    {
                        //如果點原本是雜訊點 則把點變成邊界點
                        if (pointList[j].type == MyPoint.NOISE_POINT)
                        {
                            pointList[j].type = MyPoint.BORDER_POINT;
                        }

                        points[i].addMyPoint(pointList[j]);
                    }

                    //清除pointList的資料
                    pointList.Clear();
                }
            }

            //宣告(群數)變數 預設為0個群
            int groupNumber = 0;

            for (int i = 0; i < points.Count; i++)
            {
                //如果此點為核心點就執行
                if (points[i].type == MyPoint.MAIN_POINT)
                {
                    //如果此核心點沒有貼上群組標籤 則執行DBSCAN遞迴
                    if (points[i].groupID == 0)
                    {
                        cluster(points[i],groupNumber);

                        //把目前的群數 +1
                        groupNumber = groupNumber + 1;
                    }
                }
            }

            List<MyColor> groupColor = new List<MyColor>();

            Random random = new Random();

            //產生groupNumber族群的亂數顏色
            for (int i = 0;i < groupNumber; i++)
            {
                int rColor = random.Next(0, 256);
                int gColor = random.Next(0, 256);
                int bColor = random.Next(0, 256);

                groupColor.Add(new MyColor(rColor, gColor, bColor));
            }

            int drawSize = 10;

            //繪製DBSCAN結果
            for(int i = 0; i < points.Count; i++)
            {
                if(points[i].groupID == 0)
                {
                    SolidBrush solidBrush = new SolidBrush(Color.Blue);
                    Graphics.FromImage(bitmap).FillEllipse(solidBrush, points[i].x * xMultiple - drawSize / 2, points[i].y * yMultiple - drawSize / 2, drawSize, drawSize);
                }
                else
                {
                    int rColor = groupColor[points[i].groupID - 1].rColor;
                    int gColor = groupColor[points[i].groupID - 1].gColor;
                    int bColor = groupColor[points[i].groupID - 1].bColor;

                    SolidBrush solidBrush = new SolidBrush(Color.FromArgb(rColor, gColor, bColor));
                    Graphics.FromImage(bitmap).FillEllipse(solidBrush, points[i].x * xMultiple - drawSize / 2, points[i].y * yMultiple - drawSize / 2, drawSize, drawSize);
                }

                canvas.Image = bitmap;
            }
        }

        /// <summary>
        /// DBSCAN 遞迴演算法
        /// 利用陣列指標的方式來搜尋密度直接與密度相連的點有哪些
        /// 並將這些點的groupID設定為同一個族群編號
        /// </summary>
        /// <param name="point"></param>
        /// <param name="groupNumber"></param>
        private void cluster(MyPoint point, int groupNumber)
        {
            point.groupID = groupNumber + 1;

            for (int i = 0; i < point.subPoints.Count; i++)
            {
                if (point.subPoints[i].type == MyPoint.MAIN_POINT)
                {
                    //檢驗族群編號是怕走過上一個核心點
                    if (point.subPoints[i].groupID == 0)
                    {
                        cluster(point.subPoints[i], groupNumber);
                    }
                }
                else
                {
                    if (point.subPoints[i].groupID == 0)
                    {
                        point.subPoints[i].groupID = groupNumber + 1;
                    }
                }
            }

            runButton.Enabled = false;
        }
    }

    class MyPoint
    {
        public const int NOISE_POINT = 1;
        public const int BORDER_POINT = 2;
        public const int MAIN_POINT = 3;

        public float x;
        public float y;
        public int type;
        public int groupID;

        public List<MyPoint> subPoints = new List<MyPoint>();

        public MyPoint(float x, float y)
        {
            this.x = x;
            this.y = y;

            //預設為雜訊點
            this.type = NOISE_POINT;

            //0為未分類點
            this.groupID = 0;
        }

        /// <summary>
        /// 如果為核心點 就把包含在圓心的點都放到subPoints
        /// </summary>
        /// <param name="point"></param>
        public void addMyPoint(MyPoint point)
        {
            subPoints.Add(point);
        }
    }

    class MyColor
    {
        public int rColor;
        public int gColor;
        public int bColor;

        public MyColor(int rColor, int gColor, int bColor)
        {
            this.rColor = rColor;
            this.gColor = gColor;
            this.bColor = bColor;
        }
    }
}

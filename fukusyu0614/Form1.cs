using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fukusyu0614
{
    public partial class Form1 : Form
    {
        int getCount = 3;

        //定数
        const int itemCount = 3;

        enum SCENE
        {
            TITLE,
            GAMEPLAY,
            GAMEOVER,
            CLEAR,
            NONE
        }
        /// <summary>
        /// 現在のシーン
        /// </summary>
        SCENE nowScene;

        /// <summary>
        /// 切り替えたいシーン
        /// </summary>
        SCENE nextScene;


        int [] vx =new int[itemCount];
        int[] vy = new int[itemCount];
        //配列100個分のラベル
        Label[] labels = new Label[itemCount];

        private static Random rand = new Random();



        public Form1()
        {
            //基本的に↓コレの下に書く
            InitializeComponent();

            nextScene = SCENE.TITLE;
            nowScene = SCENE.NONE;

            for (int i = 0; i < itemCount; i++)
            {
                //メモリとして使うためこっちも大事
                labels[i] = new Label();
                labels[i].AutoSize = true;
                labels[i].Text = "○";
                Controls.Add(labels[i]);
                labels[i].Font = label1.Font;
                labels[i].ForeColor = label1.ForeColor;
                labels[i].Left = rand.Next(ClientSize.Width - label1.Width);
                labels[i].Top = rand.Next(ClientSize.Height - label1.Height);
                vx[i] = rand.Next(-5, 6);
                vy[i] = rand.Next(-5, 6);
            }


            label1.Left = rand.Next(ClientSize.Width - label1.Width);
            label1.Top = rand.Next(ClientSize.Height - label1.Height);
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void initProc()
        {

            //nextSceneがNONEだったら、何もしない
            if (nextScene == SCENE.NONE) return;

            nowScene = nextScene;
            nextScene = SCENE.NONE;
            switch(nowScene)
            {
                case SCENE.TITLE:
                    label4.Visible = true;
                    button1.Visible=true; 
                    break;
                case SCENE.GAMEPLAY:
                    label4.Visible = false;
                    button1.Visible = false;
                    getCount = itemCount;
                    break;
            }

        }

        void updateProc()
        {
            if (nowScene == SCENE.GAMEPLAY)
            {
                updateGame();
            }
        }

        void updateGame()
        {
            for (int i = 0; i < itemCount; i++)
            {
                labels[i].Left += vx[i];
                labels[i].Top += vy[i];

                if (labels[i].Left < 0)
                {
                    vx[i] = Math.Abs(vx[i]);
                }
                if (labels[i].Top < 0)
                {
                    vy[i] = Math.Abs(vy[i]);
                }
                if (labels[i].Right > ClientSize.Width - labels[i].Width)
                {
                    vx[i] = -Math.Abs(vx[i]);
                }
                if (labels[i].Bottom > ClientSize.Height - labels[i].Height)
                {
                    vy[i] = -Math.Abs(vy[i]);
                }

                Point mp = PointToClient(MousePosition);
                if ((mp.X >= labels[i].Left)
                    && (mp.X < labels[i].Right)
                    && (mp.Y >= labels[i].Top)
                    && (mp.Y < labels[i].Bottom)
                    )
                {
                    labels[i].Visible = false;
                    getCount--;
                    if (getCount<=0)
                    {
                        nextScene = SCENE.CLEAR;
                    }
                }
            }
        }

       /* void CLEAR
        {

        }
        */

        private void timer1_Tick(object sender, EventArgs e)
        {
            initProc();
            updateProc();

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nextScene = SCENE.GAMEPLAY;
        }
    }
}

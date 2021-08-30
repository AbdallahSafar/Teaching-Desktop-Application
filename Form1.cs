using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
//mailSending//
using System.Web;
using System.Net.Mail;
using DataLayer;
using System.Media;
using System.Collections;
using System.Speech.Synthesis;
using System.Globalization;
using System.Net;
using sudoku_cs;

namespace ShiBBayedElWej
{
    public partial class Form1 : Form
    {
        /************************Variables************************/
        private Game game = new Game();
        private Random r1 = new Random();

        bool french = false;

        Point pdef;
        int xPos, yPos, timerTank=60;
        ArrayList animals = new ArrayList();
        ArrayList vehicul = new ArrayList();
        ArrayList batiment = new ArrayList();
        ArrayList technologie = new ArrayList();
        ArrayList rectangle = new ArrayList();
        ArrayList cercle = new ArrayList();
        ArrayList triangle = new ArrayList();
        ArrayList autreforme = new ArrayList();
        int ExtraPoints = 0;



        Label firstClicked = null;
        Label secondClicked = null;
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };


        Random number = new Random();
        int number1;
        int number2;
        int number3;
        int answer;
        int time;
        int qans;
        int iq = 0;
        int real = 1;
        int go;
        int x;
        int y;
        int set;




        int r = 185, g = 40, b = 40, i = 0,bon=0;
        int ayyaPanelLvl5 = 0;
        DataSet usersData = new DataSet("UsersData");
        Random rnd = new Random();
        string[] t = new string[20];
        string[] tableau,questCompte = Levels.CompteestBon(7);
        int count = 0;
        bool[] ok = new bool[40], doneCompte = new bool[5];
        bool signup = false, Prof = false;
        SoundPlayer player = new SoundPlayer("Alphabet.WAV");
        SoundPlayer correct = new SoundPlayer("Correct.WAV");
        SoundPlayer nombre = new SoundPlayer("nombres.WAV");
        SoundPlayer False = new SoundPlayer("False.WAV");
        SpeechSynthesizer speak = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder(new CultureInfo("fr-FR"));

        ArrayList tab1_vrai_ar;
        ArrayList tab1_faux_ar;
        string[] tab,tab1,tab2;
        bool premier = true;
        bool aller;
        int fois, nbfois, lindice = 0;
        int ental, Timer_count = 1, compteurparfois = 0;
        string sous_son, s="";
        Font fa = new Font("Lucida Fax", 14, FontStyle.Strikeout);

        /************************Variables************************/





        /************************Methods************************/
        public void sendMailForgot(string username, string body)
        {
            string toMail = "";
            string toName = "";
            string xt;
            if (username != "")
            {
                foreach (DataRow dr in usersData.Tables[0].Rows)
                {
                    if (string.CompareOrdinal(dr[1].ToString(), username) == 0)
                    {
                        toMail = dr[3].ToString();
                        toName = dr[0].ToString();
                    }
                }
                try
                {
                    var fromAddress = new MailAddress("bash.mh.99@gmail.com", "Bashar Al-Mohammad");
                    var toAddress = new MailAddress(toMail, toName);
                    const string fromPassword = "bachar.is.the.serial.killer";
                    const string subject = "Subject";
                }
                catch
                {

                }
                

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            else
            {
                MessageBox.Show("Please add a username to reset ur password or sign up if ure not a user");
            }
        }


        public void sendMail(string username)
        {
            string toName = "";
            string toMail = "";
            string marks="";
            foreach (DataRow dr in usersData.Tables[0].Rows)
            {
                if (string.CompareOrdinal(dr[1].ToString(), logUser.Text) == 0)
                {
                    toMail = dr[3].ToString();
                    toName = dr[0].ToString();
                    marks = user.GetPoints(username);
                }
            }
            var fromAddress = new MailAddress("bash.mh.99@gmail.com", "Bashar Al-Mohammad");
            var toAddress = new MailAddress(toMail, toName);
            const string fromPassword = "bachar.is.the.serial.killer";
            const string subject = "Subject";
            string body = "Dear" + toName.ToString() + ",\n" + "Hello agian, and thanks for using this again" + marks + "\n Regards";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }





        public void game_ShowClues(int[][] grid)
        {
            for (int y = 0; y <= 8; y++)
            {
                List<int> cells = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                for (int c = 1; c <= 9 - (5 - comboBox2.SelectedIndex); c++)
                {
                    int randomNumber = cells[r1.Next(0, cells.Count())];
                    cells.Remove(randomNumber);
                }
                for (int x = 0; x <= 8; x++)
                {
                    if (cells.Contains(x + 1))
                    {
                        DataGridView1.Rows[y].Cells[x].Value = grid[y][x];
                        DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Red;
                        DataGridView1.Rows[y].Cells[x].ReadOnly = true;
                    }
                    else
                    {
                        DataGridView1.Rows[y].Cells[x].Value = "";
                        DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Black;
                        DataGridView1.Rows[y].Cells[x].ReadOnly = false;
                    }
                }
            }
        }


        public void game_ShowSolution(int[][] grid)
        {
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    if (DataGridView1.Rows[y].Cells[x].Style.ForeColor == Color.Black)
                    {
                        if (string.IsNullOrEmpty(DataGridView1.Rows[y].Cells[x].Value.ToString()))
                        {
                            DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Blue;
                            DataGridView1.Rows[y].Cells[x].Value = grid[y][x];
                        }
                        else
                        {
                            if (grid[y][x].ToString() != DataGridView1.Rows[y].Cells[x].Value.ToString())
                            {
                                DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Blue;
                                DataGridView1.Rows[y].Cells[x].Value = grid[y][x];
                            }
                        }
                    }
                }
            }
        }




        public string CategoryPic(ArrayList ar,ArrayList ar1,ArrayList ar2, ArrayList ar3, PictureBox p)
        {
            if (ar.Contains(p)) return "animals";
            if (ar1.Contains(p)) return "technology";
            if (ar2.Contains(p)) return "batiment";
            if (ar3.Contains(p)) return "vehicule";
            else return "abu lsim";
        }
        public string CategoryPic1(ArrayList ar, ArrayList ar1, ArrayList ar2, ArrayList ar3, PictureBox p)
        {
            if (ar.Contains(p)) return "cercle";
            if (ar1.Contains(p)) return "rectangle";
            if (ar2.Contains(p)) return "triangle";
            if (ar3.Contains(p)) return "autreforme";
            else return "abu lsim";
        }

        public bool AtPanel(PictureBox p,FlowLayoutPanel fl)
        {
            int i;
            if (fl.Controls.Contains(p)) return (true);
            else
            {
                return (false);
            }

        }
        public int InPanel(int x,int y)
        {
            int i=0;
            if (((x < flowLayoutPanel3.Location.X + flowLayoutPanel3.Width) && (x > flowLayoutPanel3.Location.X)) && ((y < flowLayoutPanel3.Location.Y + flowLayoutPanel3.Height) && (y > flowLayoutPanel3.Location.Y))) i = 2;
            if (((x < flowLayoutPanel4.Location.X + flowLayoutPanel4.Width) && (x > flowLayoutPanel4.Location.X)) && ((y < flowLayoutPanel4.Location.Y + flowLayoutPanel4.Height) && (y > flowLayoutPanel4.Location.Y))) i = 3;
            if (((x < flowLayoutPanel5.Location.X + flowLayoutPanel5.Width) && (x > flowLayoutPanel5.Location.X)) && ((y < flowLayoutPanel5.Location.Y + flowLayoutPanel5.Height) && (y > flowLayoutPanel5.Location.Y))) i = 4;
            if (((x < flowLayoutPanel2.Location.X + flowLayoutPanel2.Width) && (x > flowLayoutPanel2.Location.X)) && ((y < flowLayoutPanel2.Location.Y + flowLayoutPanel2.Height) && (y > flowLayoutPanel2.Location.Y))) i = 1;
            return i;
        }
        public int InPanel1(int x, int y)
        {
            int i = 0;
            if (((x < flowLayoutPanel8.Location.X + flowLayoutPanel8.Width) && (x > flowLayoutPanel8.Location.X)) && ((y < flowLayoutPanel8.Location.Y + flowLayoutPanel8.Height) && (y > flowLayoutPanel8.Location.Y))) i = 2;
            if (((x < flowLayoutPanel7.Location.X + flowLayoutPanel7.Width) && (x > flowLayoutPanel7.Location.X)) && ((y < flowLayoutPanel7.Location.Y + flowLayoutPanel7.Height) && (y > flowLayoutPanel7.Location.Y))) i = 3;
            if (((x < flowLayoutPanel6.Location.X + flowLayoutPanel6.Width) && (x > flowLayoutPanel6.Location.X)) && ((y < flowLayoutPanel6.Location.Y + flowLayoutPanel6.Height) && (y > flowLayoutPanel6.Location.Y))) i = 4;
            if (((x < flowLayoutPanel9.Location.X + flowLayoutPanel9.Width) && (x > flowLayoutPanel9.Location.X)) && ((y < flowLayoutPanel9.Location.Y + flowLayoutPanel9.Height) && (y > flowLayoutPanel9.Location.Y))) i = 1;
            return i;
        }

        private void CheckForWinner()
        {
            // Go through all of the labels in the TableLayoutPanel,  
            // checking each one to see if its icon is matched.
            foreach (Control control in tableLayoutPanel9.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // If the loop didn’t return, it didn't find 
            // any unmatched icons. 
            // That means the user won. Show a message and close the form.
            MessageBox.Show("You matched all the icons!", "Congratulations!");
            tabcontrol1.SelectTab(choosegame);
        }

        private void AssignIconsToSquares()
        {
            // The TableLayoutPanel has 16 labels, 
            // and the icon list has 16 icons, 
            // so an icon is pulled at random from the list 
            // and added to each label.
            foreach (Control control in tableLayoutPanel9.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }



        public void starttest()
        {


            submit.Enabled = true;
            if (set == 1)
            {
                number1 = number.Next(0, 10);
                number2 = number.Next(0, 10);

            }
            if (set == 2)
            {
                number1 = number.Next(0, 50);
                number2 = number.Next(0, 50);

            }
            if (set == 3)
            {
                number1 = number.Next(0, 100);
                number2 = number.Next(0, 100);

            }

            number3 = number.Next(1, 4);
            if (number3 == 1)
            {
                num1.Text = number1.ToString();
                num2.Text = number2.ToString();
                symbol.Text = "+";
                qans = number1 + number2;
            }
            if (number3 == 2)
            {
                num1.Text = number1.ToString();
                num2.Text = number2.ToString();
                symbol.Text = "-";
                qans = number1 - number2;
            }
            if (number3 == 3)
            {
                num1.Text = number1.ToString();
                num2.Text = number2.ToString();
                symbol.Text = "*";
                qans = number1 * number2;
            }



        }




        public static void ClearPRO(Panel p)
        {
            foreach (MetroFramework.Controls.MetroTextBox t in p.Controls)
            {
                t.Text = "";
            }
        }
        public static bool AllInformations(Panel p)
        {
            bool ok = true;
            foreach (MetroFramework.Controls.MetroTextBox t in p.Controls)
            {
                if (t.Text == "") ok = false;
            }
            return ok;
        }
        public static bool DoneCompte(bool[] t) 
        {
            int count = 0;
            for (int i = 0; i < t.Length; i++) 
            {
                if (t[i] == true) count++;
            }
            if (count == t.Length) return true;
            else return false;
        
        }
        public static int GetPoints(string[] t, Panel p)
        {
            for (int i = 0; i < t.Length; i++)
            {
                foreach (RadioButton rd in p.Controls)
                {
                    if (rd.Text == t[i])
                    {
                        if (rd.Checked == true) return 3;
                    }
                }
            }
            return 0;
        }
        public static int Getindice(string[] t, string s) 
        {
            int j = 100;
            for (int i = 0; i < t.Length; i++) 
            {
                if (t[i] == s) j = i;
            }
            if (j == 100) return 100;
            else return j;
        }
        public static string[] RemplirTab(string[] t, int i) 
        {
            int j = 0;
            string[] tab = new string[t.Length - 5];
            for (int a = 0; a < t.Length; a=a+5) 
            {
                if (a != i)
                { 
                    tab[j] = t[a];
                    tab[j + 1] = t[a + 1];
                    tab[j + 2] = t[a + 2]; 
                    tab[j + 3] = t[a + 3];
                    tab[j + 4] = t[a + 4];
                    j = j + 5;
                }
            }
            return tab;
        }
        public static void CleanLev(Panel p1, Panel p2, Panel p3, Panel p4)
        {
            p1.Visible = false;
            p2.Visible = false;
            p3.Visible = false;
            p4.Visible = false;
        }

        ArrayList split_fichier_sans(string path)
        {
            string[] tableau;
            string mot;
            ArrayList ar = new ArrayList();
            ArrayList aro = new ArrayList();

            StreamReader fr = new StreamReader(path);
            while (fr.Peek() != -1)
            {
                mot = fr.ReadLine();
                tableau = mot.Split(' ', ',', ';', '!', '?', '(', ')', '"', '.', ':', '<', '>', '_', '-', '\n', '\r', '\t');
                ar.AddRange(tableau);

            }
            for (int i = 0; i < ar.Count; i++)
            {
                if (ar[i].ToString() != "")

                    aro.Add(ar[i].ToString().ToLower());

            }

            return aro;

        }
        ArrayList split_textbox_sans(RichTextBox box)
        {
            string[] tableau;
            ArrayList aro = new ArrayList();

            tableau = box.Text.Split(' ', ',', ';', '!', '?', '(', ')', '"', '.', ':', '<', '>', '_', '-', '\n', '\r', '\t');

            for (int i = 0; i < tableau.Length; i++)
            {
                if (tableau[i] != "")
                    aro.Add(tableau[i].ToLower());
            }
            return aro;

        }
        public int comparer_deux_mots(string m1, string m2)
        {
            if (m1.Length != m2.Length) return -1;
            for (int i = 0; i < m1.Length; i++)
                if (m1[i] != m2[i]) return i;
            return -2;
        }
        public int ou_est_il(string mot, string[] tab)
        {
            bool trouve = false;
            int i = 0;
            if (tab.Length > 0)
                while ((!trouve) && (i < tab.Length))
                {

                    if (mot == tab[i]) trouve = true;
                    i++;
                }
            if (!trouve)
                return 0;
            return (i);
        }
        public int comparer_4_mots_apres(int f1, int v1, ArrayList arf, ArrayList arv)
        {
            int long_rest_vrai = arv.Count - v1 - 1, n;
            int long_rest_faux = arf.Count - f1 - 1;

            if (long_rest_vrai > long_rest_faux) n = long_rest_faux;
            else n = long_rest_vrai;
            if (n > 4) n = 4;
            for (int i = 1; i <= n; i++)
                if (arv[v1 + i].ToString() == arf[f1 + i].ToString())
                    return (i);
            return 5;
        }

        public int check_mot(int v2, int f2, ArrayList arf1, ArrayList arv1)
        {
            bool completer = true;
            int longo = 0;
            int exf2 = f2 + 3;
            int quatro = 0, il_est = 0, limito = 0;
            string methode = "";
            while (completer)
            {
                limito = comparer_deux_mots((string)arf1[f2], (string)arv1[v2]);
                if (limito != -2)
                {
                    quatro = comparer_4_mots_apres(f2, v2, arf1, arv1);
                    if (quatro == 5)
                    {
                        longo = (((arv1.Count - v2 - 1) > 4) ? 4 : arv1.Count - v2 - 1);
                        tab = new string[longo];
                        for (int i = 0; i < longo; i++)
                            tab[i] = (string)arv1[v2 + i + 1];

                        il_est = ou_est_il((string)arf1[f2], tab);
                        if (il_est == 0)
                            if ((f2 + 1 <= exf2) && (f2 + 1 < arf1.Count))
                                f2++;
                            else
                            {
                                completer = false;
                                //faux2.Enqueue(f2);
                            }
                        else
                        {
                            methode = "ou_est_il";
                            break;

                        }
                    }
                    else
                    {
                        methode = "comparer_4_mots_apres";
                        break;
                    }
                }
                else
                {
                    methode = "comparer_deux_mots";

                    break;
                }
            }
            int check = 0;
            switch (methode)
            {
                case "comparer_deux_mots":
                    check = 100 + (limito + 2) * 10;
                    break;
                case "comparer_4_mots_apres":
                    check = 200 + quatro * 10;
                    break;
                case "ou_est_il":
                    check = 300 + il_est * 10;
                    break;
                default:
                    check = 400;
                    break;
            }
            check += (f2 - exf2 + 3);
            return check;


        }

        public int resultat_du_mot(int c, ref int ind_vrai, ref int ind_faux, Queue hi)
        {

            string s_check = c.ToString();
            switch (s_check[0])
            {
                case '1':

                    ind_faux += int.Parse(s_check[2].ToString());
                    int ma = int.Parse(s_check[1].ToString()) + int.Parse(s_check[2].ToString());
                    if (ma > 0)
                        for (int i = ma; i > 0; i--)
                            hi.Enqueue(ind_faux - i);
                    return 1;


                case '2':
                    ind_faux += int.Parse(s_check[2].ToString()) + int.Parse(s_check[1].ToString());
                    ind_vrai += int.Parse(s_check[1].ToString());
                    ma = int.Parse(s_check[1].ToString()) + int.Parse(s_check[2].ToString());
                    if (ma > 0)
                        for (int i = ma; i > 0; i--)
                            hi.Enqueue(ind_faux - i);
                    return 1;

                case '3':

                    ind_vrai += int.Parse(s_check[1].ToString());
                    ind_faux += int.Parse(s_check[2].ToString());
                    ma = int.Parse(s_check[2].ToString());
                    if (ma > 0)
                        for (int i = ma; i > 0; i--)
                            hi.Enqueue(ind_faux - i);
                    return 1;

                case '4':
                    hi.Enqueue(ind_faux);
                    if (int.Parse(s_check[2].ToString()) > 0)
                        hi.Enqueue(ind_faux + 1);
                    return 0;

                default: return 0;
            }
        }



        /************************Methods************************/

        /************************Color Change************************/

        private void timerR_Tick(object sender, EventArgs e)
        {
            if (b >= 185)
            {
                r -= 5;
                logPage.BackColor = Color.FromArgb(r, g, b);
                Frlev4.BackColor = Color.FromArgb(r, g, b);
                Frlev5.BackColor = Color.FromArgb(r, g, b);
                Malev4.BackColor = Color.FromArgb(r, g, b);
                Prolev1Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev2Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev3Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev4Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev5Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev1Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev2Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev3Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev4Ma.BackColor = Color.FromArgb(r, g, b);
                ImagesSon.BackColor = Color.FromArgb(r, g, b);
                if (r <= 40)
                {
                    timerR.Stop();
                    timerG.Start();
                }
            }

            if (b <= 40)
            {
                r += 5;
                logPage.BackColor = Color.FromArgb(r, g, b);
                Frlev4.BackColor = Color.FromArgb(r, g, b);
                Frlev5.BackColor = Color.FromArgb(r, g, b);
                Malev4.BackColor = Color.FromArgb(r, g, b);
                Prolev1Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev2Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev3Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev4Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev5Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev1Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev2Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev3Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev4Ma.BackColor = Color.FromArgb(r, g, b);
                ImagesSon.BackColor = Color.FromArgb(r, g, b);
                if (r >= 185)
                {
                    timerR.Stop();
                    timerG.Start();
                }
            }
        }

        private void timerG_Tick(object sender, EventArgs e)
        {
            if (r <= 40)
            {
                g += 5;
                logPage.BackColor = Color.FromArgb(r, g, b);
                Frlev4.BackColor = Color.FromArgb(r, g, b);
                Frlev5.BackColor = Color.FromArgb(r, g, b);
                Malev4.BackColor = Color.FromArgb(r, g, b);
                Prolev1Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev2Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev3Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev4Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev5Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev1Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev2Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev3Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev4Ma.BackColor = Color.FromArgb(r, g, b);
                ImagesSon.BackColor = Color.FromArgb(r, g, b);
                if (g >= 185)
                {
                    timerG.Stop();
                    timerB.Start();
                }
            }

            if (r >= 185)
            {
                g -= 5;
                logPage.BackColor = Color.FromArgb(r, g, b);
                Frlev4.BackColor = Color.FromArgb(r, g, b);
                Frlev5.BackColor = Color.FromArgb(r, g, b);
                Malev4.BackColor = Color.FromArgb(r, g, b);
                Prolev1Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev2Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev3Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev4Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev5Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev1Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev2Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev3Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev4Ma.BackColor = Color.FromArgb(r, g, b);
                ImagesSon.BackColor = Color.FromArgb(r, g, b);
                if (g <= 40)
                {
                    timerG.Stop();
                    timerB.Start();
                }
            }
        }

        private void timerB_Tick(object sender, EventArgs e)
        {
            if (g <= 40)
            {
                b += 5;
                logPage.BackColor = Color.FromArgb(r, g, b);
                Frlev4.BackColor = Color.FromArgb(r, g, b);
                Frlev5.BackColor = Color.FromArgb(r, g, b);
                Malev4.BackColor = Color.FromArgb(r, g, b);
                Prolev1Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev2Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev3Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev4Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev5Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev1Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev2Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev3Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev4Ma.BackColor = Color.FromArgb(r, g, b);
                ImagesSon.BackColor = Color.FromArgb(r, g, b);
                if (b >= 185)
                {
                    timerB.Stop();
                    timerR.Start();
                }
            }

            if (g >= 185)
            {
                b -= 5;
                logPage.BackColor = Color.FromArgb(r, g, b);
                Frlev4.BackColor = Color.FromArgb(r, g, b);
                Frlev5.BackColor = Color.FromArgb(r, g, b);
                Malev4.BackColor = Color.FromArgb(r, g, b);
                Prolev1Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev2Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev3Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev4Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev5Fr.BackColor = Color.FromArgb(r, g, b);
                Prolev1Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev2Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev3Ma.BackColor = Color.FromArgb(r, g, b);
                Prolev4Ma.BackColor = Color.FromArgb(r, g, b);
                ImagesSon.BackColor = Color.FromArgb(r, g, b);
                if (b <= 40)
                {
                    timerB.Stop();
                    timerR.Start();
                }
            }
        }
        /************************Color Change************************/

        private void Form1_Load(object sender, EventArgs e)
        {

           

            label34758679.Location = new Point((tabcontrol1.Width - label34758679.Width) / 2, (tabcontrol1.Height - panel12405612.Height) / 2 - label34758679.Height);
            panel12405612.Location = new Point((tabcontrol1.Width - panel12405612.Width) / 2, (tabcontrol1.Height - panel12405612.Height) / 2);
            panel21395419754.Location = new Point((tabcontrol1.Width - panel21395419754.Width) / 2,(tabcontrol1.Height-panel21395419754.Height)/2);
            label346365.Location = new Point((tabcontrol1.Width - label346365.Width) / 2);
            restart.Enabled = false;
            submit.Enabled = false;
            next.Enabled = false;
            label346365.Enabled = false;
            start.Enabled = false;
            panel21395419754.Visible = false;

            panel34.Visible = false;
            timerR.Start();//Start changing colors
            if (!File.Exists("UsersData.xml"))
            {
                DataSet ds = new DataSet("usersData");
                DataTable dt = new DataTable("users");
                dt.Columns.Add(new DataColumn("realName", typeof(string)));
                dt.Columns.Add(new DataColumn("userName", typeof(string)));
                dt.Columns.Add(new DataColumn("passWord", typeof(string)));
                dt.Columns.Add(new DataColumn("dadMail", typeof(string)));
                dt.Columns.Add(new DataColumn("dadPhone", typeof(string)));
                dt.Columns.Add(new DataColumn("recQuest", typeof(string)));
                dt.Columns.Add(new DataColumn("questAns", typeof(string)));
                dt.Columns.Add(new DataColumn("what", typeof(string)));
                dt.Rows.Add(new object[] { "realName", "userName", "passWord", "dadMail", "dadPhone", "recQuest", "questAns", "What" });




                /*DataColumn realName = new DataColumn();
                dt.Columns.Add(realName);
                DataColumn passWord = new DataColumn();
                dt.Columns.Add(passWord);
                DataColumn dadPhone = new DataColumn();
                dt.Columns.Add(dadPhone);
                DataColumn dadMail = new DataColumn();
                dt.Columns.Add(dadMail);
                DataColumn recQuestion = new DataColumn();
                dt.Columns.Add(recQuestion);
                DataColumn questAns = new DataColumn();
                dt.Columns.Add(questAns);
                DataColumn what = new DataColumn();
                dt.Columns.Add(what);
                ds.Tables.Add(dt);
                ds.AcceptChanges();*/
                ds.Tables.Add(dt);
                ds.WriteXml("UsersData.xml");
            }
            else
            {
                usersData.ReadXml("UsersData.xml");
            }
        }


        private void newUser_Click(object sender, EventArgs e)
        {
            pictureBox67.Visible = true;
            logPanel.Visible = false;
            signPanel.Visible = true;
        }

        private void signButton_Click(object sender, EventArgs e)
        {
            signup = true;
            if (signCombo.Text == "Prof") Prof = true;
            string[] t = { realName.Text, signUser.Text, signPass.Text, signPass1.Text, dadPhone.Text, dadMail.Text, questCombo.Text, questAns.Text, signCombo.Text };
            bool enter = true;
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] == "")
                {
                    label1.Text = "Please enter all informations";
                    enter = false;
                }
            }
            if (enter)
            {
                if (user.SignUp(t) == 1) label1.Text = "Username already exists";
                else
                {
                    if (user.SignUp(t) == 2) label1.Text = "Password doesn't match";
                    else tabcontrol1.SelectTab(choosePage);
                }
            }
        }

        private void resetPass_Click(object sender, EventArgs e)
        {
            string body = "";
            string encrypted = "";
            string toName = "";
            foreach (DataRow dr in usersData.Tables[0].Rows)
            {
                if (string.CompareOrdinal(dr[1].ToString(), logUser.Text) == 0)
                {
                    encrypted = dr[2].ToString();
                    toName = dr[0].ToString();
                }
            }
            body = "Dear" + toName.ToString() + ", \n you are very welcomed using our platform again, But unfortunately we need you to contact the school director and give him the encrypted password to decrypt it so you can login again \n your encrypted password is:" + encrypted.ToString() + "\n Regards";
            try
            {
                sendMailForgot(logUser.Text, body);
            }
            catch
            {
                MessageBox.Show("Mail not sent for unkown reason, Please try contacting school director");
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            if (logCombo.Text == "Prof") Prof = true;
            label12.Text = user.GetPoints(logUser.Text);
            label8.Text = user.GetPoints(logUser.Text);
            label9.Text = user.GetPoints(logUser.Text);
            label6.Text = user.GetPoints(logUser.Text);
            label14.Text = user.GetPoints(logUser.Text);
            label16.Text = user.GetPoints(logUser.Text);
            label18.Text = user.GetPoints(logUser.Text);
            label21.Text = user.GetPoints(logUser.Text);
            label27.Text = user.GetPoints(logUser.Text);
           

            bool enter = true;
            string[] t = { logUser.Text, logPass.Text, logCombo.Text };
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] == "")
                {
                    label2.Visible = true;
                    label2.Text = "Please enter all informations";
                    enter = false;
                }
            }
            if (enter)
            {
                if (user.IsUser(logUser.Text))
                {
                    if (user.RightPass(logUser.Text, logPass.Text))
                    {
                        if (user.StudProf(logUser.Text, logCombo.Text))
                        {
                            if (!Prof)
                            {
                                int Rank = user.Rank(logUser.Text);
                                label53.Visible = true;
                                label53.Text = "Rank #" + Rank.ToString();
                                tabcontrol1.SelectTab(choosePage);
                            }
                            else
                            {
                                label53.Visible = false;
                                tabcontrol1.SelectTab(choosePage);
                            }
                        }
                        else
                        {
                            label4.Visible = true;
                            label4.Text = "Check if you are student or prof";
                        }
                    }
                    else
                    {
                        label3.Visible = true;
                        label3.Text = "Wrong pass";
                    }
                }
                else
                {
                    label2.Visible = true;
                    label2.Text = "User not found";
                }
            }
        }
        private void logUser_Enter(object sender, EventArgs e)
        {
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
        }

        private void logPass_Enter(object sender, EventArgs e)
        {
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
        }

        private void logCombo_Enter(object sender, EventArgs e)
        {
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (!signup)
            {
                label12.Text = user.GetPoints(logUser.Text);
                switch (user.FGetLev(logUser.Text))
                {
                    case "1": Lev1butt.Enabled = true;
                        break;
                    case "2": Lev2butt.Enabled = true;
                        break;
                    case "3": Lev3butt.Enabled = true;
                        break;
                    case "4": Lev4butt.Enabled = true;
                        break;
                    case "5": Lev5butt.Enabled = true;
                        break;
                }
            }
            else
            {
                label12.Text = user.GetPoints(signUser.Text);
                switch (user.FGetLev(signUser.Text))
                {
                    case "1": Lev1butt.Enabled = true;
                        break;
                    case "2": Lev2butt.Enabled = true;
                        break;
                    case "3": Lev3butt.Enabled = true;
                        break;
                    case "4": Lev4butt.Enabled = true;
                        break;
                    case "5": Lev5butt.Enabled = true;
                        break;
                }
            }
            string[] quest;
            quest = Levels.GetLevels(1);
            quest1lev1.Text = quest[0];
            rad1quest1lev1.Text = quest[1];
            rad2quest1lev1.Text = quest[2];
            rad3quest1lev1.Text = quest[3];
            t[0] = quest[4];
            quest2lev1.Text = quest[5];
            rad1quest2lev1.Text = quest[6];
            rad2quest2lev1.Text = quest[7];
            rad3quest2lev1.Text = quest[8];
            t[1] = quest[9];
            quest3lev1.Text = quest[10];
            rad1quest3lev1.Text = quest[11];
            rad2quest3lev1.Text = quest[12];
            rad3quest3lev1.Text = quest[13];
            t[2] = quest[14];
            quest4lev1.Text = quest[15];
            rad1quest4lev1.Text = quest[16];
            rad2quest4lev1.Text = quest[17];
            rad3quest4lev1.Text = quest[18];
            t[3] = quest[19];
            tabcontrol1.SelectTab(Frlevels);  
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ExtraPoints = 0;
            french = true;
            animals.Add(pictureBox102);
            animals.Add(pictureBox107);
            animals.Add(pictureBox106);
            animals.Add(pictureBox114);
            vehicul.Add(pictureBox104);
            vehicul.Add(pictureBox105);
            vehicul.Add(pictureBox108);
            vehicul.Add(pictureBox113);
            technologie.Add(pictureBox99);
            technologie.Add(pictureBox112);
            technologie.Add(pictureBox111);
            technologie.Add(pictureBox109);
            batiment.Add(pictureBox100);
            batiment.Add(pictureBox101);
            batiment.Add(pictureBox110);
            batiment.Add(pictureBox103);
            foreach (PictureBox p in flowLayoutPanel2.Controls)
            {
                p.Enabled = true;
                panel101.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel3.Controls)
            {
                p1.Enabled = true;
                panel101.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel4.Controls)
            {
                p2.Enabled = true;
                panel101.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel5.Controls)
            {
                p3.Enabled = true;
                panel101.Controls.Add(p3);

            }
            flowLayoutPanel2.Location = new Point((tabcontrol1.Width - (2 * (flowLayoutPanel2.Width) + 450)) / 2, (tabcontrol1.Height - (2 * (flowLayoutPanel2.Height) + 250)) / 2);
            flowLayoutPanel3.Location = new Point(flowLayoutPanel2.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel2.Location.Y);
            flowLayoutPanel4.Location = new Point(flowLayoutPanel2.Location.X, flowLayoutPanel2.Location.Y + 150 + (flowLayoutPanel2.Height));
            flowLayoutPanel5.Location = new Point(flowLayoutPanel4.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel4.Location.Y);
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            label58.Visible = true;
            label59.Visible = true;
            label60.Visible = true;
            label61.Visible = true;
            if (!signup)
            {
                if (logCombo.Text == "Stud") tabcontrol1.SelectTab(FrCoursEx);
                else tabcontrol1.SelectTab(ProfFr);
            }
            else
            {
                if (signCombo.Text == "Stud") tabcontrol1.SelectTab(FrCoursEx);
                else tabcontrol1.SelectTab(ProfFr);
            }
        }

        private void Lev1butt_Click(object sender, EventArgs e)
        {

            if (!signup) label12.Text = user.GetPoints(logUser.Text);
            else label12.Text = user.GetPoints(signUser.Text);
            CleanLev(panel11, panel12, panel13, panel14);
            for(int i=0; i < ok.Length; i++)
            {
                ok[i] = false;
            }
            tabcontrol1.SelectTab(Frlev1);
        }

        void l_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            pb.AppendText(tableau[1]);
            speak.SpeakAsync(pb);
        }
        private void veriflev1_Click(object sender, EventArgs e)
        {
            int i = GetPoints(t, panel11) + GetPoints(t, panel12) + GetPoints(t, panel13) + GetPoints(t, panel14); ;
            if (!signup)
            {
                user.FChangeLev(logUser.Text, 2);
                count = int.Parse(user.GetPoints(logUser.Text)) + GetPoints(t, panel11) + GetPoints(t, panel12) + GetPoints(t, panel13) + GetPoints(t, panel14);
            }
            else
            {
                user.FChangeLev(signUser.Text, 2);
                count = int.Parse(user.GetPoints(signUser.Text)) + GetPoints(t, panel11) + GetPoints(t, panel12) + GetPoints(t, panel13) + GetPoints(t, panel14);
            }
            Lev1butt.Enabled = false;
            
            MessageBox.Show("Tu as collecté " +i.ToString() + " points", "SUPERBE!", MessageBoxButtons.OK);
            if (!signup)
            {
                 user.ChangePoints(logUser.Text, count);
                label12.Text = user.GetPoints(logUser.Text);
            }
            else
            {
                user.ChangePoints(signUser.Text, count);
                label12.Text = user.GetPoints(signUser.Text);
            }
            veriflev1.Visible = false;
            Avlev1.Location = veriflev1.Location;
            Avlev1.Size = veriflev1.Size;
            Avlev1.Visible = true;
            Avlev1.Enabled = true;
        }

        private void Avlev1_Click(object sender, EventArgs e)
        {
            if (!signup) label8.Text = user.GetPoints(logUser.Text);
            else label8.Text = user.GetPoints(signUser.Text);
            tabcontrol1.SelectTab(Frlev2);
            string[] quest;
            quest = Levels.GetLevels(2);
            quest1lev2.Text = quest[0];
            rad1quest1lev2.Text = quest[1];
            rad2quest1lev2.Text = quest[2];
            rad3quest1lev2.Text = quest[3];
            t[0] = quest[4];
            quest2lev2.Text = quest[5];
            rad1quest2lev2.Text = quest[6];
            rad2quest2lev2.Text = quest[7];
            rad3quest2lev2.Text = quest[8];
            t[1] = quest[9];
            quest3lev2.Text = quest[10];
            rad1quest3lev2.Text = quest[11];
            rad2quest3lev2.Text = quest[12];
            rad3quest3lev2.Text = quest[13];
            t[2] = quest[14];
            quest4lev2.Text = quest[15];
            rad1quest4lev2.Text = quest[16];
            rad2quest4lev2.Text = quest[17];
            rad3quest4lev2.Text = quest[18];
            t[3] = quest[19];

        }
        private void Présent_Click(object sender, EventArgs e)
        {
            player.Stop();
            tabControl2.SelectTab(Present);
            tabcontrol1.SelectTab(CoursChoisi);
            
           
        }

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            tabcontrol1.Width = this.Width + 50;
            tabcontrol1.Height = this.Height + 50;
            tabcontrol1.Location = new Point(-50, -50);
            label3.Location = label2.Location;
            label4.Location = label2.Location;
            logPage.Width = tabcontrol1.Width;
            logPage.Height = tabcontrol1.Height;
            logPanel.Location = new Point((tabcontrol1.Width - logPanel.Width) / 2, (tabcontrol1.Height - logPanel.Height) / 2);
            signPanel.Location = new Point((tabcontrol1.Width - signPanel.Width) / 2, (tabcontrol1.Height - signPanel.Height) / 2);
            panel1.Location = new Point((tabcontrol1.Width - panel1.Width) / 2, (tabcontrol1.Height - panel1.Height) / 2);
            panel2.Width = ((tabcontrol1.Width / 2) - 200); panel2.Height = tabcontrol1.Height;
            panel2.Location = new Point((tabcontrol1.Width - panel2.Width) / 2, (tabcontrol1.Height - panel2.Height) / 2);
            panel6.Location = new Point((panel2.Width - panel6.Width) / 2, (panel2.Height - panel6.Height) / 2);
            tableLayoutPanel1.Width = tabcontrol1.Width / 2 + 400;
            tableLayoutPanel1.Height = tabcontrol1.Height / 2 + 200;
            tableLayoutPanel1.Location = new Point((tabcontrol1.Width - tableLayoutPanel1.Width) / 2, (tabcontrol1.Height - tableLayoutPanel1.Height) / 2);
            panel9.Width = tableLayoutPanel1.Width / 2;
            panel9.Height = tableLayoutPanel1.Height / 2;
            veriflev1.Width = tableLayoutPanel1.Width - 200;
            veriflev1.Location = new Point((tabcontrol1.Width-veriflev1.Width)/2,(tabcontrol1.Height-tableLayoutPanel1.Height)/2+tableLayoutPanel1.Height+5);
            panel7.Width = tableLayoutPanel1.Width / 2;
            panel7.Height = tableLayoutPanel1.Height / 2;
            panel18.Width = tableLayoutPanel2.Width / 2;
            panel18.Height = tableLayoutPanel2.Height / 2;
            panel22.Location = new Point(panel7.Width - panel22.Width, 0);
            tableLayoutPanel2.Width = tabcontrol1.Width / 2 + 400;
            tableLayoutPanel2.Height = tabcontrol1.Height / 2 + 200;
            tableLayoutPanel2.Location = new Point((tabcontrol1.Width - tableLayoutPanel1.Width) / 2, (tabcontrol1.Height - tableLayoutPanel1.Height) / 2);
            panel18.Width = tableLayoutPanel2.Width / 2;
            panel18.Height = tableLayoutPanel2.Height / 2;
            panel4.Width = tableLayoutPanel2.Width / 2;
            panel4.Height = tableLayoutPanel2.Height / 2;
            veriflev2.Location = veriflev1.Location;
            veriflev2.Size = veriflev1.Size;
            panel23.Location = new Point(panel18.Width - panel23.Width, 0);
            panel10.Location = new Point((tabcontrol1.Width - panel10.Width) / 2, (tabcontrol1.Height - panel10.Height) / 2);
            tableLayoutPanel4.Width = tabcontrol1.Width / 2 + 400;
            tableLayoutPanel4.Height = tabcontrol1.Height / 2 + 200;
            tableLayoutPanel4.Location = new Point((tabcontrol1.Width - tableLayoutPanel4.Width) / 2, (tabcontrol1.Height - tableLayoutPanel4.Height) / 2);
            panel28.Width = tableLayoutPanel4.Width / 2;
            panel28.Height = tableLayoutPanel4.Height / 2;
            panel5.Width = tableLayoutPanel4.Width / 2;
            panel5.Height = tableLayoutPanel4.Height / 2;
            veriflev3.Location = veriflev1.Location;
            veriflev3.Size = veriflev1.Size;
            panel29.Location = new Point(panel28.Width - panel29.Width, 0);
            textBox4.Width = ((tabcontrol1.Width / 2) + 200);
            textBox4.Height = ((tabcontrol1.Height / 2));
            textBox5.Width = ((tabcontrol1.Width / 2) + 200);
            textBox5.Height = ((tabcontrol1.Height / 2));
            textBox4.Location = new Point((tabcontrol1.Width - textBox4.Width) / 2, (tabcontrol1.Height - textBox4.Height) / 2);
            textBox5.Location = new Point((tabcontrol1.Width - textBox5.Width) / 2, (tabcontrol1.Height - textBox5.Height) / 2);
            tableLayoutPanel3.Width = tabcontrol1.Width / 2 + 400;
            tableLayoutPanel3.Height = tabcontrol1.Height / 2 + 200;
            tableLayoutPanel3.Location = new Point((tabcontrol1.Width - tableLayoutPanel3.Width) / 2, (tabcontrol1.Height - tableLayoutPanel3.Height) / 2);
            tableLayoutPanel6.Width = tabcontrol1.Width / 2 + 500;
            tableLayoutPanel6.Height = tabcontrol1.Height / 2 + 200;
            tableLayoutPanel6.Location = new Point((tabcontrol1.Width - tableLayoutPanel6.Width) / 2, (tabcontrol1.Height - tableLayoutPanel6.Height) / 2);
            panel45.Width = tableLayoutPanel6.Width / 2;
            panel45.Height = tableLayoutPanel6.Height / 2;
            panel40.Width = tableLayoutPanel6.Width / 2;
            panel40.Height = tableLayoutPanel6.Height / 2;
            panel38.Width = tableLayoutPanel6.Width / 2;
            panel38.Height = tableLayoutPanel6.Height / 2;
            panel42.Width = tableLayoutPanel6.Width / 2;
            panel42.Height = tableLayoutPanel6.Height / 2;
            panel43.Location = new Point(panel42.Width - panel43.Width, 0);
            Mveriflev1.Location = veriflev1.Location;
            Mveriflev1.Size= veriflev1.Size;
            panel25.Location = new Point((tabcontrol1.Width - panel25.Width) / 2, (tabcontrol1.Height - panel25.Height) / 2);
            pictureBox70.Location = new Point(50, 40);
            pictureBox28.Location = pictureBox70.Location;
            pictureBox29.Location = pictureBox70.Location;
            pictureBox35.Location = pictureBox70.Location;
            pictureBox36.Location = pictureBox70.Location;
            pictureBox37.Location = pictureBox70.Location;
            pictureBox38.Location = pictureBox70.Location;
            pictureBox39.Location = pictureBox70.Location;
            pictureBox40.Location = pictureBox70.Location;
            pictureBox41.Location = pictureBox70.Location;
            pictureBox42.Location = pictureBox70.Location;
            pictureBox43.Location = pictureBox70.Location;
            pictureBox44.Location = pictureBox70.Location;
            pictureBox47.Location = pictureBox70.Location;
            pictureBox48.Location = pictureBox70.Location;
            pictureBox49.Location = pictureBox70.Location;
            pictureBox50.Location = pictureBox70.Location;
            pictureBox51.Location = pictureBox70.Location;
            pictureBox52.Location = pictureBox70.Location;
            pictureBox53.Location = pictureBox70.Location;
            pictureBox54.Location = pictureBox70.Location;
            pictureBox55.Location = pictureBox70.Location;
            pictureBox56.Location = pictureBox70.Location;
            pictureBox57.Location = pictureBox70.Location;
            pictureBox58.Location = pictureBox70.Location;
            pictureBox59.Location = pictureBox70.Location;
            pictureBox60.Location = pictureBox70.Location;
            pictureBox61.Location = pictureBox70.Location;
            pictureBox63.Location = pictureBox70.Location;
            pictureBox64.Location = pictureBox70.Location;
            pictureBox66.Location = pictureBox70.Location;
            pictureBox67.Location = pictureBox70.Location;
            pictureBox68.Location = pictureBox70.Location;
            pictureBox91.Location = pictureBox70.Location;
            pictureBox92.Location = pictureBox70.Location;
            pictureBox93.Location = pictureBox70.Location;
            pictureBox94.Location = pictureBox70.Location;
            pictureBox95.Location = pictureBox70.Location;
            pictureBox96.Location = pictureBox70.Location;
            pictureBox97.Location = pictureBox70.Location;
            pictureBox98.Location = pictureBox70.Location;
            pictureBox122.Location = pictureBox70.Location;
            pictureBox127.Location = pictureBox70.Location;
            pictureBox129.Location = pictureBox70.Location;
            pictureBox136.Location = pictureBox70.Location;
            panel33.Location = new Point((tabcontrol1.Width - panel33.Width) / 2, (tabcontrol1.Height - panel33.Height) / 2);
            panel35.Width = ((tabcontrol1.Width / 2) - 200); panel35.Height = tabcontrol1.Height;
            panel35.Location = new Point((tabcontrol1.Width - panel35.Width) / 2, (tabcontrol1.Height - panel35.Height) / 2);
            panel36.Location = new Point((panel35.Width - panel36.Width) / 2, (panel35.Height - panel36.Height) / 2);
            tableLayoutPanel5.Width = tabcontrol1.Width / 2 + 400;
            tableLayoutPanel5.Height = tabcontrol1.Height / 2 + 200;
            tableLayoutPanel5.Location = new Point((tabcontrol1.Width - tableLayoutPanel5.Width) / 2, (tabcontrol1.Height - tableLayoutPanel5.Height) / 2);
            panel37.Location = new Point((tabcontrol1.Width - panel37.Width) / 2, (tabcontrol1.Height - panel37.Height) / 2);
            panel100.Location = new Point((tabcontrol1.Width - panel100.Width) / 2, (tabcontrol1.Height - panel100.Height) / 2);
         
            textBox7.Width = ((tabcontrol1.Width / 2) + 200);
            textBox7.Height = ((tabcontrol1.Height / 2));
            textBox8.Width = ((tabcontrol1.Width / 2) + 200);
            textBox8.Height = ((tabcontrol1.Height / 2));
            textBox10.Width = ((tabcontrol1.Width / 2) + 200);
            textBox10.Height = ((tabcontrol1.Height / 2));
            textBox11.Width = ((tabcontrol1.Width / 2) + 200);
            textBox11.Height = ((tabcontrol1.Height / 2));
            textBox13.Width = ((tabcontrol1.Width / 2) + 200);
            textBox13.Height = ((tabcontrol1.Height / 2));
            textBox14.Width = ((tabcontrol1.Width / 2) + 200);
            textBox14.Height = ((tabcontrol1.Height / 2));
            textBox15.Width = ((tabcontrol1.Width / 2) + 200);
            textBox15.Height = ((tabcontrol1.Height / 2));
            textBox16.Width = ((tabcontrol1.Width / 2) + 200);
            textBox16.Height = ((tabcontrol1.Height / 2));
            textBox18.Width = ((tabcontrol1.Width / 2));
            textBox18.Height = ((tabcontrol1.Height / 2));
            textBox7.Location = new Point((tabcontrol1.Width - textBox7.Width) / 2, (tabcontrol1.Height - textBox7.Height) / 2);
            textBox8.Location = new Point((tabcontrol1.Width - textBox8.Width) / 2, (tabcontrol1.Height - textBox8.Height) / 2);
            textBox10.Location = new Point((tabcontrol1.Width - textBox10.Width) / 2, (tabcontrol1.Height - textBox10.Height) / 2);
            textBox11.Location = new Point((tabcontrol1.Width - textBox11.Width) / 2, (tabcontrol1.Height - textBox11.Height) / 2);
            textBox13.Location = new Point((tabcontrol1.Width - textBox13.Width) / 2, (tabcontrol1.Height - textBox13.Height) / 2);
            textBox14.Location = new Point((tabcontrol1.Width - textBox14.Width) / 2, (tabcontrol1.Height - textBox14.Height) / 2);
            textBox15.Location = new Point((tabcontrol1.Width - textBox15.Width) / 2, (tabcontrol1.Height - textBox15.Height) / 2);
            textBox16.Location = new Point((tabcontrol1.Width - textBox16.Width) / 2, (tabcontrol1.Height - textBox16.Height) / 2);
            textBox18.Location = new Point((tabcontrol1.Width - textBox18.Width) / 2-200, (tabcontrol1.Height - textBox18.Height) / 2-200);
            tableLayoutPanel7.Width = tabcontrol1.Width / 2 + 500;
            tableLayoutPanel7.Height = tabcontrol1.Height / 2 + 200;
            tableLayoutPanel7.Location = new Point((tabcontrol1.Width - tableLayoutPanel7.Width) / 2, (tabcontrol1.Height - tableLayoutPanel7.Height) / 2);
            panel51.Width = tableLayoutPanel7.Width / 2;
            panel51.Height = tableLayoutPanel7.Height / 2;
            panel47.Width = tableLayoutPanel7.Width / 2;
            panel47.Height = tableLayoutPanel7.Height / 2;
            panel54.Width = tableLayoutPanel7.Width / 2;
            panel54.Height = tableLayoutPanel7.Height / 2;
            panel49.Width = tableLayoutPanel7.Width / 2;
            panel49.Height = tableLayoutPanel7.Height / 2;
            panel52.Location = new Point(panel51.Width - panel52.Width, 0);
            Mveriflev2.Location = veriflev1.Location;
            Mveriflev2.Size = veriflev1.Size;
            tableLayoutPanel8.Width = tabcontrol1.Width / 2 + 500;
            tableLayoutPanel8.Height = tabcontrol1.Height / 2 + 200;
            tableLayoutPanel8.Location = new Point((tabcontrol1.Width - tableLayoutPanel8.Width) / 2, (tabcontrol1.Height - tableLayoutPanel8.Height) / 2);
            panel57.Width = tableLayoutPanel7.Width / 2;
            panel57.Height = tableLayoutPanel7.Height / 2;
            panel59.Width = tableLayoutPanel7.Width / 2;
            panel59.Height = tableLayoutPanel7.Height / 2;
            panel64.Width = tableLayoutPanel7.Width / 2;
            panel64.Height = tableLayoutPanel7.Height / 2;
            panel61.Width = tableLayoutPanel7.Width / 2;
            panel61.Height = tableLayoutPanel7.Height / 2;
            panel62.Location = new Point(panel61.Width - panel62.Width, 0);
            Mveriflev3.Location = veriflev1.Location;
            Mveriflev3.Size = veriflev1.Size;
            panel66.Location = new Point((tabcontrol1.Width - panel66.Width) / 2, (tabcontrol1.Height - panel66.Height) / 2);
            panel3.Width = tableLayoutPanel1.Width / 2;
            panel3.Height = tableLayoutPanel1.Height / 2;
            panel8.Width = tableLayoutPanel1.Width / 2;
            panel8.Height = tableLayoutPanel1.Height / 2;
            panel7.Width = tableLayoutPanel1.Width / 2;
            panel7.Height = tableLayoutPanel1.Height / 2;
            numb2.Location = numb1.Location;
            panel56.Height = tabcontrol1.Height / 2;
            panel56.Width = tabcontrol1.Width / 2;
            panel56.Location = new Point((tabcontrol1.Width - panel56.Width)/2, (tabcontrol1.Height - panel56.Height)/2);
            pictureBox69.Location = new Point((panel64.Width - pictureBox69.Width)/2, (panel64.Height - pictureBox69.Height)/2);
            pictureBox73.Location = new Point((panel61.Width - pictureBox73.Width)/2, (panel61.Height - pictureBox73.Height)/2);
            pictureBox72.Location = new Point((panel59.Width - pictureBox72.Width)/2, (panel59.Height - pictureBox72.Height)/2);
            pictureBox71.Location = new Point((panel57.Width - pictureBox71.Width)/2, (panel57.Height-pictureBox71.Height)/2);
            pictureBox74.Location = pictureBox69.Location;
            pictureBox77.Location = pictureBox69.Location;
            pictureBox76.Location = pictureBox69.Location;
            pictureBox75.Location = pictureBox71.Location;
            pictureBox78.Location = pictureBox69.Location;
            pictureBox80.Location = pictureBox69.Location;
            pictureBox81.Location = pictureBox69.Location;
            pictureBox79.Location = pictureBox71.Location;
            pictureBox82.Location = pictureBox69.Location;
            pictureBox83.Location = pictureBox69.Location;
            pictureBox85.Location = pictureBox69.Location;
            pictureBox84.Location = pictureBox71.Location;
            panel77.Location = new Point(panel56.Width - panel77.Width, (panel56.Height - panel77.Height)/2);
            panel76.Location = new Point(panel56.Width - panel76.Width, 0);
            panel78.Location = new Point(0, (panel56.Height - panel77.Height) / 2 + panel77.Height);
            panel79.Location = new Point((tabcontrol1.Width - panel79.Width) / 2, (tabcontrol1.Height - panel79.Height) / 2);
            panel82.Location = new Point((tabcontrol1.Width - panel82.Width) / 2, (tabcontrol1.Height - panel82.Height) / 2);
            panel81.Location = new Point((tabcontrol1.Width - panel81.Width) / 2, (tabcontrol1.Height - panel81.Height) / 2);
            panel84.Location = new Point((tabcontrol1.Width - panel84.Width) / 2, (tabcontrol1.Height - panel84.Height) / 2);
            panel91.Location = new Point((tabcontrol1.Width - panel91.Width) / 2, (tabcontrol1.Height - panel91.Height) / 2);
            panel92.Location = new Point((tabcontrol1.Width - panel92.Width) / 2, (tabcontrol1.Height - panel92.Height) / 2);
            panel94.Location = new Point((tabcontrol1.Width - panel94.Width) / 2, (tabcontrol1.Height - panel94.Height) / 2);
            panel96.Location = new Point((tabcontrol1.Width - panel96.Width) / 2, (tabcontrol1.Height - panel96.Height) / 2);
            panel98.Location = new Point((tabcontrol1.Width - panel98.Width) / 2, (tabcontrol1.Height - panel98.Height) / 2);
            pictureBox86.Location = pictureBox70.Location;
            pictureBox87.Location = pictureBox70.Location;
            pictureBox88.Location = pictureBox70.Location;
            pictureBox89.Location = pictureBox70.Location;
            pictureBox90.Location = pictureBox70.Location;
            //panel23.Location = new Point(panel18.Width - panel23.Width, 0);
            panel86.Location = new Point((tabcontrol1.Width - panel33.Width-panel86.Width) / 2, (tabcontrol1.Height - panel33.Height-panel86.Height) / 2);
            panel87.Location = panel84.Location;
            panel88.Width = ajCo.Width;
            panel88.Location = new Point((tabcontrol1.Width-panel88.Width)/2 , (tabcontrol1.Height - panel88.Height) / 2);
            panel89.Location = new Point(panel88.Location.X-panel89.Width-5, panel88.Location.Y);
            panel90.Location = new Point(panel88.Location.X +panel88.Width+5, panel88.Location.Y);
            ProfverifCo.Location = new Point((tabcontrol1.Width-ProfverifCo.Width)/2,panel88.Location.Y+panel88.Height+2);
            panel101.Width = tabcontrol1.Width;
            panel101.Height = tabcontrol1.Height;
            panel101.Location = new Point((tabcontrol1.Width - panel101.Width) / 2, (tabcontrol1.Height - panel101.Height) / 2);
            flowLayoutPanel2.Location = new Point((tabcontrol1.Width - (2 * (flowLayoutPanel2.Width) + 450)) / 2, (tabcontrol1.Height - (2 * (flowLayoutPanel2.Height) + 250)) / 2);
            flowLayoutPanel9.Location = flowLayoutPanel2.Location;
            flowLayoutPanel3.Location = new Point(flowLayoutPanel2.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel2.Location.Y);
            flowLayoutPanel8.Location = flowLayoutPanel3.Location;
            flowLayoutPanel4.Location = new Point(flowLayoutPanel2.Location.X, flowLayoutPanel2.Location.Y + 150 + (flowLayoutPanel2.Height));
            flowLayoutPanel7.Location = flowLayoutPanel4.Location;
            flowLayoutPanel5.Location = new Point(flowLayoutPanel4.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel4.Location.Y);
            flowLayoutPanel6.Location = flowLayoutPanel5.Location;
            label58.Location = new Point(flowLayoutPanel2.Location.X + (flowLayoutPanel2.Width - label58.Width) / 2, flowLayoutPanel2.Location.Y+flowLayoutPanel2.Height + 3);
            label57.Location = label58.Location;
            label59.Location = new Point(flowLayoutPanel3.Location.X + (flowLayoutPanel3.Width - label59.Width) / 2, flowLayoutPanel3.Location.Y + flowLayoutPanel2.Height + 3);
            label56.Location = label59.Location;
            label60.Location = new Point(flowLayoutPanel4.Location.X + (flowLayoutPanel4.Width - label60.Width) / 2, flowLayoutPanel4.Location.Y + flowLayoutPanel2.Height + 3);
            label55.Location = label60.Location;
            label61.Location = new Point(flowLayoutPanel5.Location.X + (flowLayoutPanel5.Width - label61.Width) / 2, flowLayoutPanel5.Location.Y + flowLayoutPanel2.Height + 3);
            label54.Location = label61.Location;
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox138.Location = pictureBox100.Location;
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox139.Location = pictureBox110.Location;
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox140.Location = pictureBox105.Location;
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox141.Location = pictureBox108.Location;
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y+pictureBox100.Height+2);
            pictureBox142.Location = pictureBox103.Location;
            pictureBox109.Location = new Point(pictureBox103.Location.X+pictureBox103.Width+2, pictureBox103.Location.Y);
            pictureBox143.Location = pictureBox109.Location;
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 +pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox144.Location = pictureBox101.Location;
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2+ pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox145.Location = pictureBox102.Location;
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y+pictureBox100.Height+2);
            pictureBox146.Location = pictureBox104.Location;
            pictureBox107.Location = new Point(pictureBox104.Location.X+ pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox147.Location = pictureBox107.Location;
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox148.Location = pictureBox106.Location;
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width+ 2, pictureBox106.Location.Y);
            pictureBox149.Location = pictureBox111.Location;
            pictureBox114.Location = new Point(pictureBox100.Location.X , pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox150.Location = pictureBox114.Location;
            pictureBox113.Location = new Point(pictureBox114.Location.X+pictureBox100.Width+2, pictureBox114.Location.Y);
            pictureBox151.Location = pictureBox113.Location;
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox152.Location = pictureBox112.Location;
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            pictureBox153.Location = pictureBox99.Location;
            label50.Location = new Point((tabcontrol1.Width - label50.Width) / 2, flowLayoutPanel4.Location.Y + flowLayoutPanel4.Height);
            label62.Location = label50.Location;

            pictureBox115.Location = pictureBox70.Location;
            pictureBox120.Width = tabcontrol1.Width / 3;pictureBox120.Height = tabcontrol1.Height / 2;
            pictureBox119.Width = tabcontrol1.Width / 3; pictureBox119.Height = tabcontrol1.Height / 2;
            pictureBox120.Location = new Point((tabcontrol1.Width - 2 * (pictureBox120.Width) - 50) / 2, (tabcontrol1.Height - pictureBox120.Height) / 2);
            pictureBox119.Location = new Point(pictureBox120.Location.X + pictureBox120.Width + 50, pictureBox120.Location.Y);
            pictureBox116.Width = tabcontrol1.Width / 2;pictureBox116.Height = tabcontrol1.Height / 2;
            pictureBox116.Location =new Point ((tabcontrol1.Width - pictureBox116.Width) / 2, (tabcontrol1.Height - pictureBox116.Height) / 2);
            pictureBox117.Width = tabcontrol1.Width / 2; pictureBox117.Height = tabcontrol1.Height / 2;
            pictureBox117.Location = new Point((tabcontrol1.Width - pictureBox117.Width) / 2, (tabcontrol1.Height - pictureBox117.Height) / 2);
            pictureBox118.Width = tabcontrol1.Width / 2; pictureBox118.Height = tabcontrol1.Height / 2;
            pictureBox118.Location = new Point((tabcontrol1.Width - pictureBox118.Width) / 2, (tabcontrol1.Height - pictureBox118.Height) / 2);
            pictureBox121.Width = tabcontrol1.Width / 2; pictureBox121.Height = tabcontrol1.Height / 2;
            pictureBox121.Location = new Point((tabcontrol1.Width - pictureBox121.Width) / 2, (tabcontrol1.Height - pictureBox121.Height) / 2);
            pictureBox126.Width = tabcontrol1.Width / 2; pictureBox126.Height = tabcontrol1.Height / 2;
            pictureBox126.Location = new Point((tabcontrol1.Width - pictureBox126.Width) / 2, (tabcontrol1.Height - pictureBox126.Height) / 2);
            pictureBox123.Width = tabcontrol1.Width / 2; pictureBox123.Height = tabcontrol1.Height / 2+100;
            pictureBox123.Location = new Point((tabcontrol1.Width - pictureBox123.Width) / 2, (tabcontrol1.Height - pictureBox123.Height) / 2);
            pictureBox124.Width = tabcontrol1.Width / 2; pictureBox124.Height = tabcontrol1.Height / 2;
            pictureBox124.Location = new Point((tabcontrol1.Width - pictureBox124.Width) / 2, (tabcontrol1.Height - pictureBox124.Height) / 2);
            pictureBox125.Width = tabcontrol1.Width / 2+150; pictureBox125.Height = tabcontrol1.Height / 2+150;
            pictureBox125.Location = new Point((tabcontrol1.Width - pictureBox125.Width) / 2, (tabcontrol1.Height - pictureBox125.Height) / 2);

            panel86.Location = new Point(panel88.Location.X + panel88.Width + 100, panel88.Location.Y - 100);
            panel104.Location = new Point(panel66.Location.X + panel66.Width + 100, panel66.Location.Y - 100);

            textBox1.Width = textBox5.Width;
            textBox1.Height = textBox5.Height;
            textBox1.Location = textBox5.Location;
            pictureBox128.Location = pictureBox29.Location;
            panel103.Location = new Point((tabcontrol1.Width - panel103.Width) / 2, (tabcontrol1.Height - panel103.Height) / 2);
            xButton1.Location = new Point(panel1.Location.X, panel1.Location.Y + panel1.Height + 5);
            xButton2.Width = textBox7.Width;
            xButton2.Location = new Point(textBox7.Location.X, textBox7.Location.Y + textBox7.Height + 5);

            button7.Location = new Point(panel33.Location.X, panel33.Location.Y + panel33.Height + 10);
            label53.Location = new Point(tabcontrol1.Width - label53.Width-50, 50);


            pictureBox6.Location = new Point(textBox18.Location.X + textBox18.Width + 200, textBox18.Location.Y);
            pictureBox131.Location = new Point(pictureBox6.Location.X, pictureBox6.Location.Y + pictureBox6.Height + 10);
            pictureBox132.Location = new Point(pictureBox6.Location.X, pictureBox131.Location.Y + pictureBox131.Height + 10);
            pictureBox134.Location = new Point(textBox18.Location.X-100, textBox18.Location.Y + textBox18.Height + 100);
            pictureBox135.Location = new Point(pictureBox134.Location.X+pictureBox134.Width+10,pictureBox134.Location.Y);
            pictureBox133.Location = new Point(pictureBox135.Location.X + pictureBox135.Width + 10, pictureBox135.Location.Y);


        }


        private void PasséSimple_Click(object sender, EventArgs e)
        {
            player.Stop();
            tabControl2.SelectTab(Passe);
            tabcontrol1.SelectTab(CoursChoisi);
            
           
        }

        private void FuturSimple_Click(object sender, EventArgs e)
        {
            player.Stop();
            tabControl2.SelectTab(Futur);
            tabcontrol1.SelectTab(CoursChoisi);
            
          
        }

        private void Images_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursChoisi);
            tabControl2.SelectTab(ImagesSon);
            player.Stop();
        }

        private void pictureBox70_Click(object sender, EventArgs e)
        {
            player.Stop();
            tabcontrol1.SelectTab(FrCoursEx);
        }

        private void pictureBox35_Click_1(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
            speak.SpeakAsyncCancelAll();
        }

        private void pictureBox36_Click_1(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {
            if (!Prof)
            {
                if (!signup)
                {
                    int Rank = user.Rank(logUser.Text);
                    label53.Visible = true;
                    label53.Text = "Rank #" + Rank.ToString();
                    tabcontrol1.SelectTab(choosePage);
                }
                else
                {
                    tabcontrol1.SelectTab(choosePage);
                }
            }
            else
            {
                tabcontrol1.SelectTab(choosePage);
            }
        }

        private void pictureBox38_Click_1(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(FrCoursEx);
        }

        private void pictureBox39_Click_1(object sender, EventArgs e)
        {
            if (!signup)
            {
                label12.Text = user.GetPoints(logUser.Text);
                switch (user.FGetLev(logUser.Text))
                {
                    case "1": Lev1butt.Enabled = true;
                        break;
                    case "2": Lev2butt.Enabled = true;
                        break;
                    case "3": Lev3butt.Enabled = true;
                        break;
                    case "4": Lev4butt.Enabled = true;
                        break;
                    case "5": Lev5butt.Enabled = true;
                        break;
                }
            }
            else
            {
                label12.Text = user.GetPoints(signUser.Text);
                switch (user.FGetLev(signUser.Text))
                {
                    case "1": Lev1butt.Enabled = true;
                        break;
                    case "2": Lev2butt.Enabled = true;
                        break;
                    case "3": Lev3butt.Enabled = true;
                        break;
                    case "4": Lev4butt.Enabled = true;
                        break;
                    case "5": Lev5butt.Enabled = true;
                        break;
                }
            }
            tabcontrol1.SelectTab(Frlevels);
        }

        private void pictureBox40_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void pictureBox41_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void MasculinFéminin_Click(object sender, EventArgs e)
        {
            player.Stop();
            tabControl2.SelectTab(MascFémin);
            tabcontrol1.SelectTab(CoursChoisi);
            
            StreamReader fr = new StreamReader("MasculinsFéminins.txt");
            textBox5.ScrollBars = ScrollBars.Both;
            textBox5.Text = fr.ReadToEnd();
            textBox5.ReadOnly = true;
        }

        private void Couleur_Click(object sender, EventArgs e)
        {
            player.Stop();
            tabControl2.SelectTab(Couleurs);
            tabcontrol1.SelectTab(CoursChoisi);
            
            
        }

        private void pictureBox42_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void Son_Click(object sender, EventArgs e)
        {
            player.Play();
        }

        private void veriflev2_Click(object sender, EventArgs e)
        {
            int i = GetPoints(t, panel21) + GetPoints(t, panel19) + GetPoints(t, panel17) + GetPoints(t, panel15);
            if (!signup)
            {
                user.FChangeLev(logUser.Text, 3);
                count = int.Parse(user.GetPoints(logUser.Text)) + GetPoints(t, panel21) + GetPoints(t, panel19) + GetPoints(t, panel17) + GetPoints(t, panel15);
            }
            else
            {
                user.FChangeLev(signUser.Text, 3);
                count = int.Parse(user.GetPoints(signUser.Text)) + GetPoints(t, panel21) + GetPoints(t, panel19) + GetPoints(t, panel17) + GetPoints(t, panel15);
            }
            Lev2butt.Enabled = false;

            MessageBox.Show("Tu as collecté " + i.ToString() + " points", "SUPERBE!", MessageBoxButtons.OK);
            if (!signup)
            {
                user.ChangePoints(logUser.Text, count);
                label8.Text = user.GetPoints(logUser.Text);  
            }
            else
            {
                user.ChangePoints(signUser.Text, count);
                label8.Text = user.GetPoints(signUser.Text);
            }
            Avlev2.Location = veriflev2.Location;
            veriflev2.Visible = false;
            Avlev2.Size = veriflev2.Size;
            Avlev2.Visible = true;
            Avlev2.Enabled = true;
        }

        private void veriflev3_Click(object sender, EventArgs e)
        {
            int i = GetPoints(t, panel32) + GetPoints(t, panel30) + GetPoints(t, panel27) + GetPoints(t, panel24);
            if (!signup)
            {
                user.FChangeLev(logUser.Text, 4);
                count = int.Parse(user.GetPoints(logUser.Text)) + GetPoints(t, panel32) + GetPoints(t, panel30) + GetPoints(t, panel27) + GetPoints(t, panel24);
            }
            else
            {
                user.FChangeLev(signUser.Text, 4);
                count = int.Parse(user.GetPoints(signUser.Text)) + GetPoints(t, panel32) + GetPoints(t, panel30) + GetPoints(t, panel27) + GetPoints(t, panel24);
            }
            Lev3butt.Enabled = false;

            MessageBox.Show("Tu as collecté " + i.ToString() + " points", "SUPERBE!", MessageBoxButtons.OK);
            if (!signup)
            {
                user.ChangePoints(logUser.Text, count);
                label9.Text = user.GetPoints(logUser.Text);
                
            }
            else
            {
                user.ChangePoints(signUser.Text, count);
                label9.Text = user.GetPoints(signUser.Text);
                
            }
            Avlev3.Location = veriflev3.Location;
            veriflev3.Visible = false;
            Avlev3.Size = veriflev3.Size;
            Avlev3.Visible = true;
            Avlev3.Enabled = true;
        }

        private void playDict_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == "") MessageBox.Show("Il faut choisir le texte pour la dictée");
            else
            {
                i = 0;
                Dicte.Start();
                nbfois = (int)numericUpDownRep.Value;
                corDict.Enabled = true;
            }
        }

        private void Lev4butt_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            numericUpDownRep.Visible = true;
            tabcontrol1.SelectTab(Frlev4);
        }

        private void corDict_Click(object sender, EventArgs e)
        {
            Dicte.Stop();
            button7.Visible = true;
            if (comboBox1.Text != "")
            {
                panel34.Visible = true;
                label17.Visible = false;
            }
            numericUpDownRep.Visible = false;
            try
            {
                Dicte.Stop();
            }
            catch { Dicte.Stop(); }//Stop talking

            /*******************************************Writing xml and dataset of right words***************************/
            string direction = "textes//" + comboBox1.Text + ".txt";
            try {
                tab1_vrai_ar = split_fichier_sans(direction);
                DataSet vrai_dataset = new DataSet();
                DataTable dt = new DataTable();
                vrai_dataset.Clear();
                dt.Columns.Add(new DataColumn("Mots", typeof(string)));
                for (int ind = 0; ind < tab1_vrai_ar.Count; ind++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = tab1_vrai_ar[ind];
                    dt.Rows.Add(dr);
                }
                vrai_dataset.Tables.Add(dt);
                //get dataset of name vrai_dataset and xml texte_vrai.xml
                /*******************************************Writing xml and dataset of right words***************************/

                /*******************************************Writing xml and dataset of Wrong words***************************/
                tab1_faux_ar = split_textbox_sans(richTextBox1); // Get an array of written words name tab_faux_ar

                DataSet textefaux_dataset = new DataSet();
                DataTable dt1 = new DataTable();
                textefaux_dataset.Clear();
                dt1.Columns.Add(new DataColumn("Mots", typeof(string)));
                for (int ind = 0; ind < tab1_faux_ar.Count; ind++)
                {
                    DataRow dr = dt1.NewRow();
                    dr[0] = tab1_faux_ar[ind];
                    dt1.Rows.Add(dr);
                }
                textefaux_dataset.Tables.Add(dt1);
                /*******************************************Writing xml and dataset of wrong words***************************/


                /*******************************************Calcul***************************/
                //VARIABLES
                int score_tab1, vivo, fifo;
                int res;
                Queue qoqo = new Queue();

                //AU TRAVAIL!!
                vivo = 0; fifo = 0; score_tab1 = 0;
                while (fifo < tab1_faux_ar.Count)
                {
                    try

                    { res = check_mot(vivo, fifo, tab1_faux_ar, tab1_vrai_ar); }

                    catch { break; }

                    if (res / 100 == 4)
                    {
                        score_tab1 += resultat_du_mot(res, ref vivo, ref fifo, qoqo);
                        break;
                    }
                    score_tab1 += resultat_du_mot(res, ref vivo, ref fifo, qoqo);
                    vivo++;
                    fifo++;
                }

                // test of words  score egate a score_tab1.
                int counto = qoqo.Count;              //qoqo est un queue
                ArrayList tablo = new ArrayList(counto);     // initiliaze arraylist with queue capacity
                for (int i = 0; i < counto; i++)
                    tablo.Add(int.Parse(qoqo.Dequeue().ToString()));// fill arraylist with index of wrong words
                                                                    /*******************************************Calcul***************************/


                /*******************************************Writing xml and index of wrong words***************************/
                DataSet fautes_dataset = new DataSet();
                fautes_dataset.Clear();
                DataTable dt2 = new DataTable();
                dt2.Columns.Add(new DataColumn("Mots", typeof(string)));
                for (int ind = 0; ind < tablo.Count; ind++)
                {
                    DataRow dr = dt2.NewRow();
                    dr[0] = tablo[ind];
                    dt2.Rows.Add(dr);
                    fautes_dataset.WriteXml(@"..\\..\\fautes.xml");
                }
                fautes_dataset.Tables.Add(dt2);
                /*******************************************Writing xml and index of wrong words***************************/
                float soso = (((float)score_tab1 / tab1_vrai_ar.Count) * 100)/12.5f;




                /***********************************Coreeabdjhakbdwakba,d********************************************/
                int j = 0, long_tot = 0, l = 0;
                string mot;
                while (l < textefaux_dataset.Tables[0].Rows.Count)
                {
                    mot = textefaux_dataset.Tables[0].Rows[l][0].ToString();
                    richTextBox2.Text += mot + " ";
                    l++;
                }
                l = 0;
                while (l < textefaux_dataset.Tables[0].Rows.Count)
                {

                    mot = textefaux_dataset.Tables[0].Rows[l][0].ToString();
                    if ((j < fautes_dataset.Tables[0].Rows.Count) && (fautes_dataset.Tables[0].Rows[0][0].ToString() != "") && (l == int.Parse(fautes_dataset.Tables[0].Rows[j][0].ToString())))
                    {
                        richTextBox2.Select(long_tot, mot.Length);
                        richTextBox2.SelectionFont = fa;
                        richTextBox2.SelectionColor = Color.Red;
                        j++;
                    }
                    long_tot += mot.Length + 1;
                    l++;
                }
                int v = 0;
                while (v < vrai_dataset.Tables[0].Rows.Count)
                {
                    richTextBox3.Text += " " + vrai_dataset.Tables[0].Rows[v][0];
                    v++;
                }
                /***********************************Coreeabdjhakbdwakba,d********************************************/



                /************************************Change lev and points************************************/

                if (!signup)
                {
                    user.FChangeLev(logUser.Text, 5);
                    count = int.Parse(user.GetPoints(logUser.Text)) + (int)soso;
                }
                else
                {
                    user.FChangeLev(signUser.Text, 5);
                    count = int.Parse(user.GetPoints(signUser.Text)) + (int)soso;
                }
                Lev4butt.Enabled = false;
                MessageBox.Show("Tu as collecté " + ((int)soso).ToString() + " points", "SUPERBE!", MessageBoxButtons.OK);
                if (!signup)
                {
                    user.ChangePoints(logUser.Text, count);
                    label6.Text = user.GetPoints(logUser.Text);
                }
                else
                {
                    user.ChangePoints(signUser.Text, count);
                    label6.Text = user.GetPoints(signUser.Text);
                }
                /************************************Change lev and points************************************/
            }
            catch
            {
                MessageBox.Show("Il faut choisir le texte pour la dictée");
            }
            }

        private void Stop_Click(object sender, EventArgs e)
        {
            i = 0;
            speak.SpeakAsyncCancelAll();
            Dicte.Stop();
        }
        

        private void Dicte_Tick(object sender, EventArgs e)
        {
            speak.Rate = -2;
            pb.Culture = new CultureInfo("fr-FR");
            string direction = "textes//" + comboBox1.Text + ".txt";
            try
            {
                StreamReader sr1 = new StreamReader(direction);
                string text = sr1.ReadToEnd();
                string[] words = text.Split('\n');
                Dicte.Interval = ((int)numericUpDownAtt.Value + 4) * 1000;
                if (nbfois != 0)
                {
                    if (i < words.Length)
                    {
                        pb.ClearContent();
                        pb.AppendText(words[i].ToString());
                        speak.SpeakAsync(pb);
                    }
                    else
                    {
                        Dicte.Stop();
                    }
                    nbfois--;
                }
                else
                {
                    nbfois = (int)numericUpDownRep.Value;
                    if (i < words.Length)
                    {
                        i++;
                    }
                    else
                    {
                        Dicte.Stop();
                    }
                }
            }
            catch { }
        }

        private void Avlev2_Click(object sender, EventArgs e)
        {
            if (!signup) label9.Text = user.GetPoints(logUser.Text);
            else label9.Text = user.GetPoints(signUser.Text);
            tabcontrol1.SelectTab(Frlev3);
            string[] quest;
            quest = Levels.GetLevels(3);
            quest1lev3.Text = quest[0];
            rad1quest1lev3.Text = quest[1];
            rad2quest1lev3.Text = quest[2];
            rad3quest1lev3.Text = quest[3];
            t[0] = quest[4];
            quest2lev3.Text = quest[5];
            rad1quest2lev3.Text = quest[6];
            rad2quest2lev3.Text = quest[7];
            rad3quest2lev3.Text = quest[8];
            t[1] = quest[9];
            quest3lev3.Text = quest[10];
            rad1quest3lev3.Text = quest[11];
            rad2quest3lev3.Text = quest[12];
            rad3quest3lev3.Text = quest[13];
            t[2] = quest[14];
            quest4lev3.Text = quest[15];
            rad1quest4lev3.Text = quest[16];
            rad2quest4lev3.Text = quest[17];
            rad3quest4lev3.Text = quest[18];
            t[3] = quest[19];
        }

        private void Lev3butt_Click(object sender, EventArgs e)
        {
            if (!signup) label9.Text = user.GetPoints(logUser.Text);
            else label9.Text = user.GetPoints(signUser.Text);
            CleanLev(panel32, panel30, panel27, panel24);
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = false;
            }
            tabcontrol1.SelectTab(Frlev3);
            string[] quest;
            quest = Levels.GetLevels(3);
            quest1lev3.Text = quest[0];
            rad1quest1lev3.Text = quest[1];
            rad2quest1lev3.Text = quest[2];
            rad3quest1lev3.Text = quest[3];
            t[0] = quest[4];
            quest2lev3.Text = quest[5];
            rad1quest2lev3.Text = quest[6];
            rad2quest2lev3.Text = quest[7];
            rad3quest2lev3.Text = quest[8];
            t[1] = quest[9];
            quest3lev3.Text = quest[10];
            rad1quest3lev3.Text = quest[11];
            rad2quest3lev3.Text = quest[12];
            rad3quest3lev3.Text = quest[13];
            t[2] = quest[14];
            quest4lev3.Text = quest[15];
            rad1quest4lev3.Text = quest[16];
            rad2quest4lev3.Text = quest[17];
            rad3quest4lev3.Text = quest[18];
            t[3] = quest[19];
        }

        private void pictureBox43_Click(object sender, EventArgs e)
        {
            if (!signup)
            {
                label12.Text = user.GetPoints(logUser.Text);
                switch (user.FGetLev(logUser.Text))
                {
                    case "1": Lev1butt.Enabled = true;
                        break;
                    case "2": Lev2butt.Enabled = true;
                        break;
                    case "3": Lev3butt.Enabled = true;
                        break;
                    case "4": Lev4butt.Enabled = true;
                        break;
                    case "5": Lev5butt.Enabled = true;
                        break;
                }
            }
            else
            {
                label12.Text = user.GetPoints(signUser.Text);
                switch (user.FGetLev(signUser.Text))
                {
                    case "1": Lev1butt.Enabled = true;
                        break;
                    case "2": Lev2butt.Enabled = true;
                        break;
                    case "3": Lev3butt.Enabled = true;
                        break;
                    case "4": Lev4butt.Enabled = true;
                        break;
                    case "5": Lev5butt.Enabled = true;
                        break;
                }
            }
            tabcontrol1.SelectTab(Frlevels);
        }

        private void pictureBox44_Click(object sender, EventArgs e)
        {
             if (!signup)
            {
                label12.Text = user.GetPoints(logUser.Text);
                switch (user.FGetLev(logUser.Text))
                {
                    case "1": Lev1butt.Enabled = true;
                        break;
                    case "2": Lev2butt.Enabled = true;
                        break;
                    case "3": Lev3butt.Enabled = true;
                        break;
                    case "4": Lev4butt.Enabled = true;
                        break;
                    case "5": Lev5butt.Enabled = true;
                        break;
                }
            }
            else
            {
                label12.Text = user.GetPoints(signUser.Text);
                switch (user.FGetLev(signUser.Text))
                {
                    case "1": Lev1butt.Enabled = true;
                        break;
                    case "2": Lev2butt.Enabled = true;
                        break;
                    case "3": Lev3butt.Enabled = true;
                        break;
                    case "4": Lev4butt.Enabled = true;
                        break;
                    case "5": Lev5butt.Enabled = true;
                        break;
                }}
            tabcontrol1.SelectTab(Frlevels);
        }

        private void SingulierPluriel_Click(object sender, EventArgs e)
        {
            player.Stop();
            SingPlur.BackColor = Color.FromArgb(69, 163, 229);
            tabcontrol1.SelectTab(CoursChoisi);
            tabControl2.SelectTab(SingPlur);
            StreamReader fr = new StreamReader("SingulierPluriel.txt");
            textBox4.ScrollBars = ScrollBars.Both;
            textBox4.Text = fr.ReadToEnd();
            textBox4.ReadOnly = true;
        }

        private void Lev2butt_Click(object sender, EventArgs e)
        {
            if (!signup) label8.Text = user.GetPoints(logUser.Text);
            else label8.Text = user.GetPoints(signUser.Text);
            CleanLev(panel21, panel19, panel17, panel15);
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = false;
            }
            tabcontrol1.SelectTab(Frlev2);
            string[] quest;
            quest = Levels.GetLevels(2);
            quest1lev2.Text = quest[0];
            rad1quest1lev2.Text = quest[1];
            rad2quest1lev2.Text = quest[2];
            rad3quest1lev2.Text = quest[3];
            t[0] = quest[4];
            quest2lev2.Text = quest[5];
            rad1quest2lev2.Text = quest[6];
            rad2quest2lev2.Text = quest[7];
            rad3quest2lev2.Text = quest[8];
            t[1] = quest[9];
            quest3lev2.Text = quest[10];
            rad1quest3lev2.Text = quest[11];
            rad2quest3lev2.Text = quest[12];
            rad3quest3lev2.Text = quest[13];
            t[2] = quest[14];
            quest4lev2.Text = quest[15];
            rad1quest4lev2.Text = quest[16];
            rad2quest4lev2.Text = quest[17];
            rad3quest4lev2.Text = quest[18];
            t[3] = quest[19];



        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ExtraPoints = 0;
            french = false;
            cercle.Add(pictureBox143);
            cercle.Add(pictureBox138);
            cercle.Add(pictureBox142);
            cercle.Add(pictureBox140);
            triangle.Add(pictureBox147);
            triangle.Add(pictureBox146);
            triangle.Add(pictureBox144);
            triangle.Add(pictureBox153);
            rectangle.Add(pictureBox141);
            rectangle.Add(pictureBox150);
            rectangle.Add(pictureBox139);
            rectangle.Add(pictureBox148);
            autreforme.Add(pictureBox145);
            autreforme.Add(pictureBox149);
            autreforme.Add(pictureBox151);
            autreforme.Add(pictureBox152);

            foreach (PictureBox p in flowLayoutPanel9.Controls)
            {
                p.Enabled = true;
                panel102.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel8.Controls)
            {
                p1.Enabled = true;
                panel102.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel7.Controls)
            {
                p2.Enabled = true;
                panel102.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel6.Controls)
            {
                p3.Enabled = true;
                panel102.Controls.Add(p3);

            }
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox138.Location = pictureBox100.Location;
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox139.Location = pictureBox110.Location;
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox140.Location = pictureBox105.Location;
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox141.Location = pictureBox108.Location;
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox142.Location = pictureBox103.Location;
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox143.Location = pictureBox109.Location;
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox144.Location = pictureBox101.Location;
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox145.Location = pictureBox102.Location;
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox146.Location = pictureBox104.Location;
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox147.Location = pictureBox107.Location;
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox148.Location = pictureBox106.Location;
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox149.Location = pictureBox111.Location;
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox150.Location = pictureBox114.Location;
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox151.Location = pictureBox113.Location;
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox152.Location = pictureBox112.Location;
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            pictureBox153.Location = pictureBox99.Location;
            label57.Visible = true;
            label56.Visible = true;
            label55.Visible = true;
            label54.Visible = true;





            if (!signup)
            {
                if (logCombo.Text == "Stud") tabcontrol1.SelectTab(MaCoursEx);
                else tabcontrol1.SelectTab(ProfMa);
            }
            else
            {
                if (signCombo.Text == "Stud") tabcontrol1.SelectTab(MaCoursEx);
                else tabcontrol1.SelectTab(ProfMa);
            }
        }
            private void pictureBox47_Click(object sender, EventArgs e)
        {
            if (!Prof)
            {
                if (!signup)
                {
                    int Rank = user.Rank(logUser.Text);
                    label53.Visible = true;
                    label53.Text = "Rank #" + Rank.ToString();
                    tabcontrol1.SelectTab(choosePage);
                }
                else
                {
                    tabcontrol1.SelectTab(choosePage);
                }
            }
            else
            {
                tabcontrol1.SelectTab(choosePage);
            }
        }

        private void pictureBox48_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(MaCoursEx);
        }

        private void CoursFr_Click(object sender, EventArgs e)
        {

        }

        private void EcritureDeNombre_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(EcriDeNomb);
            tabcontrol1.SelectTab(CoursMaChoisi);
           
            StreamReader fr = new StreamReader("EcritureNombres.txt");
            textBox7.ScrollBars = ScrollBars.Both;
            textBox7.Text = fr.ReadToEnd();
            textBox7.ReadOnly = true;
        }

        private void Complementa10_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(Compla10);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
            StreamReader fr = new StreamReader("Complementsà10.txt");
            textBox8.ScrollBars = ScrollBars.Both;
            textBox8.Text = fr.ReadToEnd();
            textBox8.ReadOnly = true;
        }

        private void Criteresdedivisibilite_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(CritDeDivi);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
        }

        private void Prioritesoperatoires_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(Prioropera);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
            StreamReader fr = new StreamReader("Prioritesoperatoires.txt");
            textBox10.ScrollBars = ScrollBars.Both;
            textBox10.Text = fr.ReadToEnd();
            textBox10.ReadOnly = true;
        }

        private void DecompPremiers_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(DecompEnNbPre);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
            StreamReader fr = new StreamReader("DecompPremiers.txt");
            textBox11.ScrollBars = ScrollBars.Both;
            textBox11.Text = fr.ReadToEnd();
            textBox11.ReadOnly = true;
        }

        private void DoubleEtMoitie_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(DoubEtMoit);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
        }

        private void PGCD_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(PGD);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
            StreamReader fr = new StreamReader("PGCD.txt");
            textBox13.ScrollBars = ScrollBars.Both;
            textBox13.Text = fr.ReadToEnd();
            textBox13.ReadOnly = true;
        }

        private void PPCM_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(PPM);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
            StreamReader fr = new StreamReader("PPCM.txt");
            textBox14.ScrollBars = ScrollBars.Both;
            textBox14.Text = fr.ReadToEnd();
            textBox14.ReadOnly = true;
        }

        private void Perimetre_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(Perim);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
            StreamReader fr = new StreamReader("Perimetre.txt");
            textBox15.ScrollBars = ScrollBars.Both;
            textBox15.Text = fr.ReadToEnd();
            textBox15.ReadOnly = true;
        }

        private void Air_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(Aire);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
            StreamReader fr = new StreamReader("Aire.txt");
            textBox16.ScrollBars = ScrollBars.Both;
            textBox16.Text = fr.ReadToEnd();
            textBox16.ReadOnly = true;
        }

        private void Fractions_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(Fraction);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
        }

        private void Heure_Click(object sender, EventArgs e)
        {
            tabcontrol6.SelectTab(Heures);
            tabcontrol1.SelectTab(CoursMaChoisi);
            
            StreamReader fr = new StreamReader("heure.txt");
            textBox18.ScrollBars = ScrollBars.Both;
            textBox18.Text = fr.ReadToEnd();
            textBox18.ReadOnly = true;
        }

        private void pictureBox46_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void Frlevels_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox45_Click(object sender, EventArgs e)
        {

            if (!signup)
            {
                label14.Text = user.GetPoints(logUser.Text);
                switch (user.MGetLev(logUser.Text))
                {
                    case "1": Mlev1butt.Enabled = true;
                        break;
                    case "2": Mlev2butt.Enabled = true;
                        break;
                    case "3": Mlev3butt.Enabled = true;
                        break;
                    case "4": Mlev4butt.Enabled = true;
                        break;
                   //  case "5": Mlev5butt.Enabled = true;
                      //  break;
                }
            }
            else
            {
                label12.Text = user.GetPoints(signUser.Text);
                switch (user.MGetLev(signUser.Text))
                {
                    case "1": Mlev1butt.Enabled = true;
                        break;
                    case "2": Mlev2butt.Enabled = true;
                        break;
                    case "3": Mlev3butt.Enabled = true;
                        break;
                    case "4": Mlev4butt.Enabled = true;
                        break;
                   // case "5": Mlev5butt.Enabled = true;
                    //    break;
                }
            }
            tabcontrol1.SelectTab(Malevels);
        }

        private void pictureBox49_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(MaCoursEx);
        }

        private void pictureBox50_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
            nombre.Stop();
        }

        private void pictureBox51_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void pictureBox52_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void pictureBox53_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void pictureBox54_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void pictureBox55_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void pictureBox56_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void pictureBox57_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void pictureBox58_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void pictureBox59_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void quest1lev1_Click(object sender, EventArgs e)
        {
            switch (quest1lev1.Text)
            {
                case  "Ecouter et répondre:":
                    pb.ClearContent();
                    pb.AppendText("le premier mot est chanson le deuxième mot est feutre");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter et répondre :":
                    pb.ClearContent();
                    pb.AppendText(" a - x - g - h - i");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter la lettre puis choisi le mot dont cette lettre est la plus réccurente :":
                    pb.ClearContent();
                    pb.AppendText("La lettre a");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter et repondre :":
                    pb.ClearContent();
                    pb.AppendText("on - en - a");
                    speak.SpeakAsync(pb);
                    break;
                case "Ecouter puis choisir :":
                    pb.ClearContent();
                    pb.AppendText("Drapeau");
                    speak.SpeakAsync(pb);
                    break;

            }



            panel11.Visible = true;
            ok [0]= true;
        }

        private void quest2lev1_Click(object sender, EventArgs e)
        {
            switch (quest2lev1.Text)
            {
                case "Ecouter et répondre:":
                    pb.ClearContent();
                    pb.AppendText("le premier mot est chanson le deuxième mot est feutre");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter et répondre :":
                    pb.ClearContent();
                    pb.AppendText(" a - x - g - h - i");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter la lettre puis choisi le mot dont cette lettre est la plus réccurente :":
                    pb.ClearContent();
                    pb.AppendText("La lettre a");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter et repondre :":
                    pb.ClearContent();
                    pb.AppendText("on - en - a");
                    speak.SpeakAsync(pb);
                    break;
                case "Ecouter puis choisir :":
                    pb.ClearContent();
                    pb.AppendText("Drapeau");
                    speak.SpeakAsync(pb);
                    break;

            }
            panel12.Visible = true;
            ok[1] = true;
        }

        private void quest3lev1_Click(object sender, EventArgs e)
        {
            switch (quest3lev1.Text)
            {
                case "Ecouter et répondre:":
                    pb.ClearContent();
                    pb.AppendText("le premier mot est chanson le deuxième mot est feutre");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter et répondre :":
                    pb.ClearContent();
                    pb.AppendText(" a - x - g - h - i");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter la lettre puis choisi le mot dont cette lettre est la plus réccurente :":
                    pb.ClearContent();
                    pb.AppendText("La lettre a");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter et repondre :":
                    pb.ClearContent();
                    pb.AppendText("on - en - a");
                    speak.SpeakAsync(pb);
                    break;
                case "Ecouter puis choisir :":
                    pb.ClearContent();
                    pb.AppendText("Drapeau");
                    speak.SpeakAsync(pb);
                    break;
            }
            panel13.Visible = true;
            ok[2] = true;
        }

        private void quest4lev1_Click(object sender, EventArgs e)
        {
            switch (quest4lev1.Text)
            {
                case "Ecouter et répondre:":
                    pb.ClearContent();
                    pb.AppendText("le premier mot est chanson le deuxième mot est feutre");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter et répondre :":
                    pb.ClearContent();
                    pb.AppendText(" a - x - g - h - i");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter la lettre puis choisi le mot dont cette lettre est la plus réccurente :":
                    pb.ClearContent();
                    pb.AppendText("La lettre a");
                    speak.SpeakAsync(pb);
                    break;
                case "Écouter et repondre :":
                    pb.ClearContent();
                    pb.AppendText("on - en - a");
                    speak.SpeakAsync(pb);
                    break;
                case "Ecouter puis choisir :":
                    pb.ClearContent();
                    pb.AppendText("Drapeau");
                    speak.SpeakAsync(pb);
                    break;

            }
            panel14.Visible = true;
            ok[3] = true;
        }

        private void quest1lev1_MouseEnter(object sender, EventArgs e)
        {
            panel11.Visible = true;
        }

        private void quest1lev1_MouseLeave(object sender, EventArgs e)
        {
           if(!ok[0]) panel11.Visible = false;
        }

        private void quest2lev1_MouseEnter(object sender, EventArgs e)
        {
            panel12.Visible = true;
        }

        private void quest2lev1_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[1]) panel12.Visible = false;
            
        }

        private void quest3lev1_MouseEnter(object sender, EventArgs e)
        {
            panel13.Visible = true;
        }

        private void quest3lev1_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[2]) panel13.Visible = false;
        }

        private void quest4lev1_MouseEnter(object sender, EventArgs e)
        {
            panel14.Visible = true;
        }

        private void quest4lev1_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[3]) panel14.Visible = false;
        }

        private void quest1lev2_Click(object sender, EventArgs e)
        {
            panel21.Visible = true;
            ok[4] = true;
        }

        private void quest2lev2_Click(object sender, EventArgs e)
        {
            panel19.Visible = true;
            ok[5] = true;
        }

        private void quest3lev2_Click(object sender, EventArgs e)
        {
            panel17.Visible = true;
            ok[6] = true;
        }

        private void quest4lev2_Click(object sender, EventArgs e)
        {
            panel15.Visible = true;
            ok[7] = true;
        }

        private void quest1lev2_MouseEnter(object sender, EventArgs e)
        {
            panel21.Visible = true;
            
        }

        private void quest2lev2_MouseEnter(object sender, EventArgs e)
        {
            panel19.Visible = true;
            
        }
    

        private void quest3lev2_MouseEnter(object sender, EventArgs e)
        {
            panel17.Visible = true;
            
        }
    

        private void quest4lev2_MouseEnter(object sender, EventArgs e)
        {
            panel15.Visible = true;
        }

        private void quest1lev2_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[4]) panel21.Visible = false;
        }

        private void quest2lev2_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[5]) panel19.Visible = false;
        }

        private void quest3lev2_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[6]) panel17.Visible = false;
        }

        private void quest4lev2_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[7]) panel15.Visible = false;
        }

        private void quest1lev3_Click(object sender, EventArgs e)
        {
            panel32.Visible = true;
            ok[8] = true;
        }

        private void quest2lev3_Click(object sender, EventArgs e)
        {
            panel30.Visible = true;
            ok[9] = true;
        }

        private void quest3lev3_Click(object sender, EventArgs e)
        {
            panel27.Visible = true;
            ok[10] = true;
        }

        private void quest4lev3_Click(object sender, EventArgs e)
        {
            panel24.Visible = true;
            ok[11] = true;
        }

        private void quest1lev3_MouseEnter(object sender, EventArgs e)
        {
            panel32.Visible = true;
        }

        private void quest2lev3_MouseEnter(object sender, EventArgs e)
        {
            panel30.Visible = true;
        }

        private void quest3lev3_MouseEnter(object sender, EventArgs e)
        {
            panel27.Visible = true;
        }

        private void quest4lev3_MouseEnter(object sender, EventArgs e)
        {
            panel24.Visible = true;
        }

        private void quest1lev3_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[8]) panel32.Visible = false;
        }

        private void quest2lev3_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[9]) panel30.Visible = false;
        }

        private void quest3lev3_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[10]) panel27.Visible = false;
        }

        private void quest4lev3_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[11]) panel24.Visible = false;
        }

        private void Mquest1lev1_Click(object sender, EventArgs e)
        {
            panel46.Visible = true;
            ok[12] = true;
        }

        private void Mquest1lev1_MouseEnter(object sender, EventArgs e)
        {
            panel46.Visible = true;
        }

        private void Mquest1lev1_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[12]) panel46.Visible = false;
        }

        private void Mquest2lev1_Click(object sender, EventArgs e)
        {
            panel44.Visible = true;
            ok[13] = true;
        }

        private void Mquest2lev1_MouseEnter(object sender, EventArgs e)
        {
            panel44.Visible = true;
        }

        private void Mquest2lev1_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[13]) panel44.Visible = false;
        }

        private void Mquest3lev1_Click(object sender, EventArgs e)
        {
            panel41.Visible = true;
            ok[14] = true;
        }

        private void Mquest3lev1_MouseEnter(object sender, EventArgs e)
        {
            panel41.Visible = true;
        }

        private void Mquest3lev1_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[14]) panel41.Visible = false;
        }

        private void Mquest4lev1_Click(object sender, EventArgs e)
        {
            panel39.Visible = true;
            ok[15] = true;
        }

        private void Mquest4lev1_MouseEnter(object sender, EventArgs e)
        {
            panel39.Visible = true;
        }

        private void Mquest4lev1_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[15]) panel39.Visible = false;
        }

        private void pictureBox62_Click(object sender, EventArgs e)
        {
            logUser.Text = "";
            logPass.Text = "";
            logCombo.Text = "";
            tabcontrol1.SelectTab(logPage);
            Lev1butt.Enabled = false;
            Lev2butt.Enabled = false;
            Lev3butt.Enabled = false;
            Lev4butt.Enabled = false;
            Lev5butt.Enabled = false;
            Mlev1butt.Enabled = false;
            Mlev2butt.Enabled = false;
            Mlev3butt.Enabled = false;
            Mlev4butt.Enabled = false;
          //  Mlev5butt.Enabled = false;
        }

        private void panel34_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox63_Click(object sender, EventArgs e)
        {
            if (!signup)
            {
                switch (user.MGetLev(logUser.Text))
                {
                    case "1": Mlev1butt.Enabled = true;
                        break;
                    case "2": Mlev2butt.Enabled = true;
                        break;
                    case "3": Mlev3butt.Enabled = true;
                        break;
                    case "4": Mlev4butt.Enabled = true;
                        break;
                    //case "5": Mlev5butt.Enabled = true;
                      //  break;
                }
            }
            else
            {
                switch (user.MGetLev(signUser.Text))
                {
                    case "1": Mlev1butt.Enabled = true;
                        break;
                    case "2": Mlev2butt.Enabled = true;
                        break;
                    case "3": Mlev3butt.Enabled = true;
                        break;
                    case "4": Mlev4butt.Enabled = true;
                        break;
               //     case "5": Mlev5butt.Enabled = true;
               //         break;
                }
            }
            tabcontrol1.SelectTab(Malevels);
        }

        private void pictureBox64_Click(object sender, EventArgs e)
        {
            if (!signup)
            {
                switch (user.MGetLev(logUser.Text))
                {
                    case "1": Mlev1butt.Enabled = true;
                        break;
                    case "2": Mlev2butt.Enabled = true;
                        break;
                    case "3": Mlev3butt.Enabled = true;
                        break;
                    case "4": Mlev4butt.Enabled = true;
                        break;
                    //case "5": Mlev5butt.Enabled = true;
                    //    break;
                }
            }
            else
            {
                switch (user.MGetLev(signUser.Text))
                {
                    case "1": Mlev1butt.Enabled = true;
                        break;
                    case "2": Mlev2butt.Enabled = true;
                        break;
                    case "3": Mlev3butt.Enabled = true;
                        break;
                    case "4": Mlev4butt.Enabled = true;
                        break;
                   // case "5": Mlev5butt.Enabled = true;
                        break;
                }
            }
            tabcontrol1.SelectTab(Malevels);
        }

        private void panel48_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Mquest1lev2_Click(object sender, EventArgs e)
        {
            panel55.Visible = true;
            ok[16] = true;
        }

        private void Mquest1lev2_MouseEnter(object sender, EventArgs e)
        {
            panel55.Visible = true;
        }

        private void Mquest1lev2_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[16]) panel55.Visible = false;
        }

        private void Mquest2lev2_Click(object sender, EventArgs e)
        {
            panel53.Visible = true;
            ok[17] = true;
        }

        private void Mquest2lev2_MouseEnter(object sender, EventArgs e)
        {
            panel53.Visible = true;
        }

        private void Mquest2lev2_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[17]) panel53.Visible = false;
        }

        private void Mquest3lev2_Click(object sender, EventArgs e)
        {
            panel50.Visible = true;
            ok[18] = true;
        }

        private void Mquest3lev2_MouseEnter(object sender, EventArgs e)
        {
            panel50.Visible = true;
        }

        private void Mquest3lev2_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[18]) panel50.Visible = false;
        }

        private void Mquest4lev2_Click(object sender, EventArgs e)
        {
            panel48.Visible = true;
            ok[19] = true;
        }

        private void Mquest4lev2_MouseEnter(object sender, EventArgs e)
        {
            panel48.Visible = true;
        }

        private void Mquest4lev2_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[19]) panel48.Visible = false;
        }

        private void panel49_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Mveriflev2_Click(object sender, EventArgs e)
        {
            int i = GetPoints(t, panel48) + GetPoints(t, panel50) + GetPoints(t, panel53) + GetPoints(t, panel55);
            if (!signup)
            {
                user.MChangeLev(logUser.Text, 3);
                count = int.Parse(user.GetPoints(logUser.Text)) + GetPoints(t, panel48) + GetPoints(t, panel50) + GetPoints(t, panel53) + GetPoints(t, panel55);
            }
            else
            {
                user.MChangeLev(signUser.Text, 3);
                count = int.Parse(user.GetPoints(signUser.Text)) + GetPoints(t, panel48) + GetPoints(t, panel50) + GetPoints(t, panel53) + GetPoints(t, panel55);
            }
            Mlev2butt.Enabled = false;

            MessageBox.Show("Tu as collecté " + i.ToString() + " points", "SUPERBE!", MessageBoxButtons.OK);
            if (!signup)
            {
               
                user.ChangePoints(logUser.Text, count);
                label18.Text = user.GetPoints(logUser.Text);
            }
            else
            {
               
                user.ChangePoints(signUser.Text, count);
                label18.Text = user.GetPoints(signUser.Text);
            }
            MAvlev2.Location = Mveriflev2.Location;
            Mveriflev2.Visible = false;
            MAvlev2.Size = Mveriflev2.Size;
            MAvlev2.Visible = true;
            MAvlev2.Enabled = true;
        }

        private void Mveriflev1_Click(object sender, EventArgs e)
        {
            int i = GetPoints(t, panel39) + GetPoints(t, panel41) + GetPoints(t, panel44) + GetPoints(t, panel46);
            if (!signup)
            {
                user.MChangeLev(logUser.Text, 2);
                count = int.Parse(user.GetPoints(logUser.Text)) + GetPoints(t, panel39) + GetPoints(t, panel41) + GetPoints(t, panel44) + GetPoints(t, panel46);
            }
            else
            {
                user.MChangeLev(signUser.Text, 2);
                count = int.Parse(user.GetPoints(signUser.Text)) + GetPoints(t, panel39) + GetPoints(t, panel41) + GetPoints(t, panel44) + GetPoints(t, panel46);
            }
            Mlev1butt.Enabled = false;

            MessageBox.Show("Tu as collecté " + i.ToString() + " points", "SUPERBE!", MessageBoxButtons.OK);
            if (!signup)
            {
                
                user.ChangePoints(logUser.Text, count);
                label14.Text = user.GetPoints(logUser.Text);
            }
            else
            {
               
                user.ChangePoints(signUser.Text, count);
                label14.Text = user.GetPoints(signUser.Text);
            }
            MAvlev1.Location = Mveriflev1.Location;
            Mveriflev1.Visible = false;
            MAvlev1.Size = Mveriflev1.Size;
            MAvlev1.Visible = true;
            MAvlev1.Enabled = true; 
        }

        private void pictureBox60_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void pictureBox61_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursMa);
        }

        private void metroTile5_Click(object sender, EventArgs e)
        {
            if (!signup) label14.Text = user.GetPoints(logUser.Text);
            else label14.Text = user.GetPoints(signUser.Text);
            tabcontrol1.SelectTab(Malev1);
            CleanLev(panel41, panel39, panel44, panel46);
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = false;
            }
           
            string[] quest;
            quest = Levels.GetLevels(4);
            Mquest1lev1.Text = quest[0];
            Mrad1quest1lev1.Text = quest[1];
            Mrad2quest1lev1.Text = quest[2];
            Mrad3quest1lev1.Text = quest[3];
            t[0] = quest[4];
            Mquest2lev1.Text = quest[5];
            Mrad1quest2lev1.Text = quest[6];
            Mrad2quest2lev1.Text = quest[7];
            Mrad3quest2lev1.Text = quest[8];
            t[1] = quest[9];
            Mquest3lev1.Text = quest[10];
            Mrad1quest3lev1.Text = quest[11];
            Mrad2quest3lev1.Text = quest[12];
            Mrad3quest3lev1.Text = quest[13];
            t[2] = quest[14];
            Mquest4lev1.Text = quest[15];
            Mrad1quest4lev1.Text = quest[16];
            Mrad2quest4lev1.Text = quest[17];
            Mrad3quest4lev1.Text = quest[18];
            t[3] = quest[19];
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            if (!signup) label18.Text = user.GetPoints(logUser.Text);
            else label18.Text = user.GetPoints(signUser.Text);
            tabcontrol1.SelectTab(Malev2);
            CleanLev(panel55, panel53, panel50, panel48);
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = false;
            }

            string[] quest;
            quest = Levels.GetLevels(5);
            Mquest1lev2.Text = quest[0];
            Mrad1quest1lev2.Text = quest[1];
            Mrad2quest1lev2.Text = quest[2];
            Mrad3quest1lev2.Text = quest[3];
            t[0] = quest[4];
            Mquest2lev2.Text = quest[5];
            Mrad1quest2lev2.Text = quest[6];
            Mrad2quest2lev2.Text = quest[7];
            Mrad3quest2lev2.Text = quest[8];
            t[1] = quest[9];
            Mquest3lev2.Text = quest[10];
            Mrad1quest3lev2.Text = quest[11];
            Mrad2quest3lev2.Text = quest[12];
            Mrad3quest3lev2.Text = quest[13];
            t[2] = quest[14];
            Mquest4lev2.Text = quest[15];
            Mrad1quest4lev2.Text = quest[16];
            Mrad2quest4lev2.Text = quest[17];
            Mrad3quest4lev2.Text = quest[18];
            t[3] = quest[19];
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            if (!signup) label21.Text = user.GetPoints(logUser.Text);
            else label21.Text = user.GetPoints(signUser.Text);
            tabcontrol1.SelectTab(Malev3);
            CleanLev(panel63, panel58, panel60, panel65);
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = false;
            }

            string[] quest;
            quest = Levels.GetLevels(6);
            Mquest1lev3.Text = quest[0];
            Mrad1quest1lev3.Text = quest[1];
            Mrad2quest1lev3.Text = quest[2];
            Mrad3quest1lev3.Text = quest[3];
            t[0] = quest[4];
            Mquest2lev3.Text = quest[5];
            Mrad1quest2lev3.Text = quest[6];
            Mrad2quest2lev3.Text = quest[7];
            Mrad3quest2lev3.Text = quest[8];
            t[1] = quest[9];
            Mquest3lev3.Text = quest[10];
            Mrad1quest3lev3.Text = quest[11];
            Mrad2quest3lev3.Text = quest[12];
            Mrad3quest3lev3.Text = quest[13];
            t[2] = quest[14];
            Mquest4lev3.Text = quest[15];
            Mrad1quest4lev3.Text = quest[16];
            Mrad2quest4lev3.Text = quest[17];
            Mrad3quest4lev3.Text = quest[18];
            t[3] = quest[19];
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(Malev4);
            string[] tab = questCompte[0].Split(':');
            Mquest1lev4.Text=tab[0];
            nb1.Text = tab[1];
            nb2.Text = tab[2];
            nb3.Text = tab[3];
            nb4.Text = tab[4];
            nb5.Text = tab[5];


            

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void MAvlev1_Click(object sender, EventArgs e)
        {
            if (!signup) label18.Text = user.GetPoints(logUser.Text);
            else label18.Text = user.GetPoints(signUser.Text);
            tabcontrol1.SelectTab(Malev2);
            Mlev1butt.Enabled = false;
            string[] quest;
            quest = Levels.GetLevels(5);
            Mquest1lev2.Text = quest[0];
            Mrad1quest1lev2.Text = quest[1];
            Mrad2quest1lev2.Text = quest[2];
            Mrad3quest1lev2.Text = quest[3];
            t[0] = quest[4];
            Mquest2lev2.Text = quest[5];
            Mrad1quest2lev2.Text = quest[6];
            Mrad2quest2lev2.Text = quest[7];
            Mrad3quest2lev2.Text = quest[8];
            t[1] = quest[9];
            Mquest3lev2.Text = quest[10];
            Mrad1quest3lev2.Text = quest[11];
            Mrad2quest3lev2.Text = quest[12];
            Mrad3quest3lev2.Text = quest[13];
            t[2] = quest[14];
            Mquest4lev2.Text = quest[15];
            Mrad1quest4lev2.Text = quest[16];
            Mrad2quest4lev2.Text = quest[17];
            Mrad3quest4lev2.Text = quest[18];
            t[3] = quest[19];
        }

        private void panel55_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Avlev3_Click(object sender, EventArgs e)
        {
            if (!signup) label6.Text = user.GetPoints(logUser.Text);
            else label6.Text = user.GetPoints(signUser.Text);
            tabcontrol1.SelectTab(Frlev4);
        }

        private void pictureBox65_Click(object sender, EventArgs e)
        {
            switch (user.FGetLev(logUser.Text))
            {
                case "1":
                    Lev1butt.Enabled = true;
                    break;
                case "2":
                    Lev2butt.Enabled = true;
                    break;
                case "3":
                    Lev3butt.Enabled = true;
                    break;
                case "4":
                    Lev4butt.Enabled = true;
                    break;
                case "5":
                    Lev5butt.Enabled = true;
                    break;
            }
            tabcontrol1.SelectTab(Frlevels);
            panel34.Visible = false;
            i = 0;
            richTextBox1.Clear();
            speak.SpeakAsyncCancelAll();
            richTextBox2.Clear();
            richTextBox3.Clear();
            Dicte.Stop();
        }

        private void MAvlev2_Click(object sender, EventArgs e)
        {
            if (!signup) label21.Text = user.GetPoints(logUser.Text);
            else label21.Text = user.GetPoints(signUser.Text);
            tabcontrol1.SelectTab(Malev3);
            CleanLev(panel39, panel41, panel44, panel46);
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = false;
            }

            string[] quest;
            quest = Levels.GetLevels(6);
            Mquest1lev3.Text = quest[0];
            Mrad1quest1lev3.Text = quest[1];
            Mrad2quest1lev3.Text = quest[2];
            Mrad3quest1lev3.Text = quest[3];
            t[0] = quest[4];
            Mquest2lev3.Text = quest[5];
            Mrad1quest2lev3.Text = quest[6];
            Mrad2quest2lev3.Text = quest[7];
            Mrad3quest2lev3.Text = quest[8];
            t[1] = quest[9];
            Mquest3lev3.Text = quest[10];
            Mrad1quest3lev3.Text = quest[11];
            Mrad2quest3lev3.Text = quest[12];
            Mrad3quest3lev3.Text = quest[13];
            t[2] = quest[14];
            Mquest4lev3.Text = quest[15];
            Mrad1quest4lev3.Text = quest[16];
            Mrad2quest4lev3.Text = quest[17];
            Mrad3quest4lev3.Text = quest[18];
            t[3] = quest[19];
        }

        private void pictureBox66_Click(object sender, EventArgs e)
        {
            if (!signup)
            {
                switch (user.MGetLev(logUser.Text))
                {
                    case "1": Mlev1butt.Enabled = true;
                        break;
                    case "2": Mlev2butt.Enabled = true;
                        break;
                    case "3": Mlev3butt.Enabled = true;
                        break;
                    case "4": Mlev4butt.Enabled = true;
                        break;
                   // case "5": Mlev5butt.Enabled = true;
                     //   break;
                }
            }
            else
            {
                switch (user.MGetLev(signUser.Text))
                {
                    case "1": Mlev1butt.Enabled = true;
                        break;
                    case "2": Mlev2butt.Enabled = true;
                        break;
                    case "3": Mlev3butt.Enabled = true;
                        break;
                    case "4": Mlev4butt.Enabled = true;
                        break;
                   // case "5": Mlev5butt.Enabled = true;
                    //    break;
                }
            }
            tabcontrol1.SelectTab(Malevels);
            pictureBox69.Visible = false;
            pictureBox74.Visible = false;
            pictureBox73.Visible = false;
            pictureBox77.Visible = false;
            pictureBox72.Visible = false;
            pictureBox76.Visible = false;
            pictureBox71.Visible = false;
            pictureBox75.Visible = false;
            pictureBox78.Visible = false;
            pictureBox81.Visible = false;
            pictureBox80.Visible = false;
            pictureBox79.Visible = false;
            pictureBox82.Visible = false;
            pictureBox83.Visible = false;
            pictureBox84.Visible = false;
            pictureBox85.Visible = false;
            

        }

        private void MAvlev3_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(Malev4);
            if (!signup) label16.Text = user.GetPoints(logUser.Text);
            else label16.Text = user.GetPoints(signUser.Text);
            string[] tab = questCompte[0].Split(':');
            Mquest1lev4.Text = tab[0];
            nb1.Text = tab[1];
            nb2.Text = tab[2];
            nb3.Text = tab[3];
            nb4.Text = tab[4];
            nb5.Text = tab[5];
            CleanLev(panel48, panel53, panel55, panel50);
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = false;
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void logUser_Click(object sender, EventArgs e)
        {

        }

        private void signUser_Click(object sender, EventArgs e)
        {

        }

        private void Mquest1lev3_Click(object sender, EventArgs e)
        {
            switch (Mquest1lev3.Text)
            {
                case"Quel est l'aire du carré suivant:":
                    pictureBox69.Visible = true;
                    break;
                case"Quel est le périmètre du réctangle suivant:":
                    pictureBox74.Visible = true;
                    break;
                case"Quelle heure est elle ?":
                    pictureBox78.Visible = true;
                    break;
                case"Quel est l'heure dans cette phigure?":
                    pictureBox82.Visible = true;
                    break;

            }
            panel65.Visible = true;
            ok[20] = true;
        }

        private void Mquest4lev3_Click(object sender, EventArgs e)
        {
            switch (Mquest4lev3.Text)
            {
                case "Quel est l'aire du carré suivant:":
                    pictureBox71.Visible = true;
                    break;
                case "Quel est le périmètre du réctangle suivant:":
                    pictureBox75.Visible = true;
                    break;
                case "Quelle heure est elle ?":
                    pictureBox79.Visible = true;
                    break;
                case "Quel est l'heure dans cette phigure?":
                    pictureBox84.Visible = true;
                    break;

            }
            panel58.Visible = true;
            ok[23] = true;
        }

        private void Mquest2lev3_Click(object sender, EventArgs e)
        {
            switch (Mquest2lev3.Text)
            {
                case "Quel est l'aire du carré suivant:":
                    pictureBox73.Visible = true;
                    break;
                case "Quel est le périmètre du réctangle suivant:":
                    pictureBox77.Visible = true;
                    break;
                case "Quelle heure est elle ?":
                    pictureBox81.Visible = true;
                    break;
                case "Quel est l'heure dans cette phigure?":
                    pictureBox85.Visible = true;
                    break;

            }
            panel63.Visible = true;
            ok[21] = true;
        }

        private void Mquest3lev3_Click(object sender, EventArgs e)
        {
            switch (Mquest3lev3.Text)
            {
                case "Quel est l'aire du carré suivant:":
                    pictureBox72.Visible = true;
                    break;
                case "Quel est le périmètre du réctangle suivant:":
                    pictureBox76.Visible = true;
                    break;
                case "Quelle heure est elle ?":
                    pictureBox80.Visible = true;
                    break;
                case "Quel est l'heure dans cette phigure?":
                    pictureBox83.Visible = true;
                    break;

            }
            panel60.Visible = true;
            ok[22] = true;
        }

        private void panel44_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Mquest2lev3_MouseEnter(object sender, EventArgs e)
        {
             panel63.Visible = true;
        }

        private void Mveriflev3_Click(object sender, EventArgs e)
        {
            int i = GetPoints(t, panel60) + GetPoints(t, panel58) + GetPoints(t, panel63) + GetPoints(t, panel65);
            if (!signup)
            {
                user.MChangeLev(logUser.Text, 4);
                count = int.Parse(user.GetPoints(logUser.Text)) + GetPoints(t, panel60) + GetPoints(t, panel58) + GetPoints(t, panel63) + GetPoints(t, panel65);
            }
            else
            {
                user.MChangeLev(signUser.Text, 4);
                count = int.Parse(user.GetPoints(signUser.Text)) + GetPoints(t, panel60) + GetPoints(t, panel58) + GetPoints(t, panel63) + GetPoints(t, panel65);
            }
            Mlev3butt.Enabled = false;

            MessageBox.Show("Tu as collecté " + i.ToString() + " points", "SUPERBE!", MessageBoxButtons.OK);
            if (!signup)
            {

                user.ChangePoints(logUser.Text, count);
                label21.Text = user.GetPoints(logUser.Text);
            }
            else
            {

                user.ChangePoints(signUser.Text, count);
                label21.Text = user.GetPoints(signUser.Text);
            }
            MAvlev3.Location = Mveriflev3.Location;
            Mveriflev3.Visible = false;
            MAvlev3.Size = Mveriflev3.Size;
            MAvlev3.Visible = true;
            MAvlev3.Enabled = true;
        }

        private void pictureBox67_Click(object sender, EventArgs e)
        {
            signPanel.Visible = false;
            logPanel.Visible = true;
            pictureBox67.Visible = false;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                MessageBox.Show("Cut/Copy and Paste Options are disabled");
            }
        }

        private void nextCo_Click(object sender, EventArgs e)
        {
            panel68.Visible = false;
            ayyaPanelLvl5++;
            switch (ayyaPanelLvl5)
            {
                case 1: panel70.Visible = true;
                    break;
                case 2:panel70.Visible = false; panel72.Visible = true;
                    break;
                case 3: panel72.Visible = false; panel74.Visible = true;
                    break;
                default: nextCo.Visible = false;
                    break;
            }
        }

        private void panel70_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel68_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel71_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel69_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Lev5butt_Click(object sender, EventArgs e)
        {
            Lev5butt.Enabled = false;
            flowLayoutPanel1.Visible = false;
            //showCo.Visible = false;
            corCo.Visible = false;
            nextCo.Visible = false;
            if (!signup) label27.Text = user.GetPoints(logUser.Text);
            else label27.Text = user.GetPoints(signUser.Text);
            CleanLev(panel69, panel71, panel73, panel75);
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = false;
            }
            tabcontrol1.SelectTab(Frlev5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!signup) label27.Text = user.GetPoints(logUser.Text);
            else label27.Text = user.GetPoints(signUser.Text);
            flowLayoutPanel1.Visible = false;
            nextCo.Visible = false;
            showCo.Visible = false;
            corCo.Visible = false;
            Dicte.Stop();
            tabcontrol1.SelectTab(Frlev5);
        }

        private void startCo_Click(object sender, EventArgs e)
        {
            showCo.Visible = false;
           // try
            {
                string[] quest;
                string txtName = comboCo.Text.ToString();
                quest = Levels.GetLevelsCo(txtName);
                quest1lev5.Text = quest[0];
                rad1quest1lev5.Text = quest[1];
                rad2quest1lev5.Text = quest[2];
                rad3quest1lev5.Text = quest[3];
                t[0] = quest[4];
                quest2lev5.Text = quest[5];
                rad1quest2lev5.Text = quest[6];
                rad2quest2lev5.Text = quest[7];
                rad3quest2lev5.Text = quest[8];
                t[1] = quest[9];
                quest3lev5.Text = quest[10];
                rad1quest3lev5.Text = quest[11];
                rad2quest3lev5.Text = quest[12];
                rad3quest3lev5.Text = quest[13];
                t[2] = quest[14];
                quest4lev5.Text = quest[15];
                rad1quest4lev5.Text = quest[16];
                rad2quest4lev5.Text = quest[17];
                rad3quest4lev5.Text = quest[18];
                t[3] = quest[19];

                string dirCo = "txtCo//" + comboCo.Text + ".txt";
                StreamReader co = new StreamReader(dirCo);
                string Co = co.ReadToEnd();
                pb.ClearContent();
                pb.AppendText(Co);
                speak.SpeakAsync(pb);
                speak.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(Speak_SpeakCompleted);
            }
            //catch { MessageBox.Show("Il faut choisir le texte pour compléter"); }
            }

        private void Speak_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            showCo.Visible = true;
        }

        private void showCo_Click(object sender, EventArgs e)
        {
            nextCo.Visible = true;
            string dirCo = "txtCo//" + comboCo.Text + ".txt";
            StreamReader co = new StreamReader(dirCo);
            string Co = co.ReadToEnd();
            SpeechSynthesizer syn = new SpeechSynthesizer();
            pb.ClearContent();
            pb.AppendText(Co);
            syn.SpeakAsync(pb);
            syn.SpeakCompleted += Syn_SpeakCompleted;

            flowLayoutPanel1.Visible = true;
            panel68.Width = flowLayoutPanel1.Width; panel68.Height = flowLayoutPanel1.Height;
            panel68.Location = new Point(0, 0);
            panel70.Width = flowLayoutPanel1.Width; panel70.Height = flowLayoutPanel1.Height;
            panel70.Location = new Point(0, 0);
            panel70.Visible = false;
            panel72.Width = flowLayoutPanel1.Width; panel72.Height = flowLayoutPanel1.Height;
            panel72.Location = new Point(0, 0);
            panel72.Visible = false;
            panel74.Width = flowLayoutPanel1.Width; panel74.Height = flowLayoutPanel1.Height;
            panel74.Location = new Point(0, 0);
            panel74.Visible = false;
            
        }

        private void Syn_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            corCo.Visible = true;
        }

        private void label23_Click(object sender, EventArgs e)
        {
            panel69.Visible = true;
            ok[24] = true;
        }

        private void label24_Click(object sender, EventArgs e)
        {
            panel71.Visible = true;
            ok[25] = true;
        }

        private void label25_Click(object sender, EventArgs e)
        {
            panel73.Visible = true;
            ok[26] = true;
        }

        private void label26_Click(object sender, EventArgs e)
        {
            panel75.Visible = true;
            ok[27] = true;
        }

        private void label23_MouseEnter(object sender, EventArgs e)
        {
            panel69.Visible = true;
        }

        private void label24_MouseEnter(object sender, EventArgs e)
        {
            panel71.Visible = true;
        }

        private void label25_MouseEnter(object sender, EventArgs e)
        {
            panel73.Visible = true;
        }

        private void label26_MouseEnter(object sender, EventArgs e)
        {
            panel75.Visible = true;
        }

        private void label23_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[24]) panel69.Visible = false;
        }

        private void label24_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[25]) panel71.Visible = false;
        }

        private void label25_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[26]) panel73.Visible = false;
        }

        private void label26_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[27]) panel75.Visible = false;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void panel72_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel74_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Mquest2lev3_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[21]) panel63.Visible = false;
        }

        private void Mquest1lev3_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[20]) panel65.Visible = false;
        }

        private void Mquest1lev3_MouseEnter(object sender, EventArgs e)
        {
            panel65.Visible = true;
        }

        private void Mquest3lev3_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[22]) panel60.Visible = false;
        }

        private void Mquest3lev3_MouseEnter(object sender, EventArgs e)
        {
            panel60.Visible = true;
        }

        private void Mquest4lev3_MouseLeave(object sender, EventArgs e)
        {
            if (!ok[23]) panel58.Visible = false;
        }

        private void Mquest4lev3_MouseEnter(object sender, EventArgs e)
        {
            panel58.Visible = true;
        }

        private void corCo_Click(object sender, EventArgs e)
        {
            int i = GetPoints(t, panel69) + GetPoints(t, panel71) + GetPoints(t, panel73) + GetPoints(t, panel75);
            if (!signup)
            {
                user.FChangeLev(logUser.Text, 2);
                count = int.Parse(user.GetPoints(logUser.Text)) + GetPoints(t, panel69) + GetPoints(t, panel71) + GetPoints(t, panel73) + GetPoints(t, panel75);
            }
            else
            {
                user.FChangeLev(signUser.Text, 2);
                count = int.Parse(user.GetPoints(signUser.Text)) + GetPoints(t, panel69) + GetPoints(t, panel71) + GetPoints(t, panel73) + GetPoints(t, panel75);
            }
            Lev1butt.Enabled = false;

            MessageBox.Show("Tu as collecté " + i.ToString() + " points", "SUPERBE!", MessageBoxButtons.OK);
            if (!signup)
            {
                user.ChangePoints(logUser.Text, count);
                label27.Text = user.GetPoints(logUser.Text);
            }
            else
            {
                user.ChangePoints(signUser.Text, count);
                label27.Text = user.GetPoints(signUser.Text);
            }
        }

        private void sami_Click(object sender, EventArgs e)
        {
            speak.SpeakAsync(s);
        }

        private void sami1_Click(object sender, EventArgs e)
        {
            speak.SpeakAsync("La lettre a ");
        }

        private void pictureBox34_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("A");
            speak.SpeakAsync(pb);
        }

        private void pictureBox33_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("B");
            speak.SpeakAsync(pb);
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("C");
            speak.SpeakAsync(pb);
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("D");
            speak.SpeakAsync(pb);
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("E");
            speak.SpeakAsync(pb);
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("F");
            speak.SpeakAsync(pb);
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("G");
            speak.SpeakAsync(pb);
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("H");
            speak.SpeakAsync(pb);
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("I");
            speak.SpeakAsync(pb);
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("J");
            speak.SpeakAsync(pb);
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("k");
            speak.SpeakAsync(pb);
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("L");
            speak.SpeakAsync(pb);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("M");
            speak.SpeakAsync(pb);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("N");
            speak.SpeakAsync(pb);
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("O");
            speak.SpeakAsync(pb);
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("P");
            speak.SpeakAsync(pb);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("Q");
            speak.SpeakAsync(pb);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("R");
            speak.SpeakAsync(pb);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("S");
            speak.SpeakAsync(pb);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("T");
            speak.SpeakAsync(pb);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("U");
            speak.SpeakAsync(pb);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("V");
            speak.SpeakAsync(pb);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText(" w  x ");
            speak.SpeakAsync(pb);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText(" y  z ");
            speak.SpeakAsync(pb);
        }

        private void sami2_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText("a-x-g-h-i");
            speak.SpeakAsync(pb);
           
        }

        private void Mlev5butt_Click(object sender, EventArgs e)
        {

        }

        private void nb1_Click(object sender, EventArgs e)
        {
            nb1.Visible = false;
            doneCompte[0] = true;
            if (numb1.Text == "") numb1.Text = nb1.Text;
            else
            {
                numb2.Text = nb1.Text;
                numb1.Visible = false;
                numb2.Visible = true;
            }
            nb1.Enabled = false;
            nb2.Enabled = false;
            nb3.Enabled = false;
            nb4.Enabled = false;
            nb5.Enabled = false;

        }

        private void nb2_Click(object sender, EventArgs e)
        {
            nb2.Visible = false;
            doneCompte[1] = true;
            if (numb1.Text == "") numb1.Text = nb2.Text;
            else
            {
                numb2.Text = nb2.Text;
                numb1.Visible = false;
                numb2.Visible = true;
            }
            nb1.Enabled = false;
            nb2.Enabled = false;
            nb3.Enabled = false;
            nb4.Enabled = false;
            nb5.Enabled = false;
        }

        private void nb3_Click(object sender, EventArgs e)
        {
            nb3.Visible = false;
            doneCompte[2] = true;
            if (numb1.Text == "") numb1.Text = nb3.Text;
            else
            {
                numb2.Text = nb3.Text;
                numb1.Visible = false;
                numb2.Visible = true;
            }
            nb1.Enabled = false;
            nb2.Enabled = false;
            nb3.Enabled = false;
            nb4.Enabled = false;
            nb5.Enabled = false;
        }

        private void nb4_Click(object sender, EventArgs e)
        {
            nb4.Visible = false;
            doneCompte[3] = true;
            if (numb1.Text == "") numb1.Text = nb4.Text;
            else
            {
                numb2.Text = nb4.Text;
                numb1.Visible = false;
                numb2.Visible = true;
            }
            nb1.Enabled = false;
            nb2.Enabled = false;
            nb3.Enabled = false;
            nb4.Enabled = false;
            nb5.Enabled = false;
        }

        private void ProFlev3_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(Prolev3Fr);
        }

        private void panel80_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ProFlev1_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(Prolev1Fr);
        }

        private void ProFVerifLev1_Click(object sender, EventArgs e)
        {
            bool ok;
            ok = AllInformations(panel80);
                if (!ok)
                {
                    label24.Visible = true;
                    label24.Text = "Enter all informations";
                }
                else
                {
                     Levels.ProfAddQuest(ProQuest1Lev1.Text, ProC1Q1L1.Text, ProC2Q1L1.Text, ProC3Q1L1.Text, ProRQ1.Text, 1);
                     ClearPRO(panel80);
                }          
        }

        private void label24_Click_1(object sender, EventArgs e)
        {

        }

        private void ProQuest1Lev1_Click(object sender, EventArgs e)
        {
            label24.Visible = false;
        }

        private void ProFlev2_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(Prolev2Fr);
        }

        private void ProQuest1Lev2_Click(object sender, EventArgs e)
        {
            label25.Visible = false;
        }

        private void ProC1Q1L2_Click(object sender, EventArgs e)
        {
            label25.Visible = false;
        }

        private void ProC2Q1L2_Click(object sender, EventArgs e)
        {
            label25.Visible = false;
        }

        private void ProC3Q1L2_Click(object sender, EventArgs e)
        {
            label25.Visible = false;
        }

        private void ProRQ2_Click(object sender, EventArgs e)
        {
            label25.Visible = false;
        }

        private void ProQuest1Lev3_Click(object sender, EventArgs e)
        {
            label26.Visible = false;
        }

        private void ProC1Q1L3_Click(object sender, EventArgs e)
        {
            label26.Visible = false;
        }

        private void ProC2Q1L3_Click(object sender, EventArgs e)
        {
            label26.Visible = false;
        }

        private void ProC3Q1L3_Click(object sender, EventArgs e)
        {
            label26.Visible = false;
        }

        private void ProRQ3_Click(object sender, EventArgs e)
        {
            label26.Visible = false;
        }

        private void pictureBox86_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(ProFrLevs);
        }

        private void pictureBox87_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(ProFrLevs);
        }

        private void pictureBox88_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(ProFrLevs);
        }

        private void pictureBox89_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(ProFrLevs);
        }

        private void pictureBox90_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(ProFrLevs);
        }
        

        private void ProFlev4_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(Prolev4Fr);
        }

        private void pictureBox91_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(choosePage);
        }

        private void Mveriflev4_Click(object sender, EventArgs e)
        {
            if (numb1.Text == questCompte[1])
            {
                if (!signup)
                {
                    user.ChangePoints(logUser.Text, int.Parse(label16.Text) + 10);
                    MessageBox.Show("Tu as collecté " + "10" + " points", "SUPERBE!", MessageBoxButtons.OK);
                    label16.Text = user.GetPoints(logUser.Text);
                    tabcontrol1.SelectTab(MaCoursEx);

                }
                else
                {
                    user.ChangePoints(signUser.Text, int.Parse(label16.Text) + 10);
                    MessageBox.Show("Tu as collecté " + "10" + " points", "SUPERBE!", MessageBoxButtons.OK);
                    label16.Text = user.GetPoints(signUser.Text);
                    tabcontrol1.SelectTab(MaCoursEx);
                }
            }
            else
            {
                MessageBox.Show("Tu as collecté " + "0" + " points", "DOMAGE!", MessageBoxButtons.OK);
                tabcontrol1.SelectTab(MaCoursEx);
            }

        }

        private void panel30_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ProFlev5_Click(object sender, EventArgs e)
        {
            tabControl3.SelectTab(Prolev5Fr);
        }

        private void ProfverifCo_Click(object sender, EventArgs e)
        {
            bool ok1, ok2, ok3;
            ok1 = AllInformations(panel89);
            ok2 = AllInformations(panel90);
            ok3 = AllInformations(panel88);
            if ((!ok1) || (!ok2) || (!ok3))
            {
                label29.Text = "Please enter all informations";
                label29.Location = new Point(ProfverifCo.Location.X, ProfverifCo.Location.Y + ProfverifCo.Height + 3);
                label29.Visible = true;
            }

            else
            {
                if (fileCoName.Text.Split(' ').Length > 1)
                {
                    label29.Text = "Le titre doit etre former d'un seul mot";
                    label29.Location = new Point(ProfverifCo.Location.X, ProfverifCo.Location.Y + ProfverifCo.Height + 3);
                    label29.Visible = true;
                }
                else
                {
                    string direction1 = "txtCo\\" + fileCoName.Text + ".txt", direction2 = "txtCo\\" + "txtCo.xml";
                    Levels.ProfAddCo(ajCo.Text, direction1);
                    Levels.ProfAddQuestCO(Q1Co.Text, Choix1Q1.Text, Choix2Q1.Text, Choix3Q1.Text, ReponseQ1.Text, fileCoName.Text, direction2);
                    Levels.ProfAddQuestCO(Q2Co.Text, Choix1Q2.Text, Choix2Q2.Text, Choix3Q2.Text, ReponseQ2.Text, fileCoName.Text, direction2);
                    Levels.ProfAddQuestCO(Q3Co.Text, Choix1Q3.Text, Choix2Q3.Text, Choix3Q3.Text, ReponseQ3.Text, fileCoName.Text, direction2);
                    Levels.ProfAddQuestCO(Q4Co.Text, Choix1Q4.Text, Choix2Q4.Text, Choix3Q4.Text, ReponseQ4.Text, fileCoName.Text, direction2);
                    comboCo.Items.Add(fileCoName.Text);
                }
            }
        }

       

        private void Q1Co_Click(object sender, EventArgs e)
        {
            label29.Visible = false;
        }

      
        private void ProC1Q1L1_Click(object sender, EventArgs e)
        {
            label24.Visible = false;
        }

        private void ProC2Q1L1_Click(object sender, EventArgs e)
        {
            label24.Visible = false;
        }

        private void ProC3Q1L1_Click(object sender, EventArgs e)
        {
            label24.Visible = false;
        }

        private void ProRQ1_Click(object sender, EventArgs e)
        {
            label24.Visible = false;
        }

        private void ProFVerifLev2_Click(object sender, EventArgs e)
        {
            bool ok;
            ok = AllInformations(panel83);
            if (!ok)
            {
                label25.Visible = true;
                label25.Text = "Enter all informations";
            }
            else
            {
                Levels.ProfAddQuest(ProQuest1Lev2.Text, ProC1Q1L2.Text, ProC1Q1L2.Text, ProC3Q1L2.Text, ProRQ2.Text, 2);
                ClearPRO(panel83);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool ok ;
            ok = AllInformations(panel85);
            if (!ok)
            {
                label26.Visible = true;
                label26.Text = "Enter all informations";
            }
            else
            {
                Levels.ProfAddQuest(ProQuest1Lev3.Text, ProC1Q1L3.Text, ProC2Q1L3.Text, ProC3Q1L3.Text, ProRQ3.Text, 3);
                ClearPRO(panel85);
            }
        }

        private void nb5_Click(object sender, EventArgs e)
        {
            nb5.Visible = false;
            doneCompte[4] = true;
            if (numb1.Text == "") numb1.Text = nb5.Text;
            else
            {
                numb2.Text = nb5.Text;
                numb1.Visible = false;
                numb2.Visible = true;
            }
            nb1.Enabled = false;
            nb2.Enabled = false;
            nb3.Enabled = false;
            nb4.Enabled = false;
            nb5.Enabled = false;
        }

        private void plus_Click(object sender, EventArgs e)
        {
            bon = 1;
            numb1.Visible = false;
            numb2.Visible = true;
            plus.Visible = false;
            moin.Enabled = false;
            foi.Enabled = false;
            divise.Enabled = false;
            nb1.Enabled = true;
            nb2.Enabled = true;
            nb3.Enabled = true;
            nb4.Enabled = true;
            nb5.Enabled = true;
        }

        private void moin_Click(object sender, EventArgs e)
        {
            bon = 2;
            numb1.Visible = false;
            numb2.Visible = true;
            plus.Enabled = false;
            moin.Visible = false;
            foi.Enabled = false;
            divise.Enabled = false;
            nb1.Enabled = true;
            nb2.Enabled = true;
            nb3.Enabled = true;
            nb4.Enabled = true;
            nb5.Enabled = true;
        }

        private void foi_Click(object sender, EventArgs e)
        {
            bon = 3;
            numb1.Visible = false;
            numb2.Visible = true;
            plus.Enabled = false;
            moin.Enabled = false;
            foi.Visible= false;
            divise.Enabled = false;
            nb1.Enabled = true;
            nb2.Enabled = true;
            nb3.Enabled = true;
            nb4.Enabled = true;
            nb5.Enabled = true;     
        }

        private void divise_Click(object sender, EventArgs e)
        {
            bon = 4;
            numb1.Visible = false;
            numb2.Visible = true;
            plus.Enabled = false;
            moin.Enabled = false;
            foi.Enabled = false;
            divise.Visible= false;
            nb1.Enabled = true;
            nb2.Enabled = true;
            nb3.Enabled = true;
            nb4.Enabled = true;
            nb5.Enabled = true;
        }

        private void egual_Click(object sender, EventArgs e)
        {
            plus.Enabled = true;
            moin.Enabled = true;
            foi.Enabled = true;
            divise.Enabled = true;
            nb1.Enabled = false;
            nb2.Enabled = false;
            nb3.Enabled = false;
            nb4.Enabled = false;
            nb5.Enabled = false;
            if (DoneCompte(doneCompte))
            {
                Mveriflev4.Visible = true;
                Mveriflev4.Location = nb1.Location;
                Mveriflev4.Width = panel78.Width - 200;
            }
            try
            {
                switch (bon)
                {
                    case 1: numb1.Text = (int.Parse(numb1.Text) + int.Parse(numb2.Text)).ToString();
                        numb1.Visible = true;
                        numb2.Visible = false;
                        numb2.Clear();


                        break;
                    case 2: numb1.Text = (int.Parse(numb1.Text) - int.Parse(numb2.Text)).ToString();
                        numb1.Visible = true;
                        numb2.Visible = false;
                        numb2.Clear();

                        break;
                    case 3: numb1.Text = (int.Parse(numb1.Text) * int.Parse(numb2.Text)).ToString();
                        numb1.Visible = true;
                        numb2.Visible = false;
                        numb2.Clear();

                        break;
                    case 4: numb1.Text = (int.Parse(numb1.Text) / int.Parse(numb2.Text)).ToString();
                        numb1.Visible = true;
                        numb2.Visible = false;
                        numb2.Clear();
                        break;
                }
            }
            catch 
            {
              
            }

        }

        private void pictureBox68_Click(object sender, EventArgs e)
        {
            speak.SpeakAsyncCancelAll();
            tabcontrol1.SelectTab(Frlevels);
        }

        private void Conjuguaison_Click(object sender, EventArgs e)
        {
            player.Stop();
            tabControl2.SelectTab(verbimp);
            tabcontrol1.SelectTab(CoursChoisi);
            
        }

        private void Adjectif_Click(object sender, EventArgs e)
        {
            player.Stop();
            SingPlur.BackColor = Color.FromArgb(255, 51, 85);
            tabControl2.SelectTab(SingPlur);
            tabcontrol1.SelectTab(CoursChoisi);
            
            StreamReader fr = new StreamReader("AccordAdjectif.txt");
            textBox4.ScrollBars = ScrollBars.Both;
            textBox4.Text = fr.ReadToEnd();
            textBox4.ReadOnly = true;
        }

        private void pictureBox92_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(choosePage);
        }

        private void metroTile5_Click_1(object sender, EventArgs e)
        {
            tabControl4.SelectTab(Prolev1Ma);
        }

        private void tabcontrol1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
        }

        private void button123456_Click(object sender, EventArgs e)
        {
            string direction = "textes//" + fileName.Text.ToString() + ".txt";
            comboBox1.Items.Add(fileName.Text.ToString());
            Levels.ProfAddDicte(ajDicte.Text.ToString(), direction);
        }

        private void metroTile4_Click_1(object sender, EventArgs e)
        {
            tabControl4.SelectTab(Prolev2Ma);
        }

        private void basic_Click(object sender, EventArgs e)
        {
            restart.Enabled = false;
            start.Enabled = true;
            set = 1;
            panel12405612.Visible = false;
            panel21395419754.Visible = true;
            label34758679.Location = new Point((tabcontrol1.Width - label34758679.Width) / 2, panel21395419754.Location.Y + panel21395419754.Height + 5);
            label346365.Visible = false;
        }

        private void inter_Click(object sender, EventArgs e)
        {
            restart.Enabled = false;
            start.Enabled = true;
            set = 2;
            panel12405612.Visible = false;
            panel21395419754.Visible = true;
            label34758679.Location = new Point((tabcontrol1.Width - label34758679.Width) / 2, panel21395419754.Location.Y + panel21395419754.Height + 5);
            label346365.Visible = false;
        }

        private void higher_Click(object sender, EventArgs e)
        {
            restart.Enabled = false;
            start.Enabled = true;
            set = 3;
            panel12405612.Visible = false;
            panel21395419754.Visible = true;
            label34758679.Location = new Point((tabcontrol1.Width - label34758679.Width) / 2, panel21395419754.Location.Y + panel21395419754.Height + 5);
            label346365.Visible = false;
        }

        private void start_Click(object sender, EventArgs e)
        {
            starttest();
            start.Enabled = false;
            restart.Enabled = false;
            basic.Enabled = false;
            inter.Enabled = false;
            higher.Enabled = false;
            time = 60;
            timer.Start();
            next.Enabled = true;
            label346365.Text = "IQ SCORE ";
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (textBox.Text != "")
            {

                try
                {
                    answer = int.Parse(textBox.Text);
                    if (answer == qans)
                    {
                        iq++;

                    }
                    real++;
                    label234069138065180.Text = "QUESTION " + real;
                    textBox.Text = "";
                    starttest();
                }
                catch
                {
                    MessageBox.Show("RECOMMENCER!IL FAUT ECRIRE UN NOMBRE");
                }
            }
            else
            {
                MessageBox.Show("PLEASE ENTER YOUR ANSWER");
            }
        }

        private void restart_Click(object sender, EventArgs e)
        {
            number1 = 0;
            number2 = 0;
            number3 = 0;
            iq = 0;
            qans = 0;
            go = 0;
            real = 1;
            answer = 0;

            submit.Enabled = false;
            label34758679.Text = "VOUS AVEZ 60 SECONDES";
            textBox.Text = "";
            label234069138065180.Text = "QUESTION 1";
            num1.Text = "00";
            num2.Text = "00";
            set = 0;
            panel12405612.Visible = true;
            panel21395419754.Visible = false;
            basic.Enabled = true;
            inter.Enabled = true;
            higher.Enabled = true;
            label346365.Visible = true;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            timer.Stop();
            restart.Enabled = true;
            next.Enabled = false;
            label346365.Enabled = true;
            submit.Enabled = false;
            x = iq;
            y = real - 1;
            if (real == 1 && x == 0)
            {
                timer.Stop();
                MessageBox.Show("YOU WAISTED TIME");


            }
            else
            {
                go = x * 100 / y;
                MessageBox.Show("VOTRE IQ EST " + go + " YOU HAD CORRECT ANSWER " + iq + " OUT OF QUESTIONS " + y);
                label346365.Text = "VOTRE IQ SCORE  " + go.ToString();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            animals.Add(pictureBox102);
            animals.Add(pictureBox107);
            animals.Add(pictureBox106);
            animals.Add(pictureBox114);
            vehicul.Add(pictureBox104);
            vehicul.Add(pictureBox105);
            vehicul.Add(pictureBox108);
            vehicul.Add(pictureBox113);
            technologie.Add(pictureBox99);
            technologie.Add(pictureBox112);
            technologie.Add(pictureBox111);
            technologie.Add(pictureBox109);
            batiment.Add(pictureBox100);
            batiment.Add(pictureBox101);
            batiment.Add(pictureBox110);
            batiment.Add(pictureBox103);
            tabcontrol1.SelectTab(Extra);
            TankVerif.Start();
            foreach (PictureBox p in flowLayoutPanel2.Controls)
            {
                p.Enabled = true;
                panel101.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel3.Controls)
            {
                p1.Enabled = true;
                panel101.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel4.Controls)
            {
                p2.Enabled = true;
                panel101.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel5.Controls)
            {
                p3.Enabled = true;
                panel101.Controls.Add(p3);

            }
            flowLayoutPanel2.Location = new Point((tabcontrol1.Width - (2 * (flowLayoutPanel2.Width) + 450)) / 2, (tabcontrol1.Height - (2 * (flowLayoutPanel2.Height) + 250)) / 2);
            flowLayoutPanel3.Location = new Point(flowLayoutPanel2.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel2.Location.Y);
            flowLayoutPanel4.Location = new Point(flowLayoutPanel2.Location.X, flowLayoutPanel2.Location.Y + 150 + (flowLayoutPanel2.Height));
            flowLayoutPanel5.Location = new Point(flowLayoutPanel4.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel4.Location.Y);
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            label58.Visible = true;
            label59.Visible = true;
            label60.Visible = true;
            label61.Visible = true;

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                label34758679.Text = "IL RESTE " + time + " SECONDES";
            }
            else
            {
                if (real == 1 && x == 0)
                {
                    timer.Stop();
                    MessageBox.Show("YOU WAISTED TIME");
                    restart.Enabled = true;
                    next.Enabled = false;
                    label346365.Enabled = false;
                    submit.Enabled = false;
                }
                else
                {
                    timer.Stop();
                    restart.Enabled = true;
                    next.Enabled = false;
                    label346365.Enabled = true;
                    submit.Enabled = false;
                    x = iq;
                    y = real - 1;
                    go = x * 100 / y;
                    MessageBox.Show("VOTRE IQ EST " + go +" , " + " VOUS AVEZ COLLECTÉ " + iq + " RÉPONSES JUSTES DE " + y +" QUESTIONS");
                    label346365.Text = "VOTRE IQ EST " + go.ToString();
                    label346365.Location = new Point((tabcontrol1.Width - label346365.Width) / 2);
                }

            }
        }

        private void metroTile3_Click_1(object sender, EventArgs e)
        {
            tabControl4.SelectTab(Prolev3Ma);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            // Hide both icons.
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset firstClicked and secondClicked  
            // so the next time a label is 
            // clicked, the program knows it's the first click.
            firstClicked = null;
            secondClicked = null;
        }

        private void metroTile8_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(IQTest);
        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(matching);
            AssignIconsToSquares();
        }

        private void pictureBox97_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(choosegame);
            timer.Stop();
        }

        private void pictureBox98_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(choosegame);
        }

        private void pictureBox99_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = sender as PictureBox;
            pdef = p.Location;
            if (e.Button == MouseButtons.Left)
            {
                xPos = e.X;
                yPos = e.Y;
            }
        }

        private void pictureBox99_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox p = sender as PictureBox;
            p.BringToFront();
            if (p != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    p.Top += (e.Y - yPos);
                    p.Left += (e.X - xPos);
                }
            }
        }

        private void pictureBox99_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox p = sender as PictureBox;
            if (french)
            {
                switch ((InPanel(p.Location.X, p.Location.Y)))
                {
                    case 1:
                        flowLayoutPanel2.Controls.Add(p);
                        p.Enabled = false;
                        break;
                    case 2:
                        flowLayoutPanel3.Controls.Add(p);
                        p.Enabled = false;
                        break;
                    case 3:
                        flowLayoutPanel4.Controls.Add(p);
                        p.Enabled = false;
                        break;
                    case 4:
                        flowLayoutPanel5.Controls.Add(p);
                        p.Enabled = false;
                        break;
                    case 0:
                        p.Location = pdef;
                        break;
                }
                switch (CategoryPic(animals, technologie, batiment, vehicul, p))
                {
                    case "animals":
                        if (AtPanel(p, flowLayoutPanel3))
                        {
                            correct.Play();
                            ExtraPoints++;
                        }
                        else False.Play();
                        break;
                    case "technology":
                        if (AtPanel(p, flowLayoutPanel2))
                        {
                            correct.Play();
                            ExtraPoints++;
                        }
                        else False.Play();
                        break;
                    case "batiment":
                        if (AtPanel(p, flowLayoutPanel4))
                        {
                            correct.Play();
                            ExtraPoints++;
                        }
                        else False.Play();
                        break;
                    case "vehicule":
                        if (AtPanel(p, flowLayoutPanel5))
                        {
                            correct.Play();
                            ExtraPoints++;
                        }
                        else False.Play();
                        break;
                }
            }
            else
            {

                switch ((InPanel1(p.Location.X, p.Location.Y)))
                {
                    case 1:
                        flowLayoutPanel9.Controls.Add(p);
                        p.Enabled = false;
                        break;
                    case 2:
                        flowLayoutPanel8.Controls.Add(p);
                        p.Enabled = false;
                        break;
                    case 3:
                        flowLayoutPanel7.Controls.Add(p);
                        p.Enabled = false;
                        break;
                    case 4:
                        flowLayoutPanel6.Controls.Add(p);
                        p.Enabled = false;
                        break;
                    case 0:
                        p.Location = pdef;
                        break;
                }

                switch (CategoryPic1(cercle, rectangle, triangle, autreforme, p))
                {
                    case "cercle":
                        if (AtPanel(p, flowLayoutPanel9))
                        {
                            correct.Play();
                            ExtraPoints++;
                        }
                        else False.Play();
                        break;
                    case "rectangle":
                        if (AtPanel(p, flowLayoutPanel8))
                        {
                            correct.Play();
                            ExtraPoints++;
                        }
                        else False.Play();
                        break;
                    case "triangle":
                        if (AtPanel(p, flowLayoutPanel7))
                        {
                            correct.Play();
                            ExtraPoints++;
                        }
                        else False.Play();
                        break;
                    case "autreforme":
                        if (AtPanel(p, flowLayoutPanel6))
                        {
                            correct.Play();
                            ExtraPoints++;
                        }
                        else False.Play();
                        break;
                }
            }


        }

        private void tankVerif_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox109_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox99_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (((flowLayoutPanel3.Location.X - flowLayoutPanel2.Location.X - flowLayoutPanel2.Width) > 5) && ((flowLayoutPanel4.Location.Y - flowLayoutPanel2.Location.Y - flowLayoutPanel2.Width) > 5) && ((flowLayoutPanel5.Location.X - flowLayoutPanel4.Location.X - flowLayoutPanel4.Width) > 5) && ((flowLayoutPanel5.Location.Y - flowLayoutPanel3.Location.Y - flowLayoutPanel3.Width) > 5))
            {
                flowLayoutPanel2.Location = new Point(flowLayoutPanel2.Location.X + 5, flowLayoutPanel2.Location.Y + 5);
                flowLayoutPanel3.Location = new Point(flowLayoutPanel3.Location.X - 5, flowLayoutPanel3.Location.Y + 5);
                flowLayoutPanel4.Location = new Point(flowLayoutPanel4.Location.X + 5, flowLayoutPanel4.Location.Y - 5);
                flowLayoutPanel5.Location = new Point(flowLayoutPanel5.Location.X - 5, flowLayoutPanel5.Location.Y - 5);
            }
            else
            {
                if (((flowLayoutPanel3.Location.X - flowLayoutPanel2.Location.X - flowLayoutPanel2.Width) > 7) && ((flowLayoutPanel5.Location.X - flowLayoutPanel4.Location.X - flowLayoutPanel4.Width) > 7))
                {
                    flowLayoutPanel2.Location = new Point(flowLayoutPanel2.Location.X +7, flowLayoutPanel2.Location.Y);
                    flowLayoutPanel3.Location = new Point(flowLayoutPanel3.Location.X - 7, flowLayoutPanel3.Location.Y);
                    flowLayoutPanel4.Location = new Point(flowLayoutPanel4.Location.X + 7, flowLayoutPanel4.Location.Y);
                    flowLayoutPanel5.Location = new Point(flowLayoutPanel5.Location.X - 7, flowLayoutPanel5.Location.Y);
                }
                else
                {
                    timer2.Stop();
                    MessageBox.Show("Vous avez collecté " + (((ExtraPoints * 100) / 16).ToString()) + " points");
                }
                
            }
        }

        private void pictureBox115_Click(object sender, EventArgs e)
        {
            ExtraPoints = 0;
            cercle.Add(pictureBox143);
            cercle.Add(pictureBox138);
            cercle.Add(pictureBox142);
            cercle.Add(pictureBox140);
            triangle.Add(pictureBox147);
            triangle.Add(pictureBox146);
            triangle.Add(pictureBox144);
            triangle.Add(pictureBox153);
            rectangle.Add(pictureBox141);
            rectangle.Add(pictureBox150);
            rectangle.Add(pictureBox139);
            rectangle.Add(pictureBox148);
            autreforme.Add(pictureBox145);
            autreforme.Add(pictureBox149);
            autreforme.Add(pictureBox151);
            autreforme.Add(pictureBox152);

            foreach (PictureBox p in flowLayoutPanel9.Controls)
            {
                p.Enabled = true;
                panel102.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel8.Controls)
            {
                p1.Enabled = true;
                panel102.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel7.Controls)
            {
                p2.Enabled = true;
                panel102.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel6.Controls)
            {
                p3.Enabled = true;
                panel102.Controls.Add(p3);

            }
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox138.Location = pictureBox100.Location;
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox139.Location = pictureBox110.Location;
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox140.Location = pictureBox105.Location;
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox141.Location = pictureBox108.Location;
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox142.Location = pictureBox103.Location;
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox143.Location = pictureBox109.Location;
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox144.Location = pictureBox101.Location;
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox145.Location = pictureBox102.Location;
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox146.Location = pictureBox104.Location;
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox147.Location = pictureBox107.Location;
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox148.Location = pictureBox106.Location;
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox149.Location = pictureBox111.Location;
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox150.Location = pictureBox114.Location;
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox151.Location = pictureBox113.Location;
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox152.Location = pictureBox112.Location;
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            pictureBox153.Location = pictureBox99.Location;
            label57.Visible = true;
            label56.Visible = true;
            label55.Visible = true;
            label54.Visible = true;


            TankVerif.Stop();
            timerTank = 60;
            tabcontrol1.SelectTab(choosePage);
            foreach (PictureBox p in flowLayoutPanel2.Controls)
            {
                p.Enabled = true;
                panel101.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel3.Controls)
            {
                p1.Enabled = true;
                panel101.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel4.Controls)
            {
                p2.Enabled = true;
                panel101.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel5.Controls)
            {
                p3.Enabled = true;
                panel101.Controls.Add(p3);

            }
            flowLayoutPanel2.Location = new Point((tabcontrol1.Width - (2 * (flowLayoutPanel2.Width) + 450)) / 2, (tabcontrol1.Height - (2 * (flowLayoutPanel2.Height) + 250)) / 2);
            flowLayoutPanel3.Location = new Point(flowLayoutPanel2.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel2.Location.Y);
            flowLayoutPanel4.Location = new Point(flowLayoutPanel2.Location.X, flowLayoutPanel2.Location.Y + 150 + (flowLayoutPanel2.Height));
            flowLayoutPanel5.Location = new Point(flowLayoutPanel4.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel4.Location.Y);
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            label58.Visible = true;
            label59.Visible = true;
            label60.Visible = true;
            label61.Visible = true;

        }

        private void metroTile9_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(Extra);
            tabControl7.SelectTab(tabPage5);
              foreach (PictureBox p in flowLayoutPanel2.Controls)
            {
                p.Enabled = true;
                panel101.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel3.Controls)
            {
                p1.Enabled = true;
                panel101.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel4.Controls)
            {
                p2.Enabled = true;
                panel101.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel5.Controls)
            {
                p3.Enabled = true;
                panel101.Controls.Add(p3);

            }
            flowLayoutPanel2.Location = new Point((tabcontrol1.Width - (2 * (flowLayoutPanel2.Width) + 450)) / 2, (tabcontrol1.Height - (2 * (flowLayoutPanel2.Height) + 250)) / 2);
            flowLayoutPanel3.Location = new Point(flowLayoutPanel2.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel2.Location.Y);
            flowLayoutPanel4.Location = new Point(flowLayoutPanel2.Location.X, flowLayoutPanel2.Location.Y + 150 + (flowLayoutPanel2.Height));
            flowLayoutPanel5.Location = new Point(flowLayoutPanel4.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel4.Location.Y);
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            label58.Visible = true;
            label59.Visible = true;
            label60.Visible = true;
            label61.Visible = true;

        }

        private void pictureBox122_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void pictureBox127_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void Alphabet_Click(object sender, EventArgs e)
        {
            player.Stop();
            tabControl2.SelectTab(typedephrase);
            tabcontrol1.SelectTab(CoursChoisi);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            player.Stop();
            StreamReader fr = new StreamReader("Homophones.txt");
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Text = fr.ReadToEnd();
            textBox1.ReadOnly = true;
            tabControl2.SelectTab(Homophones); 
            tabcontrol1.SelectTab(CoursChoisi);
        }

        private void pictureBox128_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(CoursFr);
        }

        private void pictureBox129_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(choosePage);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            game.NewGame(r1);
        }

        private void DataGridView1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 75, 0, 75, 228);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 150, 0, 150, 228);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 0, 66, 228, 66);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 0, 132, 228, 132);
        }

        private void btnSolution_Click(object sender, EventArgs e)
        {
            game.showGridSolution();
            MessageBox.Show("LES NOMBRES DE COULEUR BLEUES SONT CORRECTES!");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnNew.PerformClick();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            tabControl7.SelectTab(tabPage4);
        }

        private void xButton1_Click(object sender, EventArgs e)
        {
           
            if (!Prof)
            {


                string body = "";
                string marks = "";
                string toName = "";
                if (!signup)
                {
                    foreach (DataRow dr in usersData.Tables[0].Rows)
                    {
                        if (string.CompareOrdinal(dr[1].ToString(), logUser.Text) == 0)
                        {
                            marks = user.GetPoints(logUser.Text);
                            toName = dr[0].ToString();
                        }
                    }
                    body = "Dear" + toName + ",\n thank you for using our platform, your marks and points you got at the latest login are:" + marks + "\n Regards";
                   // try
                    {
                        sendMailForgot(logUser.Text, body);
                    }
                   // catch
                    {
                     //   MessageBox.Show("Report of ur work today is not sent");
                    }
                }
                else
                {
                    logPanel.Visible = true;
                    signPanel.Visible = false;
                    pictureBox67.Visible = false;
                }
            }
            logUser.Clear();
            logPass.Clear();
            logCombo.Text = "";
            tabcontrol1.SelectTab(logPage);
            Lev1butt.Enabled = false;
            Lev2butt.Enabled = false;
            Lev3butt.Enabled = false;
            Lev4butt.Enabled = false;
            Lev5butt.Enabled = false;
            Mlev1butt.Enabled = false;
            Mlev2butt.Enabled = false;
            Mlev3butt.Enabled = false;
            Mlev4butt.Enabled = false;

            signUser.Clear();
            signPass.Clear();
            signPass1.Clear();
            signCombo.Text = "";
            realName.Clear();
            dadPhone.Clear();
            dadMail.Clear();
            questCombo.Text = "";
            questAns.Clear();
            Prof = false;
            signup = false;
        }

        private void pictureBox130_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(Extra);
            tabControl7.SelectTab(tabPage4);
            animals.Add(pictureBox102);
            animals.Add(pictureBox107);
            animals.Add(pictureBox106);
            animals.Add(pictureBox114);
            vehicul.Add(pictureBox104);
            vehicul.Add(pictureBox105);
            vehicul.Add(pictureBox108);
            vehicul.Add(pictureBox113);
            technologie.Add(pictureBox99);
            technologie.Add(pictureBox112);
            technologie.Add(pictureBox111);
            technologie.Add(pictureBox109);
            batiment.Add(pictureBox100);
            batiment.Add(pictureBox101);
            batiment.Add(pictureBox110);
            batiment.Add(pictureBox103);
            tabcontrol1.SelectTab(Extra);
            TankVerif.Start();
            foreach (PictureBox p in flowLayoutPanel2.Controls)
            {
                p.Enabled = true;
                panel101.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel3.Controls)
            {
                p1.Enabled = true;
                panel101.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel4.Controls)
            {
                p2.Enabled = true;
                panel101.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel5.Controls)
            {
                p3.Enabled = true;
                panel101.Controls.Add(p3);

            }
            flowLayoutPanel2.Location = new Point((tabcontrol1.Width - (2 * (flowLayoutPanel2.Width) + 450)) / 2, (tabcontrol1.Height - (2 * (flowLayoutPanel2.Height) + 250)) / 2);
            flowLayoutPanel3.Location = new Point(flowLayoutPanel2.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel2.Location.Y);
            flowLayoutPanel4.Location = new Point(flowLayoutPanel2.Location.X, flowLayoutPanel2.Location.Y + 150 + (flowLayoutPanel2.Height));
            flowLayoutPanel5.Location = new Point(flowLayoutPanel4.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel4.Location.Y);
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            label58.Visible = true;
            label59.Visible = true;
            label60.Visible = true;
            label61.Visible = true;

        }

        private void pictureBox137_Click(object sender, EventArgs e)
        {
            timerTank = 60;
            foreach (PictureBox p in flowLayoutPanel2.Controls)
            {
                p.Enabled = true;
                panel101.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel3.Controls)
            {
                p1.Enabled = true;
                panel101.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel4.Controls)
            {
                p2.Enabled = true;
                panel101.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel5.Controls)
            {
                p3.Enabled = true;
                panel101.Controls.Add(p3);

            }
            flowLayoutPanel2.Location = new Point((tabcontrol1.Width - (2 * (flowLayoutPanel2.Width) + 450)) / 2, (tabcontrol1.Height - (2 * (flowLayoutPanel2.Height) + 250)) / 2);
            flowLayoutPanel3.Location = new Point(flowLayoutPanel2.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel2.Location.Y);
            flowLayoutPanel4.Location = new Point(flowLayoutPanel2.Location.X, flowLayoutPanel2.Location.Y + 150 + (flowLayoutPanel2.Height));
            flowLayoutPanel5.Location = new Point(flowLayoutPanel4.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel4.Location.Y);
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            label58.Visible = true;
            label59.Visible = true;
            label60.Visible = true;
            label61.Visible = true;



            tabcontrol1.SelectTab(Extra);
            tabControl7.SelectTab(tabPage5);
            TankVerif.Start();
            timerTank = 60;
            cercle.Add(pictureBox143);
            cercle.Add(pictureBox138);
            cercle.Add(pictureBox142);
            cercle.Add(pictureBox140);
            triangle.Add(pictureBox147);
            triangle.Add(pictureBox146);
            triangle.Add(pictureBox144);
            triangle.Add(pictureBox153);
            rectangle.Add(pictureBox141);
            rectangle.Add(pictureBox150);
            rectangle.Add(pictureBox139);
            rectangle.Add(pictureBox148);
            autreforme.Add(pictureBox145);
            autreforme.Add(pictureBox149);
            autreforme.Add(pictureBox151);
            autreforme.Add(pictureBox152);

            foreach (PictureBox p in flowLayoutPanel9.Controls)
            {
                p.Enabled = true;
                panel102.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel8.Controls)
            {
                p1.Enabled = true;
                panel102.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel7.Controls)
            {
                p2.Enabled = true;
                panel102.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel6.Controls)
            {
                p3.Enabled = true;
                panel102.Controls.Add(p3);

            }
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox138.Location = pictureBox100.Location;
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox139.Location = pictureBox110.Location;
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox140.Location = pictureBox105.Location;
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox141.Location = pictureBox108.Location;
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox142.Location = pictureBox103.Location;
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox143.Location = pictureBox109.Location;
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox144.Location = pictureBox101.Location;
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox145.Location = pictureBox102.Location;
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox146.Location = pictureBox104.Location;
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox147.Location = pictureBox107.Location;
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox148.Location = pictureBox106.Location;
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox149.Location = pictureBox111.Location;
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox150.Location = pictureBox114.Location;
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox151.Location = pictureBox113.Location;
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox152.Location = pictureBox112.Location;
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            pictureBox153.Location = pictureBox99.Location;
            label57.Visible = true;
            label56.Visible = true;
            label55.Visible = true;
            label54.Visible = true;
        }

        private void pictureBox136_Click(object sender, EventArgs e)
        {
            timerTank = 60;
            tabcontrol1.SelectTab(choosePage);
            foreach (PictureBox p in flowLayoutPanel2.Controls)
            {
                p.Enabled = true;
                panel101.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel3.Controls)
            {
                p1.Enabled = true;
                panel101.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel4.Controls)
            {
                p2.Enabled = true;
                panel101.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel5.Controls)
            {
                p3.Enabled = true;
                panel101.Controls.Add(p3);

            }
            flowLayoutPanel2.Location = new Point((tabcontrol1.Width - (2 * (flowLayoutPanel2.Width) + 450)) / 2, (tabcontrol1.Height - (2 * (flowLayoutPanel2.Height) + 250)) / 2);
            flowLayoutPanel3.Location = new Point(flowLayoutPanel2.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel2.Location.Y);
            flowLayoutPanel4.Location = new Point(flowLayoutPanel2.Location.X, flowLayoutPanel2.Location.Y + 150 + (flowLayoutPanel2.Height));
            flowLayoutPanel5.Location = new Point(flowLayoutPanel4.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel4.Location.Y);
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            label58.Visible = true;
            label59.Visible = true;
            label60.Visible = true;
            label61.Visible = true;




            TankVerif.Stop();
            timerTank = 60;
            tabcontrol1.SelectTab(choosePage);
            foreach (PictureBox p in flowLayoutPanel9.Controls)
            {
                p.Enabled = true;
                panel102.Controls.Add(p);

            }
            foreach (PictureBox p1 in flowLayoutPanel8.Controls)
            {
                p1.Enabled = true;
                panel102.Controls.Add(p1);

            }
            foreach (PictureBox p2 in flowLayoutPanel7.Controls)
            {
                p2.Enabled = true;
                panel102.Controls.Add(p2);

            }
            foreach (PictureBox p3 in flowLayoutPanel6.Controls)
            {
                p3.Enabled = true;
                panel102.Controls.Add(p3);

            }
            flowLayoutPanel9.Location = new Point((tabcontrol1.Width - (2 * (flowLayoutPanel2.Width) + 450)) / 2, (tabcontrol1.Height - (2 * (flowLayoutPanel2.Height) + 250)) / 2);
            flowLayoutPanel8.Location = new Point(flowLayoutPanel2.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel2.Location.Y);
            flowLayoutPanel7.Location = new Point(flowLayoutPanel2.Location.X, flowLayoutPanel2.Location.Y + 150 + (flowLayoutPanel2.Height));
            flowLayoutPanel6.Location = new Point(flowLayoutPanel4.Location.X + 450 + (flowLayoutPanel2.Width), flowLayoutPanel4.Location.Y);
            pictureBox100.Location = new Point(flowLayoutPanel2.Location.X + flowLayoutPanel2.Width + 20, flowLayoutPanel2.Location.Y + (flowLayoutPanel2.Height) / 2);
            pictureBox138.Location = pictureBox100.Location;
            pictureBox110.Location = new Point(pictureBox100.Location.X + pictureBox100.Width + 2, pictureBox100.Location.Y);
            pictureBox139.Location = pictureBox110.Location;
            pictureBox105.Location = new Point(pictureBox110.Location.X + pictureBox110.Width + 2, pictureBox100.Location.Y);
            pictureBox140.Location = pictureBox105.Location;
            pictureBox108.Location = new Point(pictureBox105.Location.X + pictureBox105.Width + 2, pictureBox100.Location.Y);
            pictureBox141.Location = pictureBox108.Location;
            pictureBox103.Location = new Point(pictureBox100.Location.X, pictureBox100.Location.Y + pictureBox100.Height + 2);
            pictureBox142.Location = pictureBox103.Location;
            pictureBox109.Location = new Point(pictureBox103.Location.X + pictureBox103.Width + 2, pictureBox103.Location.Y);
            pictureBox143.Location = pictureBox109.Location;
            pictureBox101.Location = new Point(pictureBox109.Location.X + 2 + pictureBox109.Width, pictureBox109.Location.Y);
            pictureBox144.Location = pictureBox101.Location;
            pictureBox102.Location = new Point(pictureBox101.Location.X + 2 + pictureBox101.Width, pictureBox101.Location.Y);
            pictureBox145.Location = pictureBox102.Location;
            pictureBox104.Location = new Point(pictureBox100.Location.X, pictureBox102.Location.Y + pictureBox100.Height + 2);
            pictureBox146.Location = pictureBox104.Location;
            pictureBox107.Location = new Point(pictureBox104.Location.X + pictureBox101.Width + 2, pictureBox104.Location.Y);
            pictureBox147.Location = pictureBox107.Location;
            pictureBox106.Location = new Point(pictureBox107.Location.X + pictureBox101.Width + 2, pictureBox107.Location.Y);
            pictureBox148.Location = pictureBox106.Location;
            pictureBox111.Location = new Point(pictureBox106.Location.X + pictureBox101.Width + 2, pictureBox106.Location.Y);
            pictureBox149.Location = pictureBox111.Location;
            pictureBox114.Location = new Point(pictureBox100.Location.X, pictureBox111.Location.Y + pictureBox100.Height + 2);
            pictureBox150.Location = pictureBox114.Location;
            pictureBox113.Location = new Point(pictureBox114.Location.X + pictureBox100.Width + 2, pictureBox114.Location.Y);
            pictureBox151.Location = pictureBox113.Location;
            pictureBox112.Location = new Point(pictureBox113.Location.X + pictureBox100.Width + 2, pictureBox113.Location.Y);
            pictureBox152.Location = pictureBox112.Location;
            pictureBox99.Location = new Point(pictureBox112.Location.X + pictureBox100.Width + 2, pictureBox112.Location.Y);
            pictureBox153.Location = pictureBox99.Location;
            label57.Visible = true;
            label56.Visible = true;
            label55.Visible = true;
            label54.Visible = true;
        }

        private void flowLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (((flowLayoutPanel8.Location.X - flowLayoutPanel9.Location.X - flowLayoutPanel9.Width) > 5) && ((flowLayoutPanel7.Location.Y - flowLayoutPanel9.Location.Y - flowLayoutPanel9.Width) > 5) && ((flowLayoutPanel6.Location.X - flowLayoutPanel7.Location.X - flowLayoutPanel7.Width) > 5) && ((flowLayoutPanel6.Location.Y - flowLayoutPanel8.Location.Y - flowLayoutPanel8.Width) > 5))
            {
                flowLayoutPanel9.Location = new Point(flowLayoutPanel9.Location.X + 5, flowLayoutPanel9.Location.Y + 5);
                flowLayoutPanel8.Location = new Point(flowLayoutPanel8.Location.X - 5, flowLayoutPanel8.Location.Y + 5);
                flowLayoutPanel7.Location = new Point(flowLayoutPanel7.Location.X + 5, flowLayoutPanel7.Location.Y - 5);
                flowLayoutPanel6.Location = new Point(flowLayoutPanel6.Location.X - 5, flowLayoutPanel6.Location.Y - 5);
            }
            else
            {
                if (((flowLayoutPanel8.Location.X - flowLayoutPanel9.Location.X - flowLayoutPanel9.Width) > 7) && ((flowLayoutPanel6.Location.X - flowLayoutPanel7.Location.X - flowLayoutPanel7.Width) > 7))
                {
                    flowLayoutPanel9.Location = new Point(flowLayoutPanel9.Location.X + 7, flowLayoutPanel9.Location.Y);
                    flowLayoutPanel8.Location = new Point(flowLayoutPanel8.Location.X - 7, flowLayoutPanel8.Location.Y);
                    flowLayoutPanel7.Location = new Point(flowLayoutPanel7.Location.X + 7, flowLayoutPanel7.Location.Y);
                    flowLayoutPanel6.Location = new Point(flowLayoutPanel6.Location.X - 7, flowLayoutPanel6.Location.Y);
                }
                else
                {
                    timer3.Stop();
                    MessageBox.Show("Vous avez collecté " + (((ExtraPoints * 100) / 16).ToString()) + " points");
                }

            }
        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void xButton2_Click(object sender, EventArgs e)
        {
            nombre.Play();
        }

        private void TankVerif_Tick(object sender, EventArgs e)
        {
            if (timerTank > 0)
            {
                label50.Text = "VOUS AVEZ " + timerTank.ToString() + " SECONDES";
                label62.Text = "VOUS AVEZ " + timerTank.ToString() + " SECONDES";
                timerTank--;
                
            }
            if ((flowLayoutPanel2.Controls.Count + flowLayoutPanel3.Controls.Count + flowLayoutPanel4.Controls.Count + flowLayoutPanel5.Controls.Count == 16) && timerTank > 0)
            {
                TankVerif.Stop();
                label58.Visible = false;
                label59.Visible = false;
                label60.Visible = false;
                label61.Visible = false;
                timer2.Start();
                // MessageBox.Show("Vous avez collecté " + (((ExtraPoints*100)/16).ToString()) +" points");
                timerTank = 60;
            }
            else
            if ((flowLayoutPanel2.Controls.Count + flowLayoutPanel3.Controls.Count + flowLayoutPanel4.Controls.Count + flowLayoutPanel5.Controls.Count != 16) && timerTank == 0)
            {
                TankVerif.Stop();
                MessageBox.Show("GAME OVER");
                timerTank = 60;
            }
            if ((flowLayoutPanel9.Controls.Count + flowLayoutPanel8.Controls.Count + flowLayoutPanel7.Controls.Count + flowLayoutPanel6.Controls.Count == 16) && timerTank > 0)
            {
                TankVerif.Stop();
                label57.Visible = false;
                label56.Visible = false;
                label55.Visible = false;
                label54.Visible = false;
                timer3.Start();
                // MessageBox.Show("Vous avez collecté " + (((ExtraPoints*100)/16).ToString()) +" points");
                timerTank = 60;
            }
            else
          if ((flowLayoutPanel9.Controls.Count + flowLayoutPanel8.Controls.Count + flowLayoutPanel7.Controls.Count + flowLayoutPanel6.Controls.Count != 16) && timerTank == 0)
          {
                TankVerif.Stop();
                MessageBox.Show("GAME OVER");
                timerTank = 60;
          }

        }

        private void metroTile6_Click(object sender, EventArgs e)
        {
            DataGridView1.Rows.Add(9);
            comboBox2.SelectedIndex = 0;
            btnNew.PerformClick();
            game.ShowClues += game_ShowClues;
            game.ShowSolution += game_ShowSolution;
            tabcontrol1.SelectTab(sudoku);
        }

        private void metroTile2_Click_1(object sender, EventArgs e)
        {
            tabControl4.SelectTab(Prolev4Ma);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool ok;
            ok = AllInformations(panel93);
            if (!ok)
            {
                label30.Visible = true;
                label30.Text = "Enter all informations";
            }
            else
            {
                Levels.ProfAddQuest(PQL1M.Text, ProC1QL1M.Text, ProC2QL1M.Text, ProC3QL1M.Text, ProRQM.Text, 4);
                ClearPRO(panel93);
            }          
        }

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox93_Click(object sender, EventArgs e)
        {
            tabControl4.SelectTab(ProMaLevs);
        }

        private void metroTextBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void ProMaVerifLev2_Click(object sender, EventArgs e)
        {
            bool ok;
            ok = AllInformations(panel95);
            if (!ok)
            {
                label31.Visible = true;
                label31.Text = "Enter all informations";
            }
            else
            {
                Levels.ProfAddQuest(PQL2M.Text, ProC1QL2M.Text, ProC2QL2M.Text, ProC3QL2M.Text, ProRQML2.Text, 5);
                ClearPRO(panel95);
            }          
        }

        private void ProMaVerifLev3_Click(object sender, EventArgs e)
        {
            bool ok;
            ok = AllInformations(panel97);
            if (!ok)
            {
                label32.Visible = true;
                label32.Text = "Enter all informations";
            }
            else
            {
                Levels.ProfAddQuest(PQL3M.Text, ProC1QL3M.Text, ProC2QL3M.Text, ProC3QL3M.Text, ProRQML3.Text, 6);
                ClearPRO(panel97);
            }          
        }

        private void pictureBox94_Click(object sender, EventArgs e)
        {
            tabControl4.SelectTab(ProMaLevs);
        }

        private void pictureBox95_Click(object sender, EventArgs e)
        {
            tabControl4.SelectTab(ProMaLevs);
        }

        private void ProMaVerifLev4_Click(object sender, EventArgs e)
        {
            bool ok;
            ok = AllInformations(panel99);
            if (!ok)
            {
                label32.Visible = true;
                label32.Text = "Enter all informations";
            }
            else
            {
                Levels.ProfAddCompte(PQL4M.Text, ProRQML4.Text,Nomb1.Text,Nomb2.Text,Nomb3.Text,Nomb4.Text,Nomb5.Text,7);
                ClearPRO(panel99);
            }          
        }

        private void pictureBox96_Click(object sender, EventArgs e)
        {
            tabControl4.SelectTab(ProMaLevs);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectTab(choosegame);
        }


        private void label_Click(object sender, EventArgs e)
        {
            // The timer is only on after two non-matching  
            // icons have been shown to the player,  
            // so ignore any clicks if the timer is running 
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is black, the player clicked 
                // an icon that's already been revealed -- 
                // ignore the click.
                if (clickedLabel.ForeColor == Color.Black)
                    // All done - leave the if statements.
                    return;

                // If firstClicked is null, this is the first icon  
                // in the pair that the player clicked, 
                // so set firstClicked to the label that the player  
                // clicked, change its color to black, and return. 
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    // All done - leave the if statements.
                    return;
                }

                // If the player gets this far, the timer isn't 
                // running and firstClicked isn't null, 
                // so this must be the second icon the player clicked 
                // Set its color to black.
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Check to see if the player won.
                CheckForWinner();

                // If the player clicked two matching icons, keep them  
                // black and reset firstClicked and secondClicked  
                // so the player can click another icon. 
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // If the player gets this far, the player  
                // clicked two different icons, so start the  
                // timer (which will wait three quarters of  
                // a second, and then hide the icons).
                timer1.Start();
            }
        }
    }
}
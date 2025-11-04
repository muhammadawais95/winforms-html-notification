using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsHtmlNotification
{
    public partial class Form1 : Form
    {
        private FloatingHtmlWindow _toastNotification = new FloatingHtmlWindow();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            HideUserNotifications();
        }

        private void buttonDebugTestSuccessToast_Click(object sender, EventArgs e)
        {
            DisplayToastNotification(ToastNotificationType.Success, "Success", "Something worked as expected");
        }

        private void buttonDebugTestWarningToast_Click(object sender, EventArgs e)
        {
            DisplayToastNotification(ToastNotificationType.Warning, "Warning", "Hmmm... You have just gone over budget!");
        }

        private void buttonDebugTestErrorToast_Click(object sender, EventArgs e)
        {
            DisplayToastNotification(ToastNotificationType.Error, "Error", "Something went bad. Didn't expect that!");
        }

        private void buttonDebugTestInfoToast_Click(object sender, EventArgs e)
        {
            DisplayToastNotification(ToastNotificationType.Info, "Info", "Something has happened. I thought you should know!");
        }

        private void buttonDisplayToast_Click(object sender, EventArgs e)
        {
            _toastNotification.Close();

            string html = GenerateHTMLToast("test", "hello world");

            int imgHeight = _toastNotification.GetHtmlHeight(html);
            int imgWidth = 1000;

            _toastNotification.SetImgSize(imgWidth, imgHeight);
            _toastNotification.SetHtml(html);

            Rectangle rect = buttonDisplayToast.Bounds;
            Point px = new Point(rect.Left, rect.Bottom);
            Point screenLocation = PointToScreen(px);

            //m_htmlToast.SetImgLocation(screenLocation.X, screenLocation.Y);
            _toastNotification.SetImgLocation(Convert.ToInt32(textBoxX.Text), Convert.ToInt32(textBoxY.Text));

            int imageTimeout = Convert.ToInt32(textBoxTimeOut.Text);
            _toastNotification.Show(imageTimeout);
        }

        public void HideUserNotifications()
        {
            _toastNotification.Close();
        }

        private string GenerateHTML1()
        {
            string str = @"

                boom!!<br/>
                <br/><br/><br/>
                <img src='data:image/gif;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAABGdBTUEAALGPC/xhBQAACgpJREFUaEPtWGlsVNcVZkkaigSCQAJJaBNIpFYNwXhf8ILxhnd7vI2XsT2bZzxjj+3ZZ + zxgg3eGLxgzE5C2BKa / Gh / VPnTqqmq / qlUVSoRv6ruTdW0WdoATQjn9DvPz24cQK1gQonkT7rye89v7j3n3O9859y3bAlLWMISIoJPL61bQZdXr1Fvv5q4dnGtXr38auLjcxv6bp7ZlKfePtxo63Stb + 90bQp5rI + oj5a9f2Sbgc48d5Wmnn5SffRwodPlW9He5a6E8b / AX1LHP3s91qF3DkQv / yj8gommthGHn / slH3ymkMKbt / LhJ1 / gl9el0utfz + bLq1epUz04uP3dK3zB3g3eQOhxh9MbgsH / 6HS5L / l8zpweny2lz2dpPdajNf56ZPvKT8a3nqXeDcT + NcxDmz + jmec / 4LPP / o4vrAvR + XXr1SkfHLo8 / jUOp + d8h8v7Z6c38EdE / qajyz0aCPpWqK8s4P3Jb9fR6JYb1PkokWEZUWAN0cTWP9CRLcnqK18OKqprV1bW1D2NvzvwN0pb3xilN1u / aXd0rUK0T8ABHhoeZ + wCjYYnOdQ / 9Fe3x7Vd / fmytyeSN914dctxPrXtJqjDFFxL7HyMuX8j08FnctXXIosqbcMand6U29rWccbm6Lpqa +/ 6m8XuuN5ia79hsbVfb23vfB / R / i2MvzEze5KDoX1w4iD94K0f8uyJU9zhdE7LPD8 / vv35D89tMNIrT2j56DMpfGhLEY1teY1Hv3GTJ7deo9EnVioLRgpV2voVepPF3NbhvGKxOW6VV9bQ7j05lJiUStGx8bQzJp6iMRKTU8lksdPI + ARdfuP7FJ48Qh1OL5nxrFFvpvZO53l1yttAE5tX0tGt++n0tg / UR5FBQ5PhRUT7p0aLjXLyCjhqZyy9tCOaZWx / aeei69z8Inb7e / jia2 / ShUtvSvS5Wd9CybvSucnQwm0Oh0Wd9o7gY0 + t4osb36aTT6xVH90fdM3GfHuH8y / gOEfHJCjGftHo + esdUTGE3eGZ2VMs0T95 + hz7Ar1cXlFDSSlpDHrdstrbnerUdwVfXJtOF9fGq7f3DkQ + E8Zfzy8sXWTs3RyIiUukTrePz196g1//7vcoPHWEW+2dnJW9l7Jy8tna1sHYyU/NrW06dYm7Ar3RJvXy3lDXqN+OZHwvL79IDCQxNCo6jjL35FJxiYYKi8spIzObonbGKf9THSGztY0mpmbp3IXL1L9vmOoamikhMYWQ/FSqqVJ+j6Bcw85mqUtFHkjYVdjun1TU1C1ENzFpF+uaTGRqsbPeaCFdk5FhHJeWVUryLuwGEpvtDif3DQ6T0+3nMk0Vp+/OIol+anqm8k5RqUZ24irW2aguGVk0Gcwmk9V+KyZ2jvPgPkNB2GxpI4PRyrpmk0SWa2p1XFldR9m5BYvoJIbWNxkIOyi8l0GFJeWSI8o7EAFJaIL09qtLRg6IyiNY+DdQmwVqFBSVQQbbSG+0UuOc8aSt1ZEYX15RTUXFGtoJes2/Pz92xsRRQtIu2gHVmn8GB0mSPTMrTxz8qL5RH9k2AYmbj+gj6vELEa2tb2KDqZWboOP1Oj1r6xrFeFEXLgGFiksrF73/+d1YdI0diMauwilOwy4ZW2wybOrSkYHZaj+lqapdtHBDo0GMZ0SLYDxX1dSTpkIrxnNRaQUSWsPYgbs6gIEdiuf4hBRO2ZVOGbuzOStnrxIY0OstUPa2/uieUF3bsBrb+quMPdmLqKCp1FJ9o4FqEXnFeNxL8hbD+KKSCsrOKRBjF/1GhjwTlYpLSJJCRukZe0gkVZRNVEzmwnrv6k3WyCQzitVmTPghFlwUxfSMLCVa1dp6qqis5dLyKoU2MhB9io1b/L4M4X1sXCInJadKUtOerFzOzSvkgsIycZ4rsMvII8Z6tyC9L6om3B80VdpnMeGCEfMGiXpk5+bLogTjxXDFgfzCMhaNX/T+HM9Zkjc1bTeLrIogFBSWgHIVrEEPVa3VcV2DnpuazWzvcBFolKOacH9oNJgT0VneRoX5EZ+QTGmgQToKWFJK+m3qIg2dOJSSmkG78Q6cpr0FxaCZhso1NQpl6uqbCYaT0dxKllYHtcEB1I1S1YT7Ayi07U478MX7LzyXCs1xCcmcnJJGKFpI0DwWnhcVlyuFrApyKxSU4idS3GJtY1tbF3d0eVg5Wna696om3B/kcAIHPkZP8z85sCMqlsFzpVFLy8jkzKxcyt1byIVFZciTShKeS7Fr0BlIj04UtUR6I3J0etjpDrDXH1IcQLu9Y86C+0RNnU5U6IqUfjHyTgOGK4VIGjfp/Xel7Vb6G+E5mj4qQX6IStWg92nQ6UlaaVOLjVptHdQOunS5/OTx9VCgu5+CPQOiQu91uf2R+xKB8n5Rtv3zkf7PiBGeczx4vis1g8FzSW7wvITR4KGwVSNBG5CgzSwV24jiB56jN3JRp9PHbm83+7v7qKdviPsHRzgQGmCc3H7s8gYjd/pCz1NjMLfeVphUnhMKkdKcSSHai8MLeE5lmmrpiVjqhPDcYLJyi7Wd7O1zPHd5giTngm4cL/v2HaDBA+M8PHaI3b5uRvR96tKRAWj0NWzru3uy8xS6yOkrNj5JeK4okDwHz9EflYukUiWkVVoLXaMRPLcoPZNN4bkb3WiAvIEQBUMD1DcAw/eP0YHRQzR+6DANj03Id6FrHn/P5rmVIwhDS6sX1RGymSI8Z9HzTOg5ChEMLxU9R0HTKgla36CnJn0LS5tttQvP3Qyes8LzngEO9e/ngaFRGB7msfAU45BDUzPH2dfdSx0ur3LAjzi09Y1rcei4AlWCnucoep4PPZeDjBwPF+u5jayi5w4neO4l8Jz8QfC8d5AGBkdIvkiMjE9SeGJGDKfZY2eU6IP7f0I+bFGXjDyajS1JoNJHwm0UIi4Hz6vgUB30vBEHG+lOLdZ2xhFXZJCRiMr3HxiOBB3mweFxMZwPTszw5OFjdOTYaT5x6iyLEzD+Xzj4l6lLfTnAkW85zq5aVMlPpBuVLhR/cSawIEHBcxQiVc+F58r3n945nisJOhaeponpozjgn+RjJ1+h069c4OnZE9zl8X/m9AQCof6hyHSg/w1Wu8OAnbhmRrTBc9FzVvVceM5B6HkveL5vaJT3j4ZJeH5oapanj5ygoyde5lNnzvHZV18j2Qkc+D91eoN9MH65Ov2DAZqtVDjxDobykUrhOQyHntPA0AjtHwnT6MEp5SOWwvPjZ+STCr189pJy3d03KAXr995gqFyd8sEDh/D1MGIQ0veuOCG0gaazGD8enmaVLmIwC9/HD02LfDKU5u9Ob2AWTjylTvX/hcPpfRpJ2AJHfoRxHQaSyxdkt79bvsgR+C39zSdw9me4d3f37duq/vThAyrpoxjf8gR6crzB3jxfd18epPE7MPox9ZUlLGEJS7gbli37N4hTjnVop6vVAAAAAElFTkSuQmCC' alt='Base64 encoded image'/>
            ";

            return str;
        }

        private string GenerateHTML2()
        {
            return "<b>hello</b> there. Visit <a href=\"https://github.com/OceanAirdrop\">github.com/OceanAirdrop</a>  for code. Heres a <span style=\"color: red\">random</span> apple ";

        }

        private string GenerateHTML3()
        {
            string str = @"
                    <html>
                        <head>
                            <title>Intro</title>
                            <link rel='Stylesheet' href='StyleSheet' />
                        </head>

                        <body style='background-color: #eee; background-gradient: #707; background-gradient-angle: 60; margin: 0; border-style: solid;'>
                            <h1 align='center' style='color: white'>
                                HTML Renderer Project - WinForms
                                <br />
                                <span style='font-size: x-small;'>Release 1.5.0.6</span>
                            </h1>
                            <blockquote class='whitehole'>
                                <p style='margin-top: 0px'>
                                    <table border='0' width='100%'>
                                        <tr style='vertical-align: top;'>
                                            <td width='32' style='padding: 2px 5px 0 0'>
                                                <img src='HtmlIcon' />
                                            </td>
                                            <td>
                                                Everything you see on this panel (see samples on the left) is <b>custom-painted</b>
                                                by the <b>HTML Renderer</b>, including tables, images, links and videos.<br />
                                                This project allows you to have the rich format power of HTML on your desktop applications
                                                without <b>WebBrowser</b> control or <b>MSHTML</b>.<br />
                                                The library is <b>100% managed code</b> without any external dependencies, the only 
                                                requirement is <b>.NET 2.0 or higher</b>, including support for Client Profile.
                                            </td>
                                        </tr>
                                    </table>
                                </p>
                            </blockquote>
                        </body>
                    </html>";

            return str;
        }

        private string GenerateHTML4()
        {
            // html taken from here: http://www.w3schools.com/html/html_css.asp
            string str = @"<div style='background-color:#ffffcc;padding:0.01em 16px;border-left:6px solid #ffeb3b;margin:20px 0;'>

                <p><strong>Tip:</strong> You can learn much more about CSS in our <a href='/css/default.asp'>CSS Tutorial</a>.
            </div>";

            return str;
        }

        private string GenerateHTML5()
        {
            // html taken from here: http://www.w3schools.com/html/html_css.asp
            string str = @"
                        <html>
                        <head>
                        <style>
                            .w3-note {
	                            background-color:#ffffcc;
	                            padding:0.01em 16px;
	                            border-left:6px solid #ffeb3b;
	                            margin:20px 0;
                            }
                        </style>
                        </head>
                            <body>
                                <div class='w3-note'>
                                    <p><strong>Tip:</strong> You can learn much more about CSS in our <a href='/css/default.asp'>CSS Tutorial</a>.
                                </div>
                            </body>
                        </html>";

            return str;
        }

        private string GenerateHTML6()
        {
            // html taken from here: http://www.w3schools.com/html/html_css.asp
            string str = @"
                        <html>
                        <head>
                        <style>
                            .w3-note {
	                            background-color:#ffffcc;
	                            border-left:6px solid #ffeb3b;
	                            margin:0;
                            }
                        </style>
                        </head>
                            <body>
                                <div class='w3-note'>
                                    <p><strong>Tip:</strong> You can learn much more about CSS in our <a href='/css/default.asp'>CSS Tutorial</a>.
                                </div>
                            </body>
                        </html>";

            return str;
        }

        private string GenerateHTMLToastWithBoarder()
        {
            string str = @"
                <html>
                <head>
                <style>
                    .w3-note {
	                    background-color:#ffffcc;
	                    border-left:8px solid #ffeb3b;
	                    margin:0;
                    }
                </style>
                </head>
                <body style='margin: 0; border-style: solid;'>
                    <div class='w3-note'>
                        <p><strong>Tip: </strong> Don't Eat Yellow Snow!.
                    </div>
                </body>
                </html>";
            return str;
        }

        private string GenerateHTMLToast(string title, string text, string type = "w3-info")
        {
            string str = @"
                    <html>
                    <head>
                    <style>

                        .w3-info {
		                    background-color:#d9edf7;
		                    border-left:10px solid #5bc0de;
		                    margin:0;
	                    }

	                    .w3-warning {
		                    background-color:#ffffcc;
		                    border-left:10px solid #ffeb3b;
		                    margin:0;
	                    }

                        .w3-error  {
	                        background-color:#ffdddd;
	                        border-left:10px solid #f44336;
	                        margin:0;
                        }

                        .w3-success {
	                        background-color:#dff0d8;
	                        border-left:10px solid #4bae4f;
	                        margin:0;
                        }
                    </style>
                    </head>
                    <body style='margin: 0; border-style: solid;'>
	                    <div class='{000}'>
		                    <p><strong>&nbsp;{111}: </strong>{222}
	                    </div>
                    </body>
                    </html>";

            str = str.Replace("{000}", type);
            str = str.Replace("{111}", title);
            str = str.Replace("{222}", text);
            return str;
        }

        private string SaveIconBase64()
        {
            // https://www.iconfinder.com/icons/22623/disk_floppy_save_icon#size=64
            return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAASrklEQVR4Xt1bCXBVVZr+X/blJe+9rIQEArITyAJt1hdsHW0Reyid6RbCntA6KNNd05Y13aPtSLu3NT0zVa10o7i3gkyVXYrCtHY5Zk9AkCVBwBEJCQJJXpKXkBDWM3XO+c85/7kvYehJVVvMLap4ue/es3zn+79/Oee5YIzXjro6NsYmxvT6Ir/fNZYGrJfz8ueNOJk4dzwMnR0csZ+GXU1j6X/M79568y0wODgELmDA/wHOiOEHl7gnv8ieOOmh999/99e0Uw1AXn4Bu33hHdAT6LEGNXhuGL452Q7Tpk4LGWzroRb40SMPw8JyPwBz6c7Fg2QwobNUX8r/+SCYfgHXQLRHn+OtqOHy6bmgvz8IlfcshZzZc64JyF27miFrYlblju3bX1MvaABy8wpYYWERDA4N2Y25wuDkyROQOT5L3pejFdfx48fgvkd/AbeVlep74gE+cDUB/r81dnxZteNyATBm2hX3VRvkRXGfPCcA6IO1Sypg0qQbrgmA+Lg42NXcDJkTMxfv/OCD7XRYoAEYHAKGExC0CgsXAIzP4AAwoOM93vY1rHvsn+Hm4mJ7jjgcghW4GAMmXjYLKaAhf99zx6KrTmTbzh3meQYQDAbh3ooKyM6eLMfFwWdIfhyogJvg6Y6Ph13NTXDgwD7RvWFAbj4rLCqGs4PK1jkpXRCmAcg0g8NBt7V9Det/uQHKC280UxYD4NDJlRaf9Uzl6rpcTCymXnZEaumiOyF70mT5HpJIsYH//exvXyBfAPQHg3AfAiAHYNCUo5d9iM9oohyApuZGaDmw3wZgbm4+KyoqhoGzZyXLcVBh4RGCARkZ40X7VDXbThyHnzz5BJTMm3dNFJRDlINxWgb/btmd34eJEyfJiYi5oHkgVs9wAMR3HETJgHUVy/AdYp2EeroJ/gEYJMS7oampAVoOHrABmDM3jwAgp8lNITw8UgAwbtx4QWMJqhzYifY2+OkzT8P83Dw5MJdce6URFpIKIt2tbIvxyeDKrVq8WGoNDlZ8h+zh6/nMb583QLtcMNDXB/cvWw4TJmRLrhGmSR6iuCLreDdutxuaOQOcAOTMyWPFxZIBkj7SXsMjJQDpaRnW8vNBt7efgIee+xXkzc6hAk2eC3UFQse0mNM/AKruuhvS0zPQbIwJSXtxwdObOAPwYgz6+/thvQIgpCu8QRwJZ12i2w1NjY3Q2upgQM6cuay4uBT6B/rRhmVHEZFRCMC4EJp3dJyAn/36X2D2jBm2e6CuIuSzstVQq7nvb34AKWlp2tQsZ+kCeGrTRuJtpAb8ePkKyMqaYCvrVUKzxIQEaGxsgEOtB20TyMmZy4pLSgWq6n2+UhGR0QKAtNR0vbJqATs62uHhf/83mDZ1CmoQChzVIz0Y7fdQyNCwhdrJz/f/8B5ISko2flbTWDb/1IscAEPtgWA//GTFCsjkADjRwjFI89JdgCchERob6+FQa4sNwGwOQHEJBPv70QSl74iMihEApKZwALiAGbBPnuyAR5//DUyamD0y7elKUPV0Lj62+eMlFeDxeAybdDAkX3jyxd9ZSj/QF4R/WLkSxo/PQqjQ5h1YUxImJkoAvjjUagMwa9YcVlJaCsH+oOVAoqJjEYBUOTAUQq4Sp051wGMbX4CsLB4jUOklbiSU6aPe+WnFcohPSLCjSCITTwgATD8cgAdXrRIAKN0SWCoXqrRMc8AFnsREaGioh8NfOACYOSuHlZaWQV8waKEcHSMBSElOsX0qMDh1+ht4/MVNkJ7O2YFLLFYdl1RHhPKeihAokGJsqPoPLV8JcbHxlLQ0VIEnNm8iU2HAAXho1WrIyOAxCnWd6jVbCPlfPo8HGurr4PDhQzYDZs6cxUpK/dAb7JMiiMjHxMYJAJKTOAD0YnD69Cl48uXNkMzBcYZ1VAdsv2hNCsVDuMOfr1oN0dHRpBPlIGVjj29+0RoBB+AfV6+GceMy8D61M+JmyW2vAKAejhxxADBj5ixWygHo67U6iY1zCwCSfLY48YfOnDkFT732Knh93lDaEuHR8yerbd2TGggPr6yEyKhI5I/KJwyhfkkBwDjg56vXQHo691DOyROT1GbjAp/XC/V1tXD06GGbAdOnz2Sl/nLo7e2xQqq4eAmAz+fD8NAsbWfnaXjmjdfBze0WAxGjuVKtNTI8F8CJ6vBWZhcYv7vgkcoqiAgPQ/YhCzEg49HjhldeIvkgwNneIPxT5RpIS6MumgYEzuCAiXlwAL48esQGYNr0GaysjAPQSxaHQbw7UQLg8ZGUVU6sq6sTnv39mxAXL+02hIAkptbCjB8UGMoL8tuPrqmCsLAwnbxwbWAouhxODgDtp78vCL+orITU1DTLBGSuQVwBWYgkBcCXDgCmTpvByvzl0NND6wEM3AkeAYAn0UvEUQ6ju7sTnt3yFnChlGEySYUxNLbpT8JkHDLPEJV8bKisAleYWnn04MLvSq/y2KsvoRuWrOEa8GhVFaSkKAAcWbXduWgmKSkJ6mpr4Kv/PmozYMrU6czvL4eABkBOKCFRApCYkEjWWH4XCHTBc+9shYjISCnmDohCPKNDqkQrZJUfr/qRXGEEkyZM/P6GVzaHMOCxqiopwv+LBqixJSclQW1tDRz76ksbgBumTGV+/wIEwBQeEj0+AUCCmwOgY0TxuacnAM/9xzYIF7Qlub4uXKgqkXFROkHRkKFOMIC8pCSHp7H/3N/TawVcA71B2LB2LUaPziVQ+kPvAyQnJUNtbTV8fewrG4DJN0xl/vIFEAh0W+vk8fqgo6MNAbAb7e0NwK+2vQPh4REGHEcKq83PMTUZ5CnVkMBevnQZLpw/j5mlaZJ/GxMTC2Hh4SSMBujt7ISn7n8AfMJDOSfsxFKyNjk5GepGBGDyFAFAdyDgJDJ4fcmCBZyuzmvRyhVQevtCYbt/yevypUvwwRtvQN2HO8Drpcyh8XfoiHhAV1NTDW3Hj9kMyJ48mZX7b4JuzQBJHVVZ8YUEQqbxS1cuQUfb8b/k/HVfHg8XZ3UZ3y9lhLpi+QwHoLb2U2g7ftwBQPYk5i+3AXC6NuW/ZbFJfisEa5TFD3mfQuRIdLSEkvjHelx1Y5cVRwCdBEBWJCw9R0oKB6AaTrQ5AJg4cRLzL1gA3V2oAc7QXqk80TU1cZqAaCzU7HVtT45Vwa6/JhOzZkPet9yLkgaVJBGUqUSPRkcBQHU1tLe32QyYMDFbakB3t3E1JlWX/heTPCpdyodLxskXRNJDqt1m5kTYEEjZrOKUbFkYnqNoaPqUb8gip65dGf9klZxsGPhXqakSgI6OEzYAWVkTmH/BTRIAnKyIqLA0plZBv0WR115TVeJkTkrfFcTUFWOiLno/RU5agkg2WUZcVsNt7UhUGkwjAjQXEZMh9qmcATXVcPJkuw1AZuYEZECXXg+O34p1D0Lxglu/FYEba6cfvbcNtr/zurVHkypEsBq+OdlhAzA+M0sEQoIBZGfn+bd3Go1z2Nuojo+ID9VnZLsWTUskQ5M/O8OUljKq4DrBunjxAkRGRsH6ijt0iM6nlZKSCrU1n8KpU9/YAGRkZAoGdAW6SOHRBRu37rTHbSkrPoqC5PTAf1ZkMMrk6G1LOKk3cAgmfeeBpQtJkM41gANQDadDARjPyvxOAAA2bv0jFmxksyF7lk5vcbVtPSwo6gGONGmnBOAzUkPMXLCIpDdwnMVnReIHltxukSMVGXDmzGmbAePGjWOlygQI0373zkfWLo5YZSpMxB1pBiAouuAbYgfGJQphxDxCZ7AyhpGpsJg5brbRWRN3T10rpSvvf92S71kAcDdYV1MDnZ0OANLT020AcDNz07aPrXqn2gGyy9CqD7NtpUp06DxlVGZlw2Zytps09kTTCqeNj/idcq3EFa774W2hANTWQFfnGZsBqWnpoh7ARVAPGgBefeHpsYrxt/p+5fpHiHbySDBVMKC7u9MGICUljZX5/dCFACgB2/TuJ3qnRlfFqZ07qgQiSMGKTEiGTGh7NVFXu8lSMqTaGYE1tWW1SaH1YQSo/+4Ht5iqHOYC9XU1EOjutgFITk5lpWV+CPTIQEiZ20vv/ldI/dL0I2ksdnsdndsUJVuVxI7RvK3tLp1jOL2eLnPRjmjgZSrjVHLu/dub9Qt8XjwZqq+vhZ6AA4Ck5BRWUloGgZ6AZaub//CpY2rKD9JgXKz7Vfz0CBnOiCErhrm4X2wBHZJxjZI1memKMd1793etO7wewPcFensCNgN8viRWwhkQCCjhFS++/G61DGvFfInPQzrrqNURstqURYBCKIqZgN79USBijK9ickEz3FpRuQbmJbplUXWWz0kXKPOLtXd91wRPggEIQG+PDYCXA1BaKg5JiWHhBF9+rwZTXrVJgfGANuLRoxB7WwMLPZhV2fm63nFTD1lsUsdr6JaXYpzSGSXcOp3CoGHtXeVW+MhLYo0NtdDX12cD4PF6WXFJmaMqDPDKe7XmtBaev5EipfYJEXUxAprF0TxYBQYYzWh1NJGQSpbI4QFjVtSolZfUjCP90Clh01WLOQCGnrwq3NhQD/1BJwAeLysqLoEevjGiLxe8ur3OxLvEDvUZIHMGBSEgYYPYTVbRo2GOHudIsTM57jfSSTk0Gr0bTKlCD5epPir/2m8ZHt8XaGqs58cAbAYkJHpYYXEx9PX26TIYn8rWV/71W/XjY+18adWDVo3T50uC5qYGGHAC4E5IYIVFJdw2pN/FqOq17fViDKJ+r6g0WkLg2IiQMiJX3lQXpamYg1Ij0IAmCyoW0JasmGS0x5wyUv0ZFq75fpkyTsFfr9cHu5oa4ezZAZsBbncCu7GwCHeHjQq+/mGjfehRCzWtBUobF5O1SlVG1Wkeaz0n4UUmE1ci5uc4cKnKUqIzknDQNnAAMo9wwZo7SywGeD1e2NXcCIODgzYA8fFu9p0bC8UBCXq9/mETOTGipR8TFXMwUSYyqgqrNNmUYnTCg1BJj6q24ZFb1P2g3ki9JLU5Uj7TXoYeW8GJqzBj1aJic04QADxeD3zW3ASDQ04A4uLYvBuLxPlbEmzCGzuaiSaqGpN9hE8tgDnKSgt+Rumla7WTIHqkUsfcIWpGVhxHY0yKBk/0yK0c66pFhfoN3neixwN7djXD0LkhmwFxsRyAQgEAjRze3LkbGxhhIrhKJDLQAYQpc6qsgp7zNWOygg4sj6o3jMIbJaF5v6msk1jECgwYrLyj0Cq58jNCe3bvhnNOAGI4APPnw8DAgOUG3/zP3UTAcFpIVZrsKA2g+Y7zHPjVFV1xQb0lC6eqfKoWRQmrgVX1aGAzCsZg5UJ+jNeMKiEhAfZ+thuGh4dtBsTExLL8efO5OloM+MNbG8fqib7V9+9evl7zisPJT4p+vvczOO8EIDo6muUXfAfODvKzwkg5F8Dv/7hHKZr8X2VzVrbm2ATQYi7vK21TB5bleWFH3qeXVFc1pFzSjRgFpa4gkT0qYqH0xeXfmy/ngwEbB2Dfnj1w/sJ5mwHRUdEst6AAhsRxebP98vZHe9Hj4DaYqAtib3oDgf62Qf3QweBmvBe+q10/7gNgWE3DC/PTAByM3mlBz6Nt3fzUQnpTOU4VPiy7jR/kNgc34t3xsH/vHrhw4YINQFRkFMvNz4fBc+oHE7KE9dbHe012KHRMndxAl0gmo2JuY3Hyk/23Wkbi81UApc3YMErvSFmWTK1KAUTyKFxB/g0HQDsVF0BcXCwc+HwfXLzoACAyMorNycuD4aFzOs/hIL/98eeka1vipBPAVJmMibpRWwCIR7DmQI7Fa5+KABtFs0vCOlUeTWLkWJfdVqCpyIGIjYmFlv2fw8VLl2wGREREsjm5eTA8fM5qccuf9kmTUtJIIjBrDcmJztDglyq1HfpaYOlODNDGtJ33qLOkLEOpQt5V3JpvzYcftGg5sA8uhQAQHsFycnO5OpKI3gVb/7SHZIPYVsjWtkLIGfo6aO4UKgvZEWhNrUVj6GAhrdHrrXoJDgsLh4q/yocjeCyW38srmM9aDu7jp1FsBoSHh7OcubkwPKyOqEhrb1xydDSOXRf3S7bOgCN4LJYPeG5eAWtt2Q9XLl8JBWBWzhyujnpifMGal17fABRvmQ5RUdEkF2NwqOUgXLniACAsLIzNnJ0DFy9e1F6Dm1HT0i+vi5UebZBFW9TvHaXp8A3TL1oP8mTOZkBYmItNmTbdkWa6oLni+gagcMtU/PmP1CN+pvHwodZQAFwuF8srmB8C5CeLuRu8fq9b3kc3SKbAAyEeGyoPyw/b8zPq/KdXn2BlWddxmv/edlvXGxSFvxlxxLcAQBsAyHgYL/77U/7rJ+v62cqb5iZ5Ytz/14mzy8zlCqelnNFb+nOevZbxPL+teW97Zx93a87rCAAcUwy4lrb+3z7zP59Ou9Q1tvj5AAAAAElFTkSuQmCC";
        }

        public void DisplayToastNotification(ToastNotificationType type, string title, string text, int timeOut = 4000)
        {
            _toastNotification.Close();

            int offset = 15;
            string cssClass = "w3-info";

            switch (type)
            {
                case ToastNotificationType.Info:
                    cssClass = "w3-info";
                    break;

                case ToastNotificationType.Success:
                    cssClass = "w3-success";
                    break;
                case ToastNotificationType.Warning:
                    cssClass = "w3-warning";
                    break;
                case ToastNotificationType.Error:
                    cssClass = "w3-error ";
                    break;
            }

            string html = GenerateHTMLToast(title, text, cssClass);
            int imgHeight = _toastNotification.GetHtmlHeight(html) + 5;
            int imgWidth = this.Width - 25;

            _toastNotification.SetImgSize(imgWidth, imgHeight);
            _toastNotification.SetHtml(html);

            Rectangle rect = this.Bounds;
            Point px = new Point(rect.Left, rect.Bottom);
            Point screenLocation = PointToScreen(px);

            _toastNotification.SetImgLocation(px.X + offset, px.Y - imgHeight - offset);
            _toastNotification.Show(timeOut);
        }        
    }
}
using System.Configuration;
using System.Net;
using System.Text;

namespace EDSMAIOTransChecker
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }
        private void LineNotify(string msg)
        {
            string? token = ConfigurationManager.AppSettings.Get("Token");
            try
            {
                //Initial LINE API
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", msg);
                var data = Encoding.UTF8.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + token);
                using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //
            }
            catch (Exception Error)
            {
                //Create Log File
                string? LogFileErrorPath = ConfigurationManager.AppSettings.Get("LogFileError");
                using (FileStream FileErrorLog = new FileStream(LogFileErrorPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter WriteErrorFile = new StreamWriter(FileErrorLog))
                {
                    WriteErrorFile.WriteLine(DateTime.Now);
                    WriteErrorFile.WriteLine(Error.Message);
                    WriteErrorFile.WriteLine("{0} Exception caught.", Error);
                    WriteErrorFile.WriteLine("-----------------------------------------------------------------");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(ConfigurationManager.AppSettings.Get("FilePath"));
                if (filePaths.Count() != 0)
                {
                    LineNotify("Input invoice ประจำวันที่ " + DateTime.Now.ToString("dd/MM/yyyy") + " \nยังไม่ได้รับการอัปเดต ประจำรอบเวลา " + DateTime.Now.ToString("HH:mm") + " น.");
                    string? LogFileDataPath = ConfigurationManager.AppSettings.Get("LogFileData");
                    using (FileStream FileDataLog = new FileStream(LogFileDataPath, FileMode.Append, FileAccess.Write))
                    using (StreamWriter WriteDataFile = new StreamWriter(FileDataLog))
                    {
                        WriteDataFile.WriteLine(DateTime.Now);
                        WriteDataFile.WriteLine("Invoice cannot be updated: " + filePaths.Count() + " File");
                        foreach (string Path in filePaths)
                        {
                            WriteDataFile.WriteLine(Path);
                        }
                        WriteDataFile.WriteLine("-----------------------------------------------------------------");
                    }
                    Application.Exit();
                }
                else if (filePaths.Count() == 0)
                {
                    LineNotify("Input invoice ประจำวันที่ " + DateTime.Now.ToString("dd/MM/yyyy") + " \nได้รับการอัปเดต ประจำรอบเวลา " + DateTime.Now.ToString("HH:mm") + " น. แล้ว");
                    Application.Exit();
                }
            }
            catch (Exception Error)
            {
                //Create Log File
                string? LogFileErrorPath = ConfigurationManager.AppSettings.Get("LogFileError");
                using (FileStream FileErrorLog = new FileStream(LogFileErrorPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter WriteErrorFile = new StreamWriter(FileErrorLog))
                {
                    WriteErrorFile.WriteLine(DateTime.Now);
                    WriteErrorFile.WriteLine(Error.Message);
                    WriteErrorFile.WriteLine("{0} Exception caught.", Error);
                    WriteErrorFile.WriteLine("-----------------------------------------------------------------");
                }
                LineNotify("ตรวจพบข้อผิดพลาดกรุณาตรวจสอบ");
                Application.Exit();
            }
        }
    }
}
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using Microsoft.VisualBasic.FileIO;

using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Collections.Generic;

namespace ALSData
{
    public partial class frm_dm : Form
    {
        private List<string> FileNameList;
        long sizeThreshold
        {
            get
            {
                return (long)this.numericUpDown1.Value;
            }
        }
        string startPath
        {
            get
            {
                return this.txt_path.Text.ToString();
            }
            set
            {
                startPath = value;
            }
        }

        bool toDelete
        {
            get
            {
                return this.chkbox_DeleteWhenComplete.Checked;
            }
        }

        public frm_dm()
        {
            InitializeComponent();
            this.progressBar1.Step = 1;
        }
        private List<string> SelectFolder(bool multi)
        {
            if (CommonFileDialog.IsPlatformSupported)
            {
                //Instantiate new common file dialog
                var folderSelectorDialog = new CommonOpenFileDialog();
                //Properties for dialog
                folderSelectorDialog.EnsureReadOnly = true;
                folderSelectorDialog.IsFolderPicker = true;
                folderSelectorDialog.AllowNonFileSystemItems = false;
                folderSelectorDialog.Multiselect = multi;
                folderSelectorDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderSelectorDialog.Title = "Select Folder";
                //Start dialog
                folderSelectorDialog.ShowDialog();
                try
                {
                    FileNameList = folderSelectorDialog.FileNames.ToList();
                }
                catch (System.InvalidOperationException exc)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        UpdateStatusConsole(string.Format("Error occurred in {1} module : {0}.", exc.Message, System.Reflection.MethodBase.GetCurrentMethod().Name));
                    }
            ));
                }


            }
            return FileNameList;
        }

        private void btn_SelectDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                this.txt_path.Text = string.Join(",", SelectFolder(false));
            }
            catch (System.ArgumentNullException exc)
            {
                this.BeginInvoke(new Action(() =>
                {
                    UpdateStatusConsole(string.Format("Error occurred in {1} module : {0}.", exc.Message, System.Reflection.MethodBase.GetCurrentMethod().Name));
                }
            ));
            }
        }

        private string ParseFolderNames(string folderName)
        {
            //Start builder class
            StringBuilder sb = new StringBuilder(string.Empty);
            //if the folderpath ends with ".b"
            if (folderName.Substring(folderName.Length - 2) == ".b")
            {
                //add name to builder without ".b"
                sb = sb.Append(folderName.Substring(0, folderName.Length - 2));
            }
            else
            {
                //add name to builder
                sb = sb.Append(folderName);
            }

            return sb.ToString();
        }

        private void PackFilesInFolder()
        {
            
            //define splitting criteria for exclusion folder textfield
            string[] splittercriteria = { "," };
            //create array for exclusion folder textfield
            string[] splitInput = this.txt_Exclusion.Text.Split(splittercriteria, StringSplitOptions.RemoveEmptyEntries);
            //append starting path for each exclusion folder
            //string[] exclusionFolders = splitInput.Select(x => startPath + "\\" + x.Trim()).ToArray();
            //Select all folders from startpath except exclusion folder
            //string[] foldersindirectory = Directory.GetDirectories(startPath).Except(exclusionFolders).ToArray();
            string[] foldersindirectory = Directory.GetDirectories(startPath).Except(splitInput).ToArray();
            //set progress bar maximum to the number of directories
            this.progressBar1.Maximum = foldersindirectory.Length;
            //disable compress button when compress starts
            this.btn_Compress.Enabled = false;
            this.BeginInvoke(new Action(() =>
            {
                UpdateStatusConsole(string.Format("Start packing {0} folders from {1}", foldersindirectory.Length, startPath));
            }
            ));
            //Loop through folders in directory
            foreach (string dir in foldersindirectory)
            {
                //start task for each folder
                Task.Factory.StartNew(() =>
                {
                    PackingFolder(dir);
                }, TaskCreationOptions.LongRunning);
            }
            Task.WaitAll();
        }

        private void UpdateProgress()
        {
            //If progressbar max is not at 0
            if (this.progressBar1.Maximum != 0)
            {
                //Increment progress bar by step
                this.progressBar1.PerformStep();
                this.label_Progress.Text = string.Format(@"{0}/{1}", this.progressBar1.Value, this.progressBar1.Maximum);

            }
            //If progressbar is at maximum
            if (this.progressBar1.Value == this.progressBar1.Maximum)
            {
                //enable all controls
                this.btn_Compress.Enabled = true;
                this.btn_SelectDirectory.Enabled = true;
                this.chkbox_DeleteWhenComplete.Enabled = true;
                this.chkbox_SkipFolders.Enabled = true;
                this.btn_SelectExclusion.Enabled = true;
                //updates status to "Complete"
                UpdateStatusConsole("Completed.");
            }

        }

        private void UpdateStatusConsole(string message)
        {
            //add message to status console
            this.txt_status.AppendText(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + " - " + message);
            //add new line to status console
            this.txt_status.AppendText(Environment.NewLine);
        }

        private void PackingFolder(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            string zipFileName = this.startPath + @"\\" + ParseFolderNames(di.Name) + ".zip";
            //need to catch zipfile existed error
            if (DirSize(di) / 1024f > sizeThreshold)
            {
                try
                {
                    this.BeginInvoke
                        (new Action(() =>
                        {
                            UpdateStatusConsole("Initiating " + di.Name + @".");
                        }
                        ));
                    //Compress file using IO.COmpression Zipfile Library
                    ZipFile.CreateFromDirectory(folderPath, zipFileName);
                    this.BeginInvoke
                        (new Action(() =>
                        {
                            UpdateStatusConsole("Zipped " + di.Name + @".");
                            UpdateProgress();
                        }
                        ));
                }
                catch (System.IO.IOException)
                {
                    this.BeginInvoke
                        (new Action(() =>
                        {
                            UpdateStatusConsole(di.Name + " Already exists. Skipped");
                        }
                        ));
                }
            }
            if (this.toDelete == true)
            {
                try
                {
                    //Delete file and move to recycling bin
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(di.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    this.txt_status.BeginInvoke
                        (new Action(() =>
                        {
                            UpdateStatusConsole(di.Name + " Deleted.");
                        }
                        ));
                }
                catch (Exception Excep1)
                {
                    Console.WriteLine(Excep1.Message);
                    throw;
                }
            }
        }

        private void btn_Compress_Click(object sender, EventArgs e)
        {
            //When progressbar is at 0
            this.btn_Compress.Enabled = false;
            this.btn_SelectDirectory.Enabled = false;
            this.chkbox_DeleteWhenComplete.Enabled = false;
            this.chkbox_SkipFolders.Enabled = false;
            this.btn_SelectExclusion.Enabled = false;
            this.progressBar1.Value = 0;
            PackFilesInFolder();

        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        private void btn_SelectExclusion_Click(object sender, EventArgs e)
        {
            try
            {
                this.txt_Exclusion.Text = string.Join(",", SelectFolder(true));
            }
            catch (System.ArgumentNullException exc)
            {
                this.BeginInvoke(new Action(() =>
                {
                    UpdateStatusConsole(string.Format("Error occurred in {1} module : {0}.", exc.Message, System.Reflection.MethodBase.GetCurrentMethod().Name));
                }
            ));
            }
        }
    }
}

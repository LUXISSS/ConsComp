using System.IO.Compression;

namespace ConsComp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Program p = new Program();

            p.InitiateDialog();

            Console.ReadKey(true);

            p.MainMenu();
        }

        private void InitiateDialog()
        {
            Console.WriteLine("Welcome to ConsComp\n");
            Console.WriteLine("Press any key to continue...");
        }

        private void MainMenu()
        {
            Console.Clear();

            string prompt = "What do you want to do?\n";
            string[] options = { "Create an Archive", "Extract Files from an Archive", "Close" };

            Menu newMenu = new Menu(2, prompt, options);
            int currentIndex = newMenu.Run();

            switch (currentIndex)
            {
                case 0:
                    ArchiveFolder();
                    break;
                case 1:
                    ExtractFromArchive();
                    break;
                case 2:
                    Application.Exit();
                    break;
            }
        }

        private bool folderSelected;

        private void ArchiveFolder()
        {
            if (!folderSelected)
            {
                Console.Clear();

                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86);

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;
                    string prompt = "Do you want to proceed?\n";
                    string[] options = { "Yes", "No" };

                    Menu newMenu = new Menu(1, prompt, options);
                    int currentIndex = newMenu.Run();

                    switch (currentIndex)
                    {
                        case 0:
                            CompleteArchivingProcess(folderPath);
                            break;
                        case 1:
                            MainMenu();
                            break;
                    }
                }
                else
                {
                    MainMenu();
                }
            }
            else
            {
                folderSelected = false;
                MainMenu();
            }
        }

        private void CompleteArchivingProcess(string selectedPath)
        {
            Console.Clear();

            Console.WriteLine("Please choose a file name: ");
            string inputOutputName = Console.ReadLine() ?? "";

            Console.Clear();

            string prompt = "Do you want to proceed?\n";
            string[] options = { "Yes", "No" };

            Menu newMenu = new Menu(1, prompt, options);
            int currentIndex = newMenu.Run();

            switch (currentIndex)
            {
                case 0:
                    try
                    {
                        switch(inputOutputName)
                        {
                            case "":
                                ZipFile.CreateFromDirectory(selectedPath, Path.GetFileName(selectedPath) + ".zip");
                                MainMenu();
                                break;
                            default:
                                ZipFile.CreateFromDirectory(selectedPath, inputOutputName + ".zip");
                                MainMenu();
                                break;
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("An error occurred: " + exception.Message);
                    }
                    break;
                case 1:
                    MainMenu();
                    break;
            }
        }

        private bool fileSelected;

        private void ExtractFromArchive()
        {
            if (!fileSelected)
            {
                Console.Clear();

                OpenFileDialog fileDialog = new OpenFileDialog();

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = fileDialog.FileName;
                    string prompt = "Do you want to proceed?\n";
                    string[] options = { "Yes", "No" };

                    Menu newMenu = new Menu(1, prompt, options);
                    int currentIndex = newMenu.Run();

                    switch (currentIndex)
                    {
                        case 0:
                            fileSelected = true;
                            CompleteExtractionProcess(filePath);
                            break;
                        case 1:
                            MainMenu();
                            break;
                    }
                }
                else
                {
                    MainMenu();
                }
            }
            else
            {
                fileSelected = false;
                MainMenu();
            }
        }

        private void CompleteExtractionProcess(string selectedPath)
        {
            Console.Clear();

            Console.WriteLine("Please choose an output name: ");
            string extractName = Console.ReadLine() ?? "";

            Console.Clear();

            string prompt = "Do you want to proceed?\n";
            string[] options = { "Yes", "No" };

            Menu newMenu = new Menu(1, prompt, options);
            int currentIndex = newMenu.Run();

            switch (currentIndex)
            {
                case 0:
                    try
                    {
                        switch (extractName)
                        {
                            case "":
                                ZipFile.ExtractToDirectory(selectedPath, $"{selectedPath}".Replace(".zip", ""));
                                MainMenu();
                                break;
                            default:
                                ZipFile.ExtractToDirectory(selectedPath, extractName);
                                MainMenu();
                                break;
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("An error occurred: " + exception.Message);
                    }
                    break;
                case 1:
                    MainMenu();
                    break;
            }
        }
    }
}
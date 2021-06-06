using System;
using System.IO;
using System.Text;

namespace FigureBulkCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string outputfilename = "figurecode.txt";
            string OutputFilePathAndName = string.Empty;

            Console.WriteLine("Figure Bulk Creator");
            Console.WriteLine("==============================================");
            Console.WriteLine("Use this program to take all the names of figures in a folder and write them into this kind of struct:");
            Console.WriteLine("");
            Console.WriteLine("\\begin{figure}[H]");
            Console.WriteLine("    \\centering");
            Console.WriteLine("    \\fbox{");
            Console.WriteLine("    \\includegraphics[width=<size>]{<Prefix><FileName>}");
            Console.WriteLine("    }");
            Console.WriteLine("\\caption{}");
            Console.WriteLine("\\label{fig: <Filename>}");
            Console.WriteLine("\\end{figure}");
            Console.WriteLine("");
            Console.WriteLine("for LaTex");
            Console.WriteLine("Flags:");
            Console.WriteLine("<Directory>  The directory in which the files are in");
            Console.WriteLine("<Prefix>     Something like \"Figures/Measurements/<FileName>\"");
            Console.WriteLine("<Size>       Size of the figure. Usually something like \"0.5\\linewidth\"");
            Console.WriteLine("");
            Console.WriteLine("Enter arguments like this:");
            Console.WriteLine("FireBuldCreator.exe \"<Directory>\" \"<Prefix>\" \"<Size>\"");
            Console.WriteLine("");
            Console.WriteLine($"When the program is done, the LaTeX code is written inside the \"{outputfilename}\" file.");

            if (args.Length != 3)
            {
                Console.WriteLine("\nERROR:\tWrong argument count. Exiting.");
                _ = Console.ReadLine();
                Environment.Exit(1);
            }
            try
            {
                string dirpath = args[0];
                string prefix = args[1];
                string size = args[2];
                DirectoryInfo d = new DirectoryInfo(dirpath);

                StringBuilder code = new StringBuilder();
                foreach (var file in d.GetFiles())
                {
                    string filename = file.Name;
                    code.AppendLine("\\begin{figure}[H]");
                    code.AppendLine("    \\centering");
                    code.AppendLine("    \\fbox{");

                    code.Append("    \\includegraphics[width=");
                    code.Append(size);
                    code.Append("]{");
                    code.Append(prefix);
                    code.Append(file.Name);
                    code.Append("}\n");

                    code.AppendLine("    }");
                    code.AppendLine("\\caption{}");

                    code.Append("\\label{fig: ");
                    code.Append(file.Name);
                    code.Append("}\n");

                    code.AppendLine("\\end{figure}");
                    code.AppendLine();
                }

                Console.WriteLine("Code: ---------------------------------------");
                Console.WriteLine(code.ToString());

                OutputFilePathAndName = Path.Combine(dirpath, outputfilename);
                using (FileStream fileStream = new FileStream(OutputFilePathAndName, FileMode.Create))
                {
                    using (StreamWriter s = new StreamWriter(fileStream))
                    {
                        s.WriteLine(code.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERROR:\t{ex.Message}. Exiting.");
                _ = Console.ReadLine();
                Environment.Exit(1);
            }

            Console.WriteLine("Code created and saved successfully here:");
            Console.WriteLine($"{OutputFilePathAndName}");
            Console.WriteLine("Now exiting program.");

            Environment.Exit(0);
        }
    }
}

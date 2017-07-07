using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    string output;
    int timeOut = 3000;
    string extension;
    string code;
    string input;
    string fileName;
    string fullCodeFile;
    string root = "D:\\home\\site\\wwwroot\\CODE\\";
    string Cppcompiler = "D:\\home\\site\\wwwroot\\TDM\\bin\\";
    string javacompiler = "D:\\home\\site\\wwwroot\\JAVA\\bin\\";
    string stderror = "" ;
    public status stat = new status();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button_click(object sender, EventArgs e)
    {

        // Generate a random string for file name
        randomString rs = new randomString();
        // GUI control 
        extension = codeList.SelectedValue;
        code = codeText.Text;
        input = codeInput.Text;

        // input text set to STDIN by default
        if (input == "STDIN")
            input = "";

        // generating code files to save them in root directory
        // the file is either C or CPP
        if (extension == ".c" || extension == ".cpp" )
        {
            // calling the method to get a file name
            fileName = rs.generateFileName();
            // making a full source code name
            fullCodeFile = fileName + extension;
            // Saving the code file into the disk for further process
            generateFile gf = new generateFile();
            gf.generateCodeFile(root + fullCodeFile, code);
        }
        // generating code files to save them in root directory
        // the file is either C or CPP
        else if (extension == ".java")
        {
            // Same goes for JAVA as above
            fileName = "Main";
            fullCodeFile = fileName + extension;
            generateFile gf = new generateFile();
            gf.generateCodeFile(root + fullCodeFile, code);
        }

        
        
        if (File.Exists(root + fullCodeFile))
        {
            // we'll only try to compile if the file is generated successfully
            try
            {
                compile(fullCodeFile, fileName, extension, input);
            }
            catch (Exception ex)
            {
                stat.Compile_status = "COMPILE FAILED!";

                //REDIRECT to another page
                
            }
        }

        show();
   
    }

    private void compile(string fullCodeFile, string fileName, string extension, string input = "")
    {
        
        // start a new process
        Process process = new Process();
        process.StartInfo.UseShellExecute = false;
        // redirecting the STDERROR stream from process
        process.StartInfo.RedirectStandardError = true;

        // setting up process and it's arguments according to file extension 
        if (extension == ".c")
        {
            process.StartInfo.FileName = Cppcompiler + "gcc.exe ";
            process.StartInfo.Arguments = " -o " + root + fileName + " " + root + fullCodeFile;
        }
        else if (extension == ".cpp")
        {
            process.StartInfo.FileName = Cppcompiler + "g++.exe ";
            process.StartInfo.Arguments = " -o " + root + fileName + " " + root + fullCodeFile;
        }
        else if (extension == ".java")
        {
            process.StartInfo.FileName = javacompiler +"javac.exe ";
            process.StartInfo.Arguments = root + "Main.java" + " -d " + root;
            
        }

        //starting the process
        process.Start();
        // Reading whether any error occured or not
        stderror = process.StandardError.ReadToEnd();
        // setting up the error
        stat.Compile_Error = stderror;

        process.WaitForExit();
        

        if (File.Exists(root + fullCodeFile))
        {
            // compiling done. So the code file isn't needed anymore
            File.Delete(root + fullCodeFile);
        }

        // if the exe is generated then try to run it
        if (File.Exists(root + fileName + ".exe"))
        {
            try
            {
                execute(fileName, extension, input);
                stat.Compile_status = "COMPILE SUCCESSFUL";
            }
            catch
            {
                stat.Run_status = "EXECUTION FAILED";

                //REDIRECT
                Server.Transfer("result.aspx", true);

            }
        }
        // IF the class file is generated then try to run
        else if (File.Exists(root + fileName + ".class"))
        {
            try
            {
                execute(fileName, extension, input);
                stat.Compile_status = "COMPILE SUCCESSFUL";
            }
            catch
            {
                stat.Run_status = "EXECUTION FAILED";
                //REDIRECT
               
            }
        }
        
    }

    private void execute(string filename, string extension, string input)
    {
        Process p = new Process();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardError = true;

        if (extension == ".c")
            p.StartInfo.FileName = root + filename + ".exe";

        else if (extension == ".cpp")
            p.StartInfo.FileName = root + filename + ".exe";

        else if (extension == ".java")
        {
            p.StartInfo.FileName = @"D:\home\site\wwwroot\JAVA\bin\java.exe";
            p.StartInfo.Arguments = "Main";
            p.StartInfo.WorkingDirectory = @"D:\home\site\wwwroot\CODE\";
        }

        p.Start();



        if (input.Trim() != null)
        {
            StreamWriter myStreamWriter = p.StandardInput;
            myStreamWriter.WriteLine(input);
            
        }
        output = p.StandardOutput.ReadToEnd();

        stat.Run_Error = p.StandardError.ReadToEnd();
        stat.Runtime = ((p.ExitTime - p.StartTime).TotalMilliseconds) / 1000 + " s";


        p.WaitForExit();
        //if (compile == false)
        //{
        //    codeInput.Text += compile.ToString();
        //    p.Kill();
        //    status.Run_status = "TIME LIMIT EXCEEDED - 3s";
        //}

        stat.Run_status = "RUN SUCCESSFUL";

        if (File.Exists(root + filename + ".exe"))
        {
            File.Delete(root + filename + ".exe");
        }
        else if (File.Exists(root + filename + ".class"))
        {
            File.Delete(root + filename + ".class");
        }

        
    }


    private void show()
    {

        stat.STDOUT = output;
        stat.FileNameExt = fullCodeFile;
        stat.Code = code;
        stat.STDIN = input;
        filename.Text = "File Name: " + stat.FileNameExt;
        compile_error.Text = "Compile Error Caused: " + stat.Compile_Error;
        compile_status.Text = "Compile Status: " + stat.Compile_status;
        run_error.Text = "Runtime Error: " + stat.Run_Error;
        run_status.Text = "Runtime Status: " + stat.Run_status;
        runtime.Text = "Total Run Time: " + stat.Runtime;
        memory.Text = stat.MemoryLimit;
    }
}
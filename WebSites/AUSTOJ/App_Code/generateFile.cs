using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

public class generateFile
{
    public generateFile()
    {
       
    }

    public void generateCodeFile(string fullCodeFile, string code)
    {
        File.WriteAllText(fullCodeFile,code);
    }

}
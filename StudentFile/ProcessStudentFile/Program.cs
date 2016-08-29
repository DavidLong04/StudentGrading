using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProcessStudentFile
{
    class Program
    {

        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Write("Usage: sortnames filename");
                return 1;
            }

            try
            {
                var inFilename = args[0];
                var outFilename = Path.GetFileNameWithoutExtension(inFilename) + "-graded" + Path.GetExtension(inFilename);

                // open the input file, sort row and output
                var file = new StudentFile(inFilename);
 
                file.SortStudents();
                file.WriteStudentsToFile(outFilename);
                Console.Write("Finished. Created file " + outFilename);
            }
            catch (FileNotFoundException ex)
            {
                Console.Write("File not found: ", ex.FileName);
                return 1;
            }
            catch (Exception ex)
            {
                Console.Write("An exception occured processing the file: ", ex.Message);
                return 1;
            }

            return 0;
        }
    }
}
